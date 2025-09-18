using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;
    public float maxSpeed;

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
        if(ctx.ReadValue<float>() == 1)
        {
            portal1.transform.position = mousePos;
        }
    }
    public void PlacePortal2(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<float>() == 1)
        {
            portal2.transform.position = mousePos;
        }
    }
    public void MoveMouse(InputAction.CallbackContext ctx) {
        mousePos = Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
    }
}
