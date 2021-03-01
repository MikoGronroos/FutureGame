using Finark.Quests;
using UnityEngine;

public class QuestTooltipHandler : TooltipHandler
{
    public override bool CanCreateTooltip()
    {
        return true;
    }

    public override void UpdateTooltip(GameObject tooltip)
    {
        QuestStatus status = GetComponent<QuestItemUI>().GetQuestStatus();
        tooltip.GetComponent<QuestTooltipUI>().Setup(status);
    }
}
