using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Player : SingletonMonoBehaviour<Player>
{
    //member
    Rigidbody _rigidbody;

    Vector3   _prePos;      //前の位置（ベクトル計算用）
    Vector3   _moveVec;     //移動ベクトル

    float _horiKeyInput;    //横方向入力
    float _vertKeyInput;    //縦方向入力

    //COEF
    float SPEED = 4.0f;


    void Initialized()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _prePos = Vector3.zero;
        _moveVec = Vector3.zero;
    }



    void Move()
    {
        //移動
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;//単位ベクトル
        Vector3 moveForward = cameraForward * _vertKeyInput + Camera.main.transform.right * _horiKeyInput;//移動ベクトル カメラの正面方向にws入力 カメラの横方向にad入力
        _rigidbody.velocity = moveForward * SPEED + new Vector3(0, _rigidbody.velocity.y, 0);//移動ベクトルにスピードをかける

        //進行方向を向く
        _moveVec = transform.position - _prePos;
        _moveVec = new Vector3(_moveVec.x, 0, _moveVec.z);
        if (_moveVec.magnitude > 0.01f)
        {
            //Quaternion targetRotation = Quaternion.LookRotation(_moveVec, Vector3.up);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);
            transform.rotation   = Quaternion.LookRotation(_moveVec, Vector3.up);
        }

        //更新
        _prePos = transform.position;
    }




    // Start is called before the first frame update
    void Start()
    {
        Initialized();
    }

    // Update is called once per frame
    void Update()
    {
        _horiKeyInput = Input.GetAxis("Horizontal");
        _vertKeyInput = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        Move();
    }



}
