using System.Text.RegularExpressions;
using PLT.Interpreter.Memory;
using PLT.Interpreter.Parsing;


namespace PLT.Interpreter.Data.Operations;

internal class ConversionOperationVisitor : AccumulatorOperationVisitorBase
{
    private readonly ConversionType _type;
    public ConversionOperationVisitor(StlInterpreter interpreter, PLCAddress addr, ConversionType type) : base(interpreter, addr)
    {
        _type = type;
    }

    private int Convert(float val)
    {
        return _type switch
        {
            ConversionType.RND => (int)Math.Round(val, MidpointRounding.AwayFromZero),
            ConversionType.TRUNC => (int)Math.Truncate(val),
            ConversionType.RND_UP => (int)Math.Ceiling(val),
            ConversionType.RND_DOWN => (int)Math.Floor(val),
            _ => throw new InvalidOperationException($"Unknown comparison type: {_type}")
        };
    }

    public override void VisitBool()
    {
        throw new InvalidOperationException("Cannot convert Bool to numeric type.");
    }

    public override void VisitInt()
    {
        Interpreter.GetMemory().SetValue(Addr, DataVar.INT, Interpreter.GetIntAccumulator());
    }

    public override void VisitWord()
    {
        Interpreter.GetMemory().SetValue(Addr, DataVar.WORD, Interpreter.GetIntAccumulator());
    }

    public override void VisitDWord()
    {
        Interpreter.GetMemory().SetValue(Addr, DataVar.DWORD, Interpreter.GetUIntAccumulator());
    }

    public override void VisitReal()
    {
        var ac1 = Interpreter.GetFloatAccumulator();
        Interpreter.SetIntAccumulator(Convert(ac1)); 
    }
}
