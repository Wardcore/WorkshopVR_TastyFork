using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public enum HandConfigs{
	Fist = 1,
	OKHand = 2,
	Rest = 3,
	FingerGun = 4,
	ThumbUp = 5,
	Point = 6
}

public class HandConfig : MonoBehaviour {
	

	public HandConfigs m_Config;
    public int HandValue;
	private VRTK_ControllerEvents m_Event;

	private HandConfigs m_configOld;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        m_Event = GetComponent<VRTK_ControllerEvents>();
    }


	void Update () 
	{
		m_Config = SetConfig(m_Event);
    }
	
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BoxHandCode"))
        {
			other.GetComponent<CodeController>().OnboolCheck(true);
			other.GetComponent<CodeController>().OncheckHand((int)m_Config);
        }
    }

	/// <summary>
	/// OnTriggerStay is called once per frame for every Collider other
	/// that is touching the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerStay(Collider other)
	{
		
	}

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BoxHandCode"))
        {
			other.GetComponent<CodeController>().OnboolCheck(false);
        }
    }

    private HandConfigs SetConfig(VRTK_ControllerEvents InputEvent){
		HandConfigs configs = HandConfigs.Rest;

		//REST
		if(!InputEvent.gripPressed && !InputEvent.triggerTouched && !(InputEvent.buttonOneTouched || InputEvent.buttonTwoTouched || InputEvent.touchpadTouched)){
			if(configs != HandConfigs.Rest){
				configs = HandConfigs.Rest;
				print("Rest");
			}
		}
		//POINT
		else if(InputEvent.gripPressed && !InputEvent.triggerTouched && (InputEvent.buttonOneTouched || InputEvent.buttonTwoTouched || InputEvent.touchpadTouched)){
			if(configs != HandConfigs.Point){
				configs = HandConfigs.Point;
			}
		}
		//FINGER GUN
		else if(InputEvent.gripPressed && !InputEvent.triggerTouched && !(InputEvent.buttonOneTouched || InputEvent.buttonTwoTouched || InputEvent.touchpadTouched)){
			if(configs != HandConfigs.FingerGun){
				configs = HandConfigs.FingerGun;
			}
		}
		//THUMB UP
		else if(InputEvent.gripPressed && InputEvent.triggerTouched && !(InputEvent.buttonOneTouched || InputEvent.buttonTwoTouched || InputEvent.touchpadTouched)){
			if(configs != HandConfigs.ThumbUp){
				configs = HandConfigs.ThumbUp;
			}
		}		
		//FIST
		else if(InputEvent.gripPressed && InputEvent.triggerTouched && (InputEvent.buttonOneTouched || InputEvent.buttonTwoTouched || InputEvent.touchpadTouched)){
			if(configs != HandConfigs.Fist){
				configs = HandConfigs.Fist;
			}
		}
		//OK
		else if(!InputEvent.gripPressed && InputEvent.triggerPressed && (InputEvent.buttonOneTouched || InputEvent.buttonTwoTouched || InputEvent.touchpadTouched)){
			if(configs != HandConfigs.OKHand){
				configs = HandConfigs.OKHand;
			}
		}
		HandValue = (int)configs;
		return configs;
	}
}
