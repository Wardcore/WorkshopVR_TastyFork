using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDetection : MonoBehaviour {

	public HandConfigs m_requiredConfig;

	public delegate void CheckDetectionEventHandler(object sender);
	public event CheckDetectionEventHandler OnValidCheckDetection;

	public void SetOnValidCheckDetection(){
		if(OnValidCheckDetection != null){
			OnValidCheckDetection(this);
		}
	}
}
