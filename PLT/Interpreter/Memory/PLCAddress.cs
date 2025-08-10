using PLT.Interpreter.Data;
using System.Text.RegularExpressions;

namespace PLT.Interpreter.Memory;

public enum MemoryArea
{
    Data,
    Counter
}

internal class PLCAddress
{
    public int Byte { get; private set; }
    public int Bit { get; private set; } = 0;
    public DataVar DataType { get; }
    public MemoryArea MemoryArea { get; } = MemoryArea.Data;

    public PLCAddress(int byteIndex, int bitIndex = 0, DataVar dataType = DataVar.BOOL, MemoryArea memoryArea = MemoryArea.Data)
    {
        Byte = byteIndex;
        Bit = bitIndex;
        DataType = dataType;
        MemoryArea = memoryArea;
    }


    public PLCAddress(string s)
    {
        s = s.Trim().ToUpperInvariant();

        var counterPattern = @"^C(\d+)$";
        var counterMatch = Regex.Match(s, counterPattern);

        if (counterMatch.Success)
        {
            if (!int.TryParse(counterMatch.Groups[1].Value, out int counterIndex))
                throw new FormatException($"Invalid counter index: {counterMatch.Groups[1].Value}");

            Byte = counterIndex * 2;
            Bit = 0;
            DataType = DataVar.WORD;
            MemoryArea = MemoryArea.Counter;
            return;
        }


        // ^(?<prefix>MX|MB|MW|MD|M)  — prefix
        // (?<addr>\d+)               — byte address
        // (?:\.(?<bit>\d+))?         — bits
        var pattern = @"^(?<prefix>MX|MB|MW|MD|M)(?<addr>\d+)(?:\.(?<bit>\d+))?$";
        var match = Regex.Match(s, pattern);

        if (!match.Success)
            throw new FormatException($"Invalid PLC address format: {s}");

        var prefix = match.Groups["prefix"].Value;
        var addr = match.Groups["addr"].Value;
        var bitGroup = match.Groups["bit"];

        if (!int.TryParse(addr, out int byteIndex))
            throw new FormatException($"Invalid byte address: {addr}");

        Byte = byteIndex;

        DataType = prefix switch
        {
            "MX" => DataVar.BOOL,
            "M" when bitGroup.Success => DataVar.BOOL,
            "MB" => DataVar.BYTE,
            "MW" => DataVar.INT,
            "MD" => DataVar.REAL,
            "M" => DataVar.BYTE,
            _ => throw new FormatException($"Unknown prefix: {prefix}")
        };

        if (bitGroup.Success)
        {
            if (!int.TryParse(bitGroup.Value, out int bitIndex))
                throw new FormatException($"Invalid bit index: {bitGroup.Value}");

            Bit = bitIndex;
        }
        else
        {
            Bit = 0;
        }
    }

    public override string ToString()
    {
        return DataType == DataVar.BOOL ? $"M{Byte}.{Bit}" : $"{DataType} {Byte}";
    }
}

