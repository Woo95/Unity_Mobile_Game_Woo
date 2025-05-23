using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
	protected Vector3 _direction;
	protected BulletType _type;
	protected float _speed;
	protected Boundries _boundaries;

	BulletManager _manager;
	void Start()
	{
		_manager = FindAnyObjectByType<BulletManager>();
	}

	public void Activate()
	{
		gameObject.SetActive(true);
		CustomizeBullet();
	}

	public void Deactivate()
	{
		gameObject.SetActive(false);
	}

	public void SetDirection(Vector3 dir)
	{
		_direction = dir;
	}

	public BulletType BulletType
	{
		get { return _type; }
		set { _type = value; }
	}

	public void SetSpeed(float speed)
	{
		_speed = speed;
	}

	public void SetBoundries(Boundries boundaries)
	{
		_boundaries = boundaries;
	}

	protected abstract void CustomizeBullet();

	void Update()
	{
		transform.position += _direction * _speed * Time.deltaTime;

		if (transform.position.y > _boundaries.max || transform.position.y < _boundaries.min)
			_manager.ReturnBullet(gameObject);
	}
}