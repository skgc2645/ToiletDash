using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public float InitTime { get; set; }
    public readonly ReactiveProperty<float> CurTime = new ReactiveProperty<float>();
    public readonly ReactiveProperty<int> IntCurTime = new ReactiveProperty<int>(0);

    bool _isStart;

    public event Action TimerEnd;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_isStart)
        {
            CurTime.Value -= Time.deltaTime;
            
            if(CurTime.Value < 0 )
            {
                TimerEnd?.Invoke();
                TimerStop();
            }
        }
        
    }


    public void TimerStart()
    {
        if(_isStart) return;
        CurTime.Value = InitTime;
        _isStart = true;
    }


    public void TimerStop()
    {
        _isStart = false;
    }


    public void TimerReset()
    {
        _isStart = false;
        CurTime.Value = InitTime;
    }


    public async UniTask CountDownBeforGame(float time, CancellationToken ct)
    {
        float curTime = time;
        while (curTime > 0)
        {
            curTime -= Time.deltaTime;
            if (IntCurTime.Value != (int)Math.Ceiling(curTime))
                IntCurTime.Value = (int)Math.Ceiling(curTime);
            await UniTask.Yield(PlayerLoopTiming.FixedUpdate, cancellationToken: ct);
        }
        TimerStop();
    }


}
