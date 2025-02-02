using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : SingletonMonoBehaviour<MessageView>
{
    [SerializeField] MessageView _messageView;
    // Start is called before the first frame update
    public void StartMessage(string text)
    {
        _messageView.SetMessagePanel(text);
    }
}
