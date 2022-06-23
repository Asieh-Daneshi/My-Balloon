using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentController : MonoBehaviour
{
    #region introduce gameObjects
    public GameObject M01;
    public GameObject M02;
    public GameObject M03;
    public GameObject F01;
    public GameObject F02;
    public GameObject F03;
    public GameObject coin;
    public Rigidbody Rb;
    public GameObject FixImage;

    public GameObject BalloonP;     // Participant's balloon
    public GameObject BalloonAF01;
    public GameObject BalloonAF02;
    public GameObject BalloonAF03;
    public GameObject BalloonAM01;
    public GameObject BalloonAM02;
    public GameObject BalloonAM03;
    public Vector3 currentSize;
    public Vector3 scaleChange;
    public GameObject particles;
    #endregion

    #region introduce animators
    Animator mAnimator1;
    Animator mAnimator2;
    Animator mAnimator3;
    Animator fAnimator1;
    Animator fAnimator2;
    Animator fAnimator3;
    #endregion
    // :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // list of agents that disappear in each trial..................................................................................
    float[,,] testParams=new float[,,]{{{5,2,1,6,3,4},{ 1, 2, 4,5, 3, 6 },{ 2,4,3, 6, 5, 1 }, { 1, 2, 5, 6, 3, 4 }, { 1, 2, 4, 5, 3, 6 }, { 2, 4, 3, 6, 5, 1 } , { 1, 2, 5, 6, 3, 4 }, { 1, 2, 4, 5, 3, 6 }, { 2, 4, 3, 6, 5, 1 } , { 1, 2, 5, 6, 3, 4 }, { 1, 2, 4, 5, 3, 6 }, { 2, 4, 3, 6, 5, 1 } }, { { 0, 0, 1.05f, 1.2f, 1.21f, 2f }, { 0, 0, 0, 0, 1.21f, 2f }, { 0, 1f, 1.05f, 1.12f, 1.22f, 2.1f }, { 0, 0, 1.05f, 1.2f, 1.21f, 2f }, { 0, 0, 0, 0, 1.21f, 2f }, { 0, 1f, 1.05f, 1.12f, 1.22f, 2.1f }, { 0, 0, 1.05f, 1.2f, 1.21f, 2f }, { 0, 0, 0, 0, 1.21f, 2f }, { 0, 1f, 1.05f, 1.12f, 1.22f, 2.1f }, { 0, 0, 1.05f, 1.2f, 1.21f, 2f }, { 0, 0, 0, 0, 1.21f, 2f }, { 0, 1f, 1.05f, 1.12f, 1.22f, 2.1f } },{ { 0, 0, 1, 1, 1, 2 }, { 0, 0, 0, 0, 1, 2 }, { 0, 2,2, 1, 1, 2 }, { 0, 0, 1, 1, 1, 2 }, { 0, 0, 0, 0, 1, 2 }, { 0, 2, 2, 1, 1, 2 }, { 0, 0, 1, 1, 1, 2 }, { 0, 0, 0, 0, 1, 2 }, { 0, 2, 2, 1, 1, 2 }, { 0, 0, 1, 1, 1, 2 }, { 0, 0, 0, 0, 1, 2 }, { 0, 2, 2, 1, 1, 2 } } };
    public float startTime;
    public float refTime;
    public int trialTrigger;




    // Start is called before the first frame update................................................................................
    void Start()
    {
        Rb = coin.GetComponent<Rigidbody>();
        Coroutine a = StartCoroutine(delayBetweenTrials());


        currentSize = new Vector3(.0f, .0f, .0f);
        currentSize = BalloonP.transform.localScale;
        // deactivatie particle system at the beginning
        ParticleSystem.EmissionModule emi = GetComponent<ParticleSystem>().emission;
        print("Horaaaaaaaaaaaaaaaaaaaa");
        emi.enabled = false;
    }

    
    void Update()
    {
        #region control the coin loop
        //Physics.gravity = new Vector3(0.0f, -.1f, 0.0f);
        Rb.AddForce(new Vector3(0, -(Time.time-startTime) * Rb.mass/100, 0));
        if (coin.transform.position[1] < 0.95f)   // if the coin reached the box, start over..........................................
        {
            //Physics.gravity = new Vector3(0.0f, -Mathf.Pow(2, (Time.time-startTime)) * 0.01f, 0.0f);
            coin.transform.position = new Vector3(47.22307f, 3f, -41.98611f);   // change the vertical position of the coin to the first point
            Rb.velocity = new Vector3(0, 0, 0);
        }
        #endregion
    }




    #region loop over trials (TN: trial number, AN= agent number)
    void TrialsLoop(int TN, int AN)
    {
        //Rb.AddForce(new Vector3(0, 0, 0));
        startTime = Time.time;
        #region agents disappearing approach
        // at the beginning of each trial set all the agents active ................................................................
        //M01.SetActive(true);
        //M02.SetActive(true);
        //M03.SetActive(true);
        //F01.SetActive(true);
        //F02.SetActive(true);
        //F03.SetActive(true);
        //..........................................................................................................................
        // determine which agent/agents must be deactivated at each trial ..........................................................
        //if (inactiveAgents[0, TN, 0] == 1 || inactiveAgents[0, TN, 1] == 1 || inactiveAgents[0, TN, 2] == 1)
        //    M01.SetActive(false); // hide first male agent
        //if (inactiveAgents[0, TN, 0] == 2 || inactiveAgents[0, TN, 1] == 2 || inactiveAgents[0, TN, 2] == 2)
        //    M02.SetActive(false); // hide second male agent
        //if (inactiveAgents[0, TN, 0] == 3 || inactiveAgents[0, TN, 1] == 3 || inactiveAgents[0, TN, 2] == 3)
        //    M03.SetActive(false); // hide third male agent
        //if (inactiveAgents[0, TN, 0] == 4 || inactiveAgents[0, TN, 1] == 4 || inactiveAgents[0, TN, 2] == 4)
        //    F01.SetActive(false); // hide first female agent
        //if (inactiveAgents[0, TN, 0] == 5 || inactiveAgents[0, TN, 1] == 5 || inactiveAgents[0, TN, 2] == 5)
        //    F02.SetActive(false); // hide second female agent
        //if (inactiveAgents[0, TN, 0] == 6 || inactiveAgents[0, TN, 1] == 6 || inactiveAgents[0, TN, 2] == 6)
        //    F03.SetActive(false); // hide third female agent
        #endregion
        //..........................................................................................................................
        #region agents remain idle approach
        // determine which agent/agents are active at each trial ...................................................................
        //fAnimator1 = F01.GetComponent<Animator>();
        //fAnimator2 = F02.GetComponent<Animator>();
        //fAnimator3 = F03.GetComponent<Animator>();
        //mAnimator1 = M01.GetComponent<Animator>();
        //mAnimator2 = M02.GetComponent<Animator>();
        //mAnimator3 = M03.GetComponent<Animator>();

        print("burstFactor: "+ testParams[2, TN, AN]);
        //if (testParams[2, TN, AN] == 1)
        //{
        //    switch (testParams[0, TN, AN])
        //    {
        //        case 1:
        //            print("fAnimator1B");
        //            fAnimator1.SetInteger("Burst", 1);
        //            fAnimator2.SetInteger("Burst", 2);
        //            fAnimator3.SetInteger("Burst", 2);
        //            mAnimator1.SetInteger("Burst", 2);
        //            mAnimator2.SetInteger("Burst", 2);
        //            mAnimator3.SetInteger("Burst", 2);
        //            break;
        //        case 2:
        //            print("fAnimator2B");
        //            fAnimator1.SetInteger("Burst", 3);
        //            fAnimator2.SetInteger("Burst", 1);
        //            fAnimator3.SetInteger("Burst", 2);
        //            mAnimator1.SetInteger("Burst", 2);
        //            mAnimator2.SetInteger("Burst", 2);
        //            mAnimator3.SetInteger("Burst", 2);
        //            break;
        //        case 3:
        //            print("fAnimator3B");
        //            fAnimator1.SetInteger("Burst", 3);
        //            fAnimator2.SetInteger("Burst", 3);
        //            fAnimator3.SetInteger("Burst", 1);
        //            mAnimator1.SetInteger("Burst", 3);
        //            mAnimator2.SetInteger("Burst", 2);
        //            mAnimator3.SetInteger("Burst", 2);
        //            break;
        //        case 4:
        //            print("mAnimator1B");
        //            fAnimator1.SetInteger("Burst", 3);
        //            fAnimator2.SetInteger("Burst", 3);
        //            fAnimator3.SetInteger("Burst", 2);
        //            mAnimator1.SetInteger("Burst", 1);
        //            mAnimator2.SetInteger("Burst", 2);
        //            mAnimator3.SetInteger("Burst", 2);
        //            break;
        //        case 5:
        //            print("mAnimator2B");
        //            fAnimator1.SetInteger("Burst", 3);
        //            fAnimator2.SetInteger("Burst", 3);
        //            fAnimator3.SetInteger("Burst", 3);
        //            mAnimator1.SetInteger("Burst", 3);
        //            mAnimator2.SetInteger("Burst", 1);
        //            mAnimator3.SetInteger("Burst", 2);
        //            break;
        //        case 6:
        //            print("mAnimator3B");
        //            fAnimator1.SetInteger("Burst", 3);
        //            fAnimator2.SetInteger("Burst", 3);
        //            fAnimator3.SetInteger("Burst", 3);
        //            mAnimator1.SetInteger("Burst", 3);
        //            mAnimator2.SetInteger("Burst", 3);
        //            mAnimator3.SetInteger("Burst", 1);
        //            break;
        //    }
        //}
        //else if (testParams[2, TN, AN] == 2)
        //{
        //    switch (testParams[0, TN, AN])
        //    {
        //        case 1:
        //            print("fAnimator1Q");
        //            fAnimator1.SetInteger("Burst", 4);
        //            break;
        //        case 2:
        //            print("fAnimator2Q");
        //            fAnimator2.SetInteger("Burst", 4);
        //            break;
        //        case 3:
        //            print("fAnimator3Q");
        //            fAnimator3.SetInteger("Burst", 4);
        //            break;
        //        case 4:
        //            print("mAnimator1Q");
        //            mAnimator1.SetInteger("Burst", 4);
        //            break;
        //        case 5:
        //            print("mAnimator2Q");
        //            mAnimator2.SetInteger("Burst", 4);
        //            break;
        //        case 6:
        //            print("mAnimator3Q");
        //            mAnimator3.SetInteger("Burst", 4);
        //            break;
        //    }
        //}
        #endregion
    }
    #endregion
    IEnumerator delayBetweenTrials()
    {
        int trialCounter;
        int numberofTrials = 10;
        int AgentCounter;
        int terminateFactor = 0;

        ParticleSystem.EmissionModule emi = GetComponent<ParticleSystem>().emission;
        scaleChange = new Vector3(.0015f, .0015f, .0015f);

        fAnimator1 = F01.GetComponent<Animator>();
        fAnimator2 = F02.GetComponent<Animator>();
        fAnimator3 = F03.GetComponent<Animator>();
        mAnimator1 = M01.GetComponent<Animator>();
        mAnimator2 = M02.GetComponent<Animator>();
        mAnimator3 = M03.GetComponent<Animator>();



        for (trialCounter = 0; trialCounter < numberofTrials; trialCounter++)
        {
            F01.transform.position = new Vector3(50.079f, 0f, -41.635f);
            F02.transform.position = new Vector3(49.32f, 0f, -41.753f);
            F03.transform.position = new Vector3(47.61678f, 0f, -40.59851f);
            M01.transform.position = new Vector3(48.443f, 0f, -41.917f);
            M02.transform.position = new Vector3(47.41962f, 0f, -39.89147f);
            M03.transform.position = new Vector3(47.362f, 0f, -39.02863f);

            BalloonAF01.SetActive(false);
            BalloonAF02.SetActive(false);
            BalloonAF03.SetActive(false);
            BalloonAM01.SetActive(false);
            BalloonAM02.SetActive(false);
            BalloonAM03.SetActive(false);
            while ((Input.GetKey("space")))
            {
                yield return null;
            }
            FixImage.SetActive(true);
            fAnimator1.Rebind();
            fAnimator2.Rebind();
            fAnimator3.Rebind();
            mAnimator1.Rebind();
            mAnimator2.Rebind();
            mAnimator3.Rebind();
            //fAnimator1.Update(0f);

            yield return new WaitForSeconds(1.5f);      // show the fixation image for 1.5 seconds..................................
            while (!(Input.GetKey("space")))
            {
                yield return null;
            }
            FixImage.SetActive(false);
            for (AgentCounter = 0; AgentCounter < 6; AgentCounter++)
            {
                if (testParams[2, trialCounter, AgentCounter] != 0)
                {
                    print("Asiiiiiiiiiiiiiii"+testParams[0, trialCounter, AgentCounter]);
                    switch (testParams[0, trialCounter, AgentCounter])
                    {
                        case 1:
                            print("F1 started!");
                            fAnimator1.SetInteger("Start", 1);
                            yield return new WaitForSeconds(.2f);
                            BalloonAF01.SetActive(true);
                            BalloonAF01.transform.localScale= new Vector3(.1f, .1f, .1f);
                            break;
                        case 2:
                            print("F2 started!");
                            fAnimator2.SetInteger("Start", 1);
                            yield return new WaitForSeconds(.2f);
                            BalloonAF02.SetActive(true);
                            BalloonAF02.transform.localScale = new Vector3(.1f, .1f, .1f);
                            break;
                        case 3:
                            print("F3 started!");
                            fAnimator3.SetInteger("Start", 1);
                            yield return new WaitForSeconds(.2f);
                            BalloonAF03.SetActive(true);
                            BalloonAF03.transform.localScale = new Vector3(.1f, .1f, .1f);
                            break;
                        case 4:
                            print("M1 started!");
                            mAnimator1.SetInteger("Start", 1);
                            yield return new WaitForSeconds(.2f);
                            BalloonAM01.SetActive(true);
                            BalloonAM01.transform.localScale = new Vector3(.1f, .1f, .1f);
                            break;
                        case 5:
                            print("M2 started!");
                            mAnimator2.SetInteger("Start", 1);
                            yield return new WaitForSeconds(.2f);
                            BalloonAM02.SetActive(true);
                            BalloonAM02.transform.localScale = new Vector3(.1f, .1f, .1f);
                            break;
                        case 6:
                            print("M3 started!");
                            mAnimator3.SetInteger("Start", 1);
                            yield return new WaitForSeconds(.2f);
                            BalloonAM03.SetActive(true);
                            BalloonAM03.transform.localScale = new Vector3(.1f, .1f, .1f);
                            break;
                    }
                }
            }

            refTime = Time.time;                        // save the time after idle phase at the beginning of each trial
            // after some agents started blowing in balloons, here we manage if some of them quit the experiment or their balloons burst before the participant stops!
            for (AgentCounter = 0;AgentCounter<6;AgentCounter++)   // this loop is on agents to determine the role of each agent in each trial
            {
                print("trialCounter: " + trialCounter + " , agentCounter: " + AgentCounter);
                while ((Time.time - refTime) <= testParams[1, trialCounter, AgentCounter])
                {
                    // Participant's balloon inflating .............................................................................
                    if ((currentSize[1] < 5f) && (Input.GetKey("space")))
                    {
                        BalloonP.transform.localScale += scaleChange;
                        currentSize = new Vector3(Mathf.Log(BalloonP.transform.localScale[0], 2), BalloonP.transform.localScale[1], BalloonP.transform.localScale[2]);
                        //print(currentSize[1]);
                        refTime = Time.time;    // save the reference time for the balloon burst initiation
                    }
                    // when threshold is reached, activate particle system (simulating burst) for a glance (0.2 sec) and also deactivate disappear Balloon
                    else if ((Input.GetKey("space")))
                    {
                        print("emmmmmmmmmmmmmmmmmmmmm");
                        emi.enabled = true;
                        print("EMMMMMMMMMMM");
                        if (Time.time >= refTime + 0.2)
                        {
                            BalloonP.SetActive(false);
                            emi.enabled = false;
                        }
                    }

                    if (!(Input.GetKey("space")))
                    {
                        print("user terminated: "+ testParams[1, trialCounter, AgentCounter]);
                        terminateFactor = 1;
                        break;
                    }
                    //print("Time: " + (Time.time - refTime));
                    yield return new WaitForSeconds(0.00001f);
                }
                if (terminateFactor==0)
                {
                    if (testParams[2, trialCounter, AgentCounter] == 1)
                    {
                        switch (testParams[0, trialCounter, AgentCounter])
                        {
                            case 1:
                                print("fAnimator1B");
                                fAnimator1.SetInteger("Burst", 1);  // 1:Burst; 2:Look right; 3:Look left; 4: Quit
                                particles.transform.position = new Vector3(49.3f, .0f, -40f);
                                emi.enabled = true;
                                yield return new WaitForSeconds(.2f);
                                BalloonAF01.SetActive(false);
                                fAnimator2.SetInteger("Burst", 2);
                                fAnimator3.SetInteger("Burst", 2);
                                mAnimator1.SetInteger("Burst", 2);
                                mAnimator2.SetInteger("Burst", 2);
                                mAnimator3.SetInteger("Burst", 2);
                                break;
                            case 2:
                                print("fAnimator2B");
                                fAnimator1.SetInteger("Burst", 3);
                                fAnimator2.SetInteger("Burst", 1);
                                particles.transform.position = new Vector3(49.3f, .0f, -40f);
                                emi.enabled = true;
                                yield return new WaitForSeconds(.2f);
                                BalloonAF02.SetActive(false);
                                fAnimator3.SetInteger("Burst", 2);
                                mAnimator1.SetInteger("Burst", 2);
                                mAnimator2.SetInteger("Burst", 2);
                                mAnimator3.SetInteger("Burst", 2);
                                break;
                            case 3:
                                print("fAnimator3B");
                                fAnimator1.SetInteger("Burst", 3);
                                fAnimator2.SetInteger("Burst", 3);
                                fAnimator3.SetInteger("Burst", 1);
                                particles.transform.position = new Vector3(49.3f, .0f, -40f);
                                emi.enabled = true;
                                yield return new WaitForSeconds(.2f);
                                BalloonAF03.SetActive(false);
                                mAnimator1.SetInteger("Burst", 3);
                                mAnimator2.SetInteger("Burst", 2);
                                mAnimator3.SetInteger("Burst", 2);
                                break;
                            case 4:
                                print("mAnimator1B");
                                fAnimator1.SetInteger("Burst", 3);
                                fAnimator2.SetInteger("Burst", 3);
                                fAnimator3.SetInteger("Burst", 2);
                                mAnimator1.SetInteger("Burst", 1);
                                particles.transform.position = new Vector3(49.3f, .0f, -40f);
                                emi.enabled = true;
                                yield return new WaitForSeconds(.2f);
                                BalloonAM01.SetActive(false);
                                mAnimator2.SetInteger("Burst", 2);
                                mAnimator3.SetInteger("Burst", 2);
                                break;
                            case 5:
                                print("mAnimator2B");
                                fAnimator1.SetInteger("Burst", 3);
                                fAnimator2.SetInteger("Burst", 3);
                                fAnimator3.SetInteger("Burst", 3);
                                mAnimator1.SetInteger("Burst", 3);
                                mAnimator2.SetInteger("Burst", 1);
                                particles.transform.position = new Vector3(49.3f, .0f, -40f);
                                emi.enabled = true;
                                yield return new WaitForSeconds(.2f);
                                BalloonAM02.SetActive(false);
                                mAnimator3.SetInteger("Burst", 2);
                                break;
                            case 6:
                                print("mAnimator3B");
                                fAnimator1.SetInteger("Burst", 3);
                                fAnimator2.SetInteger("Burst", 3);
                                fAnimator3.SetInteger("Burst", 3);
                                mAnimator1.SetInteger("Burst", 3);
                                mAnimator2.SetInteger("Burst", 3);
                                mAnimator3.SetInteger("Burst", 1);
                                particles.transform.position = new Vector3(49.3f, .0f, -40f);
                                emi.enabled = true;
                                yield return new WaitForSeconds(.2f);
                                BalloonAM03.SetActive(false);
                                break;
                        }
                    }
                    else if (testParams[2, trialCounter, AgentCounter] == 2)
                    {
                        switch (testParams[0, trialCounter, AgentCounter])
                        {
                            case 1:
                                print("fAnimator1Q");
                                fAnimator1.SetInteger("Burst", 4);
                                break;
                            case 2:
                                print("fAnimator2Q");
                                fAnimator2.SetInteger("Burst", 4);
                                break;
                            case 3:
                                print("fAnimator3Q");
                                fAnimator3.SetInteger("Burst", 4);
                                break;
                            case 4:
                                print("mAnimator1Q");
                                mAnimator1.SetInteger("Burst", 4);
                                break;
                            case 5:
                                print("mAnimator2Q");
                                mAnimator2.SetInteger("Burst", 4);
                                break;
                            case 6:
                                print("mAnimator3Q");
                                mAnimator3.SetInteger("Burst", 4);
                                break;
                        }
                    }
                    yield return new WaitForSeconds(0.1f);
                    fAnimator1.SetInteger("Burst", 0);
                    fAnimator2.SetInteger("Burst", 0);
                    fAnimator3.SetInteger("Burst", 0);
                    mAnimator1.SetInteger("Burst", 0);
                    mAnimator2.SetInteger("Burst", 0);
                    mAnimator3.SetInteger("Burst", 0);
                    
                    TrialsLoop(trialCounter, AgentCounter);
                    //trialTrigger = 1;
                }
            }
        }
        #region quit the experiment when all trials are completed
        if (trialCounter == numberofTrials)
        {
            Application.Quit();
        }
        #endregion
    }
}
