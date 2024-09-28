using Simulator.Microcode.Instructions;

namespace Simulator.Microcode;

public class InstructionRegistry
{
    private readonly IDictionary<byte, InstructionDefinition> _opCodeLookup = new Dictionary<byte, InstructionDefinition>();
    private readonly IDictionary<string, InstructionDefinition> _nameCodeLookup = new Dictionary<string, InstructionDefinition>();

    public InstructionRegistry()
    {
        LoadInstructions();
    }

    private void LoadInstructions()
    {
        RegisterInstruction(new Nop());
        RegisterInstruction(new Hlt());
        RegisterInstruction(new Jump());
        RegisterInstruction(new Jumpz());
        RegisterInstruction(new Add());
        RegisterInstruction(new Sub());
        RegisterInstruction(new Store());
        RegisterInstruction(new Load());
        RegisterInstruction(new Push());
        RegisterInstruction(new Peek());
        RegisterInstruction(new Pop());
        RegisterInstruction(new Call());
        RegisterInstruction(new Ret());
    }

    private void RegisterInstruction(InstructionDefinition instructionDefinition)
    {
        if (_opCodeLookup.ContainsKey(instructionDefinition.GetOperationCode()))
        {
            var duplicated = _opCodeLookup[instructionDefinition.GetOperationCode()];
            throw new Exception($"Instruction '{instructionDefinition.GetOperationName()}' shares OP code: 0x{instructionDefinition.GetOperationCode():x4} with '{duplicated.GetOperationName()}'");
        }
        
        if (_nameCodeLookup.ContainsKey(instructionDefinition.GetOperationName()))
        {
            throw new Exception($"Instruction name '{instructionDefinition.GetOperationName()}' is duplicated");
        }

        _opCodeLookup.Add(instructionDefinition.GetOperationCode(), instructionDefinition);
        _nameCodeLookup.Add(instructionDefinition.GetOperationName(), instructionDefinition);
    }
    
    public InstructionDefinition? GetInstructionForOperationCode(ushort opCode)
    {
        byte code = (byte)(opCode >> 8);
        if (_opCodeLookup.TryGetValue(code, out InstructionDefinition instruction))
        {
            return instruction;
        }

        return null;
    }

    public InstructionDefinition? GetInstructionForOperationName(string name)
    {
        if (_nameCodeLookup.TryGetValue(name, out InstructionDefinition instruction))
        {
            return instruction;
        }

        return null;
    }
}