using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public enum HandConfigs{
	Rest = 0,
	Point,
	FingerGun,
	ThumbUp,
	Fist,
	OKHand
}
public class InputManager : MonoBehaviour {

	public VRTK_ControllerEvents m_rightEvent;
	public VRTK_ControllerEvents m_leftEvent;

	public HandConfigs m_leftConfig;
	public HandConfigs m_rightConfig;


	// Update is called once per frame
	void Update () 
	{
		m_leftConfig = SetConfig(m_leftEvent);
		m_rightConfig = SetConfig(m_rightEvent);
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
			if(configs != HandConfigs.Point){
				configs = HandConfigs.Point;
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

		return configs;
	}
}
