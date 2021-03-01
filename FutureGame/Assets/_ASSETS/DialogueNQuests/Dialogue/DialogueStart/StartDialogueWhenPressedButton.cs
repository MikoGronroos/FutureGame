using UnityEngine;

public class StartDialogueWhenPressedButton : AiConversant
{

    [SerializeField] private KeyCode dialogueStartKeyCode;

#if UNITY_EDITOR

    private void Update()
    {

        if (thisDialogue == null)
        {
            return;
        }

        if (Input.GetKeyDown(dialogueStartKeyCode))
        {
            _playerConversant.StartDialogue(this, thisDialogue);
        }

    }
#endif
}
