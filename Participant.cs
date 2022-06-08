using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Participant : MonoBehaviour
{
    public GameObject M01;
    public GameObject M02;
    public GameObject M03;
    public GameObject F01;
    public GameObject F02;
    public GameObject F03;
    public GameObject coin;
    // ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    int[,,] listConditions= new int[,,]{ { { 1, 1, 1 }, { 4, 3, 1 }, { 5, 1, 2 } } };
    float delayTime = 5.0f;   // Time interval between two successive trials   


    //float[,,] listConditions = new float[,,] { { { 600.0f, 100.0f, -50.0f }, { 600.0f, 100.0f, -35.0f }, { 600.0f, 100.0f, -40.0f }, { 600.0f, 100.0f, -45.0f }, { 600.0f, 100.0f, -45.0f }, { 600.0f, 100.0f, -50.0f }, { 600.0f, 100.0f, -40.0f }, { 600.0f, 100.0f, -35.0f }, { 600.0f, 100.0f, -35.0f }, { 600.0f, 100.0f, -40.0f }, { 600.0f, 100.0f, -50.0f }, { 600.0f, 100.0f, -45.0f }, { 600.0f, 100.0f, -40.0f }, { 600.0f, 100.0f, -45.0f }, { 600.0f, 100.0f, -35.0f }, { 600.0f, 100.0f, -50.0f }, { 600.0f, 100.0f, -45.0f }, { 600.0f, 100.0f, -40.0f }, { 600.0f, 100.0f, -35.0f }, { 600.0f, 100.0f, -50.0f }, { 600.0f, 100.0f, -35.0f }, { 600.0f, 100.0f, -50.0f }, { 600.0f, 100.0f, -40.0f }, { 600.0f, 100.0f, -45.0f }, { 600.0f, 100.0f, -35.0f }, { 600.0f, 100.0f, -40.0f }, { 600.0f, 100.0f, -45.0f }, { 600.0f, 100.0f, -50.0f }, { 600.0f, 100.0f, -45.0f }, { 600.0f, 100.0f, -35.0f }, { 600.0f, 100.0f, -50.0f }, { 600.0f, 100.0f, -40.0f }, { 600.0f, 100.0f, -35.0f }, { 600.0f, 100.0f, -45.0f }, { 600.0f, 100.0f, -40.0f }, { 600.0f, 100.0f, -50.0f }, { 600.0f, 100.0f, -50.0f }, { 600.0f, 100.0f, -40.0f }, { 600.0f, 100.0f, -45.0f }, { 600.0f, 100.0f, -35.0f } } };


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(YourFunctionName());
    }

    // Update is called once per frame
    void Update()
    {
        if (coin.transform.position[1] < 1.5)
        {
            //Physics.gravity = new Vector3(0.0f, -Mathf.Pow(2, Time.time) * 0.01f, 0.0f);
            coin.transform.position = new Vector3(48.7f, 3f, -37.7f);   // get the vertical position of the coin
        }
    }
    void TrialsLoop(int TN)
    {
        M01.SetActive(true);
        M02.SetActive(true);
        M03.SetActive(true);
        F01.SetActive(true);
        F02.SetActive(true);
        F03.SetActive(true);
        //Debug.Log("TN: "+TN);
        if (listConditions[0, TN, 0] == 1 || listConditions[0, TN, 1] == 1 || listConditions[0, TN, 2] == 1)
            M01.SetActive(false); // hide first male agent
        if (listConditions[0, TN, 0] == 2 || listConditions[0, TN, 1] == 2 || listConditions[0, TN, 2] == 2)
            M02.SetActive(false); // hide second male agent
        if (listConditions[0, TN, 0] == 3 || listConditions[0, TN, 1] == 3 || listConditions[0, TN, 2] == 3)
            M03.SetActive(false); // hide third male agent
        if (listConditions[0, TN, 0] == 4 || listConditions[0, TN, 1] == 4 || listConditions[0, TN, 2] == 4)
            F01.SetActive(false); // hide first female agent
        if (listConditions[0, TN, 0] == 5 || listConditions[0, TN, 1] == 5 || listConditions[0, TN, 2] == 5)
            F02.SetActive(false); // hide second female agent
        if (listConditions[0, TN, 0] == 6 || listConditions[0, TN, 1] == 6 || listConditions[0, TN, 2] == 6)
            F03.SetActive(false); // hide third female agent

    }
    IEnumerator YourFunctionName()
    {
        int trialCounter = 0, numberofTrials = 3;
        while (true && trialCounter < numberofTrials)
        {
            //print("TrialNumber: " + Time.time+","+Mathf.FloorToInt(Time.time / 8));
            TrialsLoop(Mathf.FloorToInt(Time.time / 8));
            yield return new WaitForSeconds(8);
            trialCounter++;
        }
        if (trialCounter == numberofTrials)
        {
            //print("Finished");
            Application.Quit();
        }
    }
}
