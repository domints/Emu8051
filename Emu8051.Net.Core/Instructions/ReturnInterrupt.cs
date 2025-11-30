namespace Emu8051.Net.Core.Instructions
{
    public class ReturnInterrupt : IInstruction
    {
        public byte OpCode => 0x32;

        public string Mnemonic => "RETI";

        public string VariantMnemonic => Mnemonic;

        public int Cycles => 2;
        private readonly EmuCore _core;

        public ReturnInterrupt(EmuCore core)
        {
            _core = core;
        }

        public void Execute(Memory memory)
        {
            //TODO: Handle interrupt stuff
            var pcH = memory.PopFromStack();
            var pcL = memory.PopFromStack();
            memory.ProgramCounter = (ushort)((pcH << 8) | pcL);
            _core.Interrupts.ExitISR();
            //_core.ProcessReturn();
        }

        public string ValueString(Memory memory)
        {
            return Mnemonic;
        }
    }
}