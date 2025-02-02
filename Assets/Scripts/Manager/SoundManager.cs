using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum Sound
{
    click,
    communicate,
    clear,
    fail,
    BGM

}


public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    [SerializeField] float clickV;
    [SerializeField] float communicateV;
    [SerializeField] float clearV;
    [SerializeField] float failV;
    [SerializeField] float BGMV;



    
    
    
    List<AudioSource> _soundsList = new List<AudioSource>();
    List<AudioSource> _BGMList = new List<AudioSource>();

    void Awake()
    {
        Initialize();
        DontDestroyOnLoad(gameObject);
    }

    public void Initialize()
    {
        AudioClip click = Resources.Load<AudioClip>("Sounds/Clicked");
        AudioClip communicate = Resources.Load<AudioClip>("Sounds/Message");
        AudioClip clear = Resources.Load<AudioClip>("Sounds/Goal");
        AudioClip fail = Resources.Load<AudioClip>("Sounds/Fail");
        AudioClip bgm = Resources.Load<AudioClip>("Sounds/InGameBGM");
        SetSoundsList(click, clickV);
        SetSoundsList(communicate, communicateV);
        SetSoundsList(clear, clearV);
        SetSoundsList(fail, failV);
        SetSoundsList(bgm, BGMV,true);
    }

    void SetSoundsList(AudioClip clip, float v)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = v;
        audioSource.clip = clip;
        _soundsList.Add(audioSource);
    }

    void SetSoundsList(AudioClip clip, float v, bool loop)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = v;
        audioSource.clip = clip;
        audioSource.loop = loop;
        _soundsList.Add(audioSource);
    }



    public void SoundPlay(Sound e)
    {
        AudioSource sound = _soundsList[(int)e];
        Debug.Log("play" + sound.name);
        sound.Play();
    }

    public void InGameBGMPlay()
    {

    }
}