using System;
using Finark.Quests;
using TMPro;
using UnityEngine;

public class QuestTooltipUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private Transform objectiveContainer;
    [SerializeField] private GameObject objectivePrefab;
    [SerializeField] private GameObject objectiveIncompletePrefab;

    public void Setup(QuestStatus status)
    {
        Quest quest = status.GetQuest();
        title.text = quest.GetTitle();
        foreach (Transform item in objectiveContainer)
        {
            Destroy(item.gameObject);
        }
        foreach (var objective in quest.GetObjectives())
        {
            GameObject prefab = status.IsObjectiveComplete(objective.Reference) ? objectivePrefab : objectiveIncompletePrefab;
            GameObject newObjective = Instantiate(prefab, objectiveContainer);
            newObjective.GetComponentInChildren<TextMeshProUGUI>().text = objective.Description;
        }
        rewardText.text = GetRewardText(quest);
    }

    private string GetRewardText(Quest quest)
    {
        string newRewardText = "";

        foreach (var reward in quest.GetRewards())
        {
            if (newRewardText != "")
            {
                newRewardText += ", ";
            }

            if (reward.Amount > 1)
            {
                newRewardText += reward.Amount + " ";
            }

            newRewardText += reward.Item.ItemName;
        }

        if (newRewardText == "")
        {
            newRewardText = "No reward.";
        }

        newRewardText += ".";

        return newRewardText;

    }
}
