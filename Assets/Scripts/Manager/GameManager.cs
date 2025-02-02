using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System.Threading; // ‚±‚ê‚ª‚¢‚é‚æ

public enum Difficult
{
    normal,
    hard,
    expart
}

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    //member
    public readonly ReactiveProperty<bool> _goal = new ReactiveProperty<bool>(false);//‚±‚Á‚¿‚ÍRP‚Å‚ ‚é•K—v‚ª‚È‚¢
    public readonly ReactiveProperty<bool> _isGame = new ReactiveProperty<bool>(false);

    public bool IsCommnicate {  get; set; }
    [SerializeField] public float[] GAME_TIME = { 30f,25f,20f};

    [SerializeField] NPC _npc1;
    [SerializeField] NPC2 _npc2;
    [SerializeField] NPC3 _npc3;
    [SerializeField] Timer _timer;
    [SerializeField] UIPresenter _uiPresenter;

    Difficult _difficult = 0;
    CancellationTokenSource _ct;
    



    // Start is called before the first frame update
    void Start()
    {
        _ct = new CancellationTokenSource();
        _uiPresenter.Init();
        _npc1.Init();
        _npc1.Walk();
        _npc2.Init();
        _npc2.Walk();
        _npc3.Init();
        _npc3.Walk();
        _timer.TimerEnd += () => { Fail(); };
        _timer.InitTime = GAME_TIME[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Goal()
    {
        _goal.Value = true;
        _isGame.Value = false;
        SoundManager.Instance.SoundPlay(Sound.clear);
        _timer.TimerStop();
    }


    public void Fail()
    {
        _isGame.Value = false;
        SoundManager.Instance.SoundPlay(Sound.fail);
    }


    public void UpdateDiffcult(int value)
    {
        _difficult = (Difficult)value;
        _timer.InitTime = GAME_TIME[value];
        Debug.Log(_difficult);
    }


    public async void StartGame()
    {
        _uiPresenter.StartGame();
        await _timer.CountDownBeforGame(3f,_ct.Token);
        _timer.TimerStart();
        _isGame.Value = true;
        SoundManager.Instance.SoundPlay(Sound.BGM);
    }


    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

}
