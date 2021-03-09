using UnityEngine;
using System;

public class MessageReceiver : MonoBehaviour
{
    public static void SubscrideToMessage(string name, Action<string, string> method)
    {
       MessageSystem.Instance.GetMessageFromDictionary(name).thisAction += method;
    }

    public static void UnsubscribeToMessage(string name, Action<string, string> method)
    {
        MessageSystem.Instance.GetMessageFromDictionary(name).thisAction -= method;
    }
}
