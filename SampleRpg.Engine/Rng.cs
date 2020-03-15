using System;
using System.Collections.Generic;
using System.Text;

namespace SampleRpg.Engine
{
    public static class Rng
    {
        public static int Between ( int min, int max ) => s_random.Next(min, max + 1);

        private static Random s_random = new Random();
    }
}
