using PLT.Interpreter.Data;
using PLT.Interpreter.Memory;
using PLT.Interpreter.Data.Operations;
using PLT.Interpreter;

namespace PLT.Interpreter.Parsing;

internal class StatusFlags
{
    private StlInterpreter _interpreter;

    public StatusFlags(StlInterpreter interpreter)
    {
        _interpreter = interpreter;
    }

    public bool OV { get; set; } //Overflow
    public bool OS { get; set; } 
    public bool OR { get; set; }
    public bool STA { get; set; }
    public bool BR { get; set; }

    public bool CC0 {get; set; }
    public bool CC1 {get; set; }

    public bool RLO
    {
        get => _interpreter.GetAccumulator1();
        set => _interpreter.SetAccumulator1(value);
    }
}