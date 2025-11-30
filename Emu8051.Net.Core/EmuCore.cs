//#define DETAILED_TIMING
using System.Diagnostics;
using System.Reflection;
using Emu8051.Net.Core.Instructions;

namespace Emu8051.Net.Core;


public class EmuCore
{
    private long _instructionCount = 0;

    private Dictionary<byte, IInstruction> _availableInstructions;
    public Memory Memory { get; private set; }
    public Stack<StackFrame> CallStack = new();
    public Interrupts Interrupts { get; private set; }
    public EmuCore(Variant variant, string romPath)
    {
        Memory = SetupMemory(variant);
        var romBytes = File.ReadAllBytes(romPath) ?? throw new InvalidDataException("ROM load result is somehow null...");
        Memory.LoadRom(romBytes);
        Interrupts = SetupInterrupts();

        _availableInstructions = SetupInstructions();
    }

    public EmuCore(Variant variant, byte[] rom)
    {
        Memory = SetupMemory(variant);
        Memory.LoadRom(rom);
        Interrupts = SetupInterrupts();

        _availableInstructions = SetupInstructions();
    }

    private Memory SetupMemory(Variant variant)
    {
        var ramSize = variant switch
        {
            Variant._8031 or Variant._8051 => 128,
            Variant._8032 or Variant._8052 => 256,
            _ => throw new NotImplementedException()
        };
        return new Memory(/*this, */ramSize, ushort.MaxValue);
    }

    private Interrupts SetupInterrupts()
    {
        return new Interrupts(this);
    }

    private Dictionary<byte, IInstruction> SetupInstructions()
    {
        var instrunctionInterface = typeof(IInstruction);
        var instructionTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => instrunctionInterface.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract);
        var availableInstructions = new Dictionary<byte, IInstruction>();
        foreach (var type in instructionTypes)
        {
            var bitVariant = type.GetCustomAttribute<BitVariantAttribute>();
            if (bitVariant != null)
            {
                var variantCount = Math.Pow(2, bitVariant.CommonBits);
                for (int i = 0; i < variantCount; i++)
                {
                    ConstructorInfo? ctor = type.GetConstructor([typeof(int)]);
                    if (ctor != null)
                    {
                        IInstruction instruction = (IInstruction)ctor.Invoke([i]);
                        availableInstructions.Add(instruction.OpCode, instruction);
                        continue;
                    }

                    ctor = type.GetConstructor([typeof(int), typeof(EmuCore)]);
                    if (ctor != null)
                    {
                        IInstruction instruction = (IInstruction)ctor.Invoke([i, this]);
                        availableInstructions.Add(instruction.OpCode, instruction);
                        continue;
                    }

                    throw new NotImplementedException();
                }
            }
            else
            {
                ConstructorInfo? ctor = type.GetConstructor([typeof(EmuCore)]);
                if (ctor != null)
                {
                    IInstruction instruction = (IInstruction)ctor.Invoke([this]);
                    availableInstructions.Add(instruction.OpCode, instruction);
                }
                else
                {
                    var instruction = (IInstruction)Activator.CreateInstance(type)!;
                    availableInstructions.Add(instruction.OpCode, instruction);
                }
            }
        }
        Console.WriteLine($"Loaded {availableInstructions.Count} instructions.");
        return availableInstructions;
    }

    public void Warmup()
    {
        var memory = new Memory(/*this, */Memory.RAM.Length, ushort.MaxValue);
        memory.LoadRom(Enumerable.Repeat((byte)1, (int)Math.Pow(2, 15)).ToArray());
        foreach (var instruction in _availableInstructions.Values)
        {
            instruction.Execute(memory);
        }
    }

    public void Reset()
    {
        var originalRom = Memory.ROM;
        Memory = new Memory(/*this, */Memory.RAM.Length, ushort.MaxValue);
        Memory.LoadRom(originalRom.ToArray());
    }

    public int Step()
    {
        Interrupts.CheckInterrupts();
        var opCode = Memory.ROM[Memory.ProgramCounter];
        if (!_availableInstructions.ContainsKey(opCode))
            throw new NotImplementedException($"Instruction with OPCODE 0x{opCode:x2} is not implemented!");
        var instruction = _availableInstructions[opCode];
        instruction.Execute(Memory);
        _instructionCount += instruction.Cycles;
        return instruction.Cycles;
    }
}
