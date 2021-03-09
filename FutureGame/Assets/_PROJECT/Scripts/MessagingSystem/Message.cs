using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Finark/MessageSystem/Message")]
public class Message : ScriptableObject
{
    public string MessageName;

    public string OnReceivedMessage;

    public event Action<string, string> thisAction;

    public bool SendMessage()
    {
        if (thisAction != null)
        {
            thisAction(MessageName, OnReceivedMessage);
            return true;
        }
        return false;
    }
}
