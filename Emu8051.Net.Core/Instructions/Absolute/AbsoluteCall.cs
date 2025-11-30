namespace Emu8051.Net.Core.Instructions.Absolute
{
    public abstract class AbsoluteCall : IInstruction
    {
        public abstract byte OpCode { get; }

        public string Mnemonic => "ACALL";

        public string VariantMnemonic => "ACALL address";

        public int Cycles => 2;

        protected int HighNibble => (OpCode & 0xe0) >> 5;

        public void Execute(Memory memory)
        {
            var retAddr = memory.ProgramCounter + 2;
            memory.PushToStack((byte)(retAddr & 0xFF));
            memory.PushToStack((byte)((retAddr >> 8) & 0xFF));
            var pcVal = (ushort)retAddr;
            pcVal = (ushort)(pcVal & ~0x7FF);
            pcVal = (ushort)(pcVal | ((HighNibble << 8) | memory.RomOffset(1)));
            memory.ProgramCounter = pcVal;
            //memory.Core.ProcessCall(originalPC, memory.ProgramCounter);
        }

        public string ValueString(Memory memory)
        {
            return $"ACALL 0x{memory.RomOffset(1):X2}";
        }
    }

    public class AbsoluteCall0 : AbsoluteCall
    {
        public override byte OpCode => 0x11;
    }

    public class AbsoluteCall1 : AbsoluteCall
    {
        public override byte OpCode => 0x31;
    }

    public class AbsoluteCall2 : AbsoluteCall
    {
        public override byte OpCode => 0x51;
    }

    public class AbsoluteCall3 : AbsoluteCall
    {
        public override byte OpCode => 0x71;
    }

    public class AbsoluteCall4 : AbsoluteCall
    {
        public override byte OpCode => 0x91;
    }

    public class AbsoluteCall5 : AbsoluteCall
    {
        public override byte OpCode => 0xB1;
    }

    public class AbsoluteCall6 : AbsoluteCall
    {
        public override byte OpCode => 0xD1;
    }

    public class AbsoluteCall7 : AbsoluteCall
    {
        public override byte OpCode => 0xF1;
    }
}