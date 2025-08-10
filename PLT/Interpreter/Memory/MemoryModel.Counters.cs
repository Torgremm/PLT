using PLT.Interpreter.Data;
using System.Text;

namespace PLT.Interpreter.Memory;

public partial class MemoryModel
{
    private const int MaxCounters = 256; //Siemens docs
    private byte[] _counterMemory = new byte[MaxCounters * 2];

    internal ushort GetCounter(int counterIndex)
    {
        if (counterIndex < 0 || counterIndex >= MaxCounters)
            throw new ArgumentOutOfRangeException(nameof(counterIndex));

        int offset = counterIndex * 2;

        return BitConverter.ToUInt16(_counterMemory, offset);
    }

    internal ushort GetCounter(PLCAddress addr)
    {
        return GetCounter(addr.Byte);
    }

    public void SetCounter(int counterIndex, ushort value)
    {
        if (counterIndex < 0 || counterIndex >= MaxCounters)
            throw new ArgumentOutOfRangeException(nameof(counterIndex));

        int offset = counterIndex * 2;
        byte[] bytes = BitConverter.GetBytes(value);
        _counterMemory[offset] = bytes[0];
        _counterMemory[offset + 1] = bytes[1];
    }

    internal void IncrementCounter(int counterIndex)
    {
        ushort currentValue = GetCounter(counterIndex);
        SetCounter(counterIndex, (ushort)(currentValue + 1));
    }

    internal void DecrementCounter(int counterIndex)
    {
        ushort currentValue = GetCounter(counterIndex);
        if (currentValue == 0)
            return; // Avoid underflow

        SetCounter(counterIndex, (ushort)(currentValue - 1));
    }
    
    internal void StartCounter(int counterIndex)
    {
        if (counterIndex < 0 || counterIndex >= MaxCounters)
            throw new ArgumentOutOfRangeException(nameof(counterIndex));

        SetCounter(counterIndex, 0);
    }

}