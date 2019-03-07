using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    #region Singleton
    public static Timer instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion


    [Header("AiguillePivotBasCentre")]
    public Transform clock;
    [Space]
    [Header("TempsDeDepartDuChrono")]
    public float timeStart;
    [Header("TempsBonusFinModule")]
    public float timeBonus;
    [Header("PourcentageDeTempsPourTictictic")]
    public float timerpercent;
    [Header("CetteClockEstElleAGaucheDeLaBombe?")]
    public bool left;
    [Space]
    [Header("AnimationRobot")]
    public Animator robot_animator;
    [Space]
    [Header("SoundAPlusDeTemps")]
    public AudioSource Tictictic;
    [Space]
    [Header("SonAmbianceBombe3D")]
    public AudioSource Tactactac;
    [Space]
    [Header("Debug")]
    public Material red;
    public Material orange;
    public GameObject g_clock;
    private float timeleft;
    private float timepercentage;
    public float timeMax;
    private int counterModul;
    private bool go;

    private const float secondsToDegrees = 360f;

    void Start()
    {
        if (timerpercent > 1)
        {
            timerpercent = timerpercent / 100f;
        }
        timeMax = timeStart + (2 * timeBonus);
        timeleft = timeStart;
    }

    void FixedUpdate()
    {
        if (go)
        {
            OnTimeFlying();
        }
    }

    void OnTimeFlying()
    {
        timeleft -= Time.deltaTime;
        int ticTimeLeft = Mathf.FloorToInt(timeleft);
        timepercentage = (((timeleft * 100) / timeMax) / 100);
        if (left)
        {
            clock.localRotation = Quaternion.Euler((timepercentage * secondsToDegrees) * 0.75f, 0f, 0f);
        }
        else
        {
            clock.localRotation = Quaternion.Euler((timepercentage * -secondsToDegrees) * 0.75f, 0f, 0f);
        }

        //-----------

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnTimeBonus();
        }

        //-----------

        if (timepercentage < timerpercent)
        {
            if (Tictictic != null && Tactactac != null)
            {
                if (!Tictictic.isPlaying)
                {

                    Tictictic.Play();
                    Tactactac.Stop();

                }
            }
            //g_clock.GetComponent<Renderer>().material = red;
        }
        else
        {
            if (Tictictic != null && Tactactac != null)
            {
                if (!Tactactac.isPlaying)
                {

                    Tictictic.Stop();
                    Tactactac.Play();

                }
            }
            //g_clock.GetComponent<Renderer>().material = orange;
        }
    }

    public void OnTimeBonus()
    {
        counterModul++;
        timeleft += timeBonus;

        if (counterModul == 3)
        {
            OnForkRevealed();
        }
        else if (timeleft > timeMax)
        {
            timeleft = timeMax;
        }
    }

    void OnForkRevealed()
    {
        //robot_animator.Play("Robot");
    }

    public void OnRedButton(bool goBombe)
    {
        go = goBombe;
    }
}
