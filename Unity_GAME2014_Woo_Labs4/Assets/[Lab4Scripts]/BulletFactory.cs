using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
	public PlayerBullet _PlayerBulletPrefab;
	public EnemyBullet _EnemyBulletPrefab;
	public Bullet CreateBullet(BulletType type)
	{
		Bullet bullet = null;

		switch (type)
		{
			case BulletType.PLAYERBULLET:
				bullet = Instantiate(_PlayerBulletPrefab);
				break;
			case BulletType.ENEMYBULLET:
				bullet = Instantiate(_EnemyBulletPrefab);
				break;
		}

		if (bullet != null)
		{
			bullet.Activate();
		}

		return bullet;
	}
}