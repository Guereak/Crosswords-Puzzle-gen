using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3_Projet
{
    public class Randomizer
    {
        private static readonly Random _global = new Random();
        [ThreadStatic] private static Random _local;

        public int Next()
        {
            if (_local == null)
            {
                int seed;
                lock (_global)
                {
                    seed = _global.Next();
                }
                _local = new Random(seed);
            }

            return _local.Next();
        }
    }
}
