using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Finark.Dialogue
{
    [CreateAssetMenu(fileName = " New Dialogue", menuName = "QuestsNDialogue/Dialogue", order = 0)]
    public class Dialogue : ScriptableObject, ISerializationCallbackReceiver
    {

        [SerializeField] private List<DialogueNode> nodes = new List<DialogueNode>();

        [SerializeField] private Vector2 newNodeOffset = new Vector2(250,0);

        private Dictionary<string, DialogueNode> _nodeLookup = new Dictionary<string, DialogueNode>();

        private void OnValidate()
        {
            _nodeLookup.Clear();
            foreach (var node in GetAllNodes())
            {
                _nodeLookup.Add(node.name, node);
            }
        }

        public IEnumerable<DialogueNode> GetAllNodes()
        {
            return nodes;
        }

        public DialogueNode GetRootNode()
        {
            return nodes[0];
        }

        public IEnumerable<DialogueNode> GetAllChildren(DialogueNode parentNode)
        {
            foreach (string childID in parentNode.GetChildren())
            {
                if (_nodeLookup.ContainsKey(childID))
                {
                    yield return _nodeLookup[childID];
                }
            }
        }

        public IEnumerable<DialogueNode> GetPlayerChildren(DialogueNode currentNode)
        {
            foreach (DialogueNode node in GetAllChildren(currentNode))
            {
                if (node.IsPlayerSpeaking())
                {
                    yield return node;
                }
            }
        }

        public IEnumerable<DialogueNode> GetAIChildren(DialogueNode currentNode)
        {
            foreach (DialogueNode node in GetAllChildren(currentNode))
            {
                if (!node.IsPlayerSpeaking())
                {
                    yield return node;
                }
            }
        }

#if UNITY_EDITOR

        public void CreateNode(DialogueNode parent)
        {
            DialogueNode node = MakeNode(parent);
            Undo.RegisterCompleteObjectUndo(node, "Created Dialogue Node");
            Undo.RecordObject(this, "Added Dialogue Node");
            AddNode(node);
        }

        public void DeleteNode(DialogueNode noteToDelete)
        {
            Undo.RecordObject(this, "Delete Dialogue Node");
            nodes.Remove(noteToDelete);
            OnValidate();
            CleanDanglingChildren(noteToDelete);
            Undo.DestroyObjectImmediate(noteToDelete);
        }

        private DialogueNode MakeNode(DialogueNode parent)
        {
            DialogueNode node = CreateInstance<DialogueNode>();
            node.name = Guid.NewGuid().ToString();
            if (parent != null)
            {
                parent.AddChild(node.name);
                node.SetPlayerSpeaking(!parent.IsPlayerSpeaking());
                node.SetPosition(parent.GetRect().position + newNodeOffset);

            }

            return node;
        }

        private void AddNode(DialogueNode node)
        {
            nodes.Add(node);
            OnValidate();
        }

        private void CleanDanglingChildren(DialogueNode noteToDelete)
        {
            foreach (DialogueNode node in GetAllNodes())
            {
                node.RemoveChildren(noteToDelete.name);
            }
        }

#endif

        public void OnBeforeSerialize()
        {

#if UNITY_EDITOR

            if (nodes.Count == 0)
            {
                DialogueNode node = MakeNode(null);
                AddNode(node);
            }

            if (AssetDatabase.GetAssetPath(this) != "")
            {
                foreach (DialogueNode node in GetAllNodes())
                {
                    if (AssetDatabase.GetAssetPath(node) == "")
                    {
                        AssetDatabase.AddObjectToAsset(node, this);
                    }
                }
            }
#endif

        }

        public void OnAfterDeserialize()
        {
        }
    }
}
