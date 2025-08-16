using Xunit;
using PLT.Interpreter.Parsing;
using PLT.Interpreter.Memory;
using System;
using PLT.Interpreter;

namespace PLT.Tests.InterpreterUnitTests
{
    public class DataFlowTests
    {
        private StlInterpreter CreateInterpreter()
        {
            var memory = new MemoryModel();
            return new StlInterpreter(memory);
        }

        private Executor.InstructionPointer CreatePointer(int value = 0)
        {
            return new Executor.InstructionPointer { Value = value };
        }

        private Dictionary<string, int> CreateLabels(params (string, int)[] pairs)
        {
            var dict = new Dictionary<string, int>();
            
            foreach (var (label, idx) in pairs)
                dict[label] = idx;

            return dict;
        }

        [Fact]
        public void JU_ShouldJumpToLabel()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 5));

            interpreter.JU("LBL", pointer, labels);

            Assert.Equal(5, pointer.Value);
        }

        [Fact]
        public void JC_ShouldJumpIfRLOTrue()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 3));
            interpreter.SetRLO(true);

            interpreter.JC("LBL", pointer, labels);

            Assert.Equal(3, pointer.Value);
        }

        [Fact]
        public void JC_ShouldNotJumpIfRLOFalse()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 3));
            interpreter.SetRLO(false);

            interpreter.JC("LBL", pointer, labels);

            Assert.Equal(0, pointer.Value);
        }

        [Fact]
        public void JCN_ShouldJumpIfRLOFalse()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 7));
            interpreter.SetRLO(false);

            interpreter.JCN("LBL", pointer, labels);

            Assert.Equal(7, pointer.Value);
        }

        [Fact]
        public void JCN_ShouldNotJumpIfRLOTrue()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 7));
            interpreter.SetRLO(true);

            interpreter.JCN("LBL", pointer, labels);

            Assert.Equal(0, pointer.Value);
        }

        [Fact]
        public void JCB_ShouldJumpAndSetBRIfRLOTrue()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 2));
            interpreter.SetRLO(true);

            interpreter.JCB("LBL", "dummy", pointer, labels);

            Assert.Equal(2, pointer.Value);
            Assert.True(interpreter.GetStatusFlags().BR);
        }

        [Fact]
        public void JCB_ShouldNotJumpAndSetBRIfRLOFalse()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 2));
            interpreter.SetRLO(false);

            interpreter.JCB("LBL", "dummy", pointer, labels);

            Assert.Equal(0, pointer.Value);
            Assert.False(interpreter.GetStatusFlags().BR);
        }

        [Fact]
        public void JNB_ShouldJumpAndSetBRIfRLOFalse()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 4));
            interpreter.SetRLO(false);

            interpreter.JNB("LBL", "dummy", pointer, labels);

            Assert.Equal(4, pointer.Value);
            Assert.False(interpreter.GetStatusFlags().BR);
        }

        [Fact]
        public void JNB_ShouldNotJumpAndSetBRIfRLOTrue()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 4));
            interpreter.SetRLO(true);

            interpreter.JNB("LBL", "dummy", pointer, labels);

            Assert.Equal(0, pointer.Value);
            Assert.True(interpreter.GetStatusFlags().BR);
        }

        [Fact]
        public void JBI_ShouldJumpIfBRTrue()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 6));
            interpreter.GetStatusFlags().BR = true;

            interpreter.JBI("LBL", pointer, labels);

            Assert.Equal(6, pointer.Value);
        }

        [Fact]
        public void JBI_ShouldNotJumpIfBRFalse()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 6));
            interpreter.GetStatusFlags().BR =false;

            interpreter.JBI("LBL", pointer, labels);

            Assert.Equal(0, pointer.Value);
        }

        [Fact]
        public void JNBI_ShouldJumpIfBRFalse()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 8));
            interpreter.GetStatusFlags().BR = false;

            interpreter.JNBI("LBL", pointer, labels);

            Assert.Equal(8, pointer.Value);
        }

        [Fact]
        public void JNBI_ShouldNotJumpIfBRTrue()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 8));
            interpreter.GetStatusFlags().BR = true;

            interpreter.JNBI("LBL", pointer, labels);

            Assert.Equal(0, pointer.Value);
        }

        [Fact]
        public void JO_ShouldJumpIfOVTrue()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 9));
            interpreter.GetStatusFlags().OV = true;

            interpreter.JO("LBL", pointer, labels);

            Assert.Equal(9, pointer.Value);
        }

        [Fact]
        public void JO_ShouldNotJumpIfOVFalse()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 9));
            interpreter.GetStatusFlags().OV = false;

            interpreter.JO("LBL", pointer, labels);

            Assert.Equal(0, pointer.Value);
        }

        [Fact]
        public void JOS_ShouldJumpIfOSTrue()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 10));
            interpreter.GetStatusFlags().OS = true;

            interpreter.JOS("LBL", pointer, labels);

            Assert.Equal(10, pointer.Value);
        }

        [Fact]
        public void JOS_ShouldNotJumpIfOSFalse()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 10));
            interpreter.GetStatusFlags().OS = false;

            interpreter.JOS("LBL", pointer, labels);

            Assert.Equal(0, pointer.Value);
        }

        [Fact]
        public void JOZ_ShouldJumpIfShortAccumulator1IsZero()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 11));
            interpreter.SetShortAccumulator1(0);

            interpreter.JOZ("LBL", pointer, labels);

            Assert.Equal(11, pointer.Value);
        }

        [Fact]
        public void JOZ_ShouldNotJumpIfShortAccumulator1IsNotZero()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 11));
            interpreter.SetShortAccumulator1(5);

            interpreter.JOZ("LBL", pointer, labels);

            Assert.Equal(0, pointer.Value);
        }

        [Fact]
        public void JN_ShouldJumpIfShortAccumulator1IsNotZero()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 12));
            interpreter.SetShortAccumulator1(7);

            interpreter.JN("LBL", pointer, labels);

            Assert.Equal(12, pointer.Value);
        }

        [Fact]
        public void JN_ShouldNotJumpIfShortAccumulator1IsZero()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 12));
            interpreter.SetShortAccumulator1(0);

            interpreter.JN("LBL", pointer, labels);

            Assert.Equal(0, pointer.Value);
        }

        [Fact]
        public void JP_ShouldJumpIfIntAccumulator1GreaterThanZero()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 13));
            interpreter.SetIntAccumulator1(1);

            interpreter.JP("LBL", pointer, labels);

            Assert.Equal(13, pointer.Value);
        }

        [Fact]
        public void JP_ShouldNotJumpIfIntAccumulator1NotGreaterThanZero()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 13));
            interpreter.SetIntAccumulator1(0);

            interpreter.JP("LBL", pointer, labels);

            Assert.Equal(0, pointer.Value);
        }

        [Fact]
        public void JM_ShouldJumpIfIntAccumulator1LessThanZero()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 14));
            interpreter.SetIntAccumulator1(-1);

            interpreter.JM("LBL", pointer, labels);

            Assert.Equal(14, pointer.Value);
        }

        [Fact]
        public void JM_ShouldNotJumpIfIntAccumulator1NotLessThanZero()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 14));
            interpreter.SetIntAccumulator1(0);

            interpreter.JM("LBL", pointer, labels);

            Assert.Equal(0, pointer.Value);
        }

        [Fact]
        public void JPZ_ShouldJumpIfIntAccumulator1GreaterOrEqualZero()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 15));
            interpreter.SetIntAccumulator1(0);

            interpreter.JPZ("LBL", pointer, labels);

            Assert.Equal(15, pointer.Value);

            pointer.Value = 0;
            interpreter.SetIntAccumulator1(5);

            interpreter.JPZ("LBL", pointer, labels);

            Assert.Equal(15, pointer.Value);
        }

        [Fact]
        public void JPZ_ShouldNotJumpIfIntAccumulator1LessThanZero()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 15));
            interpreter.SetIntAccumulator1(-1);

            interpreter.JPZ("LBL", pointer, labels);

            Assert.Equal(0, pointer.Value);
        }

        [Fact]
        public void JMZ_ShouldJumpIfIntAccumulator1LessOrEqualZero()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 16));
            interpreter.SetIntAccumulator1(0);

            interpreter.JMZ("LBL", pointer, labels);

            Assert.Equal(16, pointer.Value);

            pointer.Value = 0;
            interpreter.SetIntAccumulator1(-5);

            interpreter.JMZ("LBL", pointer, labels);

            Assert.Equal(16, pointer.Value);
        }

        [Fact]
        public void JMZ_ShouldNotJumpIfIntAccumulator1GreaterThanZero()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 16));
            interpreter.SetIntAccumulator1(1);

            interpreter.JMZ("LBL", pointer, labels);

            Assert.Equal(0, pointer.Value);
        }

        [Fact]
        public void LOOP_ShouldDecrementAndJumpIfShortAccumulator1GreaterThanZero()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 17));
            interpreter.SetShortAccumulator1(2);

            interpreter.LOOP("LBL", pointer, labels);

            Assert.Equal(17, pointer.Value);
            Assert.Equal(1, interpreter.GetShortAccumulator1());
        }

        [Fact]
        public void LOOP_ShouldNotJumpIfShortAccumulator1ZeroOrLess()
        {
            var interpreter = CreateInterpreter();
            var pointer = CreatePointer(0);
            var labels = CreateLabels(("LBL", 17));
            interpreter.SetShortAccumulator1(0);

            interpreter.LOOP("LBL", pointer, labels);

            Assert.Equal(0, pointer.Value);
            Assert.Equal(0, interpreter.GetShortAccumulator1());
        }
    }
}