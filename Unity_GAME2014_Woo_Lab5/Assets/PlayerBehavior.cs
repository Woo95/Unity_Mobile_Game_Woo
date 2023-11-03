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
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Movement
        Move();
       //Jump

    }

    private void Move()
    {
        float xMovementDirection = Input.GetAxisRaw("Horizontal"); // Get the direction of the movement

        Vector2 force = xMovementDirection * Vector2.right * _accelerator;

        force = Vector2.ClampMagnitude(force, _maxSpeed);

        if(xMovementDirection == -1)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (xMovementDirection == 1)
        {
            transform.eulerAngles = Vector3.zero;
        }

        _rigidbody.AddForce(force);
    }

}
