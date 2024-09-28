using Simulator.System;

namespace Simulator.Microcode.Instructions;

public class Add : InstructionDefinition
{
    public override string GetOperationName()
    {
        return "ADD";
    }

    public override byte GetOperationCode()
    {
        return 0xA0;
    }

    protected override void CreateStepsInternal(Flags flags)
    {
        AddStep(ControlWord.SP_OUT | ControlWord.MAR_IN);
        AddStep(ControlWord.MEM_OUT | ControlWord.RB_IN | ControlWord.SP_POP);
        AddStep(ControlWord.SP_OUT | ControlWord.MAR_IN);
        AddStep(ControlWord.MEM_OUT | ControlWord.RA_IN);
        AddStep(ControlWord.ALU_OUT | ControlWord.MEM_IN | ControlWord.FI | ControlWord.END);
    }
}