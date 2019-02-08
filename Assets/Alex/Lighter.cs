using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Lighter : MonoBehaviour {

    public VRTK_InteractableObject linkedObject;
	public GameObject m_flame;

	protected virtual void OnEnable()
	{
		m_flame.SetActive(false);
		linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

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
		m_flame.SetActive(true);
	}

	protected virtual void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
    {
		//m_flame.SetActive(false);
    }
}
