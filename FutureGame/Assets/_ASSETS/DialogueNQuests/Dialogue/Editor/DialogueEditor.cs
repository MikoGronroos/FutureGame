﻿using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Finark.Dialogue.Editor
{
    public class DialogueEditor : EditorWindow
    {

        private Dialogue _selectedDialogue = null;
        [NonSerialized]
        private GUIStyle nodeStyle;
        [NonSerialized]
        private GUIStyle playerNodeStyle;
        [NonSerialized]
        private DialogueNode _draggingNode = null;
        [NonSerialized]
        private Vector2 _draggingOffset;
        [NonSerialized]
        private DialogueNode _creatingNode = null;
        [NonSerialized]
        private DialogueNode _deletingNode = null;
        [NonSerialized]
        private DialogueNode _linkingParentNode = null;
        Vector2 scrollPosition;
        [NonSerialized]
        private bool _draggingCanvas = false;
        [NonSerialized]
        private Vector2 _draggingCanvasOffset;

        const float canvasSize = 4000;
        const float backgroundSize = 50;

        [MenuItem("Dialogue Editor/Editor")]
        public static void ShowEditorWindow()
        {
            GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
        }

        [OnOpenAsset(1)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            if(EditorUtility.InstanceIDToObject(instanceID) is Dialogue)
            {
                ShowEditorWindow();
                return true;
            }
            return false;
        }

        private void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChanged;

            nodeStyle = new GUIStyle();

            nodeStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
            nodeStyle.normal.textColor = Color.white;
            nodeStyle.padding = new RectOffset(20,20,20,20);
            nodeStyle.border = new RectOffset(12,12,12,12);
            
            playerNodeStyle = new GUIStyle();

            playerNodeStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
            playerNodeStyle.normal.textColor = Color.white;
            playerNodeStyle.padding = new RectOffset(20, 20, 20, 20);
            playerNodeStyle.border = new RectOffset(12, 12, 12, 12);

        }

        private void OnSelectionChanged()
        {
            Dialogue newDialogue = Selection.activeObject as Dialogue;
            if (newDialogue != null)
            {
                _selectedDialogue = newDialogue;
                Repaint();
            }
        }

        private void OnGUI()
        {

            if (_selectedDialogue == null)
            {
                EditorGUILayout.LabelField("No Dialogue Selected");
            }
            else
            {
                ProcessEvents();

                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

                Rect canvas = GUILayoutUtility.GetRect(canvasSize, canvasSize);

                Texture2D backgroundTex = Resources.Load("background") as Texture2D;

                Rect texCoords = new Rect(0,0,  canvasSize / backgroundSize, canvasSize / backgroundSize);

                GUI.DrawTextureWithTexCoords(canvas, backgroundTex, texCoords);

                foreach (var node in _selectedDialogue.GetAllNodes())
                {
                    DrawConnections(node);
                }
                foreach (var node in _selectedDialogue.GetAllNodes())
                {
                    DrawNode(node);
                }

                EditorGUILayout.EndScrollView();

                if (_creatingNode != null)
                {
                    _selectedDialogue.CreateNode(_creatingNode);
                    _creatingNode = null;
                }
                if (_deletingNode != null)
                {
                    _selectedDialogue.DeleteNode(_deletingNode);
                    _deletingNode = null;
                }
            }
        }

        private void ProcessEvents()
        {
            if (Event.current.type == EventType.MouseDown && _draggingNode == null)
            {
                _draggingNode = GetNodeAtPoint(Event.current.mousePosition + scrollPosition);
                if (_draggingNode != null)
                {
                    _draggingOffset = _draggingNode.GetRect().position - Event.current.mousePosition;
                    Selection.activeObject = _draggingNode;
                }
                else
                {
                    _draggingCanvas = true;
                    _draggingCanvasOffset = Event.current.mousePosition + scrollPosition;
                    Selection.activeObject = _selectedDialogue;
                }
            }
            else if (Event.current.type == EventType.MouseDrag && _draggingNode != null)
            {
                _draggingNode.SetPosition(Event.current.mousePosition + _draggingOffset);
                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseDrag && _draggingCanvas)
            {

                scrollPosition = _draggingCanvasOffset - Event.current.mousePosition;

                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseUp && _draggingNode != null)
            {
                _draggingNode = null;
            }
            else if (Event.current.type == EventType.MouseUp && _draggingCanvas)
            {
                _draggingCanvas = false;
            }
        }

        private void DrawNode(DialogueNode node)
        {
            GUIStyle style = nodeStyle;
            if (node.IsPlayerSpeaking())
            {
                style = playerNodeStyle;
            }
            GUILayout.BeginArea(node.GetRect(), style);

            node.SetText(EditorGUILayout.TextField(node.GetText()));

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("-"))
            {
                _deletingNode = node;
            }

            DrawLinkButtons(node);

            if (GUILayout.Button("+"))
            {
                _creatingNode = node;
            }

            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }

        private void DrawLinkButtons(DialogueNode node)
        {
            if (_linkingParentNode == null)
            {
                if (GUILayout.Button("link"))
                {
                    _linkingParentNode = node;
                }
            }
            else if (_linkingParentNode == node)
            {
                if (GUILayout.Button("cancel"))
                {
                    _linkingParentNode = null;
                }
            }
            else if (_linkingParentNode.GetChildren().Contains(node.name))
            {
                if (GUILayout.Button("unlink"))
                {
                    _linkingParentNode.RemoveChildren(node.name);
                    _linkingParentNode = null;
                }
            }
            else
            {
                if (GUILayout.Button("child"))
                {
                    _linkingParentNode.AddChild(node.name);
                    _linkingParentNode = null;
                }
            }

        }

        private void DrawConnections(DialogueNode node)
        {
            Vector3 startPosition = new Vector2(node.GetRect().xMax, node.GetRect().center.y);
            foreach (DialogueNode childNode in _selectedDialogue.GetAllChildren(node))
            {
                Vector3 endPosition = new Vector2(childNode.GetRect().xMin, childNode.GetRect().center.y);
                Vector3 controlPointOffset = endPosition - startPosition;
                controlPointOffset.y = 0;
                controlPointOffset.x *= 0.8f;
                Handles.DrawBezier(startPosition, 
                    endPosition,
                    startPosition + controlPointOffset,
                    endPosition - controlPointOffset, 
                    Color.magenta, null, 4f);
            }
        }

        private DialogueNode GetNodeAtPoint(Vector2 point)
        {
            DialogueNode foundNode = null;
            foreach (var node in _selectedDialogue.GetAllNodes())
            {
                if (node.GetRect().Contains(point))
                {
                    foundNode = node;
                }
            }
            return foundNode;
        }

    }
}