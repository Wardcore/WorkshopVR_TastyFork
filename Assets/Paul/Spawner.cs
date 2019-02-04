#define USE_WAVES

using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	public GameObject m_spawnFX;
	public Transform m_spawnPoint;
	public Transform m_spawnerTarget;
	public Way m_spawnerPath;
    public GameObject m_LightOn;

	private int m_enemyCount = 0;
    private int m_activatedLightCount = 0;

    /*public void ActivateLight()
    {
        if (m_activatedLightCount == 0)
        {
            m_LightOn.SetActive(true);
        }
        m_activatedLightCount++;
    }*/

    /*public void DeactivateLight()
    {
        m_activatedLightCount--;
#if UNITY_EDITOR
        if( m_activatedLightCount < 0 )
        {
            Debug.LogError("Spawner.DeactivateLight() called more often than ActivateLight");
            m_activatedLightCount = 0;
        }
#endif // UNITY_EDITOR
        if ( m_activatedLightCount <= 0)
        {
            m_LightOn.SetActive(false);
        }
    }*/

#if USE_WAVES
    public Enemy Spawn( GameObject model )
	{
        // spawn
        Enemy newEnemy = null;//Level.AddEnemy( model, m_spawnPoint.position,m_spawnPoint.rotation );
		if( newEnemy != null )
		{
		#if UNITY_EDITOR
#endif // UNITY_EDITOR

			m_enemyCount++;
			newEnemy.SetTarget( m_spawnerTarget );
			newEnemy.SetPath( m_spawnerPath );
        }
		return newEnemy;
	}
#else // USE_WAVES
	public GameObject m_enemyModel;
	public float m_spawnDelay = 2;
	private float m_nextSpawnTime = 0;

	void Update()
	{
		if( (m_spawnerTarget != null) && (Time.time >= m_nextSpawnTime) )
		{
			Enemy newEnemy = Level.AddEnemy( m_enemyModel, 
			             m_spawnPoint.position,
 			             m_spawnPoint.rotation );
			if( newEnemy != null )
			{
#if UNITY_EDITOR
				newEnemy.name = "Enemy_" + m_enemyCount + " (Spawner \"" + this.name + "\")";
#endif // UNITY_EDITOR
				m_enemyCount++;
				newEnemy.SetTarget( m_spawnerTarget );
				newEnemy.SetPath( m_spawnerPath );
			}

			m_nextSpawnTime = Time.time + m_spawnDelay;
		}
	}
#endif // USE_WAVES

	/*void On_LivingObjectDestroyed( Transform destroyedObject )
	{
		if( destroyedObject == m_spawnerTarget )
		{
			m_spawnerTarget = null;
		}
	}*/

	void OnDrawGizmos()
	{
		if( m_spawnPoint != null )
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere( m_spawnPoint.position, 0.2f );
		}
	}
}














