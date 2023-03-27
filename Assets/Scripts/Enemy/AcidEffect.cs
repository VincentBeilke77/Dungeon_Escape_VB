using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEffect : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 5.0f);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * 3 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {            
            if (other.TryGetComponent<IDamageable>(out var hit))
            {
                hit.Damage(1);
                Destroy(gameObject);
            }
        }
    }
}
