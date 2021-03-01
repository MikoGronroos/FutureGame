using UnityEngine;

public class Bed : MonoBehaviour
    , IInteractable
{

    [SerializeField] private float sleepingTimeMultiplier = 1f;
    [SerializeField] private bool canSleep = false;
    [SerializeField] private bool isSleeping = false;

    private void Start()
    {
        WorldManager.Instance.SpawnPoint.SpawnPoint = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        MessageReceiver.SubscrideToMessage("MorningRisesEvent", MorningRisesEvent);
        MessageReceiver.SubscrideToMessage("EveningFallsEvent", EveningFallsEvent);
        isSleeping = false;
        canSleep = false;
    }

    public void Interact()
    {
        if (canSleep)
        {
            Sleep();
        }
    }

    public void Sleep()
    {
        Time.timeScale = 1f * sleepingTimeMultiplier;
        isSleeping = true;
        MessageSender.SendMessageToClients("PlayerSleepingEvent");
    }

    public void WakeUp()
    {
        Time.timeScale = 1f;
        isSleeping = false;
        MessageSender.SendMessageToClients("PlayerWakeUpEvent");
    }

    private void EveningFallsEvent(string name, string message)
    {
        canSleep = true;
    }

    private void MorningRisesEvent(string name, string message)
    {
        canSleep = false;

        if (isSleeping)
        {
            WakeUp();
        }
    }
}
