using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRedButton : MonoBehaviour {

    public AudioSource audioBombe;
    public Animator anim_Gaethan;

    //------------
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnBigRedButton();
        }
    }
    //------------

    void OnBigRedButton()
    {
        Timer.instance.OnRedButton(true);
        if (audioBombe != null)
        {
            audioBombe.Play();
        }
        //Anim et Audio
    }
}
