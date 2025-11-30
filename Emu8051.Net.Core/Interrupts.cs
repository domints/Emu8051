using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Emu8051.Net.Core
{
    public class Interrupts
    {
        public EmuCore Core { get; }
        public Interrupt TriggeredInterrupts { get; private set; }
        public bool InISR { get; private set; }
        public Interrupts(EmuCore core)
        {
            this.Core = core;
        }

        public void Trigger(Interrupt interrupt) => TriggeredInterrupts |= interrupt;

        public void ExitISR()
        {
            InISR = false;
        }

        public void CheckInterrupts()
        {
            if (!InISR && TriggeredInterrupts != 0)
            {
                var ie = Core.Memory.SFR[SpecialFunctionRegisters.IEAddr];
                if (ie < 0x80)
                {
                    TriggeredInterrupts = 0;
                    return;
                }

                foreach (var interrupt in Enum.GetValues<Interrupt>())
                {
                    // TODO: Implement priority handling
                    if (TriggeredInterrupts.HasFlag(interrupt))
                    {
                        TriggeredInterrupts &= ~interrupt;
                        if (interrupt != Interrupt.EA && (ie & (int)interrupt) != 0)
                        {
                            Core.Memory.PushToStack((byte)(Core.Memory.ProgramCounter & 0xFF));
                            Core.Memory.PushToStack((byte)((Core.Memory.ProgramCounter >> 8) & 0xFF));
                            Core.Memory.ProgramCounter = GetISRVector(interrupt);
                            InISR = true;
                            break;
                        }
                    }
                }
            }
        }

        private ushort GetISRVector(Interrupt interrupt)
        {
            return interrupt switch
            {
                Interrupt.EX0 => 0x0003,
                Interrupt.ET0 => 0x000B,
                Interrupt.EX1 => 0x0013,
                Interrupt.ET1 => 0x001B,
                Interrupt.ES => 0x0023,
                Interrupt.ET2 => 0x002B,
                _ => throw new InvalidEnumArgumentException()
            };
        }
    }

    [Flags]
    public enum Interrupt
    {
        EX0 = 1,
        ET0 = 2,
        EX1 = 4,
        ET1 = 8,
        ES = 16,
        ET2 = 32,
        //EC = 64,
        EA = 128
    }
}