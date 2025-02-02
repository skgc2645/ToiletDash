using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class NPC2 : MonoBehaviour
{
    [SerializeField] Vector3 InitPos;
    string _message = 
        "どうしたの？授業中なのにこんなところにいて。教室では大切なことを学んでいるのに、君だけ遅れてしまってもいいの？<>" +
        "勉強は君の未来につながる大事な時間よ。少しの時間くらい…なんて思うかもしれないけど、その積み重ねが大きな差になるの。<>" +
        "もし困っていることがあるなら話してくれてもいいけれど、特に理由がないなら、さあ戻りましょう。自分のために、しっかり学ぶことが大切よ。";

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
