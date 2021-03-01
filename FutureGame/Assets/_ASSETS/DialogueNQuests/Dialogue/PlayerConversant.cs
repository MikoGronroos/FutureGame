using Finark.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Finark.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {

        [SerializeField] private string conversantName;

        [SerializeField] private UnityEvent onDialogueStartEvent;
        [SerializeField] private UnityEvent onDialogueQuitEvent;

        private Dialogue _currentDialogue;

        private DialogueNode _currentNode = null;

        private AiConversant _aiConversant = null;

        private bool _isChoosing = false;

        private List<DialogueTrigger> triggers = new List<DialogueTrigger>();

        public event Action onConversationUpdated;

        public void StartDialogue(AiConversant newConversant, Dialogue newDialogue)
        {

            if (newDialogue == null ||  newConversant == null)
            {
                return;
            }

            _aiConversant = newConversant;
            _currentDialogue = newDialogue;
            foreach (DialogueTrigger trigger in _aiConversant.GetComponents<DialogueTrigger>())
            {
                triggers.Add(trigger);
            }
            _currentNode = _currentDialogue.GetRootNode();

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            TriggerEnterAction();
            onConversationUpdated?.Invoke();

            onDialogueStartEvent?.Invoke();

        }

        public void Quit()
        {
            _aiConversant = null;
            TriggerExitAction();
            triggers.Clear();
            _currentDialogue = null;
            _currentNode = null;
            _isChoosing = false;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            onConversationUpdated?.Invoke();

            onDialogueQuitEvent?.Invoke();
        }

        public bool IsActive()
        {
            return _currentDialogue != null;
        }

        public bool IsChoosing()
        {
            return _isChoosing;
        }

        public string GetText()
        {

            if (_currentNode == null)
            {
                return "";
            }

            return _currentNode.GetText();
        }

        public IEnumerable<DialogueNode> GetChoices()
        {
            return FilterOnCondition(_currentDialogue.GetPlayerChildren(_currentNode));
        }

        public void SelectChoice(DialogueNode chosenNode)
        {
            _currentNode = chosenNode;
            TriggerEnterAction();
            _isChoosing = false;
            Next();
        }

        public void Next()
        {
            int numPlayerResponses = FilterOnCondition(_currentDialogue.GetPlayerChildren(_currentNode)).Count();
            if (numPlayerResponses > 0)
            {
                _isChoosing = true;
                TriggerExitAction();
                onConversationUpdated?.Invoke();
                return;
            }

            DialogueNode[] children = FilterOnCondition(_currentDialogue.GetAllChildren(_currentNode)).ToArray();
            int randomIndex = UnityEngine.Random.Range(0, children.Count());

            if (_currentNode.IsPlayerSpeaking() && children.Count() <= 0)
            {
                Quit();
                return;
            }

            TriggerExitAction();
            _currentNode = children[randomIndex];
            TriggerEnterAction();
            onConversationUpdated?.Invoke();
        }

        public bool HasNext()
        {
            return FilterOnCondition(_currentDialogue.GetAllChildren(_currentNode)).Count() > 0;
        }

        private IEnumerable<DialogueNode> FilterOnCondition(IEnumerable<DialogueNode> inputNode)
        {
            foreach (var node in inputNode)
            {
                if (node.CheckCondition(GetEvaluators()))
                {
                    yield return node;
                }
            }
        }

        private IEnumerable<IPredicateEvaluator> GetEvaluators()
        {
            return GetComponents<IPredicateEvaluator>();
        }

        private void TriggerEnterAction()
        {
            if (_currentNode != null)
            {
                TriggerAction(_currentNode.GetOnEnterAction());
            }
        }

        private void TriggerExitAction()
        {
            if (_currentNode != null)
            {
                TriggerAction(_currentNode.GetOnExitAction());
            }
        }

        private void TriggerAction(string action)
        {
            if (action == "") return;

            for (int i = 0; i < triggers.Count; i++)
            {
                if (triggers[i].GetAction() == action)
                {
                    triggers[i].Trigger(action);
                }
            }

        }

        public string GetCurrentConversantName()
        {
            if (_isChoosing)
            {
                return conversantName;
            }
            return _aiConversant.GetName();
        }
    }
}
