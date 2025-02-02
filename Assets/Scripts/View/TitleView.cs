using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleView : MonoBehaviour
{
    [SerializeField] TMP_Dropdown _difDropDown;
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
        Show();
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show() 
    { 
        gameObject.SetActive(true);
    }


    public void DiffBtnclicked()
    {

        UIPresenter.Instance.DiffBtnClicked(_difDropDown.value);
    }

}
