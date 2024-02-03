using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    private float m_Speed = 5.0f;

    void Update()
    {
        transform.Translate(-Vector3.forward * m_Speed * Time.deltaTime);
    }
}
