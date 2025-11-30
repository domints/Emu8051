using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emu8051.Net.Core;
using Emu8051.Net.Core.Instructions.Rotate;
using Xunit;

namespace Emu8051.Net.Tests.Instructions
{
    public class RR
    {
        [Fact]
        public void BaseRotation()
        {
            var mem = new Memory(1);
            mem.Accumulator = 0x10;
            mem.Carry = false;
            var instr = new RotateRight();

            instr.Execute(mem);

            Assert.Equal(0x08, mem.Accumulator);
        }

        [Fact]
        public void DoesNotLoadCarry()
        {
            var mem = new Memory(1);
            mem.Accumulator = 0x10;
            mem.Carry = true;
            var instr = new RotateRight();

            instr.Execute(mem);

            Assert.Equal(0x08, mem.Accumulator);
        }

        [Fact]
        public void PreservesCarry()
        {
            var mem = new Memory(1);
            mem.Accumulator = 0x11;
            mem.Carry = false;
            var instr = new RotateRight();

            instr.Execute(mem);

            Assert.Equal(0x88, mem.Accumulator);
            Assert.False(mem.Carry);
        }
    }
}