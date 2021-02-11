using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{

    [SerializeField] private List<Building> buildings;

    [SerializeField] private GameObject BuildingSlot;

    [SerializeField] private GameObject neededItemSlot;

    [SerializeField] private Transform slotParent;

    private void Start()
    {
        foreach (var slot in buildings)
        {
            GameObject newSlot = Instantiate(BuildingSlot);
            newSlot.transform.SetParent(slotParent);
            newSlot.GetComponent<BuildSelection>().SetBuilding(slot);
            newSlot.transform.GetChild(0).GetComponent<Image>().sprite = slot.BuildingIcon;
            newSlot.transform.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = slot.BuildingName;
            Transform neededItemsPanel = newSlot.transform.Find("NeededItemsPanel");
            for (int j = 0; j < slot.NeededItems.Length; j++)
            {
                GameObject neededItem = Instantiate(neededItemSlot, neededItemsPanel);
                string text = $"{slot.NeededItems[j].y}x";
                neededItem.GetComponent<Image>().sprite = ItemDictionary.Instance.GetItemByID((int)slot.NeededItems[j].x).Icon;
                neededItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
            }
        }
    }
}
