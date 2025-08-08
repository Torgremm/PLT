using PLT.Interpreter;
using PLT.Interpreter.Data;
using PLT.Interpreter.Memory;
using PLT.Interpreter.Parsing;

namespace PLT.Tests;

public class TestRunner
{
    private readonly string _stlCode = default!;
    private readonly List<Instruction> _instructions = default!;
    private readonly Executor _executor = default!;
    private readonly StlInterpreter _interpreter = default!;
    private readonly MemoryModel _memory = default!;

    public TestRunner(string STLCode, MemoryModel Memory)
    {
        _memory = Memory;
        _stlCode = STLCode;
        _interpreter = new StlInterpreter(_memory);
        _executor = new Executor(_interpreter);
        _instructions = Parser.Parse(_stlCode);

    }

    public bool Execute()
    {
        _executor.Execute(_instructions);
        return true;
    }
}