namespace Emu8051.Net.Core.Instructions.DecrementJumpNotZero
{
    public class DecrementJumpNotZeroIram : IInstruction
    {
        public byte OpCode => 0xD5;

        public string Mnemonic => "DJNZ";

        public string VariantMnemonic => $"{Mnemonic} iram_addr,rel_addr";

        public int Cycles => 2;

        public void Execute(Memory memory)
        {
            var iramAddr = memory.RomOffset(1);
            var value = memory.DirectMemory[iramAddr];
            value--;
            memory.DirectMemory[iramAddr] = value;
            if (value != 0)
            {

                memory.ProgramCounter = (ushort)(memory.ProgramCounter + 3 + (sbyte)memory.RomOffset(2));
            }
            else
            {
                memory.ProgramCounter += 3;
            }
        }

        public string ValueString(Memory memory)
        {
            return $"{Mnemonic} iram 0x{memory.RomOffset(1):X2} [0x{memory.DirectMemory[memory.RomOffset(1)]:X2}], {(sbyte)memory.RomOffset(2)}";
        }
    }
}