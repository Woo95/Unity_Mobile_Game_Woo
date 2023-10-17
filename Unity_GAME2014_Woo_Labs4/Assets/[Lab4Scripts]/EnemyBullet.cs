using UnityEngine;

public class EnemyBullet : Bullet
{
	protected override void CustomizeBullet()
	{
		SetDirection(Vector3.down);
		BulletType = BulletType.ENEMYBULLET;
		SetSpeed(3.0f);
		SetBoundries(new Boundries { min = -6, max = 6 });
		gameObject.name = "EnemyBullet";
	}
}