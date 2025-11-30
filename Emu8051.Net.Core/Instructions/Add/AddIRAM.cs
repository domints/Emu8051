namespace Emu8051.Net.Core.Instructions.Add
{
    public class AddIRAM : AddBase
    {
        public override byte OpCode => 0x25;

        public override string VariantMnemonic => $"{Mnemonic} A,iram_addr";

        public override void Execute(Memory memory)
        {
            var value = memory.DirectMemory[memory.RomOffset(1)];
            Add(value, false, memory);
            memory.ProgramCounter += 2;
        }

        public override string ValueString(Memory memory)
        {
            return $"{Mnemonic} A [{memory.Accumulator:X2}], IRAM [{memory.RomOffset(1):X2}; {memory.DirectMemory[memory.RomOffset(1)]:X2}]";
        }
    }
}