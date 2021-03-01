﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Finark.Quests
{

    [CreateAssetMenu(fileName = "Quest", menuName = "QuestsNDialogue/Quest", order = 0)]
    public class Quest : ScriptableObject
    {

        [SerializeField] private List<Objective> objectives = new List<Objective>();
        [SerializeField] private List<Reward> rewards = new List<Reward>();

        [Serializable]
        public class Reward
        {
            [Min(1)]
            public int Amount;
            public Item Item;
        }

        [Serializable]
        public class Objective
        {
            public string Reference;
            public string Description;
        }

        public string GetTitle()
        {
            return name;
        }

        public int GetObjectiveCount()
        {
            return objectives.Count;
        }

        public IEnumerable<Objective> GetObjectives()
        {
            return objectives;
        }

        public IEnumerable<Reward> GetRewards()
        {
            return rewards;
        }

        public bool HasObjective(string objectiveRef)
        {
            foreach (var objective in objectives)
            {
                if (objective.Reference == objectiveRef)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasCompletedObjective()
        {
            return false;
        }

        public static Quest GetByName(string questName)
        {
            foreach (Quest quest in Resources.LoadAll<Quest>(""))
            {
                if (quest.name == questName)
                {
                    return quest;
                }
            }
            return null;
        }
    }
}