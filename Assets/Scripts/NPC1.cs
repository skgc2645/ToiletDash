using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class NPC1 : MonoBehaviour
{
    string _message = "���H���O���T�{�肩��I���傤�ǂ����Ƃ��ŉ�����ȁB�Ȃ��A�ǂ������Ƃ܂�˂����A�ꏏ�ɔ����˂��H���A���������炸���Ɖɂł��A�N���U�����Ǝv���Ă��񂾂�B<>" +
        "�@�����߂�̂��_���������A������ƊO�̋�C�ł��z���ɍs�������B����Ƃ��̈�ق̗��Ƃ��A�搶������܂藈�˂����A�������ł��邼�B����ɁA��l�Ȃ�o���Ă������󂵂₷�����ȁI<>" +
        "�@�Ă��A���O�������ɂ�����Ă��Ƃ́A�������ƂȂ񂩕����C�Ȃ�����H�������獡����^�ʖڂԂ�K�v�˂����āB�ق�A�����������Ă���搶�Ɍ����邼�B�������ƍs�������I";




    //NPC�N���X������Čp������ׂ�

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameManager.Instance.IsCommnicate = true;
            UIPresenter.Instance.StartCommunication(_message);
        }
    }

}
