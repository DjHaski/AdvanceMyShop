
using System;

namespace AdvanceMyShop;
internal class Utils
{
    public static float GetRandomNumber(float min, float max)
    { 
        Random random = new Random();
        return (float)(random.NextDouble() * (max - min) + min);
    }
}