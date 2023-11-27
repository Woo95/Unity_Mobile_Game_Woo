using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] float speed = 0.1f;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] bool isGrounded;
    [SerializeField] Transform frontGroundPoint;
    [SerializeField] bool isGroundToStepOn;
    [SerializeField] Transform frontObstaclePoint;
    [SerializeField] bool isAnyObstacle;
    [SerializeField] LayerMask groundLayerMask;

	int hitDamage = 25;

	private void Update()
    {
        isGrounded = Physics2D.Linecast(groundCheckPoint.position, groundCheckPoint.position + Vector3.down * .95f, groundLayerMask);
        isGroundToStepOn = Physics2D.Linecast(groundCheckPoint.position, frontGroundPoint.position, groundLayerMask);
        isAnyObstacle = Physics2D.Linecast(groundCheckPoint.position, frontObstaclePoint.position, groundLayerMask);
        if (isGrounded && (!isGroundToStepOn || isAnyObstacle))
        {
            ChangeDirection();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Move() 
    {
        transform.position += Vector3.left * transform.localScale.x * speed;
    }

    void ChangeDirection()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1 , 1, 1);
    }

	public float GetHitDamageAmount()
	{
		return hitDamage;
	}

	private void OnDrawGizmos()
    {
        Debug.DrawLine(groundCheckPoint.position, groundCheckPoint.position + Vector3.down * .95f, Color.green);
        Debug.DrawLine(groundCheckPoint.position, frontGroundPoint.position, Color.blue);
        Debug.DrawLine(groundCheckPoint.position, frontObstaclePoint.position, Color.red);
    }
}
