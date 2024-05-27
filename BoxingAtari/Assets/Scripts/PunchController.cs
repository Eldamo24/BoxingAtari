using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Punch"))
        {
            collision.gameObject.GetComponentInParent<PlayerController>().PlayerPoints++;
        }
    }
}
