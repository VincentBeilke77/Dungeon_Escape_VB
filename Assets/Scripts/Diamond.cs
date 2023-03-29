using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    // on trigger to collect
    // check for player
    // add the value of the diamond to player
    [SerializeField]
    public int Diamonds { get; set; } = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            _player.AddDiamonds(Diamonds);
            Destroy(gameObject);
        }
    }
}
