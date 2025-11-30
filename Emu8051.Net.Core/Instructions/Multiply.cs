namespace Emu8051.Net.Core.Instructions
{
    public class Multiply : IInstruction
    {
        public byte OpCode => 0xA4;

        public string Mnemonic => "MUL";

        public string VariantMnemonic => "MUL AB";

        public int Cycles => 4;

        public void Execute(Memory memory)
        {
            var result = memory.Accumulator * memory.SFR.B;
            memory.Carry = false;
            memory.SFR.Overflow = result > 255;
            memory.Accumulator = (byte)(result & 0xFF);
            memory.SFR.B = (byte)((result >> 8) & 0xFF);
            memory.ProgramCounter++;
        }

        public string ValueString(Memory memory)
        {
            return $"{Mnemonic} A [0x{memory.Accumulator:X2}] B [0x{memory.SFR.B:X2}]";
        }
    }
}