namespace Emu8051.Net.Core.Instructions.SetBit
{
    public class SetBitAddr : IInstruction
    {
        public byte OpCode => 0xD2;

        public string Mnemonic => "SETB";

        public string VariantMnemonic => "SETB bit_addr";

        public int Cycles => 1;

        public void Execute(Memory memory)
        {
            var bitAddress = memory.RomOffset(1);
            memory.SetBit(bitAddress, true);
            memory.ProgramCounter += 2;
        }

        public string ValueString(Memory memory)
        {
            return $"{Mnemonic} bit {memory.RomOffset(1):X2} [{memory.GetBit(memory.RomOffset(1))}]";
        }
    }
}