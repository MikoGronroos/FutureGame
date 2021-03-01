using System;
using System.Collections.Generic;
using Finark.Core;
using UnityEditor;
using UnityEngine;

namespace Finark.Dialogue
{
    public class DialogueNode : ScriptableObject
    {

        [SerializeField] private bool isPlayerSpeaking = false;

        [SerializeField] private string dialogueText;

        [SerializeField] private List<string> children = new List<string>();

        [SerializeField] private Rect thisRect = new Rect(100, 100, 200, 100);

        [SerializeField] private string onEnterAction;

        [SerializeField] private string onExitAction;

        [SerializeField] private Condition condition;


        public Rect GetRect()
        {
            return thisRect;
        }

        public string GetText()
        {
            return dialogueText;
        }

        public List<string> GetChildren()
        {
            return children;
        }

        public bool IsPlayerSpeaking()
        {
            return isPlayerSpeaking;
        }

        public string GetOnEnterAction()
        {
            return onEnterAction;
        }

        public string GetOnExitAction()
        {
            return onExitAction;
        }

        public bool CheckCondition(IEnumerable<IPredicateEvaluator> evaluators)
        {
            return condition.Check(evaluators);
        }


#if UNITY_EDITOR
        public void SetPosition(Vector2 newPosition)
        {
            Undo.RecordObject(this, "Move Dialogue Node");
            thisRect.position = newPosition;
            EditorUtility.SetDirty(this);
        }

        public void SetText(string newText)
        {
            if (newText != dialogueText)
            {
                Undo.RecordObject(this, "Update Dialogue Text");

                dialogueText = newText;

                EditorUtility.SetDirty(this);
            }
        }

        public void AddChild(string childID)
        {
            Undo.RecordObject(this, "Add Dialogue Link");
            children.Add(childID);

            EditorUtility.SetDirty(this);
        }

        public void RemoveChildren(string childID)
        {
            Undo.RecordObject(this, "Unlink");
            children.Remove(childID);

            EditorUtility.SetDirty(this);
        }

        public void SetPlayerSpeaking(bool newIsPlayerSpeaking)
        {
            Undo.RecordObject(this, "Change Dialogue Speaker");
            isPlayerSpeaking = newIsPlayerSpeaking;
            EditorUtility.SetDirty(this);
        }

#endif
    }
}

