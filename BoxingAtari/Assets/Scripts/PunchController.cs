using UnityEngine;

public class PunchController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Punch") && collision.gameObject.name == "SpritePlayer")
        {
            collision.gameObject.GetComponentInParent<PlayerController>().Hit++;
            if(collision.gameObject.GetComponentInParent<PlayerController>().Hit == 1)
            {
                collision.gameObject.GetComponentInParent<PlayerController>().PlayerPoints++;
                UIController.instance.UpdateScorePlayer();
            }

        }
    }
}
