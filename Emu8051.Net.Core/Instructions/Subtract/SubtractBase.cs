namespace Emu8051.Net.Core.Instructions.Subtract
{
    public abstract class SubtractBase : IInstruction
    {
        public abstract byte OpCode { get; }
        public string Mnemonic => "SUB";
        public abstract string VariantMnemonic { get; }
        public int Cycles => 1;

        public abstract void Execute(Memory memory);
        public abstract string ValueString(Memory memory);

        protected void SubtractAndSetFlags(byte other, Memory memory)
        {
            byte result = (byte)(memory.Accumulator - other);
            var currentCarry = memory.Carry;
            var carryVal = currentCarry ? 1 : 0;
            if (currentCarry)
                result--;

            var resetVal = (byte)(~(Consts.Carry | Consts.AuxCarry | Consts.Overflow) & 0xFF);
            memory.SFR[Consts.ProgramStatusWord] &= resetVal;
            memory.Carry = memory.Accumulator < (uint)(other + carryVal);
            memory.SFR.Overflow =
                (memory.Accumulator < 0x80 && other > 0x7f && result > 0x7f) ||
                (memory.Accumulator > 0x7f && other < 0x80 && result < 0x80);
            memory.SFR.AuxCarry =
                ((memory.Accumulator & 0x0F) < ((other + carryVal) & 0x0f)) ||
                (currentCarry && (other & 0x0f) == 0x0f);

            memory.Accumulator = result;
        }
    }
}