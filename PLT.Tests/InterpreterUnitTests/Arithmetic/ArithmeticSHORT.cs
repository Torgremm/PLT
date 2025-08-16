using Xunit;
using PLT.Interpreter.Parsing;
using PLT.Interpreter.Memory;
using PLT.Interpreter.Data;
using PLT.Interpreter;
using System;

namespace PLT.Tests.InterpreterUnitTests
{
    public class ArithmeticShortTest
    {
        private StlInterpreter CreateInterpreter(short left, short right)
        {
            MemoryModel memory = new MemoryModel();
            var interpreter = new StlInterpreter(memory);
            interpreter.SetShortAccumulator1(left);
            interpreter.SetShortAccumulator2(right);
            return interpreter;
        }

        [Fact]
        public void Add_ShouldSumValues()
        {
            var interpreter = CreateInterpreter(5, 2);
            interpreter.ADD("I");
            Assert.Equal(7, interpreter.GetShortAccumulator1());
        }

        [Fact]
        public void Sub_ShouldSubtractValues()
        {
            var interpreter = CreateInterpreter(2, 8);
            interpreter.SUB("I");
            Assert.Equal(6, interpreter.GetShortAccumulator1());
        }

        [Fact]
        public void Mul_ShouldMultiplyValues()
        {
            var interpreter = CreateInterpreter(3, 7);
            interpreter.MUL("I");
            Assert.Equal(21, interpreter.GetShortAccumulator1());
        }

        [Fact]
        public void Div_ShouldDivideValues()
        {
            var interpreter = CreateInterpreter(3, 6);
            interpreter.DIV("I");
            Assert.Equal(2, interpreter.GetShortAccumulator1());
        }
    }
}