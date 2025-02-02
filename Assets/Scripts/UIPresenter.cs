using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class UIPresenter : SingletonMonoBehaviour<UIPresenter>
{
    [SerializeField] GoalView _goalView;
    [SerializeField] Timer _Timer;
    [SerializeField] TimerView _timerView;
    [SerializeField] TitleView _titleView;
    [SerializeField] BeforGameCountDownView _countDownView;
    [SerializeField] MessageView _messageView;
    [SerializeField] FailView _failView;


    


    void Start()
    {
    }


    public void Init()
    {
        _goalView.Init();
        _timerView.Init();
        _titleView.Init();
        _countDownView.Init();
        _messageView.Init();
        _failView.Init();
        GameManager.Instance._goal.Where(x => x).Subscribe(x => { _goalView.Show(); });
        GameManager.Instance._isGame.SkipLatestValueOnSubscribe().Where(x => !x).Subscribe(x => { if(!GameManager.Instance._goal.Value)_failView.Show(); });
        _Timer.CurTime.SkipLatestValueOnSubscribe().Subscribe(time => { _timerView.UpdateTimerBar(time, _Timer.InitTime);});
        _Timer.IntCurTime.SkipLatestValueOnSubscribe().Subscribe(time => { _countDownView.SetText(time); });
        _messageView.EndMessage += () => { GameManager.Instance.IsCommnicate = false; };
    }


    public void StartGame()
    {
        _timerView.Show();
        _titleView.Hide();
    }

    public void StartCommunication(string text)
    {
        _messageView.SetMessagePanel(text);
    }


    public void ToTitleBtnClicked()
    {
        SoundManager.Instance.SoundPlay(Sound.click);
        GameManager.Instance.Restart();
    }

    public void StartBtnClicked()
    {
        SoundManager.Instance.SoundPlay(Sound.click);
        GameManager.Instance.StartGame();
    }


    public void DiffBtnClicked(int value)
    {
        SoundManager.Instance.SoundPlay(Sound.click);
        GameManager.Instance.UpdateDiffcult(value);
    }
}
