using UnityEngine;

public class BuildingLockedArea : MonoBehaviour
{

    [SerializeField] private float radious;

    [SerializeField] private Color color = Color.red;

    private SphereCollider _collider;

    private void Awake()
    {
        _collider = gameObject.AddComponent<SphereCollider>();
        _collider.radius = radious;
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponentInChildren<BuildingPlan>())
        {
            other.transform.GetComponentInChildren<BuildingPlan>().CanBuild = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.GetComponentInChildren<BuildingPlan>())
        {
            other.transform.GetComponentInChildren<BuildingPlan>().CanBuild = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireSphere(transform.position, radious);
    }

}