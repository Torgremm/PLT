using PLT.Interpreter.Parsing;

namespace PLT.Interpreter;

internal class Executor
{
    private readonly StlInterpreter _interpreter;

    internal Executor(StlInterpreter interpreter)
    {
        _interpreter = interpreter;
    }

    internal void Execute(List<Instruction> instructions)
    {
        foreach (var instr in instructions)
        {
            var operandCount = instr.Operands?.Length ?? 0;

            var method = typeof(StlInterpreter).GetMethods()
                .FirstOrDefault(m => 
                    string.Equals(m.Name, instr.OpCode, StringComparison.OrdinalIgnoreCase) &&
                    m.GetParameters().Length == operandCount);

            if (method == null)
                throw new InvalidOperationException($"Instruction not implemented: {instr.OpCode} with {operandCount} operands.");

            object?[] parameters = operandCount == 0 ? Array.Empty<object>() : (instr.Operands ?? Array.Empty<object>()).Cast<object>().ToArray();

            method.Invoke(_interpreter, parameters);
        }
    }
}

