using PLT.Interpreter.Data;
using PLT.Interpreter.Memory;
using PLT.Interpreter.Data.Operations;
using PLT.Interpreter;

namespace PLT.Interpreter.Parsing;

internal partial class StlInterpreter
{
    public void SSI(string operand)
    {
        if (int.TryParse(operand, out int shiftCount))
        {
            shiftCount &= 0xF;

            short value = GetShortAccumulator1();
            SetShortAccumulator1((short)(value >> shiftCount));
        }
        else
        {
            throw new ArgumentException("Invalid shift count for SSI instruction.");
        }
    }

    public void SSD(string operand)
    {
        if (int.TryParse(operand, out int shiftCount))
        {
            shiftCount &= 0x1F;

            int value = GetIntAccumulator1();
            SetIntAccumulator1(value >> shiftCount);
        }
        else
        {
            throw new ArgumentException("Invalid shift count for SSD instruction.");
        }
    }

    public void SLW(string operand)
    {
        if (int.TryParse(operand, out int shiftCount))
        {
            shiftCount &= 0xF;

            ushort value = GetUShortAccumulator1();
            SetUShortAccumulator1((ushort)(value << shiftCount));
        }
        else
        {
            throw new ArgumentException("Invalid shift count for SLW instruction.");
        }
    }

    public void SRW(string operand)
    {
        if (int.TryParse(operand, out int shiftCount))
        {
            shiftCount &= 0xF;

            ushort value = GetUShortAccumulator1();
            SetUShortAccumulator1((ushort)(value >> shiftCount));
        }
        else
        {
            throw new ArgumentException("Invalid shift count for SRW instruction.");
        }
    }

    public void SLD(string operand)
    {
        if (int.TryParse(operand, out int shiftCount))
        {
            shiftCount &= 0x1F;

            uint value = GetUIntAccumulator1();
            SetUIntAccumulator1(value << shiftCount);
        }
        else
        {
            throw new ArgumentException("Invalid shift count for SLD instruction.");
        }
    }

    public void SRD(string operand)
    {
        if (int.TryParse(operand, out int shiftCount))
        {
            shiftCount &= 0x1F;

            uint value = GetUIntAccumulator1();
            SetUIntAccumulator1(value >> shiftCount);
        }
        else
        {
            throw new ArgumentException("Invalid shift count for SRD instruction.");
        }
    }

    public void RLD(string operand)
    {
        if (int.TryParse(operand, out int shiftCount))
        {
            shiftCount &= 0x1F;

            uint value = GetUIntAccumulator1();
            uint rotated = (value << shiftCount) | (value >> (32 - shiftCount));
            SetUIntAccumulator1(rotated);
        }
        else
        {
            throw new ArgumentException("Invalid shift count for RLD instruction.");
        }
    }

    public void RRD(string operand)
    {
        if (int.TryParse(operand, out int shiftCount))
        {
            shiftCount &= 0x1F;

            uint value = GetUIntAccumulator1();
            uint rotated = (value >> shiftCount) | (value << (32 - shiftCount));
            SetUIntAccumulator1(rotated);
        }
        else
        {
            throw new ArgumentException("Invalid shift count for RRD instruction.");
        }
    }

    public void RLDA(string operand)
    {
        if (int.TryParse(operand, out int shiftCount))
        {
            shiftCount &= 0x1F;

            uint value = GetUIntAccumulator1();
            uint rotated = (value << shiftCount) | (value >> (32 - shiftCount));
            SetUIntAccumulator1(rotated);
        }
        else
        {
            throw new ArgumentException("Invalid shift count for RLDA instruction.");
        }
    }

    public void RRDA(string operand)
    {
        if (int.TryParse(operand, out int shiftCount))
        {
            shiftCount &= 0x1F;

            uint value = GetUIntAccumulator1();
            uint rotated = (value >> shiftCount) | (value << (32 - shiftCount));
            SetUIntAccumulator1(rotated);
        }
        else
        {
            throw new ArgumentException("Invalid shift count for RRDA instruction.");
        }
    }

}