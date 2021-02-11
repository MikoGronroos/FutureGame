using UnityEngine;

public class EquippingItem : MonoBehaviour
{

    [SerializeField] private Transform headParent;
    [SerializeField] private Transform torsoParent;
    [SerializeField] private Transform legsParent;
    [SerializeField] private Transform feetParent;
    [SerializeField] private Transform handParent;

    [SerializeField] private GameObject activeHeadSlotObject;
    [SerializeField] private GameObject activeTorsoSlotObject;
    [SerializeField] private GameObject activeLegSlotObject;
    [SerializeField] private GameObject activeFeetSlotObject;
    [SerializeField] private GameObject activeHandSlotObject;

    public void EquipItem(Item item)
    {
        if (item == null) return;

        var equipmentItem = item as EquipmentItem;

        GameObject currentWeapon = Instantiate(equipmentItem.ItemObject);

        switch (equipmentItem.ThisEquipmentType)
        {
            case EquipmentType.Head:
                activeHeadSlotObject = currentWeapon;
                currentWeapon.transform.SetParent(headParent);
                break;
            case EquipmentType.Torso:
                activeTorsoSlotObject = currentWeapon;
                currentWeapon.transform.SetParent(torsoParent);
                break;
            case EquipmentType.Legs:
                activeLegSlotObject = currentWeapon;
                currentWeapon.transform.SetParent(legsParent);
                break;
            case EquipmentType.Feet:
                activeFeetSlotObject = currentWeapon;
                currentWeapon.transform.SetParent(feetParent);
                break;
            case EquipmentType.HandHeld:
                activeHandSlotObject = currentWeapon;
                currentWeapon.transform.SetParent(handParent);
                currentWeapon.transform.position = new Vector3(0, 0, 0);
                currentWeapon.transform.rotation = Quaternion.identity;
                break;
        }

        Debug.Log("Item Equipped");

    }

    public void DequipItem(EquipmentItem item)
    {
        switch (item.ThisEquipmentType)
        {
            case EquipmentType.Head:
                Destroy(activeHeadSlotObject);
                break;
            case EquipmentType.Torso:
                Destroy(activeTorsoSlotObject);
                break;
            case EquipmentType.Legs:
                Destroy(activeLegSlotObject);
                break;
            case EquipmentType.Feet:
                Destroy(activeFeetSlotObject);
                break;
            case EquipmentType.HandHeld:
                Destroy(activeHandSlotObject);
                break;
            default:
                return;
        }
    }
}
