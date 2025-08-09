using System.Text;
using Xunit;
using PLT.Interpreter;
using PLT.Interpreter.Data;
using PLT.Interpreter.Memory;
using PLT.Interpreter.Parsing;

namespace PLT.Tests;

public class BasicOperationsTest
{
    [Fact]
    public void AddTwoIntegers()
    {
        var memory = new MemoryModel();
        memory.RegisterVariable(DataVar.INT, 8); // MW0 - Load
        memory.RegisterVariable(DataVar.INT, 2); // MW2 - Value to Add

        var stlCode = new StringBuilder()
            .AppendLine("LD    MW0")    // Load MW0-8 to accumulator
            .AppendLine("ADD   MW2")    // Add MW2-2 from accumulator
            .AppendLine("STORE MW0")    // Store result in MW0
            .ToString();

        var runner = new TestRunner(stlCode, memory);

        runner.Execute();

        short result = memory.GetValue<short>(new PLCAddress("MW0"), DataVar.INT);
        Assert.Equal(10, result);
    }

    [Fact]
    public void SubtractTwoIntegers()
    {
        var memory = new MemoryModel();
        memory.RegisterVariable(DataVar.INT, 2); // MW0 - Load
        memory.RegisterVariable(DataVar.INT, 8); // MW2 - Value to Add

        var stlCode = new StringBuilder()
            .AppendLine("LD    MW0")    // Load MW0-8 to accumulator
            .AppendLine("SUB   MW2")    // Subtract MW2-2 from accumulator
            .AppendLine("STORE MW0")    // Store result in MW0
            .ToString();

        var runner = new TestRunner(stlCode, memory);

        runner.Execute();

        short result = memory.GetValue<short>(new PLCAddress("MW0"), DataVar.INT);
        Assert.Equal(-6, result);
    }

    [Fact]
    public void AddTwoIntegers_ConditionTrue_UsingJMPC()
    {
        var memory = new MemoryModel();
        memory.RegisterVariable(DataVar.BOOL, true); // M0.0 - Condition
        memory.RegisterVariable(DataVar.INT, 5);  // MW2 - Load
        memory.RegisterVariable(DataVar.INT, 3);  // MW4 - Value to Add

        var stlCode = new StringBuilder()
            .AppendLine("LDN        M0.0")     // Check condition
            .AppendLine("JMPC       END")       // Jump to END if condition is false                                     
            .AppendLine("LD         MW2")       // Add MW2 to accumulator
            .AppendLine("ADD        MW4")       // Add MW2 to accumulator
            .AppendLine("STORE      MW2")       // Store result in MW0
            .AppendLine("END:")                 // Label: END
            .AppendLine("NOP")                  // Do nothing
            .ToString();

        var runner = new TestRunner(stlCode, memory);

        runner.Execute();

        short result = memory.GetValue<short>(new PLCAddress("MW2"), DataVar.INT);
        Assert.Equal(8, result);
    }

    [Fact]
    public void AddTwoIntegers_ConditionFalse_UsingJMPC()
    {
        var memory = new MemoryModel();
        memory.RegisterVariable(DataVar.BOOL, false); // M0.0 - Condition
        memory.RegisterVariable(DataVar.INT, 5);  // MW2 - Load
        memory.RegisterVariable(DataVar.INT, 3);  // MW4 - Value to Add

        var stlCode = new StringBuilder()
            .AppendLine("LDN        M0.0")      // Check condition
            .AppendLine("JMPC       END")       // Jump to END if condition is false                                     
            .AppendLine("LD         MW2")       // Add MW2 to accumulator
            .AppendLine("ADD        MW4")       // Add MW2 to accumulator
            .AppendLine("STORE      MW2")       // Store result in MW0
            .AppendLine("END:")                 // Label: END
            .AppendLine("NOP")                  // Do nothing
            .ToString();

        var runner = new TestRunner(stlCode, memory);

        runner.Execute();

        short result = memory.GetValue<short>(new PLCAddress("MW2"), DataVar.INT);
        Assert.Equal(5, result);
    }


}
