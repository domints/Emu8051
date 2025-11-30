namespace Emu8051.Net.Core.Instructions.MoveExternal
{
    public abstract class MoveExternalBase : IInstruction
    {
        public abstract byte OpCode { get; }
        public string Mnemonic => "MOVX";
        public abstract string VariantMnemonic { get; }
        public abstract int Cycles { get; }

        public abstract void Execute(Memory memory);

        protected void StoreAccInExternalMemory(Memory memory, int address)
        {
            if (address > memory.ExternalMemory.Length)
                throw new InvalidOperationException("8051 External Memory Out of memory!");

            memory.ExternalMemory[address] = memory.Accumulator;
        }

        protected void ReadAccFromExternalMemory(Memory memory, int address)
        {
            if (address > memory.ExternalMemory.Length)
                throw new InvalidOperationException("8051 External Memory Out of memory!");

            memory.Accumulator = memory.ExternalMemory[address];
        }

        public abstract string ValueString(Memory memory);
    }
}