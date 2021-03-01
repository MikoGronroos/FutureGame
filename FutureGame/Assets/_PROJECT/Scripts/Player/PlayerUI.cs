using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerUI : MonoBehaviour
{

    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider staminaBar;
    [SerializeField] private Slider hungerBar;
    [SerializeField] private Slider thirstBar;

    [SerializeField] private TextMeshProUGUI weightCounter;

    [SerializeField] private Button inventoryButton;
    [SerializeField] private Button craftingButton;
    [SerializeField] private Button diaryButton;

    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject craftingPanel;
    [SerializeField] private GameObject equipmentPanel;
    [SerializeField] private GameObject diaryPanel;

    //Sleeping
    [SerializeField] private GameObject blackScreen;
    
    private bool inventoryOpen;
    private bool craftingOpen;
    private bool diaryOpen;

    private void Awake()
    {
        inventoryButton.onClick.AddListener(InventoryButton);
        craftingButton.onClick.AddListener(CraftingButton);
        diaryButton.onClick.AddListener(DiaryButton);
    }

    private void Start()
    {
        MessageReceiver.SubscrideToMessage("PlayerSleepingEvent", SleepEventListener);
        MessageReceiver.SubscrideToMessage("PlayerWakeUpEvent", WakeUpEventListener);
    }

    private void InventoryButton()
    {
        if (!inventoryOpen)
        {
            inventoryOpen = true;
            craftingOpen = false;
            diaryOpen = false;
            inventoryPanel.SetActive(inventoryOpen);
            craftingPanel.SetActive(craftingOpen);
            equipmentPanel.SetActive(inventoryOpen);
            diaryPanel.SetActive(diaryOpen);
        }
    }

    private void DiaryButton()
    {
        inventoryOpen = false;
        craftingOpen = false;
        diaryOpen = true;
        inventoryPanel.SetActive(inventoryOpen);
        craftingPanel.SetActive(craftingOpen);
        equipmentPanel.SetActive(inventoryOpen);
        diaryPanel.SetActive(diaryOpen);
    }


    private void CraftingButton()
    {
        if (!craftingOpen)
        {
            craftingOpen = true;
            inventoryOpen = false;
            diaryOpen = false;
            craftingPanel.SetActive(craftingOpen);
            inventoryPanel.SetActive(inventoryOpen);
            equipmentPanel.SetActive(inventoryOpen);
            diaryPanel.SetActive(diaryOpen);
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

    public void UpdateWeightCounter(float currentWeight, float maxWeight)
    {
        string text = $"{currentWeight}kg/{maxWeight}kg";
        weightCounter.text = text;
    }

    private void SleepEventListener(string name, string message)
    {
        blackScreen.SetActive(true);
    }

    private void WakeUpEventListener(string name, string message)
    {
        blackScreen.SetActive(false);
    }

}
