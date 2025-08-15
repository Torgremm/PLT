using PLT.Interpreter.Data;
using PLT.Interpreter.Memory;
using PLT.Interpreter.Data.Operations;
using PLT.Interpreter;
using System.Text.RegularExpressions;

namespace PLT.Interpreter.Parsing;

internal partial class StlInterpreter
{
    public int CounterAddressToIndex(string operand)
    {
        var pattern = @"^C(\d+)$";
        var match = Regex.Match(operand, pattern);

        if (match.Success && match.Groups.Count > 1)
            return int.Parse(match.Groups[1].Value);

        return 0; // Default index if not a counter address
    }

    public void FR(string operand)
    {
        int index = CounterAddressToIndex(operand);
        _memory.StartCounter(index);
    }

    public void LC(string operand)
    {
        SetIntAccumulator2(GetIntAccumulator1());

        var addr = new PLCAddress(operand);
        addr.DataType.Accept(new LoadOperationVisitor(this, addr)); //Set Accu1 with counter value
        ITB(GetUShortAccumulator1());
    }

    public void CU(string operand)
    {
        int index = CounterAddressToIndex(operand);
        _memory.IncrementCounter(index);
    }

    public void CD(string operand)
    {
        int index = CounterAddressToIndex(operand);
        _memory.DecrementCounter(index);
    }

    public void S(string operand) // Must work on both bit and counters
    {
        int index = CounterAddressToIndex(operand);

        var addr = new PLCAddress(operand);

        switch (addr.MemoryArea)
        {
            case MemoryArea.Data:
                if (addr.DataType == DataVar.BOOL)
                    _memory.SetBit(addr, true);
                else
                    throw new NotSupportedException($"Set operation not supported on data type {addr.DataType}");
                break;

            case MemoryArea.Counter:
                _memory.SetCounter(index, GetUShortAccumulator1());
                break;
            default:
                throw new NotSupportedException("Unknown memory area");
        }
    }

    public void R(string operand) // Must work on both bit and counters
    {
        int index = CounterAddressToIndex(operand);

        var addr = new PLCAddress(operand);

        switch (addr.MemoryArea)
        {
            case MemoryArea.Data:
                if (addr.DataType == DataVar.BOOL)
                    _memory.SetBit(addr, false);
                else
                    throw new NotSupportedException($"Set operation not supported on data type {addr.DataType}");
                break;

            case MemoryArea.Counter:
                _memory.SetCounter(index, 0);
                break;
            default:
                throw new NotSupportedException("Unknown memory area");
        }
    }
}
