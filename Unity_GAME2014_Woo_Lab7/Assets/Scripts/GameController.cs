using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	GameObject onScreenController;
    [SerializeField] bool isTesting;
    // Start is called before the first frame update
    public void Start()
    {
        //if (!isTesting)
        //{
        //    onScreenController = GameObject.Find("LeftController");
        //    onScreenController.SetActive(Application.platform == RuntimePlatform.WindowsEditor &&
        //                                 Application.platform == RuntimePlatform.WindowsPlayer);
        //}       
    }

    public void LoadGameScene()
    {
		SceneManager.LoadScene("Game");
	}
}
