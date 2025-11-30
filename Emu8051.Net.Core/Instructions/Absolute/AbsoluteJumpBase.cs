namespace Emu8051.Net.Core.Instructions.Absolute
{
    public abstract class AbsoluteJumpBase : IInstruction
    {
        public abstract byte OpCode { get; }

        public string Mnemonic => "AJMP";

        public string VariantMnemonic => "AJMP address";

        public int Cycles => 2;

        protected int HighNibble => (OpCode & 0xe0) >> 5;

        public void Execute(Memory memory)
        {
            var pcVal = memory.ProgramCounter + 2;
            pcVal = (ushort)(pcVal & ~0x7FF);
            pcVal = (ushort)(pcVal | (HighNibble << 8) | memory.RomOffset(1));
            memory.ProgramCounter = (ushort)pcVal;
        }

        public string ValueString(Memory memory)
        {
            return $"AJMP {OpCode & 0x70 | memory.RomOffset(1):X3}";
        }
    }

    public class AbsoluteJump0 : AbsoluteJumpBase
    {
        public override byte OpCode => 0x01;
    }

    public class AbsoluteJump1 : AbsoluteJumpBase
    {
        public override byte OpCode => 0x21;
    }

    public class AbsoluteJump2 : AbsoluteJumpBase
    {
        public override byte OpCode => 0x41;
    }

    public class AbsoluteJump3 : AbsoluteJumpBase
    {
        public override byte OpCode => 0x61;
    }

    public class AbsoluteJump4 : AbsoluteJumpBase
    {
        public override byte OpCode => 0x81;
    }

    public class AbsoluteJump5 : AbsoluteJumpBase
    {
        public override byte OpCode => 0xA1;
    }

    public class AbsoluteJump6 : AbsoluteJumpBase
    {
        public override byte OpCode => 0xC1;
    }

    public class AbsoluteJump7 : AbsoluteJumpBase
    {
        public override byte OpCode => 0xE1;
    }
}