using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    Rigidbody2D _rb2D;
    [SerializeField] float _accelerator = 5.0f;
    [SerializeField] float _maxSpeed = 5.0f;
    [SerializeField] float _jumpPower = 5.0f;
    [SerializeField] Transform _groundPoint;
    [SerializeField] float _airborneMultiplier = 3.0f;

    float _moveDirection;
    Joystick leftJoystick;
    [SerializeField][Range(0,1)] float threshold;

    // Start is called before the first frame update
    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        if (GameObject.Find("LeftController"))
        {
            leftJoystick = GameObject.Find("LeftController").GetComponent<Joystick>();
        }      
    }

    private void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
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

        if (_moveDirection == -1)
            transform.eulerAngles = new Vector3(0, 180.0f, 0);
        else if (_moveDirection == 1)
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

		if ((IsGrounded() && Input.GetKeyDown(KeyCode.Space)) || leftJoystickVerticalInput > threshold)
		{
			_rb2D.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
		}
	}

	private bool IsGrounded()
    {
        return Physics2D.CircleCast(_groundPoint.position, 0.1f, Vector2.down, .1f, LayerMask.GetMask("Ground"));
    }
}
