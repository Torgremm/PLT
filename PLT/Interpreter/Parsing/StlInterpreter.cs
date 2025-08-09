using PLT.Interpreter.Data;
using PLT.Interpreter.Memory;
using PLT.Interpreter.Data.Operations;
using PLT.Interpreter;

namespace PLT.Interpreter.Parsing;

internal class StlInterpreter
{
    private readonly MemoryModel _memory;
    private bool _boolAccumulator;
    private int _intAccumulator;
    private uint _uIntAccumulator;
    private float _floatAccumulator;

    internal MemoryModel GetMemory() => _memory;

    internal bool GetBoolAccumulator() => _boolAccumulator;
    internal void SetBoolAccumulator(bool value) => _boolAccumulator = value;

    internal int GetIntAccumulator() => _intAccumulator;
    internal void SetIntAccumulator(int value) => _intAccumulator = value;

    internal uint GetUIntAccumulator() => _uIntAccumulator;
    internal void SetUIntAccumulator(uint value) => _uIntAccumulator = value;

    internal float GetFloatAccumulator() => _floatAccumulator;
    internal void SetFloatAccumulator(float value) => _floatAccumulator = value;

    public StlInterpreter(MemoryModel memory)
    {
        _memory = memory;
    }

    public void LD(string operand)
    {
        var addr = new PLCAddress(operand);
        addr.DataType.Accept(new LoadOperationVisitor(this, addr));
    }

    public void LDN(string operand)
    {
        _boolAccumulator = !_memory.GetBit(new PLCAddress(operand));
    }

    public void A(string operand)
    {
        _boolAccumulator &= _memory.GetBit(new PLCAddress(operand));
    }

    public void AN(string operand)
    {
        _boolAccumulator &= !_memory.GetBit(new PLCAddress(operand));
    }

    public void O(string operand)
    {
        _boolAccumulator |= _memory.GetBit(new PLCAddress(operand));
    }

    public void ON(string operand)
    {
        _boolAccumulator |= !_memory.GetBit(new PLCAddress(operand));
    }

    public void X(string operand)
    {
        _boolAccumulator ^= _memory.GetBit(new PLCAddress(operand));
    }

    public void NOT(string? operand = null)
    {
        _boolAccumulator = !_boolAccumulator;
    }

    public void STORE(string operand)
    {
        var addr = new PLCAddress(operand);
        addr.DataType.Accept(new StoreOperationVisitor(this, addr));
    }


    public void SET(string operand)
    {
        _memory.SetBit(new PLCAddress(operand), true);
    }

    public void R(string operand)
    {
        _memory.SetBit(new PLCAddress(operand), false);
    }

    public void NOP() { }

    public void ADD(string operand)
    {
        var addr = new PLCAddress(operand);
        addr.DataType.Accept(new AddOperationVisitor(this, addr));
    }

    public void SUB(string operand)
    {
        var addr = new PLCAddress(operand);
        addr.DataType.Accept(new SubOperationVisitor(this, addr));
    }

    public void MOV(string sourceOperand, string destinationOperand)
    {
        var sourceAddr = new PLCAddress(sourceOperand);
        var destAddr = new PLCAddress(destinationOperand);

        sourceAddr.DataType.Accept(new LoadOperationVisitor(this, sourceAddr));
        destAddr.DataType.Accept(new StoreOperationVisitor(this, destAddr));
    }

    public void JMPC(string label, Executor.InstructionPointer instructionPointer, Dictionary<string, int> labels)
    {
        if (_boolAccumulator)
        {
            if (!labels.TryGetValue(label, out var targetIndex))
                throw new InvalidOperationException($"Label not found: {label}");

            instructionPointer.Value = targetIndex + 1; //Run the instruction following the label
        }
        else
        {
            instructionPointer.Value++;
        }
    }

}
