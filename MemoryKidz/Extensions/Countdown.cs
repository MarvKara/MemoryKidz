using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryKidz
{
    public class Countdown
    {
        public static double Counter { get; set; }

        public static void Initialize(int count)
        {
            Counter = count;
        }
    }
}