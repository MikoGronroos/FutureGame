using System.Collections.Generic;
using UnityEngine;

public class CropPlot : MonoBehaviour
{

    [SerializeField] private List<FarmItem> plants = new List<FarmItem>();

    [SerializeField] private int plantsThatNeedMoisture = 0;

    [SerializeField] private int maxPlants = 0;

    [SerializeField] private float moistureLevel;

    private void Start()
    {
        MessageReceiver.SubscrideToMessage("OnFarmingTick", OnFarmingTickListener);

        foreach (FarmItem item in plants)
        {
            if (item.GetNeedMoisture())
            {
                plantsThatNeedMoisture++;
            }
        }
    }

    private void OnFarmingTickListener(string arg1, string arg2)
    {
        if (plants.Count == 0) return;

        SendMoistureToPlants();

    }

    private void SendMoistureToPlants()
    {
        float amountToSend = moistureLevel / plantsThatNeedMoisture;

        for (int i = 0; i < plants.Count; i++)
        {
            if (plants[i].GetNeedMoisture())
            {
                plants[i].AddMoisture(amountToSend);
            }
        }

        moistureLevel = 0;

    }

    public void AddPlant()
    {
        if (plants.Count >= maxPlants)
        {
            return;
        }
    }
}
