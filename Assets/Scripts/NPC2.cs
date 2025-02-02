using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class NPC2 : MonoBehaviour
{
    [SerializeField] Vector3 InitPos;
    string _message = 
        "�ǂ������́H���ƒ��Ȃ̂ɂ���ȂƂ���ɂ��āB�����ł͑�؂Ȃ��Ƃ��w��ł���̂ɁA�N�����x��Ă��܂��Ă������́H<>" +
        "�׋��͌N�̖����ɂȂ���厖�Ȏ��Ԃ�B�����̎��Ԃ��炢�c�Ȃ�Ďv����������Ȃ����ǁA���̐ςݏd�˂��傫�ȍ��ɂȂ�́B<>" +
        "���������Ă��邱�Ƃ�����Ȃ�b���Ă���Ă���������ǁA���ɗ��R���Ȃ��Ȃ�A�����߂�܂��傤�B�����̂��߂ɁA��������w�Ԃ��Ƃ���؂�B";

    Sequence _seq;
    Animator _anim;
    bool _iswalk;


    //NPC�N���X������Čp������ׂ�

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!_iswalk)
        {
            _anim.SetBool("Walk", false);
            if (!GameManager.Instance.IsCommnicate)
            {
                _iswalk = true;
                Restart();
            }
        }
        else
        {
            _anim.SetBool("Walk", true);
        }
    }


    public void Init()
    {
        _anim = GetComponent<Animator>();
        _seq = DOTween.Sequence();
        transform.position = InitPos;
        _seq.Append(this.transform.DOMoveZ(15, 2).SetEase(Ease.Linear));
        _seq.Append(this.transform.DORotate(Vector3.up * 0f, 0.1f).SetEase(Ease.Linear));
        _seq.Append(this.transform.DOMoveZ(20, 3).SetEase(Ease.Linear));
        _seq.Append(this.transform.DORotate(Vector3.up * 180f, 0.1f).SetEase(Ease.Linear));
        _seq.SetLoops(-1, LoopType.Restart);
    }

    public void Walk()
    {
        _iswalk = true;
        _seq.Play();
    }

    void Pause()
    {
        _iswalk = false;
        _seq.Pause();
    }

    void Restart()
    {
        _seq.Play();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.IsCommnicate = true;
            UIPresenter.Instance.StartCommunication(_message);
            Pause();
        }
    }
}
