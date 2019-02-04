using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawbox : MonoBehaviour {

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }
}
