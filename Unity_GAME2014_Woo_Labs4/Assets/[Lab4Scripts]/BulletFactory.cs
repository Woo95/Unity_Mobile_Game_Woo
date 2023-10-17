using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletFactory : MonoBehaviour
{
	public GameObject _bulletPrefab;
	public Bullet CreateBullet(BulletType type)
	{
		Bullet bullet = null;

		switch (type)
		{
			case BulletType.PLAYERBULLET:
				bullet = new PlayerBullet();
				break;
			case BulletType.ENEMYBULLET:
				bullet = new EnemyBullet();
				break;
		}

		if (bullet != null)
		{
			bullet.Activate();
		}

		return bullet;
	}
}