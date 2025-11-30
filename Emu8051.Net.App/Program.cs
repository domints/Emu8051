// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using Emu8051.Net.App;
using Emu8051.Net.Core;

List<MonitorValue> monitorValues = [];
//var romFile = "/Users/dominik/Documents/pixel/PIXEL-SIMS_2.1a_M27C256B.bin";
//var romFile = "/Users/dominik/Documents/8051/test.bin";
var romFile = "/Users/dominik/Documents/8051/BCD_TEST.bin";
var core = new EmuCore(Variant._8032, romFile);
for (int i = 0; i < 50; i++)
{
    core.Warmup();
}

CancellationTokenSource cts = new CancellationTokenSource();
var token = cts.Token;
var mcu = new MCU(core);

AddMonitorValues();
Console.Clear();
Console.WriteLine($"Waiting for a moment... I'm {Process.GetCurrentProcess().Id}");
Console.WriteLine("Press ESC to stop");
PrepareMonitor();

Thread.Sleep(10000);

var mcuTask = mcu.Run(token);

mcu.BreakpointHit += () =>
{
    OutputMonitor();
};

var cantKeepUp = false;
bool? halted = null;

ConsoleKeyInfo? lastKey = null;
do
{
    if (mcuTask.IsCompleted && mcuTask.Result)
        break;
    
    while (!Console.KeyAvailable)
    {
        if (mcuTask.IsCompleted && mcuTask.Result)
        break;

        if (halted != mcu.IsHalted)
        {
            halted = mcu.IsHalted;
            Console.CursorTop = 1;
            Console.CursorLeft = 0;
            Console.Write(mcu.IsHalted ? "HALT" : "RUN ");
        }
        
        if (cantKeepUp != mcu.CantKeepUp)
        {
            cantKeepUp = mcu.CantKeepUp;
            Console.CursorTop = 1;
            Console.CursorLeft = 40;
            Console.Write(cantKeepUp ? "TIME!" : "     ");
        }
    }

    lastKey = Console.ReadKey(true);
    if (mcu.IsHalted)
    {
        switch (lastKey.Value.Key) {
            case ConsoleKey.Escape:
                cts.Cancel();
                break;
            case ConsoleKey.S:
                mcu.Step();
                OutputMonitor();
                break;
            case ConsoleKey.R:
                mcuTask = mcu.Run(token, skipFirstBreakpointCheck: true);
                break;
            case ConsoleKey.D:
                Dump();
                break;
        }
    }
    else if (lastKey.Value.Key == ConsoleKey.H)
    {
        mcu.Halt();
        OutputMonitor();
    }

} while (lastKey.Value.Key != ConsoleKey.Escape);
await mcuTask;

void Dump()
{
    Console.CursorLeft = 0;
    Console.CursorTop = 6;
    var prompt = "Select memory to dump: [R]AM, [S]FR, [E]xternal RAM";
    Console.Write(prompt);
    var subKey = Console.ReadKey(true).Key;
    switch (subKey) {
        case ConsoleKey.R:
            DumpMemory(core.Memory.RAM, "ram.bin");
            break;
        case ConsoleKey.S:
            DumpMemory(core.Memory.SFR.AsBytes(), "sfr.bin");
            break;
        case ConsoleKey.E:
            DumpMemory(core.Memory.ExternalMemory, "external.bin");
            break;
    }
    Console.CursorLeft = 0;
    Console.Write(new string(Enumerable.Repeat(' ', prompt.Length).ToArray()));
}

void DumpMemory(byte[] memory, string fileName)
{
    File.WriteAllBytes(fileName, memory);
}

void PrepareMonitor()
{
    foreach (var value in monitorValues)
    {
        Console.CursorTop = value.LocationY;
        Console.CursorLeft = value.LocationX;
        Console.Write(value.Name);
        if (!value.ValueUnder)
            Console.Write(':');
    }
}

void OutputMonitor()
{
    foreach (var value in monitorValues)
    {
        if (value.Getter == null)
            continue;

        var (changed, val) = value.GetLatestValue(core.Memory);
        if (!changed)
            continue;
        
        Console.CursorTop = value.ValueUnder ? value.LocationY + 1 : value.LocationY;
        Console.CursorLeft = value.ValueUnder ? value.LocationX : value.LocationX + value.Name.Length + 1;
        Console.Write(val.ToString(value.Format, System.Globalization.CultureInfo.InvariantCulture));
    }
}

void AddMonitorValues()
{
    monitorValues.Add(new MonitorValue
    {
        Name = "PC",
        Getter = (mem) => mem.ProgramCounter,
        LocationX = 5,
        LocationY = 1,
        Format = "X4"
    });

    monitorValues.Add(new MonitorValue
    {
        Name = "ACC",
        Getter = (mem) => mem.Accumulator,
        LocationX = 0,
        LocationY = 2,
    });

    monitorValues.Add(new MonitorValue
    {
        Name = "B",
        Getter = (mem) => mem.SFR.B,
        LocationX = 7,
        LocationY = 2,
    });

    monitorValues.Add(new MonitorValue
    {
        Name = "DPTR",
        Getter = (mem) => mem.SFR.DPTR,
        LocationX = 12,
        LocationY = 2,
        Format = "X4"
    });

    monitorValues.Add(new MonitorValue
    {
        Name = "C",
        Getter = (mem) => mem.SFR.Carry ? 1 : 0,
        LocationX = 22,
        LocationY = 2,
        Format = null
    });

    monitorValues.Add(new MonitorValue
    {
        Name = "AC",
        Getter = (mem) => mem.SFR.AuxCarry ? 1 : 0,
        LocationX = 26,
        LocationY = 2,
        Format = null
    });

    monitorValues.Add(new MonitorValue
    {
        Name = "OV",
        Getter = (mem) => mem.SFR.Overflow ? 1 : 0,
        LocationX = 31,
        LocationY = 2,
        Format = null
    });

    monitorValues.Add(new MonitorValue
    {
        Name = "P",
        Getter = (mem) => mem.SFR.Parity ? 1 : 0,
        LocationX = 36,
        LocationY = 2,
        Format = null
    });

    var register = 0;
    for (; register < 8; register++)
    {
        var currentRegister = register;
        monitorValues.Add(new MonitorValue
        {
            Name = $"R{currentRegister}",
            Getter = (mem) => mem.GetBankRegisterValue(currentRegister),
            LocationX = register * 3,
            LocationY = 4,
            ValueUnder = true
        });
    }

    for (; register < 10; register++)
    {
        var currentRegister = register;
        monitorValues.Add(new MonitorValue
        {
            Name = $"@{currentRegister - 8}",
            Getter = (mem) => mem.RAM[mem.GetBankRegisterValue(currentRegister - 8)],
            LocationX = register * 3,
            LocationY = 4,
            ValueUnder = true
        });
    }
}