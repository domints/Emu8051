namespace Emu8051.Net.Core.Instructions.Clear
{
    public abstract class ClearBase : IInstruction
    {
        public abstract byte OpCode { get; }
        public string Mnemonic => "CLR";

        public int Cycles => 1;

        public abstract string VariantMnemonic { get; }

        public abstract void Execute(Memory memory);
        public abstract string ValueString(Memory memory);
    }
}