using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using System;

public class MessageView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] Image _clickIcon;

    public event Action EndMessage;


    string _allMessage = "";
    string[] _splitMessage;
    int _messageIdx = 0;
    int _textIdx = 0;
    float _elapsedTime = 0f;
    float _elapsedTime2 = 0f;
    bool _isEndMessage = false;
    bool _isOneMessage = false;

    const string SPRIT_STRING  = "<>";
    const float MESSAGE_SPEED  = 0.05f;
    const float MIN_MESSAGE_TIME = 1f;
    const float ImageFlashTime = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsCommnicate) return;

        if(!_isOneMessage)//���؂�̃��b�Z�[�W��\�����鎞
        {
            if (_elapsedTime >= MESSAGE_SPEED)
            {
                SoundManager.Instance.SoundPlay(Sound.communicate);
                _text.text += _splitMessage[_messageIdx][_textIdx];
                _textIdx++;
                _elapsedTime = 0f;
                if(_textIdx == _splitMessage[_messageIdx].Length) _isOneMessage = true; //1��؂�S���\��������I��
            }
            _elapsedTime += Time.deltaTime;
            if (Input.GetMouseButtonDown(0) && _elapsedTime2 > MIN_MESSAGE_TIME)
            {
                _text.text += _splitMessage[_messageIdx].Substring(_textIdx);
                _isOneMessage = true;
                _elapsedTime2 = 0f;
            }
            _elapsedTime2 += Time.deltaTime;
        }
        else//�P��؂��\������������
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= ImageFlashTime)
            {
                _clickIcon.enabled = !_clickIcon.enabled;
                _elapsedTime = 0f;
            }

            if(Input.GetMouseButtonDown(0))
            {
                _textIdx = 0;
                _messageIdx++;
                _text.text = "";
                _clickIcon.enabled = false;
                _elapsedTime = 0;
                _elapsedTime2 = 0;
                _isOneMessage = false;
                if(_messageIdx == _splitMessage.Length)
                {
                    _isEndMessage = true;
                    EndMessage?.Invoke();
                   gameObject.SetActive(false);
                }
            }
        }

    }


    public void SetMessagePanel(string allMessage)
    {
        _allMessage = allMessage;
        _splitMessage = Regex.Split(allMessage, @"\s*" + SPRIT_STRING + @"\s*", RegexOptions.IgnorePatternWhitespace); //��؂蕶���Ƃ��̑O��̋󔒂܂ō폜����
        _messageIdx = 0;
        _textIdx = 0;
        _isEndMessage = false;
        _isOneMessage = false;
        gameObject.SetActive(true);
    }


    public void Init()
    {
        _text.text = "";
        gameObject.SetActive(false);

    }
}
