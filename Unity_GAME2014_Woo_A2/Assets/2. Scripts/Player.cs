using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float m_Speed = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//float hInput = Input.GetAxisRaw("Horizontal");

  //      Vector3 InputVector = new Vector3(hInput,0,0);

		//transform.position += InputVector * m_Speed * Time.deltaTime;
	}

    public void MoveLeftButton()
    {
		transform.position += Vector3.left * m_Speed * Time.deltaTime;
	}
    public void MoveRightButton()
    {
		transform.position += Vector3.right * m_Speed * Time.deltaTime;
	}
    public void JumpButton()
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
