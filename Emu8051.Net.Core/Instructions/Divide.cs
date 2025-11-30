namespace Emu8051.Net.Core.Instructions
{
    public class Divide : IInstruction
    {
        public byte OpCode => 0x84;

        public string Mnemonic => "DIV";

        public string VariantMnemonic => $"{Mnemonic} AB";

        public int Cycles => 4;

        public void Execute(Memory memory)
        {
            memory.Carry = false;
            if (memory.SFR.B == 0)
            {
                memory.SFR.Overflow = true;
            }
            else
            {
                memory.SFR.Overflow = false;
                var acc = (byte)((memory.Accumulator / memory.SFR.B) & 0xff);
                var b = (byte)(memory.Accumulator % memory.SFR.B);
                memory.Accumulator = acc;
                memory.SFR.B = b;
            }

            memory.ProgramCounter++;
        }

        public string ValueString(Memory memory)
        {
            return $"{Mnemonic} A [0x{memory.Accumulator:X2}] B [0x{memory.SFR.B:X2}]";
        }
    }
}