using UnityEngine;

public class MessageSender : MonoBehaviour
{
    public static bool SendMessageToClients(string name)
    {
        return MessageSystem.Instance.GetMessageFromDictionary(name).SendMessage();
    }

    public static bool SendMessageToClientsWithContent(string name, string content)
    {
        MessageSystem.Instance.GetMessageFromDictionary(name).OnReceivedMessage = content;
        return MessageSystem.Instance.GetMessageFromDictionary(name).SendMessage();
    }

}
