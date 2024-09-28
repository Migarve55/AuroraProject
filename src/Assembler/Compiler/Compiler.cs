using System.ComponentModel.Design;
using System.Globalization;
using Assembler.Model;
using Simulator.Microcode;

namespace Assembler.Compiler;

public class Compiler
{
    private readonly Program _program;
    private readonly InstructionRegistry _instructionRegistry;

    private readonly List<ushort> _result = new();
    private readonly Dictionary<string, ushort> _labelLookup = new();
    private readonly Dictionary<ushort, string> _missingLabels = new();

    public Compiler(Program program)
    {
        _instructionRegistry = new InstructionRegistry();
        _program = program;
    }
    
    public ushort[] GetBinary()
    {
        foreach (Instruction instruction in _program.Instructions)
        {
            var definition = _instructionRegistry.GetInstructionForOperationName(instruction.OperationName);
            if (definition is null)
            {
                throw new CompilerException($"Instruction '{instruction.OperationName}' not found");
            }
            
            ushort opCode = (ushort)(definition.GetOperationCode() << 8);
            _result.Add(opCode);

            if (!string.IsNullOrWhiteSpace(instruction.Label))
            {
                ushort memAddress = (ushort)(_result.Count - 1);
                if (!_labelLookup.TryAdd(instruction.Label, memAddress))
                {
                    throw new CompilerException($"Label '{instruction.Label}' is duplicated");
                }
            }

            ProcessParameter(definition, instruction);
        }
        
        // Label post processing
        foreach (var pair in _missingLabels)
        {
            if (!_labelLookup.TryGetValue(pair.Value, out ushort pointer))
            {
                throw new CompilerException($"Can not find label '{pair.Value}'");
            }

            _result[pair.Key] = pointer;
        }

        return _result.ToArray();
    }

    private void ProcessParameter(InstructionDefinition definition, Instruction instruction)
    {
        switch (definition.ParameterType)
        {
            case ParameterType.NONE:
                if (!string.IsNullOrWhiteSpace(instruction.Parameter))
                {
                    throw new CompilerException($"{instruction.OperationName} does not need a parameter");
                }
                break;
            case ParameterType.VALUE:
                ProcessValueParameter(instruction);
                break;
            case ParameterType.LABEL:
                ProcessLabelParameter(instruction);
                break;
        }
    }

    private void ProcessValueParameter(Instruction instruction)
    {
        if (string.IsNullOrWhiteSpace(instruction.Parameter))
        {
            throw new CompilerException($"{instruction.OperationName} needs a parameter");
        }
                    
        if (ushort.TryParse(instruction.Parameter, NumberStyles.HexNumber, null, out ushort val))
        {
            _result.Add(val);
        }
        else
        {
            throw new CompilerException("Could not parse value parameter");
        }
    }

    private void ProcessLabelParameter(Instruction instruction)
    {
        if (string.IsNullOrWhiteSpace(instruction.Parameter))
        {
            throw new CompilerException($"{instruction.OperationName} needs a parameter");
        }
                    
        _result.Add(0); // Temp value
        ushort memAddress = (ushort)(_result.Count - 1);
        _missingLabels.Add(memAddress, instruction.Parameter);
    }
}