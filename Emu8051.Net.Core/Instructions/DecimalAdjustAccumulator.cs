namespace Emu8051.Net.Core.Instructions
{
    public class DecimalAdjustAccumulator : IInstruction
    {
        public byte OpCode => 0xD4;

        public string Mnemonic => "DA";

        public string VariantMnemonic => "DA A";

        public int Cycles => 1;

        public void Execute(Memory memory)
        {
            var acc = (ushort)memory.Accumulator;
            if ((acc & 0x0f) > 0 || memory.SFR.AuxCarry)
            {
                acc += 0x06;
            }
            if ((acc & 0xf0) > 0 || memory.Carry)
            {
                acc += 0x60;
            }
            memory.Accumulator = (byte)acc;
            memory.Carry = acc > 0x99;
            memory.ProgramCounter++;
        }

        public string ValueString(Memory memory)
        {
            return $"{Mnemonic} A [0x{memory.Accumulator:X2}]";
        }
    }
}