using UnityEngine;

public class WorldClock : MonoBehaviour
{
    [Header("Time")]
    [SerializeField] private int days = 0;
    [SerializeField] private int months = 0;
    [SerializeField] private int years = 0;

    public void AddDay(int amount)
    {
        days += amount;
        if (days >= 30)
        {
            months++;
            if (months >= 12)
            {
                years++;
            }
        }
    }
}
