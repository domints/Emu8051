namespace Emu8051.Net.Core.Instructions.Jump
{
    public class JumpIfBitNotSet : IInstruction
    {
        public byte OpCode => 0x30;

        public string Mnemonic => "JNB";

        public string VariantMnemonic => "JNB bit_addr,rel_addr";

        public int Cycles => 2;

        public void Execute(Memory memory)
        {
            var bitAddress = memory.RomOffset(1);
            if(!memory.GetBit(bitAddress))
            {
                var offset = (sbyte)memory.RomOffset(2);
                var newPc = memory.ProgramCounter + 3 + offset;
                memory.ProgramCounter = (ushort)newPc;
            }
            else
            {
                memory.ProgramCounter += 3;
            }
        }

        public string ValueString(Memory memory)
        {
            return $"{Mnemonic} bit {memory.RomOffset(1)} [{(memory.GetBit(memory.RomOffset(1))?1:0)}] rel {(sbyte)memory.RomOffset(2)}";
        }
    }
}