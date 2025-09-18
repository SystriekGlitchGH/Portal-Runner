using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;

    private Rigidbody2D rb2d;

    private float _movement;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _Srenderer;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(rb2d.position, UnityEngine.Vector2.down * 1.2f, Color.red);
        rb2d.linearVelocityX = _movement;
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        _movement = ctx.ReadValue<UnityEngine.Vector2>().x * moveSpeed;
        if (_movement != 0)
        {
            _animator.SetBool("IsRunning", true);
            if (_movement < 0)
            {
                _Srenderer.flipX = true;
            }
            if (_movement > 0)
            {
                _Srenderer.flipX = false;
            }
        }
        else
        {
            _animator.SetBool("IsRunning", false);
        }
    }

    private bool GetIsGrounded()
    {
        return Physics2D.Raycast(rb2d.position, UnityEngine.Vector2.down, 1.2f, LayerMask.GetMask("Ground"));
    }
    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<float>() == 1 && GetIsGrounded())
        { // if proper key is pressed and you are on the ground, then jump
            rb2d.linearVelocityY = jumpHeight;
        }
    }
}
