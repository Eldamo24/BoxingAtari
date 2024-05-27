using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Punch") && collision.gameObject.name == "SpritePlayer")
        {
            collision.gameObject.GetComponentInParent<PlayerController>().PlayerPoints++;
        }
        else if(collision.gameObject.CompareTag("Punch") && collision.gameObject.name == "SpriteEnemy")
        {
            collision.gameObject.GetComponentInParent<EnemyController>().EnemyPoints++;
        }
    }
}
