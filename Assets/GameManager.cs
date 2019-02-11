using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class GameManager : MonoBehaviour {

	[Header("Enigme \"Tuto\" ")]
	public GameObject m_door;
	public GameObject m_fork;

	[Header("Enigme \"Cigar\" ")]
	public Lightable m_cigar;
	public VRTK_SnapDropZone m_snapCigar;
	private bool m_isCigarSnapped = false;

	[Header("OTHER")]
	public MeshRenderer m_led_cigar;
	public MeshRenderer m_led_fly;
	public MeshRenderer m_led_hand;
	public Color m_ledON;
	public Color m_ledOFF;


	// Use this for initialization
	void Start () 
	{
		m_snapCigar.ObjectSnappedToDropZone += DetectSnapCigar;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//CIGAR
		if(m_isCigarSnapped && m_cigar.isLighted && m_led_cigar.material.color != m_ledON){
			m_led_cigar.material.color = m_ledON;
		}
		else if(!m_isCigarSnapped || !m_cigar.isLighted){
			if(m_led_cigar.material.color != m_ledOFF)
				m_led_cigar.material.color = m_ledOFF;
		}
	}

	public void ButtonAction(){
		m_door.SetActive(false);
		m_fork.SetActive(false);
	}

	private void DetectSnapCigar(object sender, SnapDropZoneEventArgs e){
		m_isCigarSnapped = true;
	}
	
	private void DetectUnsnapCigar(object sender, SnapDropZoneEventArgs e){
		m_isCigarSnapped = false;
	}
}
