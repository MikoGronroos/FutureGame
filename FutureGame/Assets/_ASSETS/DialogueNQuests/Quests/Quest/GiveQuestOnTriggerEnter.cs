using Finark.Quests;
using UnityEngine;

public class GiveQuestOnTriggerEnter : QuestGiver
{

    public override void GiveQuest()
    {
        base.GiveQuest();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            GiveQuest();
        }
    }
}
