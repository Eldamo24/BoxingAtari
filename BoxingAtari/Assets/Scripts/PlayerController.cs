using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    private Rigidbody2D rbPlayer;
    private PlayerInput playerInput;
    private Transform playerPosition;
    private Vector2 input;
    private float forceMovement = 3f;

    [Header("CheckFlip")]
    private Transform enemyPosition;
    private SpriteRenderer playerRenderer;

    [Header("Punch")]
    private bool isPunching = false;
    private float offset = 0.8f;
    private float coolDown = 3f;
    private bool canPunch = true;

    [Header("Animations")]
    private Animator animPlayer;

    [Header("Points")]
    private int _playerPoints = 0;
    public int PlayerPoints { get => _playerPoints; set => _playerPoints = value; }

    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerPosition = GetComponent<Transform>();
        enemyPosition = GameObject.Find("Enemy").GetComponent<Transform>();
        playerRenderer = GetComponentInChildren<SpriteRenderer>();
        animPlayer = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfCanPunch();
        if (!isPunching)
        {
            Movement();
            CheckFlip();
        }
    }

    private void FixedUpdate()
    {
        if(!isPunching)
            rbPlayer.MovePosition(playerPosition.position + new Vector3(input.x, input.y, 0f) * Time.deltaTime * forceMovement);
    }

    private void Movement()
    {
        input = playerInput.actions["Movement"].ReadValue<Vector2>();
    }

    public void Punch(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            if (canPunch)
            {
                canPunch = false;
                if (playerPosition.position.y - offset >= enemyPosition.position.y)
                {
                    animPlayer.SetTrigger("IsPunchingRight");
                }
                else
                {
                    animPlayer.SetTrigger("IsPunchingLeft");
                }
            }
        }
    }

    private void CheckFlip()
    {
        if(playerPosition.position.x >= enemyPosition.position.x)
        {
            playerRenderer.flipX = true;
        }
        else
        {
            playerRenderer.flipX = false;
        }
    }

    private void CheckIfCanPunch()
    {
        if (animPlayer.GetCurrentAnimatorStateInfo(0).IsName("PunchRight") || animPlayer.GetCurrentAnimatorStateInfo(0).IsName("PunchLeft"))
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
