using UnityEngine;

public class EquippingItem : MonoBehaviour
{

    [SerializeField] private Animator playerAnimator;
    [SerializeField] private AnimatorOverrideController startingAnimatorOverride;

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
        GameObject currentWeapon = null;

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
                currentWeapon = Instantiate(equipmentItem.ItemObject, handParent);
                activeHandSlotObject = currentWeapon;
                if (equipmentItem.ThisAnimatorOverrideController != null)
                {
                    playerAnimator.runtimeAnimatorController = equipmentItem.ThisAnimatorOverrideController;
                }
                break;
        }

        Debug.Log("Item Equipped");

    }

    public void DequipItem(Item item)
    {
        Debug.Log($"Dequipping {item.ItemName}");
        if (item == null) return;

        var equipmentItem = item as EquipmentItem;

        playerAnimator.runtimeAnimatorController = startingAnimatorOverride;

        switch (equipmentItem.ThisEquipmentType)
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
