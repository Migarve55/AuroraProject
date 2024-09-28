using Simulator.System;

namespace Simulator.Microcode.Instructions;

public class Jump : InstructionDefinition
{
    public override ParameterType ParameterType => ParameterType.LABEL;

    public override string GetOperationName()
    {
        return "JUMP";
    }

    public override byte GetOperationCode()
    {
        return 0x0F;
    }

    protected override void CreateStepsInternal(Flags flags)
    {
        AddStep(ControlWord.PC_OUT | ControlWord.MAR_IN);
        AddStep(ControlWord.MEM_OUT | ControlWord.PC_IN | ControlWord.END);
    }
}