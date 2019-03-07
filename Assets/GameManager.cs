using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Controllables;
using VRTK.Controllables.ArtificialBased;
public class GameManager : MonoBehaviour {

	[Header("Enigme \"Button start\" ")]
	public Animator m_BeginningAnim;


	[Header("Enigme \"Bombe activation\" ")]
	public GameObject m_bomb;
	public AudioClip m_bombClip;
	public Rope m_cable;


	[Header("Enigme \"Cigare\" ")]
	public VRTK_SnapDropZone m_snapPhoto;
	public Animator m_cadreAnim_Cigar;
	public Lightable m_cigar;
	public VRTK_SnapDropZone m_snapCigar;
	private bool m_isCigarSnapped = false;
	public bool m_cigarOK = false;

	
	[Header("Enigme \"Mouche\" ")]
	public VRTK_ArtificialRotator m_LampRotator;
	public GameObject m_Paper;
	public Animator m_cadreAnim_Fly;
	public VRTK_SnapDropZone m_snapFly;
	public bool m_flyOK = false;


	[Header("Enigme \"Check\" ")]
	public Rope m_LightCable;
	public Light m_checkLight;
	public Animator m_handAnim;
	public CheckDetection m_hand_1;
	public CheckDetection m_hand_2;
	public bool m_checkOK = false;


	[Header("Enigme \"Final\" ")]
	public GameObject m_cloche;
	public GameObject m_finalFork;


	[Header("LEDS")]
	public MeshRenderer m_led_cigar;
	public MeshRenderer m_led_fly;
	public MeshRenderer m_led_hand;
	public Material m_ledON;
	public Material m_ledOFF;

	public bool win = false;
	private bool hasWon = false;


	// Use this for initialization
	void Start () 
	{

		//
		//m_androide.SetActive(false);
		m_cable.RopeCutEvent += ActivateBomb;

		//cigar event
		m_cigar.transform.parent.gameObject.SetActive(false);
		m_snapPhoto.ObjectSnappedToDropZone += OpenPainting_Cigar;
		m_snapCigar.ObjectSnappedToDropZone += DetectSnapCigar;
		m_snapCigar.ObjectUnsnappedFromDropZone += DetectUnsnapCigar;
		//Fly event
		m_Paper.SetActive(false);
		m_LampRotator.MinLimitReached += OpenPainting_Fly;
		m_snapFly.ObjectSnappedToDropZone += DetectSnapFly;
		m_snapFly.ObjectUnsnappedFromDropZone += DetectUnsnapFly;
		//Check event 
		m_LightCable.RopeCutEvent += TurnOffLight;
		m_hand_1.OnValidCheckDetection += DetectHand_1;
		m_hand_2.OnValidCheckDetection += DetectHand_2;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//CIGAR
		if(m_isCigarSnapped && m_cigar.isLighted && m_led_cigar.material != m_ledON){
			m_led_cigar.material = m_ledON;
			m_cigarOK = true;
		}
		else if((!m_isCigarSnapped || !m_cigar.isLighted) && m_led_cigar.material != m_ledOFF){
			m_led_cigar.material = m_ledOFF;
			m_cigarOK = false;
		}

		//Final
		if((m_cigarOK && m_flyOK && m_checkOK && !hasWon) || (win && !hasWon)){
			Win();
		}
	}


	//1 button
	public void ButtonAction(){
		m_BeginningAnim.SetTrigger("Forkhide");
	}

	//2 cable
	public void ActivateBomb(object sender){
		//m_androide.SetActive(true);
		m_bomb.GetComponent<AudioSource>().PlayOneShot(m_bombClip, 2);
		m_bomb.GetComponent<Animator>().SetTrigger("StartBomb");
	}

	//3 cigar
	public void OpenPainting_Cigar(object sender, SnapDropZoneEventArgs e){
		m_cadreAnim_Cigar.SetTrigger("Open_Cadre");
		m_cigar.transform.parent.gameObject.SetActive(true);
	}

	private void DetectSnapCigar(object sender, SnapDropZoneEventArgs e){
		m_isCigarSnapped = true;
	}
	
	private void DetectUnsnapCigar(object sender, SnapDropZoneEventArgs e){
		m_isCigarSnapped = false;
	}

	//4 fly
	private void OpenPainting_Fly(object sender, ControllableEventArgs e){
		m_cadreAnim_Fly.SetTrigger("Open_Cadre");
		m_Paper.SetActive(true);
	}

	private void DetectSnapFly(object sender, SnapDropZoneEventArgs e){
		m_led_fly.material = m_ledON;
		m_flyOK = true;
	}
	
	private void DetectUnsnapFly(object sender, SnapDropZoneEventArgs e){
		m_led_fly.material = m_ledOFF;
		m_flyOK = false;
	}

	//5 check
	private void TurnOffLight(object sender){
		m_checkLight.enabled = false;
	}
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
		m_checkOK = true;
	}

	//6 FInal
	private void Win(){
		print("win");
		hasWon = true;
		m_cloche.SetActive(false);
		
		m_finalFork.GetComponent<Rotate>().rotate = false;
		m_finalFork.transform.rotation = Quaternion.Euler(0,0,0);

		m_finalFork.GetComponent<VRTK_InteractableObject>().enabled = true;
		m_finalFork.GetComponent<Collider>().enabled = true;
		m_finalFork.GetComponent<Rigidbody>().isKinematic = false;
		m_finalFork.GetComponent<Rigidbody>().useGravity = true;
		m_finalFork.GetComponent<Rigidbody>().AddForce(0,0, 0.5f, ForceMode.Impulse);
	}
}
