using UnityEngine;
using static Racer.Utilities.SingletonPattern;

public class TimeController : Singleton<TimeController>
{
    float initialTime;

    float finalTime;

    float totalTime;



    protected override void Awake()
    {
        base.Awake();
    
        initialTime = 0;

        finalTime = 0;

        totalTime = 0;
    }


    public void StartTimer()
    {
        initialTime = Time.unscaledTime;
    }

    public void EndTimer()
    {
        finalTime = (Time.unscaledTime - initialTime);

        totalTime += finalTime;
    }

    public float GetTotalTime()
    {
        return totalTime;
    }

    public float GetCurrentTime()
    {
        return finalTime;
    }
}
