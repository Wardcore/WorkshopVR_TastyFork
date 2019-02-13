using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class FlyController : MonoBehaviour {

    public Transform rotatePoint;

    [Range(45,65)]
    public float speed;

    private AudioSource BruitDeMouche;

    private int randomTime;
    private float randomSpeed;
    private int randomPoint;

    private bool vivante;
    private Rigidbody rb;
    private VRTK_InteractableObject m_interact;

    private void Start()
    {
        vivante = true;
        rb = GetComponent<Rigidbody>();
        BruitDeMouche = GetComponent<AudioSource>();
        m_interact = GetComponent<VRTK_InteractableObject>();
        m_interact.enabled = false;
        StartCoroutine(SpeedChange());
    }


    void Update () {

        if (vivante)
        {
            Rotate();
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
        //Vector3 move = new Vector3(0,0,transform.position.z) + new Vector3(transform.position.x, 0, 0);
        //Debug.Log(move);
        transform.RotateAround(rotatePoint.position, Vector3.up, speed * Time.deltaTime);
        //transform.localRotation = Quaternion.LookRotation(move);
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
            m_interact.enabled = true;
            vivante = false;
            rb.isKinematic = false;
            rb.useGravity = true;
            if(BruitDeMouche)
                BruitDeMouche.Stop();
            Destroy(this);
        }
    }
}
