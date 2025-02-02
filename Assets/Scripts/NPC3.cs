using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor.VersionControl;
using UnityEngine;

public class NPC3 : MonoBehaviour
{
    [SerializeField] Vector3 InitPos;
    string _message = 
        "������ƁA���񂽁I���ƒ��ɉ������Ă�̂�I�܂������A�����ɂ���ׂ����Ԃɂ���ȂƂ���Ŗ��𔄂��āA�����l���Ă�́H�݂�Ȃ͂����Ǝ��Ƃ��󂯂Ă�̂ɁA���񂽂����T�{���Ă������R�Ȃ�Ăǂ��ɂ��Ȃ����B<>" +
        "�׋��������Ȃ́H����Ƃ��A�ʓ|��������ē����Ă�́H����Ȃ��Ƃ��Ă���A���ƂŎ��������邾����B�@���́w�ʂɂ�����x�Ȃ�Ďv���Ă邩������Ȃ����ǁA���̃c�P�͕K������Ă���́B<>" +
        "��l�ɂȂ��Ă���w���̂Ƃ������Ƃ���Ă����΂悩�����x���Č�����Ă��x���̂�B���̒�����ȂɊÂ��Ȃ��񂾂���ˁB���񂽂̏������l���Č����Ă�̂�B�搶�����āA�D���ł��������Ă�킯����Ȃ��񂾂���B<>" +
        "����ɂˁA����Ȃӂ��ɘL����������Ă���A����̎q�����ɂ������e����^����̂�B�w���̐l������Ă邩��A������������x�Ȃ�Ďv��ꂽ��A�����ň��B<>" +
        "���񂽂͂ǂ��Ȃ́H���̎q�����Ɂw���̐l�݂����ɂȂ肽���x���Ďv����悤�Ȑl�ɂȂ肽���Ȃ��́H�J�b�R���ăT�{���Ă���肩������Ȃ����ǁA���ۂ͂����̓�������Ȃ��́B<>" + 
        "�����H�����������ɖ߂�Ȃ����B�����ŗ����~�܂�񂶂�Ȃ��́B�׋������ł��A�ʓ|�ł��A�����̂��߂ɂ����Ɗw�Ԃ��Ƃ��厖�Ȃ̂�B���ꂪ�Ō�̒����I�����A�������Ɩ߂�I";

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
        _seq.Append(this.transform.DOMoveX(14, 3).SetEase(Ease.Linear));
        _seq.Append(this.transform.DORotate(Vector3.up * 0f, 0.1f).SetEase(Ease.Linear));
        _seq.Append(this.transform.DOMoveZ(20, 2f).SetEase(Ease.Linear));
        _seq.Append(this.transform.DORotate(Vector3.up * 90f, 0.1f).SetEase(Ease.Linear));
        _seq.Append(this.transform.DOMoveX(22, 3).SetEase(Ease.Linear));
        _seq.Append(this.transform.DORotate(Vector3.up * 180f, 0.1f).SetEase(Ease.Linear));
        _seq.Append(this.transform.DOMoveZ(16, 2f).SetEase(Ease.Linear));
        _seq.Append(this.transform.DORotate(Vector3.up * -90f, 0.1f).SetEase(Ease.Linear));
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
