using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildSelection : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] private Building building;

    public Image ItemIcon;

    public void OnPointerClick(PointerEventData eventData)
    {
        var buildingPlan = FindObjectOfType<BuildingPlan>();
        buildingPlan.ChangeInspectObject(building.InspectElement);
        buildingPlan.SetRealObject(building.RealElement);
        buildingPlan.SetHasObject(true);
        buildingPlan.SetNeededItems(building.NeededItems);
    }

    public void SetBuilding(Building building)
    {
        this.building = building;
    }

}
