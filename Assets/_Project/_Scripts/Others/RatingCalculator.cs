using System;
using UnityEngine;

internal class RatingCalculator
{
    public static event Action<Color> OnColorReturned;


    public static int CalculateRating(float elapsedTime, float requiredTime, int healthStatus)
    {
        if (elapsedTime <= requiredTime)
        {
            OnColorReturned?.Invoke(Color.green);

            switch (healthStatus + 1)
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

            if (elapsedTime <= (requiredTime + .15f))
                return UnityEngine.Random.Range(1, healthStatus);
        }

        return 0;
    }
}
