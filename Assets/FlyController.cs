using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyController : MonoBehaviour {

    public Transform rotatePoint;

    [Range(45,65)]
    public float speed;

    private int randomTime;
    private float randomSpeed;
    private int randomPoint;

    private bool vivante;
    private Rigidbody rb;

    private void Start()
    {
        vivante = true;
        rb = GetComponent<Rigidbody>();
        StartCoroutine(SpeedChange());
    }


    void Update () {

        if (vivante)
        {
            Rotate();
        }
        else
        {
            Kill();
        }
    }

    IEnumerator SpeedChange()
    {
        randomTime = Random.Range(2, 5);
        randomSpeed = Random.Range(45, 65);
        yield return new WaitForSeconds(randomTime);
        speed = randomSpeed;
        StartCoroutine(SpeedChange());
    }

    void Rotate()
    {
        Vector3 move = new Vector3(0,0,transform.position.z) + new Vector3(transform.position.x, 0, 0);
        Debug.Log(move);
        transform.RotateAround(rotatePoint.position, Vector3.up, speed * Time.deltaTime);
        transform.localRotation = Quaternion.LookRotation(move);
    }

    void Kill()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(rotatePoint.position, 0.25f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tapette"))
        {
            vivante = false;
        }
    }
}
