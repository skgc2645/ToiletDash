using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Player : SingletonMonoBehaviour<Player>
{
    //member
    Rigidbody _rigidbody;

    Vector3   _prePos;      //�O�̈ʒu�i�x�N�g���v�Z�p�j
    Vector3   _moveVec;     //�ړ��x�N�g��

    float _horiKeyInput;    //����������
    float _vertKeyInput;    //�c��������

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
        //�ړ�
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;//�P�ʃx�N�g��
        Vector3 moveForward = cameraForward * _vertKeyInput + Camera.main.transform.right * _horiKeyInput;//�ړ��x�N�g�� �J�����̐��ʕ�����ws���� �J�����̉�������ad����
        _rigidbody.velocity = moveForward * SPEED + new Vector3(0, _rigidbody.velocity.y, 0);//�ړ��x�N�g���ɃX�s�[�h��������

        //�i�s����������
        _moveVec = transform.position - _prePos;
        _moveVec = new Vector3(_moveVec.x, 0, _moveVec.z);
        if (_moveVec.magnitude > 0.01f)
        {
            //Quaternion targetRotation = Quaternion.LookRotation(_moveVec, Vector3.up);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);
            transform.rotation   = Quaternion.LookRotation(_moveVec, Vector3.up);
        }

        //�X�V
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
