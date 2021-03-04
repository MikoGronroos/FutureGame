using UnityEngine;

public class EquippingItem : MonoBehaviour
{

    [SerializeField] private Item defaultRightHandItem;

    [SerializeField] private Animator playerAnimator;
    [SerializeField] private PlayerAttack playerAttack;

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

    [SerializeField] private Item activeHeadSlotItem;
    [SerializeField] private Item activeTorsoSlotItem;
    [SerializeField] private Item activeLegSlotItem;
    [SerializeField] private Item activeFeetSlotItem;
    [SerializeField] private Item activeHandSlotItem;

    private bool _dequippingFromEquipping;

    private void Start()
    {
        EquipItem(defaultRightHandItem);
    }

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
                activeHeadSlotItem = item;
                break;
            case EquipmentType.Torso:
                activeTorsoSlotObject = currentWeapon;
                currentWeapon.transform.SetParent(torsoParent);
                activeTorsoSlotItem = item;
                break;
            case EquipmentType.Legs:
                activeLegSlotObject = currentWeapon;
                currentWeapon.transform.SetParent(legsParent);
                activeLegSlotItem = item;
                break;
            case EquipmentType.Feet:
                activeFeetSlotObject = currentWeapon;
                currentWeapon.transform.SetParent(feetParent);
                activeFeetSlotItem = item;
                break;
            #region HandHeld
            case EquipmentType.HandHeld:

                if (activeHandSlotItem != null)
                {
                    _dequippingFromEquipping = true;
                    DequipItem(activeHandSlotItem);
                }

                activeHandSlotItem = item;

                currentWeapon = Instantiate(equipmentItem.ItemObject, handParent);
                activeHandSlotObject = currentWeapon;
                if (equipmentItem.ThisAnimatorOverrideController != null)
                {
                    playerAnimator.runtimeAnimatorController = equipmentItem.ThisAnimatorOverrideController;
                }
                currentWeapon.TryGetComponent(out HitDetection detection);
                playerAttack.LoadWeapon(equipmentItem.Damage, 0.7f, detection);
                break;
                #endregion
        }
    }

    public void DequipItem(Item item)
    {
        if (item == null) return;

        var equipmentItem = item as EquipmentItem;

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
            #region HandHeld
            case EquipmentType.HandHeld:
                Destroy(activeHandSlotObject);
                if (!_dequippingFromEquipping)
                {
                    activeHandSlotItem = null;
                    EquipItem(defaultRightHandItem);
                }
                _dequippingFromEquipping = false;
                break;
            #endregion
            default:
                return;
        }
    }
}
