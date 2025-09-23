using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlatformerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;
    public float maxSpeed;
    public static bool hasOrb;

    public Portal portal1;
    public Portal portal2;
    public Transform player;

    private Rigidbody2D rb2d;

    private float _movement;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _Srenderer;

    private Vector2 mousePos;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Room-1" || sceneName == "Room0" || sceneName == "Room1")
        {
            hasOrb = false;
        }
        else
        {
            hasOrb = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(rb2d.position, UnityEngine.Vector2.down * 1.2f, Color.red);
        rb2d.linearVelocityX = _movement;
        if(rb2d.linearVelocityY > maxSpeed)
        {
            rb2d.linearVelocityY = maxSpeed;
        }
        if(rb2d.linearVelocityY < -maxSpeed)
        {
            rb2d.linearVelocityY = -maxSpeed;
        }
        if (hasOrb == true)
        {
            _animator.SetBool("HasPortal", true);
        }
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
    public void PlacePortal1(InputAction.CallbackContext ctx)
    {
        float distance = Vector2.Distance(mousePos, player.transform.position);
        if (ctx.ReadValue<float>() == 1 && distance <= 5 && hasOrb)
        {
            portal1.transform.position = mousePos;
        }
    }
    public void PlacePortal2(InputAction.CallbackContext ctx)
    {
        float distance = Vector2.Distance(mousePos, player.transform.position);
        if (ctx.ReadValue<float>() == 1 && distance <= 5 && hasOrb)
        {
            portal2.transform.position = mousePos;
        }
    }
    public void MoveMouse(InputAction.CallbackContext ctx) {
        mousePos = Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
    }
}
