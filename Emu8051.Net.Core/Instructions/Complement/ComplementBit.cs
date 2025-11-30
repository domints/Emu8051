namespace Emu8051.Net.Core.Instructions.Complement
{
    public class ComplementBit : IInstruction
    {
        public byte OpCode => 0xB2;

        public string Mnemonic => "CPL";

        public string VariantMnemonic => "CPL bit_addr";

        public int Cycles => 1;

        public void Execute(Memory memory)
        {
            var bitAddr = memory.RomOffset(1);
            memory.SetBit(bitAddr, memory.GetBit(bitAddr));

            memory.ProgramCounter += 2;
        }

        public string ValueString(Memory memory)
        {
            return $"{Mnemonic} bit {memory.RomOffset(1):X2} [{memory.GetBit(memory.RomOffset(1))}]";
        }
    }
}