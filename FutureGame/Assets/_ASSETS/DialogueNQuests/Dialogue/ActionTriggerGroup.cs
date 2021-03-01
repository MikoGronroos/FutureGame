using UnityEngine;

public class ActionTriggerGroup : MonoBehaviour
{

    [SerializeField] private MonoBehaviour[] group;

    [SerializeField] private bool activateOnStart = false;

    private void Start()
    {
        Activate(activateOnStart);
    }

    public void Activate(bool shouldActivate)
    {
        foreach (MonoBehaviour script in group)
        {
            script.enabled = shouldActivate;
        }
    }

}
