using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField]
    private float _scrollingSpeed = 3;

    [SerializeField]
    private Boundries _boundries;

    private Vector3 _spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - _scrollingSpeed * Time.deltaTime,
            transform.position.z); 

        if(transform.position.y < _boundries.min)
        {
            transform.position = new Vector3(transform.position.x, _boundries.max, transform.position.z); //_spawnPoint;
        }
    }
}
