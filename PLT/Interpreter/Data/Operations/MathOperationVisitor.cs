using PLT.Interpreter.Memory;
using PLT.Interpreter.Parsing;
using System.Numerics;


namespace PLT.Interpreter.Data.Operations;

internal class MathOperationVisitor : AccumulatorOperationVisitorBase
{
    private readonly MathOperationType _type;
    public MathOperationVisitor(StlInterpreter interpreter, PLCAddress addr, MathOperationType type) : base(interpreter, addr)
    {
        _type = type;
    }

    private T PerformSigned<T>(T left, T right) where T : IBinaryInteger<T>
    {
        if (_type == MathOperationType.DIV && right == T.Zero)
            throw new DivideByZeroException("Division by zero on signed type");

        return _type switch
        {
            MathOperationType.ADD => left + right,
            MathOperationType.SUB => left - right,
            MathOperationType.MUL => left * right,
            MathOperationType.DIV => left / right,
            _ => throw new InvalidOperationException($"Unknown operation type: {_type}")
        };
    }
    
    private T PerformUnsigned<T>(T left, T right) where T : IBinaryInteger<T>, IUnsignedNumber<T>
    {
        if (_type == MathOperationType.DIV && right == T.Zero)
            throw new DivideByZeroException("Division by zero on unsigned type");

        return _type switch
        {
            MathOperationType.ADD => left + right,
            MathOperationType.SUB => left - right,
            MathOperationType.MUL => left * right,
            MathOperationType.DIV => left / right,
            _ => throw new InvalidOperationException($"Unknown operation type: {_type}")
        };
    }
    
    private T PerformReal<T>(T left, T right) where T : IFloatingPoint<T>
    {
        if (_type == MathOperationType.DIV && right == T.Zero)
            throw new DivideByZeroException("Division by zero on real type");

        return _type switch
        {
            MathOperationType.ADD => left + right,
            MathOperationType.SUB => left - right,
            MathOperationType.MUL => left * right,
            MathOperationType.DIV => left / right,
            _ => throw new InvalidOperationException($"Unknown operation type: {_type}")
        };
    }

    public override void VisitInt()
    {
        var ac1 = Interpreter.GetIntAccumulator1();
        var ac2 = Interpreter.GetIntAccumulator2();
        Interpreter.SetIntAccumulator1(PerformSigned(ac2, ac1));
    }

    public override void VisitShort()
    {
        var ac1 = Interpreter.GetShortAccumulator1();
        var ac2 = Interpreter.GetShortAccumulator2();
        Interpreter.SetShortAccumulator1(PerformSigned(ac2, ac1));
    }

    public override void VisitWord()
    {
        var ac1 = Interpreter.GetUShortAccumulator1();
        var ac2 = Interpreter.GetUShortAccumulator2();
        Interpreter.SetUShortAccumulator1(PerformUnsigned(ac2, ac1));
    }

    public override void VisitDWord()
    {
        var ac1 = Interpreter.GetUIntAccumulator1();
        var ac2 = Interpreter.GetUIntAccumulator2();
        Interpreter.SetUIntAccumulator1(PerformUnsigned(ac2, ac1));
    }

    public override void VisitReal()
    {
        var ac1 = Interpreter.GetFloatAccumulator1();
        var ac2 = Interpreter.GetFloatAccumulator2();
        Interpreter.SetFloatAccumulator1(PerformReal(ac2, ac1));
    }

    public override void VisitBool()
    {
        throw new InvalidOperationException("Cannot peform math operations on boolean types.");
    }
}
