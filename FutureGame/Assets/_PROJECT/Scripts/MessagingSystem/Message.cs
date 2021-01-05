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
            Debug.Log($"{MessageName} has sent message which reads: {OnReceivedMessage}. This message has {thisAction.GetInvocationList().Length} listeners!");
            return true;
        }
        Debug.LogWarning($"{MessageName} has zero listeners!");
        return false;
    }
}
