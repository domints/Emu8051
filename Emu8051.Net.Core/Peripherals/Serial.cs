using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Emu8051.Net.Core.Peripherals
{
    public class Serial : IPeripheral
    {
        private readonly TimeSpan _cycleTime;
        private readonly EmuCore _core;
        private int _txWaitClockCycles = -1;
        private int _rxWaitClockCycles = -1;
        private ulong clockCount = 0;
        private Queue<(ulong time, byte value)> _rxQueue = new();
        public Serial(TimeSpan cycleTime, EmuCore core)
        {
            _core = core;
            _cycleTime = cycleTime;
            List<(ulong time, byte[] data)> entries = new();
            foreach (var file in Directory.EnumerateFiles("serial_in"))
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                if (!ulong.TryParse(fileName, out ulong time))
                    continue;

                entries.Add((time, File.ReadAllBytes(file)));
            }

            foreach (var entry in entries.OrderBy(e => e.time))
            {
                foreach(var b in entry.data)
                {
                    _rxQueue.Enqueue((entry.time, b));
                }
            }
        }

        public void Process()
        {
            clockCount++;
            if ((_core.Memory.SFR.TCON & 0x40) != 0)
            {
                if (_txWaitClockCycles > 0)
                {
                    if (_core.Memory.SFR.TxSBUFWritten)
                        throw new InvalidOperationException("SBUF for TX written before previous byte was sent out!");

                    _txWaitClockCycles--;
                }
                else if (_txWaitClockCycles == 0)
                {
                    _core.Interrupts.Trigger(Interrupt.ES);
                    _core.Memory.SFR.SCON |= 0x02;
                    _txWaitClockCycles--;
                }
                if (_txWaitClockCycles < 1 && _core.Memory.SFR.TxSBUFWritten)
                {
                    _core.Memory.SFR.TxSBUFWritten = false;
                    File.AppendAllText("serial_out.txt", new string(new[] { (char)_core.Memory.SFR.TCON }));
                    _txWaitClockCycles = GetByteClockCount();
                }

                if (_rxWaitClockCycles > 0)
                {
                    _rxWaitClockCycles--;
                }
                else
                {
                    if ((_core.Memory.SFR.SCON & 0x10) != 0 && _rxQueue.TryPeek(out var r) && r.time < clockCount)
                    {
                        var data = _rxQueue.Dequeue();
                        _core.Memory.SFR.SBUF = data.value;
                        if (_core.Memory.SFR.SM0) {
                            var even = (BitOperations.PopCount(data.value) % 2) == 0;
                            if (even)
                                _core.Memory.SFR.SCON = (byte)(_core.Memory.SFR.SCON | 0x04 | ((_core.Memory.SFR.SM0 ? 1 : 0) << 7));
                            else
                                _core.Memory.SFR.SCON = (byte)((_core.Memory.SFR.SCON & 0xFB) | ((_core.Memory.SFR.SM0 ? 1 : 0) << 7));
                        }
                        _core.Interrupts.Trigger(Interrupt.ES);
                        _core.Memory.SFR.SCON |= 0x01;
                        _rxWaitClockCycles = GetByteClockCount();
                    }
                }
            }
        }

        private int GetBitClockCount()
        {
            var timerDivider = 0x100 - _core.Memory.SFR.TH1;
            if ((_core.Memory.SFR.PCON & 0x80) != 0)
                timerDivider /= 2;
            return 32 * timerDivider;
        }

        private int GetByteClockCount()
        {
            var baudsInByte = 10;
            if (_core.Memory.SFR.SM0)
                baudsInByte++;
            return GetBitClockCount() * baudsInByte;
        }
    }
}