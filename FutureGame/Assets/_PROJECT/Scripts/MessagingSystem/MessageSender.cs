using UnityEngine;

public class MessageSender : MonoBehaviour
{
    public static bool SendMessageToClients(string name)
    {
        return MessageSystem.Instance.GetMessageFromDictionary(name).SendMessage();
    }

}
