using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emu8051.Net.Core;
using Emu8051.Net.Core.Instructions.Rotate;
using Xunit;

namespace Emu8051.Net.Tests.Instructions
{
    public class RRC
    {
        [Fact]
        public void BaseRotation()
        {
            var mem = new Memory(1);
            mem.Accumulator = 0x10;
            mem.Carry = false;
            var instr = new RotateRightThroughCarry();

            instr.Execute(mem);

            Assert.Equal(0x08, mem.Accumulator);
        }

        [Fact]
        public void LoadsCarry()
        {
            var mem = new Memory(1);
            mem.Accumulator = 0x10;
            mem.Carry = true;
            var instr = new RotateRightThroughCarry();

            instr.Execute(mem);

            Assert.Equal(0x88, mem.Accumulator);
        }

        [Fact]
        public void StoresTrueCarry()
        {
            var mem = new Memory(1);
            mem.Accumulator = 0x11;
            mem.Carry = false;
            var instr = new RotateRightThroughCarry();

            instr.Execute(mem);

            Assert.Equal(0x08, mem.Accumulator);
            Assert.True(mem.Carry);
        }

        [Fact]
        public void StoresFalseCarry()
        {
            var mem = new Memory(1);
            mem.Accumulator = 0x10;
            mem.Carry = true;
            var instr = new RotateRightThroughCarry();

            instr.Execute(mem);

            Assert.Equal(0x88, mem.Accumulator);
            Assert.False(mem.Carry);
        }
    }
}