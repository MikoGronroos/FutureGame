using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    public List<SaveSlot> SaveSlots = new List<SaveSlot>();

    public void NewSaveSlot()
    {
        SaveSlot slot = new SaveSlot("Slot" + SaveSlots.Count);
        slot.LoadSlotData();
    }

}
