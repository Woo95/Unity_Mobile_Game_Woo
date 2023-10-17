using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    Vector2 _speedRange;

    float _verticalSpeed;
    float _horizontalSpeed;

    [SerializeField]
    Boundries _verticalBoundries;
    [SerializeField]
    Boundries _horizontalBoundries;

    Vector3 _defaultSize;

    bool _isDying = false;
    float _dyingCounter = 0;
    float _timer = 2;

    GameObject _bullet;

    int count = 0;

    BulletManager _bulletManager;
    GameController _gameController;

    [SerializeField]
    Transform _bulletPoint;

    int _heath = 5;

    // Start is called before the first frame update
    void Start()
    {
        _bulletManager = FindAnyObjectByType<BulletManager>();
        _gameController = FindAnyObjectByType<GameController>();
        _defaultSize = transform.localScale;
        Reset();
        
    }

    // Update is called once per frame
    void Update()
    {
        _bullet = Resources.Load<GameObject>("Prefabs/Bullet");
       

        transform.position = new Vector2( Mathf.PingPong(_horizontalSpeed * Time.time,_horizontalBoundries.max - _horizontalBoundries.min) + _horizontalBoundries.min
            , transform.position.y - _verticalSpeed * Time.deltaTime);

        //Spawn / Reset
        if(_verticalBoundries.min > transform.position.y)
        {
            Reset();
        }

        if(_isDying)
        {
            _dyingCounter += Time.deltaTime;

            Vector3 newScale = transform.localScale;

            transform.localScale = new Vector3(newScale.x - Time.deltaTime, newScale.y - Time.deltaTime, newScale.z - Time.deltaTime);

            transform.Rotate(0, 0, Random.Range(-1, 1));

            GetComponent<SpriteRenderer>().color = Color.red;

            if (_dyingCounter > _timer)
                Destroy(gameObject);

        }

    }

    private void FixedUpdate()
    {
        if (count > 10)
        {
            GameObject bullet = _bulletManager.GetBullet(BulletType.ENEMYBULLET);
            bullet.transform.position = _bulletPoint.position;
            
            count = 0;
        }

        count++;
    }


    void Reset()
    {
        Color[] _colorList = { Color.blue, Color.green, Color.red, Color.yellow, Color.white, Color.cyan, Color.magenta };

        Color randomColor = _colorList[Random.Range(0, _colorList.Length - 1)];

        GetComponent<SpriteRenderer>().color = randomColor;

        _verticalSpeed = Random.Range(_speedRange.x, _speedRange.y);
        _horizontalSpeed = Random.Range(_speedRange.x, _speedRange.y);
        transform.position = new Vector2(Random.Range(_horizontalBoundries.min, _horizontalBoundries.max), _verticalBoundries.max);

        transform.localScale = new Vector3(_defaultSize.x + Random.Range(-.6f, .6f), _defaultSize.y + Random.Range(-.6f, .6f), _defaultSize.z + Random.Range(-.6f, .6f));
    }

    public void DyingSequence()
    {
        _isDying = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            if(collision.GetComponent<BulletBehavior>()._type == BulletType.PLAYERBULLET)
            {
                Debug.Log("Enemy got hit");
                _gameController.ChangeScore(7);

                _bulletManager.ReturnBullet(collision.gameObject);

                _heath--;
                if (_heath < 0)
                {
                    DyingSequence();
                }
            }
          
        }
    }
}
