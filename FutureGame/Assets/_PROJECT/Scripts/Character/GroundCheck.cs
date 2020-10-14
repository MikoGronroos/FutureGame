using UnityEngine;

public class GroundCheck : MonoBehaviour, IGroundCheck
{

    [SerializeField] private float distToGround = 1f;

    [SerializeField] private LayerMask groundLayer;

    RaycastHit hit;

    public bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, out hit, distToGround + 0.1f, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Vector3.down * (distToGround + 0.1f));
    }
}
