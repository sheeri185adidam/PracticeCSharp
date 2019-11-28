using System;
using System.Timers;

namespace Timers
{
    public class TimerTest
    {
        private Timer _timer;

        public TimerTest()
        {
            _timer = new Timer
            {
                Enabled = false,
                AutoReset = false
            };
            _timer.Elapsed += (sender, args) =>
              {                  
                  Console.WriteLine($"TimerEvent: Current Time in seconds: " +
                      $"{System.DateTime.Now.Second}");
              };
        }

        public bool StartTimer(int interval)
        {
            if (_timer.Enabled)
                return false;
            if (interval < 0)
                throw new ArgumentOutOfRangeException($"Invalid argument {interval}");            
            _timer.Interval = interval;
            _timer.Start();
            Console.WriteLine($"Started Timer at {System.DateTime.Now.Second} " +
                $" for {interval} millisecond interval");
            return true;
        }


    }
    class Program
    {
        static void Main(string[] args)
        {
            var timer = new TimerTest();
            int interval = 10000;
            while(true)
            {
                if (timer.StartTimer(interval))
                    interval += interval;
            }
        }
    }
}
