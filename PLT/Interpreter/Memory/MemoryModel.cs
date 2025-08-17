using PLT.Interpreter.Data;
using System.Text;

namespace PLT.Interpreter.Memory;

public partial class MemoryModel
{
    private const int InitialMemorySize = 1024;
    private byte[] _memory = new byte[InitialMemorySize];
    private int _bytePointer = 0;
    private int _bitPointer = 0;

    public void RegisterVariable(DataVar variableType, object value)
    {
        PLCAddress address;

        if (variableType == DataVar.BOOL) //Stack and reset for each byte
        {
            int byteIndex = _bitPointer / 8;
            int bitIndex = _bitPointer % 8;
            address = new PLCAddress(byteIndex, bitIndex, DataVar.BOOL);
            _bitPointer++;

            EnsureMemoryCapacity(byteIndex + 1);
        }
        else
        {
            AlignBitPointer();

            int alignment = variableType switch //Siemens stack logic
            {
                DataVar.BYTE => 1,
                DataVar.SHORT => 2,           // SHORT = 2 bytes
                DataVar.WORD => 2,            // WORD = 2 bytes (if still used)
                DataVar.INT => 4,             // INT = 4 bytes now (double word)
                DataVar.DWORD => 4,
                DataVar.REAL => 4,
                _ => throw new ArgumentOutOfRangeException(nameof(variableType))
            };

            if (_bytePointer % alignment != 0)
                _bytePointer += alignment - (_bytePointer % alignment);

            address = new PLCAddress(_bytePointer, 0, variableType);

            int bytes = GetBitSize(variableType) / 8;
            EnsureMemoryCapacity(_bytePointer + bytes);

            _bytePointer += bytes;
            _bitPointer = _bytePointer * 8;
        }

        SetValue(address, variableType, value);
    }

    private void EnsureMemoryCapacity(int requiredBytes)
    {
        if (requiredBytes > _memory.Length)
        {
            int newSize = Math.Max(requiredBytes, _memory.Length * 2);
            Array.Resize(ref _memory, newSize);
        }
    }
    private void AlignBitPointer()
    {
        if (_bitPointer % 8 != 0)
            _bitPointer += 8 - (_bitPointer % 8);

        _bytePointer = _bitPointer / 8;
    }

    internal int GetBitSize(DataVar type) =>
        type switch
        {
            DataVar.BOOL => 1,
            DataVar.BYTE => 8,
            DataVar.WORD => 16,
            DataVar.SHORT => 16,
            DataVar.INT => 32,
            DataVar.DWORD => 32,
            DataVar.REAL => 32,
            _ => throw new ArgumentOutOfRangeException(nameof(type), $"Unsupported type: {type}")
        };

    internal void SetBit(PLCAddress address, bool value)
    {
        int index = address.Byte;
        int mask = 1 << address.Bit;

        if (value)
            _memory[index] |= (byte)mask;
        else
            _memory[index] &= (byte)~mask;
    }

    internal bool GetBit(PLCAddress address)
    {
        int index = address.Byte;
        int mask = 1 << address.Bit;

        return (_memory[index] & mask) != 0;
    }

    internal void SetValue(PLCAddress address, DataVar type, object value)
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
                BitConverter.GetBytes(Convert.ToUInt16(value)).CopyTo(targetSpan);
                break;
            case DataVar.SHORT:
                BitConverter.GetBytes(Convert.ToInt16(value)).CopyTo(targetSpan);
                break;
            case DataVar.INT:
                BitConverter.GetBytes(Convert.ToInt32(value)).CopyTo(targetSpan);
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

    internal T GetValue<T>(PLCAddress address, DataVar type)
    {
        int byteIndex = address.Byte;
        Span<byte> sourceSpan = _memory.AsSpan(byteIndex);

        object result = type switch
        {
            DataVar.BOOL => GetBit(address),
            DataVar.BYTE => sourceSpan[0],
            DataVar.WORD => BitConverter.ToUInt16(sourceSpan),
            DataVar.SHORT => BitConverter.ToInt16(sourceSpan),
            DataVar.INT => BitConverter.ToInt32(sourceSpan),
            DataVar.DWORD => BitConverter.ToUInt32(sourceSpan),
            DataVar.REAL => BitConverter.ToSingle(sourceSpan),
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };

        return (T)Convert.ChangeType(result, typeof(T));
    }

    internal void Reset()
    {
        Array.Clear(_memory, 0, _memory.Length);
        Array.Clear(_counterMemory, 0, _counterMemory.Length);
        _bytePointer = 0;
    }

}

