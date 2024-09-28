using Simulator.Microcode;

namespace Simulator.System;

public class ControlUnit
{
    private readonly Register _instructionRegister;
    private readonly LogicUnit _logicUnit;
    private readonly InstructionRegistry _instructionRegistry;

    private InstructionDefinition? _currentInstruction;

    public ControlUnit(Register instructionRegister, LogicUnit logicUnit)
    {
        _instructionRegister = instructionRegister;
        _logicUnit = logicUnit;
        _instructionRegistry = new InstructionRegistry();
    }
    
    public ControlWord ControlWord { get; private set; }
    public int MicroStep { get; private set; }
    public Flags Flags { get; private set; }

    public void Step()
    {
        MicroStep++;
        if (MicroStep >= 15)
        {
            MicroStep = 0;
        }
    }

    public void ResetStep()
    {
        MicroStep = 0;
    }

    public string GetInstructionName()
    {
        return _currentInstruction?.GetOperationName() ?? "----";
    }

    public void FlagsIn()
    {
        Flags = _logicUnit.Flags;
    }

    public void UpdateControlUnit()
    {
        _currentInstruction = _instructionRegistry.GetInstructionForOperationCode(_instructionRegister.Value);
        ControlWord = _currentInstruction!.GetControlWord(MicroStep, Flags);
    }

    public void Reset()
    {
        Flags = 0;
        MicroStep = 0;
        ControlWord = 0;
    }
}