using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalView : MonoBehaviour
{
    [SerializeField] Button _toTitleBtn;

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
        Hide();
        _toTitleBtn.onClick.AddListener(UIPresenter.Instance.ToTitleBtnClicked);
    }


    public void Show()
    {
         gameObject.SetActive(true);
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
