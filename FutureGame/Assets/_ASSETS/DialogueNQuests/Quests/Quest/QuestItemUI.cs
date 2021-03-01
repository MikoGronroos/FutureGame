using Finark.Quests;
using UnityEngine;
using TMPro;

public class QuestItemUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI progress;

    private QuestStatus _status;

    public void Setup(QuestStatus status)
    {
        _status = status;
        title.text = _status.GetQuest().GetTitle();
        progress.text = $"{_status.GetCompletedCount()}/{_status.GetQuest().GetObjectiveCount()}";
    }

    public QuestStatus GetQuestStatus()
    {
        return _status;
    }

}
