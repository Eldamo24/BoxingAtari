using UnityEngine;

public class PunchController : MonoBehaviour
{
    public float pushDuration = 1f; 
    public float forceMagnitude = 30f; 
    private Vector2 pushDirection;
    private float pushTimeRemaining = 0f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Punch") && collision.gameObject.name == "SpritePlayer")
        {
            collision.gameObject.GetComponentInParent<PlayerController>().Hit++;
            if(collision.gameObject.GetComponentInParent<PlayerController>().Hit == 1)
            {
                pushDuration = 1f;
                gameObject.GetComponentInParent<EnemyController>().Punched();
                collision.gameObject.GetComponentInParent<PlayerController>().PlayerPoints++;
                UIController.instance.UpdateScorePlayer();
                pushDirection = (transform.parent.transform.position - collision.transform.position).normalized;
                pushTimeRemaining = pushDuration;
            }

        }
    }

    private void FixedUpdate()
    {
        if (pushTimeRemaining > 0)
        {

            pushTimeRemaining -= Time.fixedDeltaTime;

            Vector2 newPosition = rb.position + pushDirection * forceMagnitude * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
        }
    }
}
