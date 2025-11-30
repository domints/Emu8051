using System.Collections.Concurrent;
using System.Diagnostics;
using Emu8051.Net.Core.Tools;

namespace Emu8051.Net.Core
{
    public class CycleTimer : IDisposable
    {
        readonly Stopwatch _stopwatch;
        readonly long _cycleTicks;
        readonly ConcurrentQueue<long> _tickQueue = new();
        readonly FifoSemaphore _semaphore = new(1);
        long? _lastTicks = null;
        bool _run;
        Task? timerLoopTask;

        public int TickQueueLength => _tickQueue.Count;

        public static int? TimerTaskId;

        public CycleTimer(TimeSpan cycleTime)
        {
            var swTicksInTsTicks = Stopwatch.Frequency / TimeSpan.TicksPerSecond;
            _cycleTicks = cycleTime.Ticks * swTicksInTsTicks;
            _stopwatch = new Stopwatch();
        }

        public void Stop()
        {
            _stopwatch.Stop();
            _run = false;
        }

        public void Start()
        {
            _run = true;
            _stopwatch.Start();
            TimerLoop();

            _lastTicks ??= _stopwatch.ElapsedTicks;
        }

        private void TimerLoop()
        {
            timerLoopTask = Task.Run(() =>
            {
                TimerTaskId = Task.CurrentId;
                //var acquired = false;
                while (_run)
                {
                    //_semaphore.Acquire();
                    //acquired = true;
                    while (_run)
                    {
                        var currTicks = _stopwatch.ElapsedTicks;
                        if (currTicks >= _lastTicks + _cycleTicks)
                        {
                            _tickQueue.Enqueue(currTicks);
                            //_semaphore.Release();
                            //acquired = false;
                            _lastTicks = currTicks;
                            break;
                        }
                    }
                }
                // if (acquired)
                //     _semaphore.Release();
            });
        }

        public void WaitForNextTick()
        {
            if (!_stopwatch.IsRunning)
                Start();

            while (!_tickQueue.TryDequeue(out long _))
            {
                //_semaphore.Acquire();
                //_semaphore.Release();
            }
        }

        public void Dispose()
        {
            _run = false;
            Stop();
            timerLoopTask?.Wait();
        }
    }
}