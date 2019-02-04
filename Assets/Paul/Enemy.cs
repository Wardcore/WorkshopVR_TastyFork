using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[AddComponentMenu( "TowerDefense/Enemy" )]
public class Enemy : MovingObject
{
	public int m_damages = 1;
    

	private Way m_path;
	private int m_currentNode = -1;
	private Transform m_finalTarget;

	public void SetPath(Way path )
	{
		m_path = path;
		m_currentNode = -1;
		m_finalTarget = m_target;
		ChooseNextTarget();
	}

	void ChooseNextTarget()
	{
		m_currentNode++;
		if( m_currentNode < m_path.m_nodes.Length )
		{
			SetTarget( m_path.m_nodes[m_currentNode].transform );
		}
		else
		{
			SetTarget( m_finalTarget );
		}
	}

	protected override void Start() 
	{
		base.Start();
	}

	protected override void On_TargetReached()
	{
		ChooseNextTarget();
	}

	protected override void On_LivingObjectDestroyed( Transform destroyedObject )
	{
		if( (destroyedObject == m_target) || (destroyedObject == m_finalTarget))
		{
			m_target = null;
		}
	}
}









