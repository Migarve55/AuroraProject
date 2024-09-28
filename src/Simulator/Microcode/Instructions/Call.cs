using Simulator.System;

namespace Simulator.Microcode.Instructions;

public class Call : InstructionDefinition
{
    public override ParameterType ParameterType => ParameterType.LABEL;

    public override string GetOperationName()
    {
        return "CALL";
    }

    public override byte GetOperationCode()
    {
        return 0xF0;
    }

    protected override void CreateStepsInternal(Flags flags)
    {
        ModifyStep(ControlWord.SP_PUSH);
        AddStep(ControlWord.SP_OUT | ControlWord.MAR_IN);
        AddStep(ControlWord.PC_OUT | ControlWord.MEM_IN);
        AddStep(ControlWord.PC_OUT | ControlWord.MAR_IN);
        AddStep(ControlWord.MEM_OUT | ControlWord.PC_IN | ControlWord.END);
    }
}