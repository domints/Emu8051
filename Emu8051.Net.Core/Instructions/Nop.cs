namespace Emu8051.Net.Core.Instructions
{
    public class Nop : IInstruction
    {
        public byte OpCode => 0x00;

        public string Mnemonic => "NOP";

        public string VariantMnemonic => Mnemonic;

        public int Cycles => 1;

        public void Execute(Memory memory)
        {
            memory.ProgramCounter++;
        }

        public string ValueString(Memory memory)
        {
            return Mnemonic;
        }
    }
}