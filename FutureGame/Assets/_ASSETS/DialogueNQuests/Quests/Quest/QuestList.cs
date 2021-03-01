using Finark.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Finark.Quests
{
    public class QuestList : MonoBehaviour, IPredicateEvaluator
    {

        [SerializeField] private List<QuestStatus> _statuses = new List<QuestStatus>();

        public event Action onUpdate;

        public void AddQuest(Quest quest)
        {

            if (HasQuest(quest)) return;

             QuestStatus newStatus = new QuestStatus(quest);
            _statuses.Add(newStatus);
            onUpdate?.Invoke();
        }

        public void CompleteObjective(Quest quest, string objective)
        {

            QuestStatus status = GetQuestStatus(quest);
            status.CompleteObjective(objective);

            if (status.IsComplete())
            {
                GiveReward(quest);
            }

            onUpdate?.Invoke();
        }

        public bool HasQuest(Quest quest)
        {
            return GetQuestStatus(quest) != null;
        }

        public IEnumerable<QuestStatus> GetStatuses()
        {
            return _statuses;
        }

        private QuestStatus GetQuestStatus(Quest quest)
        {
            foreach (QuestStatus status in _statuses)
            {
                if (status.GetQuest() == quest)
                {
                    return status;
                }
            }
            return null;
        }

        private void GiveReward(Quest quest)
        {
            foreach (var reward in quest.GetRewards())
            {
                Inventory.Instance.AddToInventory(reward.Item, reward.Amount);
            }
        }

        public bool? Evaluate(string predicate, string[] parameters)
        {
            switch (predicate)
            {
                case "HasQuest":
                    return HasQuest(Quest.GetByName(parameters[0]));
                case "CompletedQuest":
                    if(GetQuestStatus(Quest.GetByName(parameters[0])) != null)
                    {
                        return GetQuestStatus(Quest.GetByName(parameters[0])).IsComplete();
                    }
                    return false;
                case "InventoryContainsItem":
                    return Inventory.Instance.InventoryHasItem(int.Parse(parameters[0]));
                case "HasCompeletedObjective":
                    if (GetQuestStatus(Quest.GetByName(parameters[0])) != null)
                    {
                        return GetQuestStatus(Quest.GetByName(parameters[0])).IsObjectiveComplete(parameters[1]);
                    }
                    return false;
                default:
                    return null;
            }
        }
    }
}
