using System.Globalization;
using Assembler.Model;

namespace Assembler.Parser;

public class Parser
{
    private IList<Instruction> _instructions = new List<Instruction>();

    public Program Parse(string[] lines)
    {
        for (int i = 0; i < lines.Count(); i++)
        {
            ProcessLine(i + 1, lines[i]);
        }

        return new Program(_instructions);
    }

    private void ProcessLine(int lineNumber, string line)
    {
        // Ignore comments
        line = line.Trim();
        if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
        {
            return;
        }
        
        var args = line.Split(" ");
        
        // Check if it has a label
        string label = string.Empty;
        if (args[0].EndsWith(":"))
        {
            label = args[0];
            label = label.Remove(label.Length - 1);
            args = args[1..];
        }

        Instruction instruction;
        switch (args.Length)
        {
            case 1:
                instruction = new Instruction(args[0]);
                break;
            case 2:
                instruction = new Instruction(args[0], args[1]);
                break;
            default:
                throw new ParserException(lineNumber, "Wrong line format");
        }

        instruction.LineNumber = lineNumber;
        instruction.Label = label;
        _instructions.Add(instruction);
    }
}