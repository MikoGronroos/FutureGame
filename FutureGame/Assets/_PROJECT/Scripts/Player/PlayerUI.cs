using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{

    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider staminaBar;
    [SerializeField] private Slider hungerBar;
    [SerializeField] private Slider thirstBar;

    [SerializeField] private Button inventoryButton;
    [SerializeField] private Button craftingButton;

    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject craftingPanel;
    [SerializeField] private GameObject equipmentPanel;

    //Sleeping
    [SerializeField] private Image blackScreen;
    [SerializeField] private TMP_Text sleepingText;
    
    private bool inventoryOpen;
    private bool craftingOpen;

    private void Awake()
    {
        inventoryButton.onClick.AddListener(InventoryButton);
        craftingButton.onClick.AddListener(CraftingButton);
    }

    private void Start()
    {
        MessageReceiver.SubscrideToMessage("PlayerSleepingEvent", SleepEvent);
        MessageReceiver.SubscrideToMessage("PlayerWakeUpEvent", WakeUpEvent);
    }

    private void InventoryButton()
    {
        if (!inventoryOpen)
        {
            inventoryOpen = true;
            craftingOpen = false;
            inventoryPanel.SetActive(inventoryOpen);
            craftingPanel.SetActive(craftingOpen);
            equipmentPanel.SetActive(inventoryOpen);
        }
    }

    private void CraftingButton()
    {
        if (!craftingOpen)
        {
            craftingOpen = true;
            inventoryOpen = false;
            craftingPanel.SetActive(craftingOpen);
            inventoryPanel.SetActive(inventoryOpen);
            equipmentPanel.SetActive(inventoryOpen);
        }
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthBar.value = currentHealth / maxHealth;
    }

    public void UpdateStaminaBar(float currentStamina, float maxStamina)
    {
        staminaBar.value = currentStamina / maxStamina;
    }

    public void UpdateHungerBar(float currentHunger, float maxHunger)
    {
        hungerBar.value = currentHunger / maxHunger;
    }
    public void UpdateThirstBar(float currentThirst, float maxThirst)
    {
        thirstBar.value = currentThirst / maxThirst;
    }

    private void SleepEvent(string name, string message)
    {
        Debug.Log("Screen Fade In");
        sleepingText.enabled = true;
        blackScreen.ChangeAlpha(1);
    }

    private void WakeUpEvent(string name, string message)
    {
        Debug.Log("Screen Fade Out");
        sleepingText.enabled = false;
        blackScreen.ChangeAlpha(0);
    }

}
