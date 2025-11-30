using System.Diagnostics;
using Emu8051.Net.Core.Peripherals;

namespace Emu8051.Net.Core
{
    public class MCU
    {
        private readonly EmuCore _core;
        private CycleTimer? _timer;

        private List<IPeripheral> _peripherals = [];

        public EmuCore Core => _core;

        public delegate void BreakpointHitDelegate();
        public event BreakpointHitDelegate? BreakpointHit;

        public readonly TimeSpan CycleTime = TimeSpan.FromMicroseconds(1);

        public bool CantKeepUp => _timer != null && _timer.TickQueueLength > 0;

        public bool IsHalted { get; private set; }

        public IReadOnlyCollection<IPeripheral> Peripherals => _peripherals;

        public MCU(EmuCore core)
        {
            _core = core;
            _peripherals.Add(new Serial(CycleTime, core));
        }

        public void Halt()
        {
            _timer?.Stop();
            IsHalted = true;
        }

        public void Step()
        {
            _core.Step();
        }

        /// <summary>
        /// Runs instructions from ROM memory.
        /// </summary>
        /// <returns>true is finished because of task cancellation</returns>
        public async Task<bool> Run(CancellationToken cancellationToken, bool skipFirstBreakpointCheck = false)
        {
            try
            {
                _timer ??= new(CycleTime);
                var cycleCount = 0L;
                var runTime = new Stopwatch();
                runTime.Restart();
                var result = await Task.Run(() =>
                {
                    Console.WriteLine($"MCU Thread ID: {Thread.CurrentThread.ManagedThreadId}");
                    bool skipBreakpointCheck = skipFirstBreakpointCheck;
                    IsHalted = false;
                    int skipCoreCycles = 0;
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        foreach (var peripheral in Peripherals)
                            peripheral.Process();

                        if (skipCoreCycles > 0)
                            skipCoreCycles--;
                        else
                        {
                            if (!skipBreakpointCheck && CheckBreakpoint())
                            {
                                Halt();
                                return false;
                            }
                            else
                            {
                                skipBreakpointCheck = false;
                                skipCoreCycles = _core.Step() - 1;
                            }
                        }

                        cycleCount++;

                        _timer.WaitForNextTick();
                    }

                    return true;
                }, cancellationToken);
                Console.WriteLine($"Done {cycleCount} cycles in {runTime.ElapsedMilliseconds}ms, which is {((double)cycleCount) / ((double)runTime.ElapsedMilliseconds)} cycles per ms");
                return result;
            }
            catch
            {
                if (_timer != null)
                    _timer.Dispose();
                throw;
            }
        }

        private bool CheckBreakpoint()
        {
            if (Breakpoints.Contains(_core.Memory.ProgramCounter))
            {
                BreakpointHit?.Invoke();
                return true;
            }

            return false;
        }

        private HashSet<ushort> Breakpoints = [0x1fff, 0x026e, 0x0291];
    }
}