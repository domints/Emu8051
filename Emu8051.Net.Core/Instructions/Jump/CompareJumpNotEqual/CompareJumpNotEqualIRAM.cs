namespace Emu8051.Net.Core.Instructions.Jump.CompareJumpNotEqual
{
    public class CompareJumpNotEqualIRAM : CompareJumpNotEqualBase
    {

        public override byte OpCode => 0xB5;
        public override string VariantMnemonic => $"{Mnemonic} A,iram_addr,rel_addr";

        public override void Execute(Memory memory)
        {
            var iram_addr = memory.RomOffset(1);
            var value = memory.DirectMemory[iram_addr];
            var addressOffset = (sbyte)memory.RomOffset(2);
            memory.ProgramCounter += 3;
            var registerValue = memory.Accumulator;
            if (registerValue != value)
                memory.ProgramCounter = (ushort)(memory.ProgramCounter + addressOffset);

            memory.Carry = registerValue < value;
        }

        public override string ValueString(Memory memory)
        {
            return $"{Mnemonic} A [{memory.Accumulator:X2}], IRAM [{memory.RomOffset(1):X2}; {memory.DirectMemory[memory.RomOffset(1)]:X2}], {(sbyte)memory.RomOffset(2)}";
        }
    }
}