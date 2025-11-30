namespace Emu8051.Net.Core.Instructions.Move
{
    public class MoveIRAMBitCarry : MoveBase
    {
        public override byte OpCode => 0x92;
        public override string VariantMnemonic => $"{Mnemonic} bit_addr,C";

        public override int Cycles => 2;

        public override void Execute(Memory memory)
        {
            var bitAddress = memory.RomOffset(1);
            memory.SetBit(bitAddress, memory.Carry);
            memory.ProgramCounter += 2;
        }

        public override string ValueString(Memory memory)
        {
            return $"{Mnemonic} IRAM [{memory.RomOffset(1):X2}; {(memory.GetBit(memory.RomOffset(1))?1:0)}], C [{(memory.Carry?1:0)}]";
        }
    }
}