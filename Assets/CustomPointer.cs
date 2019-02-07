using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class CustomPointer : VRTK_Pointer 
{
    [Header("CUSTOM")]
    public float m_timerMaxTime;

    public float m_timer = 0;

    public bool canTeleport = false;
    public bool pointerActivated = false;

    private VRTK_ControllerReference m_controller;
    protected override void Update()
    {
        base.Update();

        if((controllerEvents.GetTouchpadAxis().y >= 0.2f || controllerEvents.GetTouchpadAxis().y <= -0.2f) && pointerActivated && !canTeleport){
            m_timer += Time.deltaTime;
            if(!pointerActivated)
                CustomActivatePointer();
            //print("timer");
        }
        else if (m_timer != 0 && controllerEvents.GetTouchpadAxis().y < 0.2f && controllerEvents.GetTouchpadAxis().x < 0.2f && controllerEvents.GetTouchpadAxis().y > -0.2f && controllerEvents.GetTouchpadAxis().x > -0.2f){
            m_timer = 0;
            canTeleport = false;
            if(pointerActivated){
                pointerActivated = false;
                CustomDeactivatePointer();
            }
            //print("Reset");
        }

        
        if(pointerActivated && m_timer > m_timerMaxTime && !canTeleport){
            m_timer = m_timerMaxTime;
            canTeleport = true;
            //print("timer end");
        }
    }


    protected override void SubscribeActivationButton()
    {
        controllerEvents.SubscribeToAxisAliasEvent(SDK_BaseController.ButtonTypes.Touchpad, VRTK_ControllerEvents.AxisType.Axis, AxisTest);
    }

    protected override void UnsubscribeActivationButton()
    {
        controllerEvents.UnsubscribeToAxisAliasEvent(SDK_BaseController.ButtonTypes.Touchpad, VRTK_ControllerEvents.AxisType.Axis, AxisTest);
    }

    //
    private void AxisTest(object sender, ControllerInteractionEventArgs e){
        m_controller = e.controllerReference;
        if(controllerEvents.GetTouchpadAxis().y >= 0.2f || controllerEvents.GetTouchpadAxis().y <= -0.2f){
            pointerActivated = true;
            CustomActivatePointer();
        }
        else if(controllerEvents.GetTouchpadAxis().y < 0.2f && controllerEvents.GetTouchpadAxis().x < 0.2f && controllerEvents.GetTouchpadAxis().y > -0.2f && controllerEvents.GetTouchpadAxis().x > -0.2f){
            CustomDeactivatePointer();
            pointerActivated = false;
        }
    }


    //
    private void CustomActivatePointer(){
        controllerReference = m_controller;
        OnActivationButtonPressed(controllerEvents.SetControllerEvent(ref activationButtonPressed, true));
        PointerActivated();
    }

    private void CustomDeactivatePointer(){
        controllerReference = m_controller;
        PointerDeactivated();
        OnActivationButtonReleased(controllerEvents.SetControllerEvent(ref activationButtonPressed, false));
    }
}
