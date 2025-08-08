using System.Text;
using Xunit;
using PLT.Interpreter;
using PLT.Interpreter.Data;
using PLT.Interpreter.Memory;
using PLT.Interpreter.Parsing;

namespace PLT.Testing;

public class PlcInterpreterTests
{
    [Fact]
    public void AddsTwoIntegers_WhenBothConditionsAreTrue()
    {
        // Arrange
        var memory = new MemoryModel();
        memory.RegisterVariable(DataVar.BOOL, true);    // M0.0 - Condition 1
        memory.RegisterVariable(DataVar.BOOL, true);    // M0.1 - Condition 2
        memory.RegisterVariable(DataVar.INT, 8);        // MW0 - Target
        memory.RegisterVariable(DataVar.INT, 2);        // MW2 - Value to Add

        var stlCode = new StringBuilder()
            .AppendLine("LD    M0.0")   // Load condition 1
            .AppendLine("A     M0.1")   // AND condition 2
            .AppendLine("ADD   MW2")    // Add MW2 to accumulator
            .AppendLine("STORE MW0")    // Store result in MW0
            .ToString();

        var runner = new TestRunner(stlCode, memory);

        runner.Execute();

        short result = memory.GetValue<short>(new PLCAddress("MW0"), DataVar.INT);
        Assert.Equal(10, result);
    }
}
