using System.Collections;
using UnityEngine;

public class AiAttack : MonoBehaviour
{

    [SerializeField] private bool isAttacking;
    [SerializeField] private float attackTime;

    public void StartAttack()
    {
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        yield return new WaitForSeconds(attackTime);
        isAttacking = false;
    }

}
