using Xunit;
using PLT.Interpreter.Parsing;
using PLT.Interpreter.Memory;
using System;

namespace PLT.Tests.InterpreterUnitTests
{
    public class ConversionTests
    {
        private StlInterpreter CreateInterpreter()
        {
            var memory = new MemoryModel();
            return new StlInterpreter(memory);
        }

        [Fact]
        public void RND_ShouldRoundCorrectly()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetFloatAccumulator1(2.6f);
            interpreter.RND();
            Assert.Equal(3, interpreter.GetIntAccumulator1());

            interpreter.SetFloatAccumulator1(2.4f);
            interpreter.RND();
            Assert.Equal(2, interpreter.GetIntAccumulator1());

            interpreter.SetFloatAccumulator1(-2.6f);
            interpreter.RND();
            Assert.Equal(-3, interpreter.GetIntAccumulator1());
        }

        [Fact]
        public void TRUNC_ShouldTruncateCorrectly()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetFloatAccumulator1(2.9f);
            interpreter.TRUNC();
            Assert.Equal(2, interpreter.GetIntAccumulator1());

            interpreter.SetFloatAccumulator1(-2.9f);
            interpreter.TRUNC();
            Assert.Equal(-2, interpreter.GetIntAccumulator1());
        }

        [Fact]
        public void RND_UP_ShouldCeilCorrectly()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetFloatAccumulator1(2.1f);
            interpreter.RND_UP();
            Assert.Equal(3, interpreter.GetIntAccumulator1());

            interpreter.SetFloatAccumulator1(-2.1f);
            interpreter.RND_UP();
            Assert.Equal(-2, interpreter.GetIntAccumulator1());
        }

        [Fact]
        public void RND_DOWN_ShouldFloorCorrectly()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetFloatAccumulator1(2.9f);
            interpreter.RND_DOWN();
            Assert.Equal(2, interpreter.GetIntAccumulator1());

            interpreter.SetFloatAccumulator1(-2.9f);
            interpreter.RND_DOWN();
            Assert.Equal(-3, interpreter.GetIntAccumulator1());
        }

        [Fact]
        public void CAW_ShouldSwapBytesCorrectly()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetUIntAccumulator1(0x12345678);
            interpreter.CAW();
            Assert.Equal(0x12347856u, interpreter.GetUIntAccumulator1());
        }

        [Fact]
        public void CAD_ShouldSwapDoubleWordBytesCorrectly()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetUIntAccumulator1(0x12345678);
            interpreter.CAD();
            Assert.Equal(0x78563412u, interpreter.GetUIntAccumulator1());
        }

        [Fact]
        public void NEGR_ShouldNegateFloat()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetFloatAccumulator1(5.5f);
            interpreter.NEGR();
            Assert.Equal(-5.5f, interpreter.GetFloatAccumulator1());
        }

        [Fact]
        public void NEGD_ShouldNegateInt()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetIntAccumulator1(42);
            interpreter.NEGD();
            Assert.Equal(-42, interpreter.GetIntAccumulator1());
        }

        [Fact]
        public void NEGI_ShouldNegateShort()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetShortAccumulator1((short)1234);
            interpreter.NEGI();
            Assert.Equal((short)-1234, interpreter.GetIntAccumulator1());
        }

        [Fact]
        public void INVI_ShouldInvertUShort()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetUShortAccumulator1(0xAAAA);
            interpreter.INVI();
            Assert.Equal(unchecked((short)~0xAAAA), interpreter.GetIntAccumulator1());
        }

        [Fact]
        public void INVD_ShouldInvertInt()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetIntAccumulator1(0x12345678);
            interpreter.INVD();
            Assert.Equal(~0x12345678, interpreter.GetIntAccumulator1());
        }

        [Fact]
        public void DTR_ShouldConvertIntToFloat()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetIntAccumulator1(123);
            interpreter.DTR();
            Assert.Equal(123f, interpreter.GetFloatAccumulator1());
        }

        [Fact]
        public void DTB_ShouldConvertIntToBCD()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetIntAccumulator1(1234567);
            interpreter.DTB();
            Assert.Equal(0x01234567u, interpreter.GetUIntAccumulator1());

            interpreter.SetIntAccumulator1(-1234567);
            interpreter.DTB();
            Assert.Equal(0xF1234567u, interpreter.GetUIntAccumulator1());
        }

        [Fact]
        public void ITD_ShouldConvertShortToInt()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetShortAccumulator1((short)12345);
            interpreter.ITD();
            Assert.Equal(12345, interpreter.GetIntAccumulator1());
        }

        [Fact]
        public void BTD_ShouldConvertBCDToInt()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetUIntAccumulator1(0x01234567u);
            interpreter.BTD();
            Assert.Equal(1234567, interpreter.GetIntAccumulator1());

            interpreter.SetUIntAccumulator1(0xF1234567u);
            interpreter.BTD();
            Assert.Equal(-1234567, interpreter.GetIntAccumulator1());
        }

        [Fact]
        public void ITB_ShouldConvertShortToBCD()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetShortAccumulator1((short)123);
            interpreter.ITB();
            Assert.Equal((ushort)0x0123, interpreter.GetUShortAccumulator1());

            interpreter.SetShortAccumulator1((short)-123);
            interpreter.ITB();
            Assert.Equal((ushort)0xF123, interpreter.GetUShortAccumulator1());
        }

        [Fact]
        public void ITB_WithUShort_ShouldConvertUShortToBCD()
        {
            var interpreter = CreateInterpreter();
            interpreter.ITB((ushort)123);
            Assert.Equal(0x0123u, interpreter.GetUIntAccumulator1());
        }

        [Fact]
        public void BTI_ShouldConvertBCDToShort()
        {
            var interpreter = CreateInterpreter();
            interpreter.SetUShortAccumulator1(0x0123);
            interpreter.BTI();
            Assert.Equal((short)123, interpreter.GetShortAccumulator1());

            interpreter.SetUShortAccumulator1(0xF123);
            interpreter.BTI();
            Assert.Equal((short)-123, interpreter.GetShortAccumulator1());
        }
    }
}