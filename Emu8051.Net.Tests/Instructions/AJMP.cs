using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emu8051.Net.Core;
using Emu8051.Net.Core.Instructions.Absolute;
using Xunit;

namespace Emu8051.Net.Tests.Instructions
{
    public class AJMP
    {
        [Fact]
        public void TestJumpToZero()
        {
            var mem = new Memory(1);
            mem.LoadRom(new byte[] { 0x00, 0x01, 0x00, 0x00, 0x00, 0x00 });
            mem.ProgramCounter = 1;
            var instr = new AbsoluteJump0();

            instr.Execute(mem);

            Assert.Equal(0, mem.ProgramCounter);
        }

        [Fact]
        public void TestJumpToFour()
        {
            var mem = new Memory(1);
            mem.LoadRom(new byte[] { 0x00, 0x01, 0x04, 0x00, 0x00, 0x00 });
            mem.ProgramCounter = 1;
            var instr = new AbsoluteJump0();

            instr.Execute(mem);

            Assert.Equal(4, mem.ProgramCounter);
        }

        [Fact]
        public void JumpsToPlaceInSame2KPage()
        {
            var mem = new Memory(1);
            var rom = Enumerable.Repeat((byte)0x00, 0x2137).Concat(Enumerable.Repeat((byte)0x00, 3)).Concat(new byte[] { 0x21, 0x37 }).ToArray();
            mem.LoadRom(rom);
            mem.ProgramCounter = 0x2137 + 3;
            var instr = new AbsoluteJump1();

            instr.Execute(mem);

            Assert.Equal(0x2137, mem.ProgramCounter);
        }
    }
}