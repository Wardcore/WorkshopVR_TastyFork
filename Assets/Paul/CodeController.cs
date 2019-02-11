using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeController : MonoBehaviour {

    public Material[] material;
    public string code;
    private AudioSource analyse;
    public GameObject shader;
    public Animator anim;
    private GameObject[] main;
    private int countEnter;
    private bool go;
    private string handCode;
    private bool rightcode;
    private float timewait;
    private float timewaitShader;
    private int HandedHand;
    private void Start()
    {
        countEnter = -1;
        analyse = gameObject.GetComponent<AudioSource>();
        main = new GameObject[transform.childCount];
        int i = 0;
        foreach(Transform child in transform)
        {
            main[i] = child.gameObject;
            i++;
        }
        for (int b = 0; b < main.Length; b++)
        {
            main[b].GetComponent<Renderer>().material = material[0];
        }
    }
    private void Update()
    {
        if (go && shader.activeSelf && !rightcode)
        {
            timewaitShader = 0;
            timewait += Time.deltaTime;
            if(timewait >= analyse.clip.length)
            {
                OnCheckHandCode();
                timewait = 0;
            }
        }
        else
        {
            timewait = 0;
        }

        if(!go && !shader.activeSelf && !rightcode)
        {
            timewaitShader += Time.deltaTime;
            if(timewaitShader >= 1f)
            {
                WaitFadeOut();
            }
        }
    }
    public void OnboolCheck(bool b)
    {
        go = b;
        if (go && shader.activeSelf && !rightcode)
        {
            analyse.Play();
            analyse.volume = 1f;
        }
        else
        {
            analyse.Stop();
        }
    }

    void WaitFadeOut()
    {
        shader.SetActive(true);
        timewaitShader = 0;
    }

    public void OnCheckHandCode()
    {
        print("check hand");
        if (go && !rightcode)
        {
            countEnter++;
            handCode = handCode + HandedHand;
            shader.SetActive(false);
            timewait = 0;
            if (countEnter < main.Length - 1)
            {
                main[countEnter].GetComponent<Renderer>().material = material[2];
            }
            else if (countEnter == main.Length - 1)
            {
                main[countEnter].GetComponent<Renderer>().material = material[2];
                countEnter = -1;
                StartCoroutine(OnWaitForCheck(handCode));
            }
        }
    }
    public void OncheckHand(int Hand)
    {
        HandedHand = Hand;
    }
    IEnumerator OnWaitForCheck(string CheckCode)
    {
        if(CheckCode == code)
        {
            print("CodeChecked");
            rightcode = true;
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < main.Length; i++)
        {
            if(rightcode)
            {
                main[i].GetComponent<Renderer>().material = material[1];
                anim.SetBool("RightCode", true);
            }
            else
            {
                main[i].GetComponent<Renderer>().material = material[3];
                handCode = "";
            }
        }
        yield return new WaitForSeconds(1f);
        for (int a = 0; a < main.Length; a++)
        {
            if (!rightcode)
            {
                main[a].GetComponent<Renderer>().material = material[0];
            }
        }
    }
}
