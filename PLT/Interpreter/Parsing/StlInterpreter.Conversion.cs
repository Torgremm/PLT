using PLT.Interpreter.Data;
using PLT.Interpreter.Memory;
using PLT.Interpreter.Data.Operations;
using PLT.Interpreter;

namespace PLT.Interpreter.Parsing;

internal partial class StlInterpreter
{

    public void RND()
    {
        float value = GetFloatAccumulator1();
        SetIntAccumulator1((int)Math.Round(value, MidpointRounding.AwayFromZero));
    }

    public void TRUNC()
    {
        float value = GetFloatAccumulator1();
        SetIntAccumulator1((int)Math.Truncate(value));
    }

    public void RND_UP()
    {
        float value = GetFloatAccumulator1();
        SetIntAccumulator1((int)Math.Ceiling(value));
    }

    public void RND_DOWN()
    {
        float value = GetFloatAccumulator1();
        SetIntAccumulator1((int)Math.Floor(value));
    }

    public void CAW()
    {
        uint full = GetUIntAccumulator1();
        uint high = full & 0xFFFF0000;
        uint low2 = (full & 0x0000FF00) >> 8;
        uint low1 = (full & 0x000000FF) << 8;

        SetUIntAccumulator1(high | low1 | low2);
    }

    public void CAD()
    {
        uint full = GetUIntAccumulator1();
        uint high2 = (full & 0xFF000000) >> 24;
        uint high1 = (full & 0x00FF0000) >> 8;
        uint low2 = (full & 0x0000FF00) << 8;
        uint low1 = (full & 0x000000FF) << 24;

        SetUIntAccumulator1(high2 | high1 | low2 | low1);
    }

    public void NEGR()
    {
        float full = GetFloatAccumulator1();
        SetFloatAccumulator1(-full);
    }

    public void NEGD()
    {
        int current = GetIntAccumulator1();
        SetIntAccumulator1(-current);
    }

    public void NEGI()
    {
        uint current = GetUIntAccumulator1();

        short low16 = (short)(current & 0xFFFF);
        short negated = unchecked((short)-low16);

        SetIntAccumulator1(negated);
    }

    public void INVI()
    {
        uint current = GetUIntAccumulator1();

        ushort low16 = (ushort)(current & 0xFFFF);
        ushort inverted = (ushort)~low16;

        SetIntAccumulator1((short)inverted);
    }

    public void INVD()
    {
        int current = GetIntAccumulator1();
        SetIntAccumulator1(~current);
    }

    public void DTR()
    {
        int current = GetIntAccumulator1();
        SetFloatAccumulator1(current);
    }

    public void DTB()
    {
        int value = GetIntAccumulator1();

        int absValue = Math.Abs(value);
        uint bcdResult = 0;

        for (int digitIndex = 0; digitIndex < 7; digitIndex++)
        {
            int digit = absValue % 10; //Get last digit
            absValue /= 10; //Divide by 10 to move to next digit

            bcdResult |= (uint)(digit << (digitIndex * 4)); //Move our result to the correct position
        }

        // Set sign bit
        uint signBits = value < 0 ? 0xF0000000u : 0;

        // Combine BCD with sign bits
        uint result = bcdResult | signBits;

        SetUIntAccumulator1(result);
    }

    public void ITD()
    {
        short input = (short)(GetUIntAccumulator1() & 0xFFFF);
        int result = input;
        SetIntAccumulator1(result);
    }

    public void BTD()
    {
        uint fullValue = GetUIntAccumulator1();
        uint signs = fullValue & 0xF0000000u;
        uint bcdValue = fullValue & 0x0FFFFFFFu;

        uint val = 0;
        uint mult = 1;

        for (int digitIndex = 0; digitIndex < 7; digitIndex++)
        {
            uint digit = (bcdValue >> (digitIndex * 4)) & 0xF; // Extract digit
            val += digit * mult;
            mult *= 10;
        }

        int result;
        if (signs == 0xF0000000u) // negative sign bits
            result = -(int)val;
        else
            result = (int)val;

        SetIntAccumulator1(result);
    }

    public void ITB()
    {
        int value = GetIntAccumulator1();
        uint absValue = (uint)Math.Abs(value);
        uint bcdResult = 0;

        for (int digitIndex = 0; digitIndex < 3; digitIndex++)
        {
            uint digit = absValue % 10;
            absValue /= 10;

            bcdResult |= digit << (digitIndex * 4);
        }

        uint signBits = value < 0 ? 0xF000u : 0;

        uint result = bcdResult | signBits;

        SetUIntAccumulator1(result);
    }

    public void BTI()
    {
        uint fullValue = GetUIntAccumulator1();

        uint bcdValue = fullValue & 0x00000FFFu;
        uint signs = (fullValue >> 12) & 0xFu;

        uint val = 0;
        uint mult = 1;

        for (int digitIndex = 0; digitIndex < 3; digitIndex++)
        {
            uint digit = (bcdValue >> (digitIndex * 4)) & 0xF;
            val += digit * mult;
            mult *= 10;
        }

        int result;
        if (signs == 0xF)
            result = -(int)val;
        else
            result = (int)val;

        SetIntAccumulator1(result);
    }

}
