using System.Collections.Generic;
using System;
using UnityEngine;

public class MessageSystem : MonoBehaviour
{

    #region Singleton

    private static MessageSystem _instance;

    public static MessageSystem Instance
    {
        get
        {
            return _instance;
        }
    }

    #endregion

    public Message[] Messages;

    public Dictionary<string, Message> MessageDictionary = new Dictionary<string, Message>();

    private void Awake()
    {
        _instance = this;
        foreach (Message message in Messages)
        {
            MessageDictionary.Add(message.MessageName, message);
        }
    }

    public Message GetMessageFromDictionary(string name)
    {
        return MessageDictionary[name];
    }

}
