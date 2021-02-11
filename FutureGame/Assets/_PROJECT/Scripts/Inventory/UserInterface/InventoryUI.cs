using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    [SerializeField] private GameObject _characterInfoPanel;

    private bool _inventoryIsOpen;

    private void Update()
    {
        if (CharacterOwner.Instance.Input.InventoryInput())
        {
            if (_inventoryIsOpen)
            {
                _inventoryIsOpen = false;
                Cursor.visible = _inventoryIsOpen;
                Cursor.lockState = CursorLockMode.Locked;
                _characterInfoPanel.SetActive(_inventoryIsOpen);
                MessageSender.SendMessageToClients("InventoryToggle");
            }
            else if (!_inventoryIsOpen)
            {
                _inventoryIsOpen = true;
                Cursor.visible = _inventoryIsOpen;
                Cursor.lockState = CursorLockMode.None;
                _characterInfoPanel.SetActive(_inventoryIsOpen);
                MessageSender.SendMessageToClients("InventoryToggle");
            }
        }
    }

}
