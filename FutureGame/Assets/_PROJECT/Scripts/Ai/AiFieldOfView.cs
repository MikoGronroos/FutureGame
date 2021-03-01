using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AiFieldOfView : MonoBehaviour
{

    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;

    [SerializeField] private List<Transform> visibleTargets = new List<Transform>();

    private AiController _aiController;

    public float ViewRadius;

    [Range(0,360)]
    public float ViewAngle;

    private void Awake()
    {
        _aiController = GetComponent<AiController>();
    }

    private void Start()
    {
        StartCoroutine(FindTargetsWithDelay(_aiController.GetReactionTime()));
    }

    private IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    private void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadious = Physics.OverlapSphere(transform.position, ViewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadious.Length; i++)
        {
            Transform target = targetsInViewRadious[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < ViewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    if (target.TryGetComponent(out Character character))
                    {
                        _aiController.SawCharacter(character.GetCharacterRace(), target);
                    }
                    visibleTargets.Add(target);
                }
            }
        }

    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}
