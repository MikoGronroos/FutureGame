using Finark.Dialogue;
using UnityEngine;

public class AiConversant : MonoBehaviour
{

    [SerializeField] protected string conversantName;

    [SerializeField] protected Dialogue thisDialogue;

    protected PlayerConversant _playerConversant;

    private void Awake()
    {
        _playerConversant = FindObjectOfType<PlayerConversant>();
    }

    public string GetName()
    {
        return conversantName;
    }

}
