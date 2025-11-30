using System.Collections;
using System.Numerics;

namespace Emu8051.Net.Core
{
    public class SpecialFunctionRegisters : IReadOnlyList<byte>
    {
        public const byte PCONAddr = 0x87;
        public const byte TCONAddr = 0x88;
        public const byte TH1Addr = 0x8D;
        public const byte SCONAddr = 0x98;
        public const byte SBUFAddr = 0x99;
        public const byte IEAddr = 0xA8;
        public const byte AccAddr = 0xE0;

        private readonly byte[] _values = new byte[128];
        private byte _sbufTx;

        public bool SM0 = false;

        public byte this[int index] { get => _values[index & 0x7f]; set => UpdateRegisters((byte)index, value); }

        public int Count => 128;

        public IEnumerator<byte> GetEnumerator()
        {
            return (IEnumerator<byte>)_values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        public byte StackPointer
        {
            get
            {
                return _values[1];
            }
            set
            {
                _values[1] = value;
            }
        }

        public byte Accumulator { get => this[AccAddr]; set => this[AccAddr] = value; }

        public ushort DPTR
        {
            get
            {
                return (ushort)(this[0x83] << 8 | this[0x82]);
            }
            set
            {
                this[0x82] = (byte)(value & 0xff);
                this[0x83] = (byte)((value >> 8) & 0xff);
            }
        }

        public byte DPH { get => this[0x83]; set => this[0x83] = value; }
        public byte DPL { get => this[0x82]; set => this[0x82] = value; }

        public byte B { get => this[0xf0]; set => this[0xF0] = value; }

        public byte TH1 { get => this[TH1Addr]; set => this[TH1Addr] = value; }
        public byte TCON { get => this[TCONAddr]; set => this[TCONAddr] = value; }
        public byte SCON { get => this[SCONAddr]; set => this[SCONAddr] = value; }
        public byte PCON { get => this[PCONAddr]; set => this[PCONAddr] = value; }

        /// <summary>
        /// It's magic value to make sure RX procedure won't overwrite TX buffer and vice versa
        /// </summary>
        public byte SBUF { get => _sbufTx; set => _values[SBUFAddr & 0x7f] = value; }
        public bool TxSBUFWritten { get; set; }

        public bool Carry
        {
            get => (this[0x50] & Consts.Carry) > 0;
            set
            {
                if (value)
                    this[0x50] |= Consts.Carry;
                else
                    this[0x50] &= 0xFF - Consts.Carry;
            }
        }

        public bool Overflow
        {
            get => (this[0x50] & Consts.Overflow) > 0;
            set
            {
                if (value)
                    this[0x50] |= Consts.Overflow;
                else
                    this[0x50] &= 0xFF - Consts.Overflow;
            }
        }

        public bool AuxCarry
        {
            get => (this[0x50] & Consts.AuxCarry) > 0;
            set
            {
                if (value)
                    this[0x50] |= Consts.AuxCarry;
                else
                    this[0x50] &= 0xFF - Consts.AuxCarry;
            }
        }

        public bool Parity
        {
            get => (this[0x50] & Consts.Parity) > 0;
        }

        public byte CurrentBank
        {
            get => (byte)((this[0x50] & Consts.BankSelect) >> 3);
            set
            {
                if (value > 3)
                    throw new InvalidOperationException("No such bank!");

                this[0x50] &= 0xE7;
                this[0x50] |= (byte)((value & 0x03) << 3);
            }
        }

        public byte[] AsBytes()
        {
            return _values;
        }

        private void UpdateRegisters(byte address, byte value)
        {
            var index = address & 0x7f;
            bool store = true;
            if (index == 0x60) // Update Parity flag when updating accumulator
            {
                var odd = (BitOperations.PopCount(value) % 2) == 1;
                if (odd)
                    this[0x50] |= Consts.Parity;
                else
                    this[0x50] &= 0xFF - Consts.Parity;
            }
            else if (index == (SBUFAddr & 0x7f))
            {
                _sbufTx = value;
                TxSBUFWritten = true;
                store = false;
            }
            else if (index == (SCONAddr & 0x7f))
            {
                SM0 = (value & 0x80) != 0;
                _values[index] = (byte)(value & 0x7f);
                store = false;
            }

            if(store)
                _values[index] = value;
        }
    }
}