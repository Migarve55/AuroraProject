using Simulator.System;

namespace Simulator.Microcode.Instructions;

public class Push : InstructionDefinition
{
    public override ParameterType ParameterType => ParameterType.VALUE;

    public override string GetOperationName()
    {
        return "PUSH";
    }

    public override byte GetOperationCode()
    {
        return 0xD3;
    }

    protected override void CreateStepsInternal(Flags flags)
    {
        AddStep(ControlWord.PC_OUT | ControlWord.MAR_IN);
        AddStep(ControlWord.MEM_OUT | ControlWord.RA_IN | ControlWord.PC_INC | ControlWord.SP_PUSH);
        AddStep(ControlWord.SP_OUT | ControlWord.MAR_IN);
        AddStep(ControlWord.RA_OUT | ControlWord.MEM_IN | ControlWord.END);
    }
}