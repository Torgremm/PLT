using PLT.Interpreter.Data;
using PLT.Interpreter.Memory;
using PLT.Interpreter.Data.Operations;
using PLT.Interpreter;

namespace PLT.Interpreter.Parsing;

internal partial class StlInterpreter
{

    private int GetLabelIndex(string label, Dictionary<string, int> labels)
    {
        if (!labels.TryGetValue(label, out var targetIndex))
            throw new InvalidOperationException($"Label not found: {label}");

        return targetIndex;
    }

    [JumpInstruction]
    public void JU(string label, Executor.InstructionPointer instructionPointer, Dictionary<string, int> labels)
    {
        int targetIndex = GetLabelIndex(label, labels);

        instructionPointer.Value = targetIndex;
    }

    [JumpInstruction]
    public void JL(string label, Executor.InstructionPointer instructionPointer, Dictionary<string, int> labels)
    {
        throw new NotImplementedException($"");
        //CASE TYPE LIST
        //JL LABEL : IF OUT OF RANGE
        //JU LABEL : IF ACCU1 LOW 8 = 0
        //JU LABEL : IF ACCU1 LOW 8 = 1
        //JU LABEL : IF ACCU1 LOW 8 = 2
        //JU LABEL : IF ACCU1 LOW 8 = 3
    }

    [JumpInstruction]
    public void JC(string label, Executor.InstructionPointer instructionPointer, Dictionary<string, int> labels)
    {
        if (GetRLO())
        {
            int targetIndex = GetLabelIndex(label, labels);
            instructionPointer.Value = targetIndex;
        }
    }

    [JumpInstruction]
    public void JCN(string label, Executor.InstructionPointer instructionPointer, Dictionary<string, int> labels)
    {
        if (!GetRLO())
        {
            int targetIndex = GetLabelIndex(label, labels);
            instructionPointer.Value = targetIndex;
        }
    }

    [JumpInstruction]
    public void JCB(string label, string operand, Executor.InstructionPointer instructionPointer, Dictionary<string, int> labels)
    {
        if (GetRLO())
        {
            int targetIndex = GetLabelIndex(label, labels);
            instructionPointer.Value = targetIndex;
        }
        else
        {
            SetRLO(false);
        }

        _statusFlags.BR = GetRLO();
    }

    [JumpInstruction]
    public void JNB(string label, string operand, Executor.InstructionPointer instructionPointer, Dictionary<string, int> labels)
    {
        if (!GetRLO())
        {
            int targetIndex = GetLabelIndex(label, labels);
            instructionPointer.Value = targetIndex;
        }
        else
        {
            SetRLO(true);
        }

        _statusFlags.BR = GetRLO();
    }

    [JumpInstruction]
    public void JBI(string label, Executor.InstructionPointer instructionPointer, Dictionary<string, int> labels)
    {
        if (_statusFlags.BR)
        {
            int targetIndex = GetLabelIndex(label, labels);
            instructionPointer.Value = targetIndex;
        }
    }

    [JumpInstruction]
    public void JNBI(string label, Executor.InstructionPointer instructionPointer, Dictionary<string, int> labels)
    {
        if (!_statusFlags.BR)
        {
            int targetIndex = GetLabelIndex(label, labels);
            instructionPointer.Value = targetIndex;
        }
    }

    [JumpInstruction]
    public void JO(string label, Executor.InstructionPointer instructionPointer, Dictionary<string, int> labels)
    {
        if (_statusFlags.OV)
        {
            int targetIndex = GetLabelIndex(label, labels);
            instructionPointer.Value = targetIndex;
        }
    }

    [JumpInstruction]
    public void JOS(string label, Executor.InstructionPointer instructionPointer, Dictionary<string, int> labels)
    {
        if (_statusFlags.OS)
        {
            int targetIndex = GetLabelIndex(label, labels);
            instructionPointer.Value = targetIndex;
        }
    }

    [JumpInstruction]
    public void JOZ(string label, Executor.InstructionPointer instructionPointer, Dictionary<string, int> labels)
    {
        if (GetShortAccumulator1() == 0)
        {
            int targetIndex = GetLabelIndex(label, labels);
            instructionPointer.Value = targetIndex;
        }
    }

    [JumpInstruction]
    public void JN(string label, Executor.InstructionPointer instructionPointer, Dictionary<string, int> labels)
    {
        if (GetShortAccumulator1() != 0)
        {
            int targetIndex = GetLabelIndex(label, labels);
            instructionPointer.Value = targetIndex;
        }
    }

    [JumpInstruction]
    public void JP(string label, Executor.InstructionPointer instructionPointer, Dictionary<string, int> labels)
    {
        if (GetIntAccumulator1() > 0)
        {
            int targetIndex = GetLabelIndex(label, labels);
            instructionPointer.Value = targetIndex;
        }
    }

    [JumpInstruction]
    public void JM(string label, Executor.InstructionPointer instructionPointer, Dictionary<string, int> labels)
    {
        if (GetIntAccumulator1() < 0)
        {
            int targetIndex = GetLabelIndex(label, labels);
            instructionPointer.Value = targetIndex;
        }
    }

    [JumpInstruction]
    public void JPZ(string label, Executor.InstructionPointer instructionPointer, Dictionary<string, int> labels)
    {
        if (GetIntAccumulator1() >= 0)
        {
            int targetIndex = GetLabelIndex(label, labels);
            instructionPointer.Value = targetIndex;
        }
    }

    [JumpInstruction]
    public void JMZ(string label, Executor.InstructionPointer instructionPointer, Dictionary<string, int> labels)
    {
        if (GetIntAccumulator1() <= 0)
        {
            int targetIndex = GetLabelIndex(label, labels);
            instructionPointer.Value = targetIndex;
        }
    }

    [JumpInstruction]
    public void JUO(string label, Executor.InstructionPointer instructionPointer, Dictionary<string, int> labels)
    {
        throw new NotImplementedException($"JUO instruction is not implemented yet.");
    }
    
    [JumpInstruction]
    public void LOOP(string label, Executor.InstructionPointer instructionPointer, Dictionary<string, int> labels)
    {
        if (GetShortAccumulator1() > 0)
        {
            SetShortAccumulator1((short)(GetShortAccumulator1() - 1));
            
            int targetIndex = GetLabelIndex(label, labels);
            instructionPointer.Value = targetIndex;
        }
    }
}
