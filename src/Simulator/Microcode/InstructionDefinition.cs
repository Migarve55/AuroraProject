using Simulator.System;

namespace Simulator.Microcode;

public abstract class InstructionDefinition
{
    private IList<ControlWord> _controlWords = new List<ControlWord>();

    public ControlWord GetControlWord(int step, Flags flags)
    {
        CreateSteps(flags);
        if (step < _controlWords.Count)
        {
            return _controlWords[step];
        }
        
        throw new Exception($"Instruction {GetOperationName()} has no definition for step {step}");
    }

    public virtual ParameterType ParameterType => ParameterType.NONE;
    
    public abstract string GetOperationName();
    public abstract byte GetOperationCode();
    
    protected abstract void CreateStepsInternal(Flags flags);
    
    private void CreateSteps(Flags flags)
    {
        _controlWords.Clear();
        AddStep(ControlWord.PC_OUT | ControlWord.MAR_IN);
        AddStep(ControlWord.MEM_OUT | ControlWord.IR_IN | ControlWord.PC_INC);
        CreateStepsInternal(flags);
    }

    protected void AddStep(ControlWord word)
    {
        _controlWords.Add(word);
    }
    
    protected void ModifyStep(ControlWord word)
    {
        _controlWords[^1] |= word;
    }
}