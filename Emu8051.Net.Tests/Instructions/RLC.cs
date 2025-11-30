using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emu8051.Net.Core;
using Emu8051.Net.Core.Instructions.Rotate;
using Xunit;

namespace Emu8051.Net.Tests.Instructions
{
    public class RLC
    {
        [Fact]
        public void BaseRotation()
        {
            var mem = new Memory(1);
            mem.Accumulator = 0x08;
            mem.Carry = false;
            var instr = new RotateLeftThroughCarry();

            instr.Execute(mem);

            Assert.Equal(0x10, mem.Accumulator);
        }

        [Fact]
        public void LoadsCarry()
        {
            var mem = new Memory(1);
            mem.Accumulator = 0x08;
            mem.Carry = true;
            var instr = new RotateLeftThroughCarry();

            instr.Execute(mem);

            Assert.Equal(0x11, mem.Accumulator);
        }

        [Fact]
        public void StoresTrueCarry()
        {
            var mem = new Memory(1);
            mem.Accumulator = 0x88;
            mem.Carry = false;
            var instr = new RotateLeftThroughCarry();

            instr.Execute(mem);

            Assert.Equal(0x10, mem.Accumulator);
            Assert.True(mem.Carry);
        }

        [Fact]
        public void StoresFalseCarry()
        {
            var mem = new Memory(1);
            mem.Accumulator = 0x08;
            mem.Carry = true;
            var instr = new RotateLeftThroughCarry();

            instr.Execute(mem);

            Assert.Equal(0x11, mem.Accumulator);
            Assert.False(mem.Carry);
        }
    }
}