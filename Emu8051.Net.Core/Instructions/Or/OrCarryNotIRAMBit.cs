namespace Emu8051.Net.Core.Instructions.Or
{
    public class OrCarryNotIRAMBit : OrBase
    {
        public override byte OpCode => 0xA0;
        public override string VariantMnemonic => $"{Mnemonic} C,/bit_addr";

        public override int Cycles => 2;

        public override void Execute(Memory memory)
        {
            var bitAddress = memory.RomOffset(1);
            memory.Carry = memory.Carry | !memory.GetBit(bitAddress);
            memory.ProgramCounter += 2;
        }

        public override string ValueString(Memory memory)
        {
            return $"{Mnemonic} C [{(memory.Carry?1:0)}], /IRAM [{memory.RomOffset(1):X2}; {(memory.GetBit(memory.RomOffset(1))?1:0)}]";
        }
    }
}