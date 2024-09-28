using Simulator.System;

namespace Simulator.Microcode.Instructions;

public class Jumpz : InstructionDefinition
{
    public override ParameterType ParameterType => ParameterType.LABEL;

    public override string GetOperationName()
    {
        return "JMPZ";
    }

    public override byte GetOperationCode()
    {
        return 0x1F;
    }

    protected override void CreateStepsInternal(Flags flags)
    {
        if (flags.HasFlag(Flags.ZF))
        {
            AddStep(ControlWord.PC_OUT | ControlWord.MAR_IN);
            AddStep(ControlWord.MEM_OUT | ControlWord.PC_IN | ControlWord.END);
        }
        else
        {
            ModifyStep(ControlWord.END);
        }
    }
}