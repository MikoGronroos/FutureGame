using UnityEngine;

public class StartDialogueOnTriggerEnter : AiConversant
{

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag.Equals("Player"))
        {
            _playerConversant.StartDialogue(this, thisDialogue);
        }

    }
}
