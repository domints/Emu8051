namespace Emu8051.Net.Core.Instructions.And
{
    public abstract class AndBase : IInstruction
    {
        public abstract byte OpCode { get; }
        public string Mnemonic => "ANL";
        public abstract string VariantMnemonic { get; }
        public abstract int Cycles { get; }

        public abstract void Execute(Memory memory);
        public abstract string ValueString(Memory memory);
    }
}