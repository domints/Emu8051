namespace Emu8051.Net.Core.Instructions.Add
{
    public class AddCarryIRAM : AddCarryBase
    {
        public override byte OpCode => 0x35;

        public override string VariantMnemonic => $"{Mnemonic} A,iram_addr";

        public override void Execute(Memory memory)
        {
            var value = memory.DirectMemory[memory.RomOffset(1)];
            Add(value, true, memory);
            memory.ProgramCounter += 2;
        }

        public override string ValueString(Memory memory)
        {
            return $"{Mnemonic} A [0x{memory.Accumulator:X2}], IRAM [0x{memory.RomOffset(1):X2}; 0x{memory.DirectMemory[memory.RomOffset(1)]:X2}]";
        }
    }
}