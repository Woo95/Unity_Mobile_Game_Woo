using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    Rigidbody2D _rb2D;
    [SerializeField] float _accelerator = 5.0f;
    [SerializeField] float _maxSpeed = 5.0f;
    [SerializeField] float _jumpPower = 5.0f;
    [SerializeField] Transform _groundPoint;
    [SerializeField] float _airborneMultiplier = 3.0f;
    [SerializeField] float jumpSpeedLimit;

    float _moveDirection;
    Joystick leftJoystick;
    [SerializeField][Range(0,1)] float threshold;
    [SerializeField] LayerMask groundLayer;
    Animator animator;
    [SerializeField]bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (GameObject.Find("LeftController"))
        {
            leftJoystick = GameObject.Find("LeftController").GetComponent<Joystick>();
        }      
    }

    private void Update()
    {
        Jump();
        if (_rb2D.velocity.y > jumpSpeedLimit)
        {
            _rb2D.velocity = new Vector2(_rb2D.velocity.x, jumpSpeedLimit);
        }
    }

    private void FixedUpdate()
    {
        Move();

        if (!IsGrounded())
        {
            if (_rb2D.velocity.y > 0)
            {
                animator.SetInteger("State", (int)AnimationState.JUMP);
            }
            else 
            {
                animator.SetInteger("State", (int)AnimationState.FALL);
            }
        }
        else
        {
            if (Mathf.Abs(_rb2D.velocity.x) > 0)
            {
                animator.SetInteger("State", (int)AnimationState.WALK);
            }
            else
            {
                animator.SetInteger("State", (int)AnimationState.IDLE);
            }
           
        }
    }

    private void Move()
    {
        float leftJoystickHorizontalInput = 0;

        if (leftJoystick != null)
        {
            leftJoystickHorizontalInput = leftJoystick.Horizontal;
        }

        _moveDirection = Input.GetAxisRaw("Horizontal") + leftJoystickHorizontalInput;
        float applibleAcceleration = _accelerator;

        if (!IsGrounded())
        {
            applibleAcceleration *= _airborneMultiplier;
        }

        Vector2 force = _moveDirection * Vector2.right * applibleAcceleration;


        force = Vector2.ClampMagnitude(force, _maxSpeed);

        if (_moveDirection < 0)
            transform.eulerAngles = new Vector3(0, 180.0f, 0);
        else if (_moveDirection > 0)
            transform.eulerAngles = Vector3.zero;

        _rb2D.AddForce(force);
    }

    private void Jump()
    {
        float leftJoystickVerticalInput = 0;
        if (leftJoystick != null)
        {
            leftJoystickVerticalInput = leftJoystick.Vertical;
        }

        if (IsGrounded() && (Input.GetKeyDown(KeyCode.Space) || leftJoystickVerticalInput > threshold))
        {
            _rb2D.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        }
    }

    private bool IsGrounded()
    {
        isGrounded = Physics2D.CircleCast(_groundPoint.position, 0.1f, Vector2.down, .1f, LayerMask.GetMask("Ground"));
        return isGrounded;
    }
}
