using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3_Projet
{

    /// <summary>
    /// Cette classe permet de s'assurer de l'effet d'aléatoire lorsqu'on génére des nombres aléatoires à haute vitesse
    /// </summary>
    public class Randomizer
    {
        private static readonly Random global = new Random();
        [ThreadStatic] private static Random local;

        public int Next()
        {
            if (local == null)
            {
                int seed;
                lock (global)
                {
                    seed = global.Next();
                }
                local = new Random(seed);
            }

            return local.Next();
        }
    }
}
