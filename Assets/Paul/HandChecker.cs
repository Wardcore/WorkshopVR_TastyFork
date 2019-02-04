using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandChecker : MonoBehaviour {

    public int HandValue;
    private GameObject boxhand;
    private void Start()
    {
        boxhand = GameObject.FindGameObjectWithTag("BoxHandCode");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BoxHandCode"))
        {
            boxhand.SendMessage("OncheckHand",HandValue, SendMessageOptions.DontRequireReceiver);
        }
    }
}
