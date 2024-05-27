using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform target;
    private SpriteRenderer enemyRenderer;
    private float speedMovement = 2f;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        enemyRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckFlip();
        Movement();
    }

    private void CheckFlip()
    {
        if(transform.position.x <= target.position.x)
        {
            enemyRenderer.flipX = true;
        }
        else
        {
            enemyRenderer.flipX = false;
        }
    }

    private void Movement()
    {
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        transform.Translate(direction * speedMovement * Time.deltaTime);
    }
}
