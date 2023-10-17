using UnityEngine;

public class PlayerBullet : Bullet
{
	protected override void CustomizeBullet()
	{
		// Customize player bullet here
		SetDirection(Vector3.up);
		BulletType = BulletType.PLAYERBULLET;
		SetSpeed(3.0f);
		SetBoundries(new Boundries { min = -6, max = 6 });
		gameObject.name = "PlayerBullet";
	}
}