using Xunit;
using PLT.Interpreter.Parsing;
using PLT.Interpreter.Memory;

namespace PLT.Tests.InterpreterUnitTests
{
    public class AccumulatorsTests
    {
        private StlInterpreter CreateInterpreter()
        {
            var memory = new MemoryModel();
            return new StlInterpreter(memory);
        }

        [Fact]
        public void TAK_ShouldSwapIntAccumulators()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetIntAccumulator1(10);
            interpreter.SetIntAccumulator2(20);
            interpreter.TAK();
            Assert.Equal(20, interpreter.GetIntAccumulator1());
            Assert.Equal(10, interpreter.GetIntAccumulator2());
        }

        [Fact]
        public void PUSH_ShouldCopyAccumulator1ToAccumulator2()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetIntAccumulator1(42);
            interpreter.SetIntAccumulator2(0);
            interpreter.PUSH();
            Assert.Equal(42, interpreter.GetIntAccumulator2());
        }

        [Fact]
        public void POP_ShouldCopyAccumulator2ToAccumulator1()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetIntAccumulator2(99);
            interpreter.SetIntAccumulator1(0);
            interpreter.POP();
            Assert.Equal(99, interpreter.GetIntAccumulator1());
        }

        [Fact]
        public void INC_ShouldIncrementByteAccumulator1ByDefault()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetByteAccumulator1(10);
            interpreter.INC();
            Assert.Equal(11, interpreter.GetByteAccumulator1());
        }

        [Fact]
        public void INC_ShouldIncrementByteAccumulator1ByGivenStep()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetByteAccumulator1(250);
            interpreter.INC("5");
            Assert.Equal((byte)255, interpreter.GetByteAccumulator1());
        }

        [Fact]
        public void INC_ShouldWrapAroundByteAccumulator1()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetByteAccumulator1(250);
            interpreter.INC("10");
            Assert.Equal((byte)4, interpreter.GetByteAccumulator1());
        }

        [Fact]
        public void INC_ShouldThrowOnInvalidStep()
        {
            var interpreter = CreateInterpreter();
            Assert.Throws<ArgumentException>(() => interpreter.INC("-1"));
            Assert.Throws<ArgumentException>(() => interpreter.INC("256"));
            Assert.Throws<ArgumentException>(() => interpreter.INC("notanumber"));
        }

        [Fact]
        public void DEC_ShouldDecrementByteAccumulator1ByDefault()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetByteAccumulator1(10);
            interpreter.DEC();
            Assert.Equal(9, interpreter.GetByteAccumulator1());
        }

        [Fact]
        public void DEC_ShouldDecrementByteAccumulator1ByGivenStep()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetByteAccumulator1(10);
            interpreter.DEC("5");
            Assert.Equal((byte)5, interpreter.GetByteAccumulator1());
        }

        [Fact]
        public void DEC_ShouldWrapAroundByteAccumulator1()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetByteAccumulator1(5);
            interpreter.DEC("10");
            Assert.Equal((byte)251, interpreter.GetByteAccumulator1());
        }

        [Fact]
        public void DEC_ShouldThrowOnInvalidStep()
        {
            var interpreter = CreateInterpreter();
            Assert.Throws<ArgumentException>(() => interpreter.DEC("-1"));
            Assert.Throws<ArgumentException>(() => interpreter.DEC("256"));
            Assert.Throws<ArgumentException>(() => interpreter.DEC("notanumber"));
        }
    }
}