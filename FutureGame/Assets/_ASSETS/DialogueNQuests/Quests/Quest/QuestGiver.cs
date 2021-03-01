using UnityEngine;

namespace Finark.Quests
{
    public class QuestGiver : MonoBehaviour
    {

        [SerializeField] protected Quest quest;

        public virtual void GiveQuest()
        {
            QuestList questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            questList.AddQuest(quest);
        }

    }
}
