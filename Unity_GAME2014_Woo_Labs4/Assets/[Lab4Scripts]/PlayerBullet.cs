using UnityEngine;

public class PlayerBullet : Bullet
{
	public Sprite _playerBulletSprite;
	protected override void CustomizeBullet()
	{
		// Customize player bullet here
		SetDirection(Vector3.up);
		SetSprite(_playerBulletSprite);
		BulletType = BulletType.PLAYERBULLET;
		SetSpeed(3.0f);
		SetBoundries(new Boundries { min = -6, max = 6 });
		gameObject.name = "PlayerBullet";
	}
}