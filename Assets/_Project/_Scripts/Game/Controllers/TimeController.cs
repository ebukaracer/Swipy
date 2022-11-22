using UnityEngine;
using static Racer.Utilities.SingletonPattern;

internal class TimeController : Singleton<TimeController>
{
    private float _initialTime;
    private float _finalTime;
    private float _totalTime;


    protected override void Awake()
    {
        base.Awake();
    
        _initialTime = 0;
        _finalTime = 0;
        _totalTime = 0;
    }

    public void StartTimer()
    {
        _initialTime = Time.unscaledTime;
    }

    public void EndTimer()
    {
        _finalTime = (Time.unscaledTime - _initialTime);

        _totalTime += _finalTime;
    }

    public float GetTotalTime() => _totalTime;
}
