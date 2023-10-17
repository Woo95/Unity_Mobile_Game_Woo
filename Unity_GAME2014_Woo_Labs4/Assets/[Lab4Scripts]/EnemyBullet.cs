using UnityEngine;

public class EnemyBullet : Bullet
{
	public Sprite _enemyBulletSprite;
	protected override void CustomizeBullet()
	{
		// Customize player bullet here
		SetDirection(Vector3.down);
		SetSprite(_enemyBulletSprite);
		BulletType = BulletType.ENEMYBULLET;
		SetSpeed(3.0f);
		SetBoundries(new Boundries { min = -6, max = 6 });
		gameObject.name = "EnemyBullet";

		Instantiate(gameObject);
	}
}