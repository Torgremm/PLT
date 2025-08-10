namespace PLT.Interpreter.Data;

public enum DataVar
{
    BOOL,
    BYTE,
    WORD,
    INT,
    SHORT,
    DWORD,
    REAL
}

internal static class DataVarExtensions
{
    public static void Accept(this DataVar dataVar, IDataVarVisitor visitor)
    {
        switch (dataVar)
        {
            case DataVar.BOOL:
                visitor.VisitBool();
                break;
            case DataVar.INT:
                visitor.VisitInt();
                break;
            case DataVar.SHORT:
                visitor.VisitShort();
                break;
            case DataVar.WORD:
                visitor.VisitWord();
                break;
            case DataVar.DWORD:
                visitor.VisitDWord();
                break;
            case DataVar.REAL:
                visitor.VisitReal();
                break;
            default:
                throw new NotSupportedException($"DataVar {dataVar} not supported.");
        }
    }
}