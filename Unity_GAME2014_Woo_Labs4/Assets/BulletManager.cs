using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    private int _playerBulletTotal = 10;

    [SerializeField]
    private int _enemyBulletTotal = 50;

    Queue<GameObject> _playerBulletPool = new Queue<GameObject>();
    Queue<GameObject> _enemyBulletPool = new Queue<GameObject>();

    BulletFactory _factory;

    GameObject _bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        _bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        _factory = FindAnyObjectByType<BulletFactory>();

        PoolBuilder();
    }

    void PoolBuilder()
    {
        // take how many bullet we want
        // Create that amount of bullet

        for (int i = 0; i < _playerBulletTotal; i++)
        {
            //Create a bullet
            GameObject bullet = _factory.CreateBullet(BulletType.PLAYERBULLET);
            _playerBulletPool.Enqueue(bullet);
        }
        for (int i = 0; i < _enemyBulletTotal; i++)
        {
            //Create an enemy bullet and add to enemy bullet queue
            GameObject bullet = _factory.CreateBullet(BulletType.ENEMYBULLET);
            _enemyBulletPool.Enqueue(bullet);
        }



    }

/*    void CreateBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab);
        bullet.transform.parent = transform;
        bullet.SetActive(false);
        _bulletPool.Enqueue(bullet);

    }*/

    public GameObject GetBullet(BulletType type)
    {
        GameObject bullet;
        switch (type)
        {
            case BulletType.PLAYERBULLET:
                //give player bullet
                if(_playerBulletPool.Count <= 1)
                {
                    _playerBulletPool.Enqueue(_factory.CreateBullet(BulletType.PLAYERBULLET));
                }

                bullet = _playerBulletPool.Dequeue();

                break;
            case BulletType.ENEMYBULLET:
                //give enemy bullet
                if(_enemyBulletPool.Count <= 1)
                {
                    _playerBulletPool.Enqueue(_factory.CreateBullet(BulletType.ENEMYBULLET));
                }

                bullet = _enemyBulletPool.Dequeue();
                break;
            default:
                Debug.LogError("Bullet type that asking from pool doesn't exist");
                return null;
                break;
        }

        bullet.SetActive(true);
        return bullet;


        /*        bullet.transform.localEulerAngles = rotation;

                bullet.tag = tagName;

                bullet.transform.position = pos;
                bullet.GetComponent<BulletBehavior>().SetDirection(dir);
                bullet.GetComponent<SpriteRenderer>().color = col;*/


    }

    public void ReturnBullet(GameObject bullet)
    {
       

        switch(bullet.GetComponent<BulletBehavior>()._type)
        {
            case BulletType.PLAYERBULLET:
                _playerBulletPool.Enqueue(bullet);
                break;
            case BulletType.ENEMYBULLET:
                _enemyBulletPool.Enqueue(bullet);
                break;
            default:
                Debug.Log("The type of bullet you want to return is not exist");
                break;
        }

        bullet.SetActive(false);
    }
}
