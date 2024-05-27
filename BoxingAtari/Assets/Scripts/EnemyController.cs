using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform target;
    private SpriteRenderer enemyRenderer;
    private Animator animEnemy;
    private bool isPunching = false;
    private bool canPunch = true;
    private float coolDown = 2f;
    private float offset = 0.8f;
    private float speedMovement = 2f;
    private float distance;
    private float minDistance = 1.4f;
    private int _enemyPoints = 0;
    public int EnemyPoints { get => _enemyPoints; set => _enemyPoints = value; }

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        enemyRenderer = GetComponentInChildren<SpriteRenderer>();
        animEnemy = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
        CheckFlip();
        CheckIfCanPunch();
        if (!isPunching)
        {
            Movement();
        }
        if(distance < minDistance)
        {
            TryAttack();
        }
    }

    private void TryAttack()
    {
        if (canPunch)
        {
            canPunch = false;
            if (transform.position.y - offset  >= target.position.y)
            {
                animEnemy.SetTrigger("IsPunchingRight");
            }
            else
            {
                animEnemy.SetTrigger("IsPunchingLeft");
            }
        }
    }

    private void CheckDistance()
    {
        distance = Vector3.Distance(target.position, transform.position);
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

    private void CheckIfCanPunch()
    {
        if (animEnemy.GetCurrentAnimatorStateInfo(0).IsName("EnemyPunchRight") || animEnemy.GetCurrentAnimatorStateInfo(0).IsName("EnemyPunchLeft"))
        {
            isPunching = true;
            StartCoroutine("PunchCoolDown");
        }
        else
        {
            isPunching = false;
        }
    }

    IEnumerator PunchCoolDown()
    {
        yield return new WaitForSeconds(coolDown);
        canPunch = true;
        StopCoroutine("PunchCoolDown");
    }
}
