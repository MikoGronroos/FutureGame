using System.Collections.Generic;
using UnityEngine;

namespace Finark.Quests
{
    [System.Serializable]
    public class QuestStatus
    {

        [SerializeField] private Quest _quest;
        [SerializeField] private List<string> _completedObjectives = new List<string>();

        public QuestStatus(Quest quest)
        {
            this._quest = quest;
        }

        public Quest GetQuest()
        {
            return _quest;
        }

        public bool IsComplete()
        {
            foreach (var objective in _quest.GetObjectives())
            {
                if (!_completedObjectives.Contains(objective.Reference))
                {
                    return false;
                }
            }
            return true;
        }

        public int GetCompletedCount()
        {
            return _completedObjectives.Count;
        }

        public bool IsObjectiveComplete(string objective)
        {
            return _completedObjectives.Contains(objective);
        }

        public void CompleteObjective(string objective)
        {
            if (_quest.HasObjective(objective) && !_completedObjectives.Contains(objective))
            {
                _completedObjectives.Add(objective);
            }
        }
    }
}
