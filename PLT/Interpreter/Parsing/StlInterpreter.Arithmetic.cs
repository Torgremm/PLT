using PLT.Interpreter.Data;
using PLT.Interpreter.Memory;
using PLT.Interpreter.Data.Operations;
using PLT.Interpreter;

namespace PLT.Interpreter.Parsing;

internal partial class StlInterpreter
{
    private float IsOverflow(float value)
    {
        GetStatusFlags().OV = false;

        if (float.IsNaN(value) || float.IsInfinity(value))
            GetStatusFlags().OV = true;

        return value;
    }

    private static readonly PLCAddress dummyAddress = new PLCAddress("MW0");
    private DataVar GetDataVar(string c)
    {
        return c switch
        {
            "D" => DataVar.INT,
            "I" => DataVar.SHORT,
            "R" => DataVar.REAL,
            _ => throw new NotSupportedException($"Data type '{c}' is not supported.")
        };
    }

    public void ADD(string operand)
    {
        GetDataVar(operand).Accept(new MathOperationVisitor(this, dummyAddress, MathOperationType.ADD));
    }

    public void SUB(string operand)
    {
        GetDataVar(operand).Accept(new MathOperationVisitor(this, dummyAddress, MathOperationType.SUB));
    }

    public void MUL(string operand)
    {
        GetDataVar(operand).Accept(new MathOperationVisitor(this, dummyAddress, MathOperationType.MUL));
    }

    public void DIV(string operand)
    {
        GetDataVar(operand).Accept(new MathOperationVisitor(this, dummyAddress, MathOperationType.DIV));
    }

    public void ABS()
    {
        SetFloatAccumulator1(Math.Abs(GetFloatAccumulator1()));
    }

    public void SQR()
    {
        SetFloatAccumulator1(IsOverflow(GetFloatAccumulator1() * GetFloatAccumulator1()));
    }

    public void SQRT()
    {
        float value = GetFloatAccumulator1();
        if (value < 0)
            throw new InvalidOperationException("Cannot compute square root of a negative number.");
        
        SetFloatAccumulator1(MathF.Sqrt(value));
    }

    public void EXP()
    {
        SetFloatAccumulator1(IsOverflow(MathF.Exp(GetFloatAccumulator1())));
    }

    public void LN()
    {
        float value = GetFloatAccumulator1();
        if (value <= 0)
            throw new InvalidOperationException("Cannot compute natural logarithm of a non-positive number.");
        
        SetFloatAccumulator1(MathF.Log(value));
    }

    public void SIN()
    {
        SetFloatAccumulator1(MathF.Sin(GetFloatAccumulator1()));
    }

    public void COS()
    {
        SetFloatAccumulator1(MathF.Cos(GetFloatAccumulator1()));
    }

    public void TAN()
    {
        SetFloatAccumulator1(IsOverflow(MathF.Tan(GetFloatAccumulator1())));
    }

    public void ASIN()
    {
        float value = GetFloatAccumulator1();
        if (value < -1 || value > 1)
            throw new InvalidOperationException("Cannot compute arcsine of a value outside the range [-1, 1].");
        
        SetFloatAccumulator1(MathF.Asin(value));
    }

    public void ACOS()
    {
        float value = GetFloatAccumulator1();
        if (value < -1 || value > 1)
            throw new InvalidOperationException("Cannot compute arccosine of a value outside the range [-1, 1].");
        
        SetFloatAccumulator1(MathF.Acos(value));
    }
    
    public void ATAN()
    {
        SetFloatAccumulator1(MathF.Atan(GetFloatAccumulator1()));        
    }

}
