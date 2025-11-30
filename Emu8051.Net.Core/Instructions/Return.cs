namespace Emu8051.Net.Core.Instructions
{
    public class Return : IInstruction
    {
        public byte OpCode => 0x22;

        public string Mnemonic => "RET";

        public string VariantMnemonic => Mnemonic;

        public int Cycles => 2;

        public void Execute(Memory memory)
        {
            var pcH = memory.PopFromStack();
            var pcL = memory.PopFromStack();
            memory.ProgramCounter = (ushort)((pcH << 8) | pcL);
            //memory.Core.ProcessReturn();
        }

        public string ValueString(Memory memory)
        {
            return Mnemonic;
        }
    }
}