namespace PLT.Interpreter;
public enum DataVar
{
    BOOL,
    BYTE,
    WORD,
    INT,
    DWORD,
    REAL
}

internal static class DataVarExtensions
{
    internal static void Accept(this DataVar dataVar, IDataVarVisitor visitor)
    {
        switch (dataVar)
        {
            case DataVar.BOOL:
                visitor.VisitBool();
                break;
            case DataVar.INT:
                visitor.VisitInt();
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