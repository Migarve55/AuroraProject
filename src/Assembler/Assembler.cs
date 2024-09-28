using Assembler.Compiler;
using Assembler.Parser;

namespace Assembler;

public class Assembler
{
    public ushort[] Compile(string path)
    {
        var lines = File.ReadLines(path);
        try
        {
            var parser = new Parser.Parser();
            var program = parser.Parse(lines.ToArray());
            var compiler = new Compiler.Compiler(program);
            return compiler.GetBinary();
        }
        catch (ParserException e)
        {
            throw new Exception($"Format error at line {e.LineNumber}", e);
        }
        catch (CompilerException e)
        {
            throw new Exception("Could not compile program", e);
        }
    }
}