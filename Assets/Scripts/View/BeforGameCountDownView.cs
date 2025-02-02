using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class BeforGameCountDownView : MonoBehaviour
{

    const float EndTextSize = 4f;
    const float InitTextSize = 6f;

    TextMeshProUGUI _countText;


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
        _countText = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(float time)
    {
        if(_countText.text != Mathf.Ceil(time).ToString())
        {
            if (Mathf.Ceil(time) == 0)
            {
                _countText.text = "スタート";
                _countText.DOFade(0.0f, 0.5f).OnComplete(() => { gameObject.SetActive(false); });
            }
            else
            {
                gameObject.SetActive(true);
                transform.localScale = Vector3.one * InitTextSize;
                _countText.text = Mathf.Floor(time).ToString();
                transform.DOScale(Vector3.one * EndTextSize, 0.5f);
            }
                
        }

    }
}
