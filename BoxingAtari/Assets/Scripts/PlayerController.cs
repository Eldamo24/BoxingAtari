using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rbPlayer;
    private PlayerInput playerInput;
    private Vector2 input;

    private float forceMovement = 3f;
    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void FixedUpdate()
    {
        rbPlayer.MovePosition(transform.position + new Vector3(input.x, input.y, 0f) * Time.deltaTime * forceMovement);
    }

    private void Movement()
    {
        input = playerInput.actions["Movement"].ReadValue<Vector2>();
    }

    public void Punch(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            Debug.Log("Punch");
        }
    }
}
