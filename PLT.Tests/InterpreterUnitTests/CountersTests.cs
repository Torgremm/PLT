using Xunit;
using PLT.Interpreter.Parsing;
using PLT.Interpreter.Memory;
using System;

namespace PLT.Tests.InterpreterUnitTests
{
    public class CountersTests
    {
        private StlInterpreter CreateInterpreter()
        {
            var memory = new MemoryModel();
            return new StlInterpreter(memory);
        }

        [Fact]
        public void CounterAddressToIndex_ShouldParseCounterIndex()
        {
            var interpreter = CreateInterpreter();
            Assert.Equal(5, interpreter.CounterAddressToIndex("C5"));
            Assert.Equal(0, interpreter.CounterAddressToIndex("C0"));
            Assert.Equal(0, interpreter.CounterAddressToIndex("X123"));
        }

        [Fact]
        public void FR_ShouldStartCounter()
        {
            var interpreter = CreateInterpreter(); //Pointless
            interpreter.FR("C2");
            Assert.Equal(0, interpreter.GetMemory().GetCounter(2));
        }

        [Fact]
        public void CU_ShouldIncrementCounter()
        {
            var interpreter = CreateInterpreter();
            interpreter.FR("C3");
            interpreter.CU("C3");
            Assert.Equal(1, interpreter.GetMemory().GetCounter(3));
        }

        [Fact]
        public void CD_ShouldDecrementCounter()
        {
            var interpreter = CreateInterpreter();
            interpreter.FR("C4");
            interpreter.CU("C4");
            interpreter.CD("C4");
            Assert.Equal(0, interpreter.GetMemory().GetCounter(4));
        }

        [Fact]
        public void S_ShouldSetCounter()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetUShortAccumulator1(1234);
            interpreter.S("C7");
            Assert.Equal(1234, interpreter.GetMemory().GetCounter(7));
        }

        [Fact]
        public void S_ShouldThrowOnUnsupportedDataType()
        {
            var interpreter = CreateInterpreter();
            Assert.Throws<FormatException>(() => interpreter.S("DB1.DBD0"));
        }

        [Fact]
        public void R_ShouldResetCounter()
        {
            var interpreter = CreateInterpreter();
            interpreter.FR("C8");
            interpreter.S("C8");
            interpreter.R("C8");
            Assert.Equal(0, interpreter.GetMemory().GetCounter(8));
        }

        [Fact]
        public void R_ShouldThrowOnUnsupportedDataType()
        {
            var interpreter = CreateInterpreter();
            Assert.Throws<FormatException>(() => interpreter.R("DB1.DBD0"));
        }
    }
}