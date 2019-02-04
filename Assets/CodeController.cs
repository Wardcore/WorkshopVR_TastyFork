using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeController : MonoBehaviour {

    public Material[] material;

    private GameObject[] main;
    private int countEnter;

    private void Start()
    {
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

    void OncheckHand(int i)
    {
        countEnter++;
        main[countEnter].GetComponent<Renderer>().material = material[2];
    }

}
