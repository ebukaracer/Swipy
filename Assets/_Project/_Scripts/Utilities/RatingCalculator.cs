using System;
using UnityEngine;

class RatingCalculator
{
    public static event Action<Color> OnColorReturned;

    public static int CalculateRating(float currentTime, float totalTime, float healthCount)
    {
        if (currentTime <= totalTime)
        {
            OnColorReturned?.Invoke(Color.green);

            switch (healthCount)
            {
                case 1:
                    return 1;
                case 2:
                    return 2;
                case 3:
                    return 3;
            }
        }

        else
        {
            OnColorReturned?.Invoke(Color.red);

            // Little Mercy
            if (currentTime <= (totalTime + .15f))
                return UnityEngine.Random.Range(1, 3);
        }

        return 0;
    }
}
