using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emu8051.Net.Core;
using Emu8051.Net.Core.Instructions;
using Xunit;

namespace Emu8051.Net.Tests.Instructions
{
    public class NOP
    {
        [Fact]
        public void MovesPC()
        {
            var mem = new Memory(1);
            var instr = new Nop();

            instr.Execute(mem);

            Assert.Equal(1, mem.ProgramCounter);
        }
    }
}