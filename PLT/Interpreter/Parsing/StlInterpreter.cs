using PLT.Interpreter.Data;
using PLT.Interpreter.Memory;
using PLT.Interpreter.Data.Operations;
using PLT.Interpreter;

namespace PLT.Interpreter.Parsing;

internal partial class StlInterpreter
{
    private readonly MemoryModel _memory;
    private int _accu1;
    private int _accu2;
    private StatusFlags _statusFlags;

    internal StatusFlags GetStatusFlags() => _statusFlags;
    internal MemoryModel GetMemory() => _memory;
    internal bool GetRLO() => GetStatusFlags().RLO;
    internal bool SetRLO(bool value) => GetStatusFlags().RLO = value;

    internal bool GetAccumulator1() => (_accu1 & 1) != 0;
    internal void SetAccumulator1(bool value)
    {
        if (value)
            _accu1 |= 1;
        else
            _accu1 &= ~1;
    }

    internal bool GetAccumulator2() => (_accu2 & 1) != 0;
    internal void SetAccumulator2(bool value)
    {
        if (value)
            _accu2 |= 1;
        else
            _accu2 &= ~1;
    }

    internal int GetIntAccumulator1() => _accu1;
    internal void SetIntAccumulator1(int value) => _accu1 = value;

    internal int GetIntAccumulator2() => _accu2;
    internal void SetIntAccumulator2(int value) => _accu2 = value;

    internal short GetShortAccumulator1() => unchecked((short)_accu1);
    internal void SetShortAccumulator1(short value) => _accu1 = unchecked((int)value);
    internal short GetShortAccumulator2() => unchecked((short)_accu2);
    internal void SetShortAccumulator2(short value) => _accu2 = unchecked((int)value);


    internal uint GetUIntAccumulator1() => unchecked((uint)_accu1);
    internal void SetUIntAccumulator1(uint value) => _accu1 = unchecked((int)value);

    internal uint GetUIntAccumulator2() => unchecked((uint)_accu2);
    internal void SetUIntAccumulator2(uint value) => _accu2 = unchecked((int)value);

    internal ushort GetUShortAccumulator1() => unchecked((ushort)_accu1);
    internal void SetUShortAccumulator1(ushort value) => _accu1 = unchecked((int)value);

    internal ushort GetUShortAccumulator2() => unchecked((ushort)_accu2);
    internal void SetUShortAccumulator2(ushort value) => _accu2 = unchecked((int)value);

    internal float GetFloatAccumulator1() => BitConverter.Int32BitsToSingle(_accu1);
    internal void SetFloatAccumulator1(float value) => _accu1 = BitConverter.SingleToInt32Bits(value);

    internal float GetFloatAccumulator2() => BitConverter.Int32BitsToSingle(_accu2);
    internal void SetFloatAccumulator2(float value) => _accu2 = BitConverter.SingleToInt32Bits(value);

    public StlInterpreter(MemoryModel memory)
    {
        _memory = memory;
        _statusFlags = new StatusFlags(this);
    }

    public void L(string operand)
    {
        SetIntAccumulator2(GetIntAccumulator1());

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
