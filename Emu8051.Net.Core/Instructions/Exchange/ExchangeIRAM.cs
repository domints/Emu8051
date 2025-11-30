namespace Emu8051.Net.Core.Instructions.Exchange
{
    public class ExchangeIRAM : ExchangeBase
    {
        public override byte OpCode => 0xC5;
        public override string VariantMnemonic => $"{Mnemonic} A,iram_addr";

        public override void Execute(Memory memory)
        {
            var accumulatorValue = memory.Accumulator;
            memory.Accumulator = memory.DirectMemory[memory.RomOffset(1)];
            memory.DirectMemory[memory.RomOffset(1)] = accumulatorValue;
            memory.ProgramCounter += 2;
        }

        public override string ValueString(Memory memory)
        {
            return $"{Mnemonic} A [{memory.Accumulator:X2}], IRAM [{memory.RomOffset(1):X2}; {memory.DirectMemory[memory.RomOffset(1)]:X2}]";
        }
    }
}