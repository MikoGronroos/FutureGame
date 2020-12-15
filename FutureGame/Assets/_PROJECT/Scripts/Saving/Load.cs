using UnityEngine;
using System.IO;

public class Load : MonoBehaviour
{

    public static void LoadData(string dataPath)
    {
        if (!File.Exists(Application.persistentDataPath + "/" + dataPath + ".json"))
        {
            Debug.LogWarning($"Path {Application.persistentDataPath + "/" + dataPath} has no data in it! Creating new file!");
            try
            {
                File.Create(Application.persistentDataPath + "/" + dataPath + ".json");
            }
            catch
            {
                Debug.LogError("Couldn't create new file!");
            }
            return;
        }
    }

}
