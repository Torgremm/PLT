using Xunit;
using PLT.Interpreter.Parsing;
using PLT.Interpreter.Memory;
using PLT.Interpreter.Data;
using PLT.Interpreter;
using System;
using System.Reflection;


namespace PLT.Tests.InterpreterUnitTests
{
    public class CompareShortTest
    {
        private bool[] CreateInterpreter(string op, string type)
        {
            var method = typeof(StlInterpreter).GetMethods()
                .FirstOrDefault(m => string.Equals(m.Name, op, StringComparison.OrdinalIgnoreCase));

            MemoryModel memory = new MemoryModel();
            var interpreter = new StlInterpreter(memory);
            
            interpreter.SetShortAccumulator1(3);
            interpreter.SetShortAccumulator2(2);

            method?.Invoke(interpreter, [type]);
            bool result1 = interpreter.GetRLO();

            interpreter.SetShortAccumulator1(3);
            interpreter.SetShortAccumulator2(3);

            method?.Invoke(interpreter, [type]);
            bool result2 = interpreter.GetRLO();

            interpreter.SetShortAccumulator1(3);
            interpreter.SetShortAccumulator2(4);

            method?.Invoke(interpreter, [type]);
            bool result3 = interpreter.GetRLO();

            return new[] { result1, result2, result3 };
        }

        [Fact]
        public void LEQ_ShouldReturnTrueTrueFalse()
        {
            bool[] result = CreateInterpreter("LEQ", "I");
            Assert.Equal(new[] { true, true, false }, result);
        }

        [Fact]
        public void GEQ_ShouldReturnFalseTrueTrue()
        {
            bool[] result = CreateInterpreter("GEQ", "I");
            Assert.Equal(new[] { false, true, true }, result);
        }

        [Fact]
        public void LT_ShouldReturnTrueFalseFalse()
        {
            bool[] result = CreateInterpreter("LT", "I");
            Assert.Equal(new[] { true, false, false }, result);
        }

        [Fact]
        public void GT_ShouldReturnFalseFalseTrue()
        {
            bool[] result = CreateInterpreter("GT", "I");
            Assert.Equal(new[] { false, false, true }, result);
        }

        [Fact]
        public void NEQ_ShouldReturnTrueFalseTrue()
        {
            bool[] result = CreateInterpreter("NEQ", "I");
            Assert.Equal(new[] { true, false, true }, result);
        }
        
        [Fact]
        public void EQ_ShouldReturnFalseTrueFalse()
        {
            bool[] result = CreateInterpreter("EQ", "I");
            Assert.Equal( new[] {false,true,false}, result);
        }
    }
}