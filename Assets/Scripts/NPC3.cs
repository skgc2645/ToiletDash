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
        "ちょっと、あんた！授業中に何をしてるのよ！まったく、教室にいるべき時間にこんなところで油を売って、何を考えてるの？みんなはちゃんと授業を受けてるのに、あんただけサボっていい理由なんてどこにもないわよ。<>" +
        "勉強が嫌いなの？それとも、面倒だからって逃げてるの？そんなことしてたら、あとで自分が困るだけよ。　今は『別にいいや』なんて思ってるかもしれないけど、そのツケは必ず回ってくるの。<>" +
        "大人になってから『あのときちゃんとやっておけばよかった』って後悔しても遅いのよ。世の中そんなに甘くないんだからね。あんたの将来を考えて言ってるのよ。先生だって、好きでお説教してるわけじゃないんだから。<>" +
        "それにね、こんなふうに廊下をうろついてたら、周りの子たちにも悪い影響を与えるのよ。『あの人がやってるから、自分もいいや』なんて思われたら、もう最悪。<>" +
        "あんたはどうなの？他の子たちに『あの人みたいになりたい』って思われるような人になりたくないの？カッコつけてサボってるつもりかもしれないけど、実際はただの逃げじゃないの。<>" + 
        "いい？今すぐ教室に戻りなさい。ここで立ち止まるんじゃないの。勉強が嫌でも、面倒でも、自分のためにちゃんと学ぶことが大事なのよ。これが最後の忠告！さあ、さっさと戻る！";

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
