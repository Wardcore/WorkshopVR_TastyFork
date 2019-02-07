using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandChecker : MonoBehaviour {

    public int HandValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BoxHandCode"))
        {
            CodeController.instance.OnboolCheck(true);
            CodeController.instance.OncheckHand(HandValue);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BoxHandCode"))
        {
            CodeController.instance.OnboolCheck(false);
        }
    }
}
