﻿using System.IO;
using UnityEditor;
using UnityEngine;

namespace Finark.Dialogue
{
    public class DialogModificationProcessor : UnityEditor.AssetModificationProcessor
    {
        private static AssetMoveResult OnWillMoveAsset(string sourcePath, string destinationPath)
        {

            Dialogue dialogue = AssetDatabase.LoadMainAssetAtPath(sourcePath) as Dialogue;

            if (dialogue == null)
            {
                return AssetMoveResult.DidNotMove;
            }

            if (Path.GetDirectoryName(sourcePath) != Path.GetDirectoryName(destinationPath))
            {

                return AssetMoveResult.DidNotMove;
            }

            dialogue.name = Path.GetFileNameWithoutExtension(destinationPath);

            return AssetMoveResult.DidNotMove;
        }
    }
}