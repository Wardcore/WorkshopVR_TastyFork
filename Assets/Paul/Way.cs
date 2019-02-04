using UnityEngine;
using System.Collections;

public class Way : MonoBehaviour
{
	public PathNode[] m_nodes;

	void OnDrawGizmos()
	{
		if( m_nodes.Length > 0 )
		{
			Gizmos.color = Color.magenta;
			for( int n = 0; n < m_nodes.Length-1; n++ )
			{
				if( (m_nodes[n] != null) && (m_nodes[n+1] != null) )
				{
					Gizmos.DrawLine( m_nodes[n].transform.position,
					                 m_nodes[n+1].transform.position );
				}
			}
		}
	}
}







