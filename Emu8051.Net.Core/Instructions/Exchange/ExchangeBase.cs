namespace Emu8051.Net.Core.Instructions.Exchange
{
    public abstract class ExchangeBase : IInstruction
    {
        public abstract byte OpCode { get; }
        public string Mnemonic => "XCH";
        public abstract string VariantMnemonic { get; }
        public int Cycles => 1;

        public abstract void Execute(Memory memory);
        public abstract string ValueString(Memory memory);
    }
}