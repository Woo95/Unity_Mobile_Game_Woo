using System.Collections;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Animator m_Animator;
	public Transform m_HitAreaA, m_HitAreaB;

	public LayerMask m_HitLayerMask;


	void Start()
    {
        StartCoroutine("Co_Attack");
	}

    IEnumerator Co_Attack()
    {
        while (true)
        {
            float randTime = Random.Range(3f, 7f);
            yield return new WaitForSeconds(randTime);
            m_Animator.SetTrigger("Attack");
        }
    }

	public void Animator_KillPlayer()
	{
		Collider2D killPlayer = Physics2D.OverlapArea(m_HitAreaA.position, m_HitAreaB.position, m_HitLayerMask);

		if (killPlayer != null)
		{
			PlayerManager.instance.LoseLife();
		}
	}

	#region Gizmo Drawing
	private void OnDrawGizmos()
	{
		if (m_HitAreaA && m_HitAreaB)
		{
			Vector3 p00 = m_HitAreaA.position;
			Vector3 p11 = m_HitAreaB.position;
			Vector3 p10 = new Vector3(p11.x, p00.y, p00.z);
			Vector3 p01 = new Vector3(p00.x, p11.y, p00.z);

			Gizmos.color = Color.red;
			Gizmos.DrawLine(p00, p10);
			Gizmos.DrawLine(p10, p11);
			Gizmos.DrawLine(p11, p01);
			Gizmos.DrawLine(p01, p00);
		}
	}
	#endregion
}
