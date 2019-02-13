using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class GameManager : MonoBehaviour {

	[Header("Enigme \"Button start\" ")]
	public GameObject m_door;
	public GameObject m_falseFork;

	[Header("Enigme \"Bombe activation\" ")]
	public GameObject m_bomb;
	public AudioClip m_bombClip;
	public Rope m_cable;

	[Header("Enigme \"Cigare\" ")]
	public Lightable m_cigar;
	public VRTK_SnapDropZone m_snapCigar;
	private bool m_isCigarSnapped = false;
	
	[Header("Enigme \"Mouche\" ")]
	public VRTK_SnapDropZone m_snapFly;


	[Header("Enigme \"Check\" ")]
	public Animator m_handAnim;
	public CheckDetection m_hand_1;
	public CheckDetection m_hand_2;


	[Header("Enigme \"Final\" ")]
	public GameObject m_cloche;
	public VRTK_InteractableObject m_finalFork;

	[Header("LEDS")]
	public MeshRenderer m_led_cigar;
	public MeshRenderer m_led_fly;
	public MeshRenderer m_led_hand;
	public Material m_ledON;
	public Material m_ledOFF;

	private bool hasWon = false;


	// Use this for initialization
	void Start () 
	{
		//
		m_door.SetActive(true);
		m_falseFork.SetActive(true);

		//
		//m_androide.SetActive(false);
		m_cable.RopeCutEvent += ActivateBomb;

		//cigar event
		m_snapCigar.ObjectSnappedToDropZone += DetectSnapCigar;
		m_snapCigar.ObjectUnsnappedFromDropZone += DetectUnsnapCigar;
		//Fly event
		m_snapFly.ObjectSnappedToDropZone += DetectSnapFly;
		m_snapFly.ObjectUnsnappedFromDropZone += DetectUnsnapFly;
		//Check event 
		m_hand_1.OnValidCheckDetection += DetectHand_1;
		m_hand_2.OnValidCheckDetection += DetectHand_2;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//CIGAR
		if(m_isCigarSnapped && m_cigar.isLighted && m_led_cigar.material != m_ledON){
			m_led_cigar.material = m_ledON;
		}
		else if((!m_isCigarSnapped || !m_cigar.isLighted) && m_led_cigar.material != m_ledOFF){
			m_led_cigar.material = m_ledOFF;
		}

		//Final
		if(m_led_cigar.material == m_led_fly == m_led_hand == m_ledON && !hasWon){
			Win();
		}
	}


	//1 button
	public void ButtonAction(){
		m_door.SetActive(false);
		m_falseFork.SetActive(false);
	}

	//2 cable
	public void ActivateBomb(object sender){
		//m_androide.SetActive(true);
		m_bomb.GetComponent<AudioSource>().PlayOneShot(m_bombClip, 2);
		m_bomb.GetComponent<Animator>().SetTrigger("StartBomb");
	}

	//3 cigar
	private void DetectSnapCigar(object sender, SnapDropZoneEventArgs e){
		m_isCigarSnapped = true;
	}
	
	private void DetectUnsnapCigar(object sender, SnapDropZoneEventArgs e){
		m_isCigarSnapped = false;
	}

	//4 fly
	private void DetectSnapFly(object sender, SnapDropZoneEventArgs e){
		m_led_fly.material = m_ledON;
	}
	
	private void DetectUnsnapFly(object sender, SnapDropZoneEventArgs e){
		m_led_fly.material = m_ledOFF;
	}

	//5 check
	private void DetectHand_1(object sender){
		StartCoroutine("ChangeHand");
	}
	
	IEnumerator ChangeHand(){
		m_handAnim.SetTrigger("HandCheck");
		yield return new WaitForSeconds(2.30f);
		m_hand_1.gameObject.SetActive(false);
		m_hand_2.gameObject.SetActive(true);
	}

	private void DetectHand_2(object sender){
		m_hand_2.gameObject.SetActive(false);
		m_led_hand.material = m_ledON;
	}

	//6 FInal
	private void Win(){
		print("win");
		hasWon = true;
		m_cloche.SetActive(false);
	}

}
