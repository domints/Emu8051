namespace Emu8051.Net.Core.Instructions.Add
{
    public abstract class AddCommonBase : IInstruction
    {
        public abstract byte OpCode { get; }
        public abstract string Mnemonic { get; }
        public abstract string VariantMnemonic { get; }
        public int Cycles => 1;

        public abstract void Execute(Memory memory);
        public abstract string ValueString(Memory memory);

        protected void Add(byte value, bool includeCarry, Memory memory)
        {
            var originalCarry = memory.Carry;
            var carryValue = includeCarry && originalCarry ? 1 : 0;
            var result = memory.Accumulator + value + carryValue;
            memory.Carry = result > 0xff;
            memory.SFR.AuxCarry = (((memory.Accumulator & 0x0f) + (value & 0x0f) + carryValue) & 0xf0) > 0;
            var c6 = (((memory.Accumulator & 0x7f) + (value & 0x7f) + carryValue) & 0x80) > 0;
            memory.SFR.Overflow = memory.Carry ^ c6;
            memory.Accumulator = (byte)(result & 0xff);
        }
    }

    public abstract class AddBase : AddCommonBase
    {
        public override string Mnemonic => "ADD";
    }

    public abstract class AddCarryBase : AddCommonBase
    {
        public override string Mnemonic => "ADDC";
    }
}