using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Markup;

namespace PLT.Interpreter;

internal class MemoryModel : IMemoryModel
{
    private const int InitialMemorySize = 1024;
    private byte[] _memory = new byte[InitialMemorySize];
    private int _memoryPointer = 0;

    public void RegisterVariable(DataVar variableType)
    {
        int bits = GetBitSize(variableType);
        int bytes = (bits + 7) / 8;

        if (_memoryPointer + bytes > _memory.Length)
            Array.Resize(ref _memory, _memory.Length * 2);

        _memoryPointer += bytes;
    }

    private int GetBitSize(DataVar type) =>
        type switch
        {
            DataVar.BOOL => 1,
            DataVar.BYTE => 8,
            DataVar.WORD => 16,
            DataVar.INT => 16,
            DataVar.DWORD => 32,
            DataVar.REAL => 32,
            _ => throw new ArgumentOutOfRangeException(nameof(type), $"Unsupported type: {type}")
        };

    public void SetBit(PLCAddress address, bool value)
    {
        int index = address.Byte;
        int mask = 1 << address.Bit;

        if (value)
            _memory[index] |= (byte)mask;
        else
            _memory[index] &= (byte)~mask;
    }

    public bool GetBit(PLCAddress address)
    {
        int index = address.Byte;
        int mask = 1 << address.Bit;

        return (_memory[index] & mask) != 0;
    }

    public void SetValue(PLCAddress address, DataVar type, object value)
    {
        int byteIndex = address.Byte;

        Span<byte> targetSpan = _memory.AsSpan(byteIndex);

        switch (type)
        {
            case DataVar.BOOL:
                if (value is bool bVal)
                    SetBit(address, bVal);
                else throw new InvalidCastException();
                break;

            case DataVar.BYTE:
                targetSpan[0] = Convert.ToByte(value);
                break;

            case DataVar.WORD:
            case DataVar.INT:
                BitConverter.GetBytes(Convert.ToInt16(value)).CopyTo(targetSpan);
                break;

            case DataVar.DWORD:
                BitConverter.GetBytes(Convert.ToUInt32(value)).CopyTo(targetSpan);
                break;

            case DataVar.REAL:
                BitConverter.GetBytes(Convert.ToSingle(value)).CopyTo(targetSpan);
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(type));
        }
    }

    public T GetValue<T>(PLCAddress address, DataVar type)
    {
        int byteIndex = address.Byte;
        Span<byte> sourceSpan = _memory.AsSpan(byteIndex);

        object result = type switch
        {
            DataVar.BOOL => GetBit(address),
            DataVar.BYTE => sourceSpan[0],
            DataVar.WORD => BitConverter.ToUInt16(sourceSpan),
            DataVar.INT => BitConverter.ToInt16(sourceSpan),
            DataVar.DWORD => BitConverter.ToUInt32(sourceSpan),
            DataVar.REAL => BitConverter.ToSingle(sourceSpan),
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };

        return (T)Convert.ChangeType(result, typeof(T));
    }

    public void Reset()
    {
        Array.Clear(_memory, 0, _memory.Length);
        _memoryPointer = 0;
    }
}

