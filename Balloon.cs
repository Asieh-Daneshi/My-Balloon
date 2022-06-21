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
public class Balloon : MonoBehaviour
{
    public GameObject Balon;
    //
    public Vector3 scaleChange;
    public Vector3 currentSize;
    public float refTime = 0.0f;   // Clock
    public StreamWriter sw;
    public ParticleSystem em;
    float[,,] listConditions = new float[,,] { { { 600.0f, 100.0f, -50.0f }, { 600.0f, 100.0f, -35.0f }, { 600.0f, 100.0f, -40.0f }, { 600.0f, 100.0f, -45.0f }, { 600.0f, 100.0f, -45.0f }, { 600.0f, 100.0f, -50.0f }, { 600.0f, 100.0f, -40.0f }, { 600.0f, 100.0f, -35.0f }, { 600.0f, 100.0f, -35.0f }, { 600.0f, 100.0f, -40.0f }, { 600.0f, 100.0f, -50.0f }, { 600.0f, 100.0f, -45.0f }, { 600.0f, 100.0f, -40.0f }, { 600.0f, 100.0f, -45.0f }, { 600.0f, 100.0f, -35.0f }, { 600.0f, 100.0f, -50.0f }, { 600.0f, 100.0f, -45.0f }, { 600.0f, 100.0f, -40.0f }, { 600.0f, 100.0f, -35.0f }, { 600.0f, 100.0f, -50.0f }, { 600.0f, 100.0f, -35.0f }, { 600.0f, 100.0f, -50.0f }, { 600.0f, 100.0f, -40.0f }, { 600.0f, 100.0f, -45.0f }, { 600.0f, 100.0f, -35.0f }, { 600.0f, 100.0f, -40.0f }, { 600.0f, 100.0f, -45.0f }, { 600.0f, 100.0f, -50.0f }, { 600.0f, 100.0f, -45.0f }, { 600.0f, 100.0f, -35.0f }, { 600.0f, 100.0f, -50.0f }, { 600.0f, 100.0f, -40.0f }, { 600.0f, 100.0f, -35.0f }, { 600.0f, 100.0f, -45.0f }, { 600.0f, 100.0f, -40.0f }, { 600.0f, 100.0f, -50.0f }, { 600.0f, 100.0f, -50.0f }, { 600.0f, 100.0f, -40.0f }, { 600.0f, 100.0f, -45.0f }, { 600.0f, 100.0f, -35.0f } } };
    //=======================================================================================
    void Start()
    {
        // set the initial size of the balloon
        currentSize = new Vector3(.0f, .0f, .0f);
        currentSize = Balon.transform.localScale;
        // deactivatie particle system at the beginning
        ParticleSystem.EmissionModule em = GetComponent<ParticleSystem>().emission;
        em.enabled = false;
    }
    //..............................................................................................................................
        
    void Update()
    {
        ParticleSystem.EmissionModule em = GetComponent<ParticleSystem>().emission;
        scaleChange = new Vector3(.0015f, .0015f, .0015f);
        
        // as long as the size of the balloon has not reached the threshold, continue to inflate
        if (currentSize[1] < .5f)
        {
            //print(currentSize[1]);
            Balon.transform.localScale += scaleChange;
            currentSize = new Vector3(Mathf.Log(Balon.transform.localScale[0],2), Balon.transform.localScale[1], Balon.transform.localScale[2]);
            //print(currentSize[1]);
            refTime = Time.time;    // save the reference time for the balloon burst initiation
        }
        // when threshold is reached, activate particle system (simulating burst) for a glance (0.2 sec) and also deactivate disappear Balloon
        else
        {
            em.enabled = true;
            if (Time.time >= refTime + 0.2)
            {
                Balon.SetActive(false);
                em.enabled = false;
            }
        }
    }
}