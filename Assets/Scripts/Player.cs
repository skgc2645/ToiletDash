using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using DG.Tweening;

public class Player : SingletonMonoBehaviour<Player>
{
    [SerializeField] Transform _stepRay;
    [SerializeField] Transform _movePoint1;
    [SerializeField] Transform _movePoint2;

    //property
    public Vector3 PlayerHipPos { get { return transform.position + Vector3.up; } }


    //member
    Rigidbody _rigidbody;
    Animator _anim;

    Vector3   _prePos;      //�O�̈ʒu�i�x�N�g���v�Z�p�j
    Vector3   _moveVec;     //�ړ��x�N�g��

    float _horiKeyInput;    //����������
    float _vertKeyInput;    //�c��������
    [SerializeField]float _stepDistance = 0.2f; //�K�i�����p
    [SerializeField]float _stepOffset = 0.3f; //�K�i�����p

    //COEF
    float SPEED = 4.0f;
    float SPEEDY = 0.5f;
    public float MAXDIST = 1.5f;

    void Initialized()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _prePos = Vector3.zero;
        _moveVec = Vector3.zero;
    }



    void Move()
    {
        //�ړ�
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;//�P�ʃx�N�g��
        Vector3 moveForward = cameraForward * _vertKeyInput + Camera.main.transform.right * _horiKeyInput;//�ړ��x�N�g�� �J�����̐��ʕ�����ws���� �J�����̉�������ad����
        RaycastHit stepHit;
        Vector3 velocity = Vector3.zero;
        Debug.DrawLine(_stepRay.position, _stepRay.position + _stepRay.forward * _stepDistance, Color.red);
        if (Physics.Linecast(_stepRay.position, _stepRay.position + _stepRay.forward * _stepDistance, out stepHit, LayerMask.GetMask("Step")))
        {
            Debug.DrawLine(transform.position + Vector3.up * _stepOffset, transform.position + Vector3.up * _stepOffset + transform.forward * _stepDistance, Color.green);
            if (!Physics.Linecast(transform.position + Vector3.up * _stepOffset, transform.position + Vector3.up * _stepOffset + transform.forward * _stepDistance, LayerMask.GetMask("Step")))
            {
                velocity = new Vector3(0f, (Quaternion.FromToRotation(Vector3.up, stepHit.normal) * transform.forward * SPEEDY).y, 0f);
                Debug.Log(velocity);
            }
        }
        _rigidbody.velocity = moveForward * SPEED + new Vector3(0, _rigidbody.velocity.y, 0) + velocity;//�ړ��x�N�g���ɃX�s�[�h��������

        //�i�s����������
        _moveVec = transform.position - _prePos;
        _moveVec = new Vector3(_moveVec.x, 0, _moveVec.z);
        if (_moveVec.magnitude > 0.01f)
        {
            transform.rotation   = Quaternion.LookRotation(_moveVec, Vector3.up);
        }



        //�X�V
        _prePos = transform.position;
    }


    void SetWalkAnim()
    {
        if (_horiKeyInput == 0 && _vertKeyInput == 0)
            _anim.SetBool("Walk", false);
        else
            _anim.SetBool("Walk", true);
    }


    void AfterGoal()
    {
        //transform.position = Vector3.MoveTowards(transform.position, _movePoint1.position, MAXDIST);
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMoveZ(_movePoint1.position.z,1f).SetEase(Ease.Linear));
        seq.Append(transform.DORotate(Vector3.up * -90, 0.1f));
        seq.Append(transform.DOMoveX(_movePoint2.position.x,0.3f).SetEase(Ease.Linear));
        seq.Play();
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
        if(!GameManager.Instance._goal.Value)
        {
            if(GameManager.Instance._isGame.Value && !GameManager.Instance.IsCommnicate)
            {
                Move();
                SetWalkAnim();
            }
                
        }
        else
        {
            AfterGoal();
        }
    }



}
