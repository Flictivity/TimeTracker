using System;
using System.Diagnostics;

namespace TimeTracker.PagesApp
{
    public class StopWatchWithOffset
    {
        private Stopwatch _stopwatch = null;
        TimeSpan _offsetTimeSpan;

        public StopWatchWithOffset(TimeSpan offsetElapsedTimeSpan)
        {
            _offsetTimeSpan = offsetElapsedTimeSpan;
            _stopwatch = new Stopwatch();
        }

        public void Start()
        {
            _stopwatch.Start();
        }

        public void Reset()
        {
            _stopwatch.Reset();
        }

        public void Stop()
        {
            _stopwatch.Stop();
        }

        public TimeSpan ElapsedTimeSpan
        {
            get
            {
                return _stopwatch.Elapsed + _offsetTimeSpan;
            }
            set
            {
                _offsetTimeSpan = value;
            }
        }

        public bool IsRunning
        {
            get
            {
                if(_stopwatch == null)
                {
                    return false;
                }
                return _stopwatch.IsRunning;
            }
        }
    }
}
