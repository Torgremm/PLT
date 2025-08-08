namespace PLT.Interpreter;

internal class Executor
{
    private readonly IStlInstructionSet _interpreter;

    internal Executor(IStlInstructionSet interpreter)
    {
        _interpreter = interpreter;
    }

    internal void Execute(List<Instruction> instructions)
    {
        foreach (var instr in instructions)
        {
            var operandCount = instr.Operands?.Length ?? 0;

            var method = typeof(IStlInstructionSet).GetMethods()
                .FirstOrDefault(m => 
                    string.Equals(m.Name, instr.OpCode, StringComparison.OrdinalIgnoreCase) &&
                    m.GetParameters().Length == operandCount);

            if (method == null)
                throw new InvalidOperationException($"Instruction not implemented: {instr.OpCode} with {operandCount} operands.");

            // Prepare parameters or null if none
            object?[] parameters = operandCount == 0 ? Array.Empty<object>() : instr.Operands.Cast<object>().ToArray();

            method.Invoke(_interpreter, parameters);
        }
    }
}

