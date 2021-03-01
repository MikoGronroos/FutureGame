using UnityEngine;

namespace Finark.Quests
{
    public class QuestCompletion : MonoBehaviour
    {

        [SerializeField] private Quest quest;
        [SerializeField] private string objective;

        public void CompleteObjective()
        {
            QuestList questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            if (questList.HasQuest(quest))
            {
                questList.CompleteObjective(quest, objective);
            }
        }
    }
}
