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

	public void Invoke_PlayButton()
	{
		SceneManager.LoadScene("PlayScene", LoadSceneMode.Single);
	}

	public void Invoke_InstructionButton()
	{
		if (m_Instruction.activeInHierarchy)
			m_Instruction.SetActive(false);

		else if (!m_Instruction.activeInHierarchy)
			m_Instruction.SetActive(true);
	}

	public void Invoke_ExitButton()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
		Application.Quit();
	}
}
