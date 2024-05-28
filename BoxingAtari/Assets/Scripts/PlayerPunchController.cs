using UnityEngine;

public class PlayerPunchController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Punch") && collision.gameObject.name == "SpriteEnemy")
        {
            collision.gameObject.GetComponentInParent<EnemyController>().HitEnemy++;
            if(collision.gameObject.GetComponentInParent<EnemyController>().HitEnemy == 1)
            {
                collision.gameObject.GetComponentInParent<EnemyController>().EnemyPoints++;
                UIController.instance.UpdateScoreEnemy();
            }
        }
    }
}
