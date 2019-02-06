using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.GrabAttachMechanics;
using VRTK.Controllables.PhysicsBased;

public class CustomPhysicsSlider : VRTK_PhysicsSlider {

    public float m_breakForce;
    public float m_force;
    public bool isBroken = false;


    protected override void SetupJoint(){
        base.SetupJoint();
        controlJoint.breakForce = m_breakForce;
    }

    protected override void Update()
    {
        base.Update();
        if(controlRigidbody != null){
            m_force = controlRigidbody.velocity.magnitude;
        }
    }
    protected override void OnEnable()
    {
        atMinLimit = false;
        atMaxLimit = false;
        SetupCollider();
        processAtEndOfFrame = StartCoroutine(ProcessAtEndOfFrame());
        SetupRigidbody();
        SetupRigidbodyActivator();
        if(isBroken){
            SetupInteractableObject();
            SetupJoint();
            previousLocalPosition = Vector3.one * float.MaxValue;
            previousPositionTarget = float.MaxValue;
            stillResting = false;

            SetValue(storedValue);
        }
        
    }

    protected override void OnDisable(){
        storedValue = GetValue();
        ManageInteractableObjectListeners(false);
        if (createControlJoint)
        {
            Destroy(controlJoint);
        }
    }

    protected override void SetupGrabMechanic(){
        VRTK_ChildOfControllerGrabAttach CUSTOMcontrolGrabAttach = controlInteractableObject.gameObject.AddComponent<VRTK_ChildOfControllerGrabAttach>();
        CUSTOMcontrolGrabAttach.precisionGrab = precisionGrab;
        controlInteractableObject.grabAttachMechanicScript = controlGrabAttach;
    }

    void OnJointBreak(float breakForce)
    {
        isBroken= true;
        Debug.Log("A joint has just been broken!, force: " + breakForce);
    }
}

