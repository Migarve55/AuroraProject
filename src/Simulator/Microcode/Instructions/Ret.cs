using Simulator.System;

namespace Simulator.Microcode.Instructions;

public class Ret : InstructionDefinition
{
    public override string GetOperationName()
    {
        return "RET";
    }

    public override byte GetOperationCode()
    {
        return 0xF1;
    }

    protected override void CreateStepsInternal(Flags flags)
    {
        AddStep(ControlWord.SP_OUT | ControlWord.MAR_IN);
        AddStep(ControlWord.MEM_OUT | ControlWord.PC_IN | ControlWord.SP_POP | ControlWord.END);
    }
}