using Simulator.System;

namespace Simulator.Microcode.Instructions;

public class Nop : InstructionDefinition
{
    public override string GetOperationName()
    {
        return "NOOP";
    }

    public override byte GetOperationCode()
    {
        return 0x00;
    }

    protected override void CreateStepsInternal(Flags flags)
    {
        ModifyStep(ControlWord.END);
    }
}