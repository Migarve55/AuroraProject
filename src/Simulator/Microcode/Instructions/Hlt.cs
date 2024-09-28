using Simulator.System;

namespace Simulator.Microcode.Instructions;

public class Hlt : InstructionDefinition
{
    public override string GetOperationName()
    {
        return "HALT";
    }

    public override byte GetOperationCode()
    {
        return 0xFF;
    }

    protected override void CreateStepsInternal(Flags flags)
    {
        AddStep(ControlWord.HALT);
    }
}