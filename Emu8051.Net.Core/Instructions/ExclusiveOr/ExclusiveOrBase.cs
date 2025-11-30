namespace Emu8051.Net.Core.Instructions.ExclusiveOr
{
    public abstract class ExclusiveOrBase : IInstruction
    {
        public abstract byte OpCode { get; }
        public string Mnemonic => "XLR";
        public abstract string VariantMnemonic { get; }
        public abstract int Cycles { get; }

        public abstract void Execute(Memory memory);
        public abstract string ValueString(Memory memory);
    }
}