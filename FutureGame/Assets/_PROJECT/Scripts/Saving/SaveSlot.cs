using UnityEngine;

public class SaveSlot : MonoBehaviour
{

    private string thisPath;

    public SaveSlot(string path)
    {
        thisPath = path;
    }

    public void LoadSlotData()
    {
        Load.LoadData(thisPath);
    }

    public void SaveSlotData()
    {
    }

}
