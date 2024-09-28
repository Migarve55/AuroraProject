using Simulator.System;

namespace Simulator.Microcode.Instructions;

public class Pop : InstructionDefinition
{
    public override string GetOperationName()
    {
        return "POP";
    }

    public override byte GetOperationCode()
    {
        return 0xD4;
    }

    protected override void CreateStepsInternal(Flags flags)
    {
        ModifyStep(ControlWord.SP_POP | ControlWord.END);
    }
}