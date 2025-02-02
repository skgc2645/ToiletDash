using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FailView : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI _titleText;
    [SerializeField] TextMeshProUGUI _commentText;
    [SerializeField] Button _toTitleBtn;

    Image _backgroundImage;

    const float END_IMG_ALPHA = 233f;
    const float FADE_SPEED = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Init()
    {
        _backgroundImage = GetComponent<Image>();
        Hide();
        _toTitleBtn.onClick.AddListener(UIPresenter.Instance.ToTitleBtnClicked);
    }


    public void Show()
    {
        Sequence _seq = DOTween.Sequence();
        _seq.Append(_backgroundImage.DOFade(END_IMG_ALPHA / 256f, FADE_SPEED))
            .OnComplete(async () =>
            {
                _titleText.gameObject.SetActive(true);
                await UniTask.Delay(500); // 1•b‘Ò‹@
                _commentText.gameObject.SetActive(true);
                _toTitleBtn.gameObject.SetActive(true);
            }); ;
        gameObject.SetActive(true);
        _seq.Play();
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
