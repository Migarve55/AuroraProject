using Simulator.System;

namespace Simulator.Microcode.Instructions;

public class Peek : InstructionDefinition
{
    public override ParameterType ParameterType => ParameterType.LABEL;

    public override string GetOperationName()
    {
        return "PEEK";
    }

    public override byte GetOperationCode()
    {
        return 0xD2;
    }

    protected override void CreateStepsInternal(Flags flags)
    {
        AddStep(ControlWord.SP_OUT | ControlWord.MAR_IN);
        AddStep(ControlWord.MEM_OUT | ControlWord.RA_IN);
        AddStep(ControlWord.PC_OUT | ControlWord.MAR_IN);
        AddStep(ControlWord.MEM_OUT | ControlWord.MAR_IN | ControlWord.PC_INC);
        AddStep(ControlWord.RA_OUT | ControlWord.MEM_IN | ControlWord.END);
    }
}