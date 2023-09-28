using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    public GameObject m_Instruction;

    void Start()
    {
        if (m_Instruction.activeInHierarchy)
			m_Instruction.SetActive(false);
    }

    public void Invoke_Play()
    {
        SceneManager.LoadScene("PlayScene", LoadSceneMode.Single);
    }

    public void Invoke_Instruction()
    {
		if (m_Instruction.activeInHierarchy)
            m_Instruction.SetActive(false);

		else if (!m_Instruction.activeInHierarchy)
			m_Instruction.SetActive(true);
	}
}
