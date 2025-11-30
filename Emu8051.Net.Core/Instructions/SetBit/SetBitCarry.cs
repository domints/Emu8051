namespace Emu8051.Net.Core.Instructions.SetBit
{
    public class SetBitCarry : IInstruction
    {
        public byte OpCode => 0xD3;

        public string Mnemonic => "SETB";

        public string VariantMnemonic => "SETB C";

        public int Cycles => 1;

        public void Execute(Memory memory)
        {
            memory.Carry = true;
            memory.ProgramCounter++;
        }

        public string ValueString(Memory memory)
        {
            return VariantMnemonic;
        }
    }
}