using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Detect if ball has crossed and destroy it.
/// </summary>
public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            Destroy(collision.gameObject);
            //decrement the ball count of balls currently in scene

        }
    }
}
