using PLT.Interpreter.Parsing;

namespace PLT.Interpreter;

internal class Executor
{
    private readonly StlInterpreter _interpreter;

    internal Executor(StlInterpreter interpreter)
    {
        _interpreter = interpreter;
    }
    internal class InstructionPointer
    {
        public int Value { get; set; }
    }

    internal void Execute(List<Instruction> instructions, Dictionary<string, int> labels)
    {
        var instructionPointer = new InstructionPointer { Value = 0 };
        int cycle = 0;
        
        while (instructionPointer.Value < instructions.Count())
        {
            if (cycle++ > 1000)
                throw new InvalidOperationException("Execution cycle limit exceeded. Possible infinite loop detected.");

            var instr = instructions[instructionPointer.Value];

            object?[] parameters;
            if (string.Equals(instr.OpCode, "JMPC", StringComparison.OrdinalIgnoreCase))
            {
                _interpreter.JMPC(instr.Operands[0], instructionPointer, labels);
                continue;
            }

            var operandCount = instr.Operands?.Length ?? 0;

            var method = typeof(StlInterpreter).GetMethods()
                .FirstOrDefault(m =>
                    string.Equals(m.Name, instr.OpCode, StringComparison.OrdinalIgnoreCase) &&
                    m.GetParameters().Length == operandCount);

            if (method == null)
                throw new InvalidOperationException($"Instruction not implemented: {instr.OpCode} with {operandCount} operands.");


            parameters = operandCount == 0 ? Array.Empty<object>() : (instr.Operands ?? Array.Empty<object>()).Cast<object>().ToArray();

            method.Invoke(_interpreter, parameters);

            instructionPointer.Value++;
        }
    }
}

