using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace S3_Projet
{
    class Timer
    {
        private int time;
        private bool isPause = false;

        public int Time
        {
            get { return time; }
        }

        public bool IsPause
        {
            get { return isPause; }
        }

        public void StartTimer()
        {
            time = 0;

            while (!isPause)
            {
                Thread.Sleep(1000);
                time++;
            }
            
        }

        public void PauseTimer() 
        {
            isPause = !isPause;
        }
    }
}
