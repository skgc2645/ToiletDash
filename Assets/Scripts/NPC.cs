using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] Vector3 InitPos;
    string _message = 
        "おい、授業中に何をしているんだ？君がこうして廊下にいる間に、教室では大事なことを学んでいるんだぞ。<>" +
        "勉強は君の将来をつくる大切な時間だ。サボってしまえば、それだけ自分の可能性を狭めてしまうことになる。<>" +
        "もちろん、何か事情があるのかもしれないが、理由もなくここにいるのなら、すぐに戻りなさい。<>" +
        "今の時間をどう使うかで、君の未来は大きく変わるんだぞ。自分自身のために、しっかり授業を受けるんだ。";

    Sequence _seq;
    Animator _anim;
    bool _iswalk;


    //NPCクラスを作って継承するべき

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!_iswalk)
        {
            _anim.SetBool("Walk",false);
            if(!GameManager.Instance.IsCommnicate)
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
        _seq.Append(this.transform.DOMoveX(-14, 4).SetEase(Ease.Linear));
        _seq.Append(this.transform.DORotate(Vector3.up * -180f, 0.1f).SetEase(Ease.Linear));
        _seq.Append(this.transform.DOMoveZ(16, 2).SetEase(Ease.Linear));
        _seq.Append(this.transform.DORotate(Vector3.up * 90f, 0.1f).SetEase(Ease.Linear));
        _seq.Append(this.transform.DOMoveX(-9, 4).SetEase(Ease.Linear)).SetEase(Ease.Linear);
        _seq.Append(this.transform.DORotate(Vector3.up * 0f, 0.1f).SetEase(Ease.Linear));
        _seq.Append(this.transform.DOMoveZ(20, 2).SetEase(Ease.Linear));
        _seq.Append(this.transform.DORotate(Vector3.up * -90f, 0.1f).SetEase(Ease.Linear));
        _seq.SetLoops(-1, LoopType.Restart);
        Walk();
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
        if(other.gameObject.tag == "Player")
        {
            GameManager.Instance.IsCommnicate = true;
            UIPresenter.Instance.StartCommunication(_message);
            Pause();
        }
    }

}
