using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslatePoint : MonoBehaviour {

    public Transform pointA, pointB;
    private float speed;

    private float calc = 0.5f;

    void FixedUpdate()
    {
        speed = Mathf.PingPong(Time.time*2, 1);
        transform.position = Vector3.Lerp(pointA.position, pointB.position, speed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(pointA.position, 0.25f);
        Gizmos.DrawWireSphere(pointB.position, 0.25f);
    }
}
