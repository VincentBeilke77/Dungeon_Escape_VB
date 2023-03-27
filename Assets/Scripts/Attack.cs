using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // variable to determine if damage function can be called
    bool canHit = true;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Hit: {other.name}");

        var hit = other.GetComponent<IDamageable>();
        
        if (hit != null)
        {
            if (canHit)
            {
                hit.Damage(1);
                canHit = false;
            }
        }

        StartCoroutine(ResetHitRoutine());
    }

    IEnumerator ResetHitRoutine()
    {
        yield return new WaitForSeconds(.5f);
        canHit = true;
    }
}