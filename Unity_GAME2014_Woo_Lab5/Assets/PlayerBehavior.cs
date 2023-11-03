using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    Rigidbody2D _rigidbody;

    [SerializeField]
    float _accelerator = 100;
    [SerializeField]
    float _maxSpeed = 5;
    // Start is called before the first frame update

    [SerializeField]
    Transform _groundPoint;

    [SerializeField]
    float _jumpingPower = 20;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        //Jump
        Jump();
    }

    private void FixedUpdate()
    {
        //Movement
        Move();
    }

    private void Move()
    {
        float xMovementDirection = Input.GetAxisRaw("Horizontal"); // Get the direction of the movement

        Vector2 force = xMovementDirection * Vector2.right * _accelerator;

       

        if(xMovementDirection == -1)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (xMovementDirection == 1)
        {
            transform.eulerAngles = Vector3.zero;
        }

        _rigidbody.AddForce(force);

        _rigidbody.velocity =  Vector2.ClampMagnitude(_rigidbody.velocity, _maxSpeed);
    }

    private void Jump()
    {
        RaycastHit2D hit = Physics2D.CircleCast(_groundPoint.position, .1f, Vector2.down, .1f, LayerMask.GetMask("Ground"));

        if(hit && Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce(Vector2.up * _jumpingPower, ForceMode2D.Impulse);
        }
    }

}
