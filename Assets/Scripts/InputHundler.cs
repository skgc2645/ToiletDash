using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class InputHundler : SingletonMonoBehaviour<InputHundler>
{
    [SerializeField] KeyCode _keyLeft;
    [SerializeField] KeyCode _keyRight;
    [SerializeField] KeyCode _keyUp;
    [SerializeField] KeyCode _keyDown;

    public readonly ReactiveProperty<bool> BtnLeft   = new ReactiveProperty<bool>();
    public readonly ReactiveProperty<bool> BtnRight  = new ReactiveProperty<bool>();
    public readonly ReactiveProperty<bool> BtnUp     = new ReactiveProperty<bool>();
    public readonly ReactiveProperty<bool> BtnDown   = new ReactiveProperty<bool>();

    // Start is called before the first frame update
    void Start()
    {
        BtnLeft .AddTo(this);
        BtnRight.AddTo(this);
        BtnUp   .AddTo(this);
        BtnDown .AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        BtnLeft.Value = Input.GetKeyDown(_keyLeft);
        BtnLeft.Value = Input.GetKeyDown(_keyRight);
        BtnLeft.Value = Input.GetKeyDown(_keyUp);
        BtnLeft.Value = Input.GetKeyDown(_keyDown);
    }
}
