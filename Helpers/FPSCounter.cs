using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmadaEngine.Helpers
{
    class FPSCounter
    {
        int RollingAverageSeconds;
        int[] fpsArray = new int[60];
        int index = 0;
        double currentSecondTimer = 0;

        int currentFPS;
        public void SetAverageTime(int seconds)
        {
            RollingAverageSeconds = seconds;
        }

        public void Update(GameTime gt)
        {
            currentSecondTimer += gt.ElapsedGameTime.TotalSeconds;
            if(currentSecondTimer >= 1)
            {
                fpsArray[index] = (int)(1 / gt.ElapsedGameTime.TotalSeconds);
                index++;
                if(index > 59)
                {
                    index = 0;
                }

                currentFPS = (int)fpsArray.Average();
                currentSecondTimer -= 1;
            }
        }

        public int GetFrameRate()
        {
            return currentFPS;
        }
    }
}
