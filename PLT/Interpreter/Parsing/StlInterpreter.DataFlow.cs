using PLT.Interpreter.Data;
using PLT.Interpreter.Memory;
using PLT.Interpreter.Data.Operations;
using PLT.Interpreter;

namespace PLT.Interpreter.Parsing;

internal partial class StlInterpreter
{
    [JumpInstruction]
    public void JU(string label, Executor.InstructionPointer instructionPointer, Dictionary<string, int> labels)
    {
        if (!labels.TryGetValue(label, out var targetIndex))
            throw new InvalidOperationException($"Label not found: {label}");

        instructionPointer.Value = targetIndex;
    }

    [JumpInstruction]
    public void JL(string label, Executor.InstructionPointer instructionPointer, Dictionary<string, int> labels)
    {
        if (!labels.TryGetValue(label, out var targetIndex))
            throw new InvalidOperationException($"Label not found: {label}");

        instructionPointer.Value = targetIndex;
    }

    [JumpInstruction]
    public void JC(string label, Executor.InstructionPointer instructionPointer, Dictionary<string, int> labels)
    {
        if (_boolAccumulator)
        {
            if (!labels.TryGetValue(label, out var targetIndex))
                throw new InvalidOperationException($"Label not found: {label}");

            instructionPointer.Value = targetIndex;
        }
    }

    [JumpInstruction]
    public void JCN(string label, Executor.InstructionPointer instructionPointer, Dictionary<string, int> labels)
    {
        if (!_boolAccumulator)
        {
            if (!labels.TryGetValue(label, out var targetIndex))
                throw new InvalidOperationException($"Label not found: {label}");

            instructionPointer.Value = targetIndex;
        }
    }

    [JumpInstruction]
    public void JCB(string label, string operand, Executor.InstructionPointer instructionPointer, Dictionary<string, int> labels)
    {
        if (_memory.GetBit(new PLCAddress(operand)))
        {
            if (!labels.TryGetValue(label, out var targetIndex))
                throw new InvalidOperationException($"Label not found: {label}");

            instructionPointer.Value = targetIndex;
        }
    }

    [JumpInstruction]
    public void JNB(string label, string operand, Executor.InstructionPointer instructionPointer, Dictionary<string, int> labels)
    {
        if (!_memory.GetBit(new PLCAddress(operand)))
        {
            if (!labels.TryGetValue(label, out var targetIndex))
                throw new InvalidOperationException($"Label not found: {label}");

            instructionPointer.Value = targetIndex;
        }
    }

}
