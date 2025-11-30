using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emu8051.Net.Core;
using Emu8051.Net.Core.Instructions.Absolute;
using Xunit;

namespace Emu8051.Net.Tests.Instructions
{
    public class ACALL
    {
        [Fact]
        public void CallToZero()
        {
            var mem = new Memory(16);
            mem.LoadRom(new byte[] { 0x00, 0x11, 0x00, 0x00, 0x00, 0x00 });
            mem.ProgramCounter = 1;
            var originalSP = mem.StackPointer;
            var instr = new AbsoluteCall0();

            instr.Execute(mem);

            Assert.Equal(0, mem.ProgramCounter);
            Assert.Equal(originalSP + 2, mem.StackPointer);
            Assert.Equal(0x03, mem.RAM[mem.StackPointer - 1]);
            Assert.Equal(0x00, mem.RAM[mem.StackPointer]);
        }

        [Fact]
        public void CallToFour()
        {
            var mem = new Memory(16);
            mem.LoadRom(new byte[] { 0x00, 0x11, 0x04, 0x00, 0x00, 0x00 });
            mem.ProgramCounter = 1;
            var originalSP = mem.StackPointer;
            var instr = new AbsoluteCall0();

            instr.Execute(mem);

            Assert.Equal(4, mem.ProgramCounter);
            Assert.Equal(originalSP + 2, mem.StackPointer);
            Assert.Equal(0x03, mem.RAM[mem.StackPointer - 1]);
            Assert.Equal(0x00, mem.RAM[mem.StackPointer]);
        }

        [Fact]
        public void CallsToPlaceInSame2KPage()
        {
            var mem = new Memory(16);
            var rom = Enumerable.Repeat((byte)0x00, 0x2137).Concat(Enumerable.Repeat((byte)0x00, 3)).Concat(new byte[] { 0x31, 0x37 }).ToArray();
            mem.LoadRom(rom);
            mem.ProgramCounter = 0x2137 + 3;
            var targetPC = mem.ProgramCounter + 2;
            var originalSP = mem.StackPointer;
            var instr = new AbsoluteCall1();

            instr.Execute(mem);

            Assert.Equal(0x2137, mem.ProgramCounter);
            Assert.Equal(originalSP + 2, mem.StackPointer);

            Assert.Equal(targetPC & 0xff, mem.RAM[mem.StackPointer - 1]);
            Assert.Equal((targetPC & 0xff00) >> 8, mem.RAM[mem.StackPointer]);
        }
    }
}