using Xunit;
using PLT.Interpreter.Parsing;
using PLT.Interpreter.Memory;
using PLT.Interpreter.Data;
using PLT.Interpreter;
using System;

namespace PLT.Tests.InterpreterUnitTests
{
    public class ArithmeticIntTest
    {
        private StlInterpreter CreateInterpreter(int left, int right)
        {
            MemoryModel memory = new MemoryModel();
            var interpreter = new StlInterpreter(memory);
            interpreter.SetIntAccumulator1(left);
            interpreter.SetIntAccumulator2(right);
            return interpreter;
        }

        [Fact]
        public void Add_ShouldSumValues()
        {
            var interpreter = CreateInterpreter(5, 2);
            interpreter.ADD("D");
            Assert.Equal(7, interpreter.GetIntAccumulator1());
        }

        [Fact]
        public void Sub_ShouldSubtractValues()
        {
            var interpreter = CreateInterpreter(2, 8);
            interpreter.SUB("D");
            Assert.Equal(6, interpreter.GetIntAccumulator1());
        }

        [Fact]
        public void Mul_ShouldMultiplyValues()
        {
            var interpreter = CreateInterpreter(3, 7);
            interpreter.MUL("D");
            Assert.Equal(21, interpreter.GetIntAccumulator1());
        }

        [Fact]
        public void Div_ShouldDivideValues()
        {
            var interpreter = CreateInterpreter(3, 6);
            interpreter.DIV("D");
            Assert.Equal(2, interpreter.GetIntAccumulator1());
        }
    }
}