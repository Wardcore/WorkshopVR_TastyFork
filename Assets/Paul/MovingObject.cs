using UnityEngine;
using System.Collections;

[RequireComponent( typeof(CharacterController) )]
public class MovingObject : MonoBehaviour
{
    public float m_moveSpeed = 10.0f;
   // public float m_moveSpeed = 4.0f;
	public float m_rotationSpeed = 120.0f;
	public bool m_useGravity = true;
	public float m_gravity = -9.81f;
	public float m_jumpHeight = 6;

	protected Transform m_target = null; 

	private CharacterController m_controller;
	private float m_ySpeed = 0f;

	public void SetTarget( Transform target )
	{
		m_target = target;
	}

	void Awake()
	{
     //   m_moveSpeedIon = m_moveSpeed;

        m_controller = GetComponent<CharacterController>();
	#if UNITY_EDITOR
		if( m_controller == null )
		{
			Debug.LogError( "Enemy component require a CharacterController" );
		}
	#endif // UNITY_EDITOR
	}

	protected virtual void Start() 
	{
		//Debug.Log( "MovingObject.Start()" );
	}
	
	void Update() 
	{

		Vector3 step;

		if( m_target != null )
		{
			Vector3 targetPos = m_target.position;
			targetPos.y = transform.position.y;
			/*
			transform.LookAt( targetPos  );
			/*/
			Vector3 direction = (targetPos - transform.position).normalized;
			Quaternion targetRot = Quaternion.LookRotation( direction );
			transform.rotation = Quaternion.RotateTowards(
										transform.rotation,
										targetRot,
										m_rotationSpeed * Time.deltaTime );
			//*/

			step = transform.forward * (m_moveSpeed * Time.deltaTime);
		}
		else
		{
			step = Vector3.zero;
		}

		if( m_useGravity )
		{
			m_ySpeed += m_gravity * Time.deltaTime;
			step.y += m_ySpeed * Time.deltaTime;
		}

		m_controller.Move( step );
		if( m_controller.isGrounded )
		{
			if( (m_target == null))
			{
				m_ySpeed = m_jumpHeight;
			}
			else if( m_ySpeed <= -0.1f )
			{
				m_ySpeed = -m_ySpeed / 2;
			}
			else
			{
				m_ySpeed = 0f;
			}
		}
	}

	void OnTriggerEnter( Collider other )
	{
		if( other.transform == m_target )
		{
			On_TargetReached();
		}
	}

	protected virtual void On_TargetReached()
	{
		m_target = null;
	}

	protected virtual void On_LivingObjectDestroyed( Transform destroyedObject )
	{
		if( destroyedObject == m_target )
		{
			m_target = null;
		}
	}
}










