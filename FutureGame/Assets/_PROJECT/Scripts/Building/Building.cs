using UnityEngine;

[CreateAssetMenu(menuName = "Finark/Building")]
public class Building : ScriptableObject
{

    public int BuildingId;
    public string BuildingName;
    public Sprite BuildingIcon;
    public GameObject InspectElement;
    public GameObject RealElement;
    public Vector2[] NeededItems;

}
