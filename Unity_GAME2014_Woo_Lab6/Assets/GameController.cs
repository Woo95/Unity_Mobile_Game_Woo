using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    GameObject onScreenController;
    [SerializeField] bool isTesting;
    // Start is called before the first frame update
    void Start()
    {
        if (!isTesting)
        {
            onScreenController = GameObject.Find("LeftController");
            onScreenController.SetActive(Application.platform == RuntimePlatform.WindowsEditor &&
                                         Application.platform == RuntimePlatform.WindowsPlayer);
        }       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
