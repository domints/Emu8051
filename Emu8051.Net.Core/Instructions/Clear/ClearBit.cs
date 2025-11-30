namespace Emu8051.Net.Core.Instructions.Clear
{
    public class ClearBit : ClearBase
    {
        public override byte OpCode => 0xC2;

        public override string VariantMnemonic => $"{Mnemonic} bit_addr";

        public override void Execute(Memory memory)
        {
            var bitAddress = memory.RomOffset(1);
            memory.SetBit(bitAddress, false);
            memory.ProgramCounter += 2;
        }

        public override string ValueString(Memory memory)
        {
            return $"{Mnemonic} bit {memory.RomOffset(1):X2} [{memory.GetBit(memory.RomOffset(1))}]";
        }
    }
}