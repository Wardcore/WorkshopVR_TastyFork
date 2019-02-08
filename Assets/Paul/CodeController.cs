using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeController : MonoBehaviour {

    #region Singleton
    public static CodeController instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public Material[] material;
    public string code;

    private GameObject[] main;
    private int countEnter;
    private bool go;
    private string handCode;
    private bool rightcode = false;
    private void Start()
    {
        countEnter = -1;
        
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
        //Debug.Log(main.Length);
    }

    public void OncheckHand(int Hand)
    {
        print("check hand");
        if (go && !rightcode)
        {
            countEnter++;
            handCode = handCode + Hand;
            if (countEnter < main.Length-1)
            {
                main[countEnter].GetComponent<Renderer>().material = material[2];
            }
            else if(countEnter == main.Length - 1)
            {
                main[countEnter].GetComponent<Renderer>().material = material[2];
                countEnter = -1;
                StartCoroutine(OnWaitForCheck(handCode));
            }
        }
    }

    public void OnboolCheck(bool b)
    {
        go = b;
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
