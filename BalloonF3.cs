/********************************************************************************************
*********************************** Code for Balloon1 ***************************************
********************************************************************************************/
using UnityEngine;
using UnityEngine.UI;   //
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
//-------------------------------------------------------------------------------------------
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
//-------------------------------------------------------------------------------------------
public class BalloonF3 : MonoBehaviour
{
    public GameObject Ball3F;
    //
    public Vector3 scaleChange;
    public Vector3 currentSize;
    public float refTime = 0.0f;   // Clock
    public StreamWriter sw;
    //public ParticleSystem em;
    float[,,] listConditions = new float[,,] { { { 600.0f, 100.0f, -50.0f }, { 600.0f, 100.0f, -35.0f }, { 600.0f, 100.0f, -40.0f }, { 600.0f, 100.0f, -45.0f }, { 600.0f, 100.0f, -45.0f }, { 600.0f, 100.0f, -50.0f }, { 600.0f, 100.0f, -40.0f }, { 600.0f, 100.0f, -35.0f }, { 600.0f, 100.0f, -35.0f }, { 600.0f, 100.0f, -40.0f }, { 600.0f, 100.0f, -50.0f }, { 600.0f, 100.0f, -45.0f }, { 600.0f, 100.0f, -40.0f }, { 600.0f, 100.0f, -45.0f }, { 600.0f, 100.0f, -35.0f }, { 600.0f, 100.0f, -50.0f }, { 600.0f, 100.0f, -45.0f }, { 600.0f, 100.0f, -40.0f }, { 600.0f, 100.0f, -35.0f }, { 600.0f, 100.0f, -50.0f }, { 600.0f, 100.0f, -35.0f }, { 600.0f, 100.0f, -50.0f }, { 600.0f, 100.0f, -40.0f }, { 600.0f, 100.0f, -45.0f }, { 600.0f, 100.0f, -35.0f }, { 600.0f, 100.0f, -40.0f }, { 600.0f, 100.0f, -45.0f }, { 600.0f, 100.0f, -50.0f }, { 600.0f, 100.0f, -45.0f }, { 600.0f, 100.0f, -35.0f }, { 600.0f, 100.0f, -50.0f }, { 600.0f, 100.0f, -40.0f }, { 600.0f, 100.0f, -35.0f }, { 600.0f, 100.0f, -45.0f }, { 600.0f, 100.0f, -40.0f }, { 600.0f, 100.0f, -50.0f }, { 600.0f, 100.0f, -50.0f }, { 600.0f, 100.0f, -40.0f }, { 600.0f, 100.0f, -45.0f }, { 600.0f, 100.0f, -35.0f } } };
    //=======================================================================================
    void Start()
    {
        currentSize = new Vector3(.0f, .0f, .0f);
        currentSize = Ball3F.transform.localScale;
        ParticleSystem.EmissionModule em = GetComponent<ParticleSystem>().emission;
        em.enabled = false;
    }
    //=======================================================================================
    // Update is called once per frame.......................................................
        
    void Update()
    {
        ParticleSystem.EmissionModule em = GetComponent<ParticleSystem>().emission;
        scaleChange = new Vector3(.0015f, .0015f, .0015f);
        
        //ParticleSystem.EmissionModule em = GetComponent<ParticleSystem>().emission;
        if (currentSize[1] < .5f)
        {
            //print(currentSize[1]);
            Ball3F.transform.localScale += scaleChange;
            currentSize = new Vector3(Mathf.Log(Ball3F.transform.localScale[0],2), Ball3F.transform.localScale[1], Ball3F.transform.localScale[2]);
            print(currentSize[1]);
            refTime = Time.time;    // set the refernce time for the balloon burst initiation
        }

        else
        {
            this.GetComponent<ParticleSystem>().startColor = new Color(0, 0, 0, .5f);
            em.enabled = true;
            if (Time.time >= refTime + 0.2)
            {
                em.enabled = false;
            }
        }
    }
}