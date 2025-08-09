using PLT.Interpreter.Data;
using PLT.Interpreter.Memory;
using PLT.Interpreter.Data.Operations;
using PLT.Interpreter;

namespace PLT.Interpreter.Parsing;

internal partial class StlInterpreter
{
    private readonly MemoryModel _memory;
    private bool _boolAccumulator;
    private bool _boolAccumulator2;
    private int _intAccumulator;
    private int _intAccumulator2;
    private uint _uIntAccumulator;
    private uint _uIntAccumulator2;
    private float _floatAccumulator;
    private float _floatAccumulator2;

    internal MemoryModel GetMemory() => _memory;

    internal bool GetBoolAccumulator() => _boolAccumulator;
    internal void SetBoolAccumulator(bool value) => _boolAccumulator = value;
    internal bool GetBoolAccumulator2() => _boolAccumulator2;
    internal void SetBoolAccumulator2(bool value) => _boolAccumulator2 = value;

    internal int GetIntAccumulator() => _intAccumulator;
    internal void SetIntAccumulator(int value) => _intAccumulator = value;
    internal int GetIntAccumulator2() => _intAccumulator2;
    internal void SetIntAccumulator2(int value) => _intAccumulator2 = value;

    internal uint GetUIntAccumulator() => _uIntAccumulator;
    internal void SetUIntAccumulator(uint value) => _uIntAccumulator = value;
    internal uint GetUIntAccumulator2() => _uIntAccumulator2;
    internal void SetUIntAccumulator2(uint value) => _uIntAccumulator2 = value;

    internal float GetFloatAccumulator() => _floatAccumulator;
    internal void SetFloatAccumulator(float value) => _floatAccumulator = value;
    internal float GetFloatAccumulator2() => _floatAccumulator2;
    internal void SetFloatAccumulator2(float value) => _floatAccumulator2 = value;

    public StlInterpreter(MemoryModel memory)
    {
        _memory = memory;
    }

    public void L(string operand)
    {
        var addr = new PLCAddress(operand);
        addr.DataType.Accept(new LoadOperationVisitor(this, addr));
    }

    public void STORE(string operand)
    {
        var addr = new PLCAddress(operand);
        addr.DataType.Accept(new StoreOperationVisitor(this, addr));
    }

    public void NOP() { }

    public void MOV(string sourceOperand, string destinationOperand)
    {
        var sourceAddr = new PLCAddress(sourceOperand);
        var destAddr = new PLCAddress(destinationOperand);

        sourceAddr.DataType.Accept(new LoadOperationVisitor(this, sourceAddr));
        destAddr.DataType.Accept(new StoreOperationVisitor(this, destAddr));
    }

}
