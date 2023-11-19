using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlatform(bool OnPlatform, Transform parent = null)
    {
        if (OnPlatform)
            transform.SetParent(parent);
        else if (!OnPlatform)
            transform.SetParent(parent);
    }
}
