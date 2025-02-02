using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerView : MonoBehaviour
{
    Image _timerImage;

    // Start is called before the first frame update
    public void Init()
    {
        _timerImage = GetComponent<Image>();
        _timerImage.fillAmount = 1;
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void UpdateTimerBar(float curTime, float initTime)
    {
        _timerImage.fillAmount = curTime / initTime;
    }

    public void Hide()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.transform.parent.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }


}
