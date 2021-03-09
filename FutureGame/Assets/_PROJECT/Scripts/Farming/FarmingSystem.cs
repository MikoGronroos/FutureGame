using System.Collections;
using UnityEngine;

public class FarmingSystem : MonoBehaviour
{

    [Tooltip("Tick Rate In Seconds")]
    [SerializeField] private float farmingTickRate;

    private void Start()
    {
        StartCoroutine(FarmTick());
    }

    private IEnumerator FarmTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(farmingTickRate);
            MessageSender.SendMessageToClients("OnFarmingTick");
        }
    }
}
