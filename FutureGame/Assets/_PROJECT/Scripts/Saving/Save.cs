using UnityEngine;
using System.IO;
using System.Collections;

public static class Save
{

    public static void SaveData(string dataPath, IList saveFiles)
    {
        string json = JsonUtility.ToJson(saveFiles, true);
        File.WriteAllText(Application.persistentDataPath + "/" + dataPath, json);
    }
}
