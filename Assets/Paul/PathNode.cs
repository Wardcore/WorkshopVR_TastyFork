using UnityEngine;
using System.Collections;

[RequireComponent( typeof(CapsuleCollider) )]
public class PathNode : MonoBehaviour
{

    void OnDrawGizmos()
	{
		Gizmos.color = Color.magenta;
		CapsuleCollider col = GetComponent<CapsuleCollider>();
		Gizmos.DrawWireSphere( transform.position, col.radius );
	}
}
