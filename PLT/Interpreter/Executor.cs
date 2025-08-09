using PLT.Interpreter.Parsing;
using System.Reflection;

namespace PLT.Interpreter;

internal class Executor
{
    private readonly StlInterpreter _interpreter;
    private readonly HashSet<string> _dataFlowMethods;

    internal Executor(StlInterpreter interpreter)
    {
        _interpreter = interpreter;

        _dataFlowMethods = new HashSet<string>(
            typeof(StlInterpreter).GetMethods()
                .Where(m => m.GetCustomAttributes(typeof(JumpInstructionAttribute), false).Any())
                .Select(m => m.Name),
            StringComparer.OrdinalIgnoreCase);
    }
    internal class InstructionPointer
    {
        public int Value { get; set; }
    }

    internal void Execute(List<Instruction> instructions, Dictionary<string, int> labels)
    {
        var instructionPointer = new InstructionPointer { Value = 0 };
        int cycle = 0;

        while (instructionPointer.Value < instructions.Count)
        {
            if (cycle++ > 1000)
                throw new InvalidOperationException("Execution cycle limit exceeded. Possible infinite loop detected.");

            var instr = instructions[instructionPointer.Value];
            var op = instr.OpCode;
            var operands = instr.Operands ?? Array.Empty<string>();

            var isDataFlow = _dataFlowMethods.Contains(op);

            var methods = typeof(StlInterpreter).GetMethods()
                .Where(m => string.Equals(m.Name, op, StringComparison.OrdinalIgnoreCase));

            MethodInfo? method = methods.FirstOrDefault(m =>
            {
                var paramCount = m.GetParameters().Length;
                return paramCount == (isDataFlow ? operands.Length + 2 : operands.Length); //Make room for pointer and labels in data flow methods
            });

            if (method == null)
                throw new InvalidOperationException($"Instruction not implemented: {op} with {operands.Length} operands.");

            var parameters = new List<object>();
            parameters.AddRange(operands.Cast<object>());

            if (isDataFlow)
            {
                parameters.Add(instructionPointer);
                parameters.Add(labels);
            }

            method.Invoke(_interpreter, parameters.ToArray());

            instructionPointer.Value++;
        }
    }
}

