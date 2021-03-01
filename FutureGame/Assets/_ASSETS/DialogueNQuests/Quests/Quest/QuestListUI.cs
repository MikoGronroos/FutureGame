using Finark.Quests;
using UnityEngine;

public class QuestListUI : MonoBehaviour
{

    [SerializeField] private QuestItemUI questPrefab;

    [SerializeField] private QuestList _questList;

    private void Awake()
    {
        _questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
    }

    private void Start()
    {
        _questList.onUpdate += Redraw;
        Redraw();
    }

    private void Redraw()
    {
        foreach (Transform item in transform)
        {
            Destroy(item.gameObject);
        }
        foreach (QuestStatus status in _questList.GetStatuses())
        {
            QuestItemUI uiInstance = Instantiate(questPrefab, transform);
            uiInstance.Setup(status);
        }
    }
}
