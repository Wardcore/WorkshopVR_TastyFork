using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Scissors : MonoBehaviour {

    public VRTK_InteractableObject linkedObject;
	private Animator m_anim;

	protected virtual void OnEnable()
	{
		linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);
		m_anim = GetComponent<Animator>();

		if (linkedObject != null)
		{
			linkedObject.InteractableObjectUsed += InteractableObjectUsed;
            linkedObject.InteractableObjectUnused += InteractableObjectUnused;
		}
	}

	protected virtual void OnDisable()
	{
		if (linkedObject != null)
		{
			linkedObject.InteractableObjectUsed -= InteractableObjectUsed;
		}
	}

	protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
	{
		m_anim.SetBool("CutBool", true);
	}

	protected virtual void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
    {
		m_anim.SetBool("CutBool", false);
    }

	/*void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Cutable"){
			print("cutable");
			other.gameObject.GetComponent<RopePart>().BreakLink();
		}
	}*/
}
