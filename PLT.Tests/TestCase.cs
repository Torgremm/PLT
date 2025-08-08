using System.Text;
using Xunit;
using PLT.Interpreter;
using PLT.Interpreter.Data;
using PLT.Interpreter.Memory;
using PLT.Interpreter.Parsing;

namespace PLT.Tests;

public class PlcInterpreterTests
{
    [Fact]
    public void AddsTwoIntegers_WhenBothConditionsAreTrue()
    {
        // Arrange
        var memory = new MemoryModel();
        memory.RegisterVariable(DataVar.INT, 8); // MW0 - Load
        memory.RegisterVariable(DataVar.INT, 2); // MW2 - Value to Add

        var stlCode = new StringBuilder()
            .AppendLine("LD    MW0")    // Load MW0-8 to accumulator
            .AppendLine("SUB   MW2")    // Subtract MW2-2 from accumulator
            .AppendLine("STORE MW0")    // Store result in MW0
            .ToString();

        var runner = new TestRunner(stlCode, memory);

        runner.Execute();

        short result = memory.GetValue<short>(new PLCAddress("MW0"), DataVar.INT);
        Assert.Equal(6, result);
    }
}
