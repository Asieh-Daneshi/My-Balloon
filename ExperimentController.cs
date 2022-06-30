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
    // in testParams, the first set is the number of the target agent, the second set is the time of the events, and the third set indicates the corresponding action
    float[,,] testParams=new float[,,]{{{5,2,1,6,3,4,0},{ 1, 2, 4,5, 3, 6,0 },{ 2,4,3, 6, 5, 1,0 }, { 1, 2, 5, 6, 3, 4,0 }, { 1, 2, 4, 5, 3, 6,0 }, { 2, 4, 3, 6, 5, 1,0 } , { 1, 2, 5, 6, 3, 4,0 }, { 1, 2, 4, 5, 3, 6,0 }, { 2, 4, 3, 6, 5, 1,0 } , { 1, 2, 5, 6, 3, 4,0 }, { 1, 2, 4, 5, 3, 6,0 }, { 2, 4, 3, 6, 5, 1,0 } }, { { 0, 0, 0.5f, 1.2f, 2.5f, 3.5f,10f }, { 0, 0, 0, 0, 1.21f, 2f, 10f }, { 0, 1f, 1.05f, 1.12f, 1.22f, 2.1f, 10f }, { 0, 0, 1.05f, 1.2f, 1.21f, 2f, 10f }, { 0, 0, 0, 0, 1.21f, 2f, 10f }, { 0, 1f, 1.05f, 1.12f, 1.22f, 2.1f, 10f }, { 0, 0, 1.05f, 1.2f, 1.21f, 2f, 10f }, { 0, 0, 0, 0, 1.21f, 2f, 10f }, { 0, 1f, 1.05f, 1.12f, 1.22f, 2.1f, 10f }, { 0, 0, 1.05f, 1.2f, 1.21f, 2f, 10f }, { 0, 0, 0, 0, 1.21f, 2f, 10f }, { 0, 1f, 1.05f, 1.12f, 1.22f, 2.1f, 10f } },{ { 0, 0, 1, 1, 1, 2,0 }, { 0, 0, 0, 0, 1, 2, 0 }, { 0, 2,2, 1, 1, 2, 0 }, { 0, 0, 1, 1, 1, 2, 0 }, { 0, 0, 0, 0, 1, 2, 0 }, { 0, 2, 2, 1, 1, 2, 0 }, { 0, 0, 1, 1, 1, 2, 0 }, { 0, 0, 0, 0, 1, 2, 0 }, { 0, 2, 2, 1, 1, 2, 0 }, { 0, 0, 1, 1, 1, 2, 0 }, { 0, 0, 0, 0, 1, 2, 0 }, { 0, 2, 2, 1, 1, 2, 0 } } };
    public float startTime;
    public float refTime1;
    public float refTime2;
    public int trialTrigger;




    // Start is called before the first frame update................................................................................
    void Start()
    {
        Rb = coin.GetComponent<Rigidbody>();
        Coroutine a = StartCoroutine(delayBetweenTrials());     // all the important things happen in this coroutine
        ParticleSystem.EmissionModule emi = GetComponent<ParticleSystem>().emission;    // deactivate particle system at the beginning of experiment
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


// ***********************************************************************************************************************************
// ***********************************************************************************************************************************
    IEnumerator delayBetweenTrials()
    {
        int trialCounter;
        int numberofTrials = 10;
        int AgentCounter;
        int terminateFactor = 0;    // when the participant releases the space key, the experiment terminates

        ParticleSystem.EmissionModule emi = GetComponent<ParticleSystem>().emission;
        scaleChange = new Vector3(.015f, .015f, .015f);

        // introducing animators .....................................................................................................
        fAnimator1 = F01.GetComponent<Animator>();
        fAnimator2 = F02.GetComponent<Animator>();
        fAnimator3 = F03.GetComponent<Animator>();
        mAnimator1 = M01.GetComponent<Animator>();
        mAnimator2 = M02.GetComponent<Animator>();
        mAnimator3 = M03.GetComponent<Animator>();

        #region loop on trials 
        for (trialCounter = 0; trialCounter < numberofTrials; trialCounter++)
        {
            // set the position of agents at the beginning of each trial. It is necessary because in some trials, when the balloon bursts, the participant gets too shocked and takes one step back!
            F01.transform.position = new Vector3(50.079f, 0f, -41.635f);
            F02.transform.position = new Vector3(49.32f, 0f, -41.753f);
            F03.transform.position = new Vector3(47.61678f, 0f, -40.59851f);
            M01.transform.position = new Vector3(48.443f, 0f, -41.917f);
            M02.transform.position = new Vector3(47.41962f, 0f, -39.89147f);
            M03.transform.position = new Vector3(47.362f, 0f, -39.02863f);

            // deactivate (disappear) all the balloons at the beginning of each trial
            BalloonAF01.SetActive(false);
            BalloonAF02.SetActive(false);
            BalloonAF03.SetActive(false);
            BalloonAM01.SetActive(false);
            BalloonAM02.SetActive(false);
            BalloonAM03.SetActive(false);

            // **********************************************************************************************************************
            BalloonP.SetActive(false);
            FixImage.SetActive(true);   // at the beginning of each trial, show the fication image
            // reset all the animators ..............................................................................................
            fAnimator1.Rebind();
            fAnimator2.Rebind();
            fAnimator3.Rebind();
            mAnimator1.Rebind();
            mAnimator2.Rebind();
            mAnimator3.Rebind();
            //fAnimator1.Update(0f);

            yield return new WaitForSeconds(1.5f);      // show the fixation image for 1.5 seconds ..................................
            // after 1.5 seconds showing the fixation image, if still the participant is not pressing the space key, don't start the trial. Start if only the fixation image has been showed for 1.5 seconds, and also the participant holds the space key!
            while (!(Input.GetKey("space")))
            {
                yield return null;
            }
            FixImage.SetActive(false);  // deactivate the fixation image, so that the experimental environment is visible!
            BalloonP.SetActive(true);
            BalloonP.transform.localScale = new Vector3(.1f, .1f, .1f);
            for (AgentCounter = 0; AgentCounter < 6; AgentCounter++)    // loop over agents to control them
            {
                if (testParams[2, trialCounter, AgentCounter] != 0)     // check the third set of testparams to see if an action is required or not (1: burst, 2: quit, 0: nothing), in case of burst or quit, agent must start blowing anyway!
                {
                    //print("Asiiiiiiiiiiiiiii"+testParams[0, trialCounter, AgentCounter]);
                    switch (testParams[0, trialCounter, AgentCounter])
                    {
                        case 1:
                            print("F1 started!");
                            fAnimator1.SetInteger("Start", 1);  // after the agent starts blowing, wait for 0.2 seconds so that the agent has enough time to get the blowing posture, and then the balloon starts inflating
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

            currentSize = new Vector3(.1f, .1f, .1f);
            refTime1 = Time.time;                        // save the time after idle phase at the beginning of each trial
            // after some agents started blowing in balloons, here we manage if some of them quit the experiment or their balloons burst before the participant stops!
            for (AgentCounter = 0;AgentCounter<7;AgentCounter++)   // this loop is on agents to determine the role of each agent in each trial
            {
                //print("trialCounter: " + trialCounter + " , agentCounter: " + AgentCounter);
                // the strategy is to go for each time point that one of the agents is supposed to make a specific action, then continue to the next time point, and so on...
                while ((Time.time - refTime1) <= testParams[1, trialCounter, AgentCounter])
                {
                    print("time:"+(Time.time-refTime1)+"testParam:"+ testParams[1, trialCounter, AgentCounter]);
                    if ((currentSize[1] < 5f) && (Input.GetKey("space")))   // if the participant's balloon is smaller than the currentSize, and the participant is holding the space key, then participant's balloon keeps inflating
                    {
                        BalloonP.transform.localScale += scaleChange;
                        currentSize = new Vector3(BalloonP.transform.localScale[0], BalloonP.transform.localScale[1], BalloonP.transform.localScale[2]);
                        refTime2 = Time.time;    // save the reference time for the balloon burst initiation
                    }
                    // when threshold is reached, activate particle system (simulating burst) for a glance (0.2 sec) and also deactivate Balloon
                    else if ((Input.GetKey("space")))   // participant is still holding the space key
                    {
                        emi.enabled = true;
                        if (Time.time >= refTime2 + 0.2)
                        {
                            BalloonP.SetActive(false);
                            emi.enabled = false;
                        }
                    }
                    // if the participant releases the space key, the trial is terminated
                    if (!(Input.GetKey("space")))
                    {
                        print("user terminated: "+ testParams[1, trialCounter, AgentCounter]);
                        terminateFactor = 1;
                        break;
                    }
                    yield return new WaitForSeconds(0.00001f);  // temporal resolution for updating the experiment!
                }


                if (terminateFactor==0)     // if the participant has not terminated the trial!
                {
                    if (testParams[2, trialCounter, AgentCounter] == 1)     // look at the current third set of testParams which indicates the corresponding action (1: burst, 2: quit, 0: nothing)
                    {
                        switch (testParams[0, trialCounter, AgentCounter])
                        {
                            case 1:
                                fAnimator1.SetInteger("Burst", 1);  // 1:Burst; 2:Look right; 3:Look left; 4: Quit
                                //particles.transform.position = new Vector3(49.3f, .0f, -40f);
                                //emi.enabled = true;
                                //yield return new WaitForSeconds(.2f);
                                BalloonAF01.SetActive(false);
                                print("F1Burst");
                                fAnimator2.SetInteger("Burst", 2);
                                fAnimator3.SetInteger("Burst", 2);
                                mAnimator1.SetInteger("Burst", 2);
                                mAnimator2.SetInteger("Burst", 2);
                                mAnimator3.SetInteger("Burst", 2);
                                break;
                            case 2:
                                fAnimator1.SetInteger("Burst", 3);
                                fAnimator2.SetInteger("Burst", 1);
                                //particles.transform.position = new Vector3(49.3f, .0f, -40f);
                                //emi.enabled = true;
                                //yield return new WaitForSeconds(.2f);
                                BalloonAF02.SetActive(false);
                                print("F2Burst");
                                fAnimator3.SetInteger("Burst", 2);
                                mAnimator1.SetInteger("Burst", 2);
                                mAnimator2.SetInteger("Burst", 2);
                                mAnimator3.SetInteger("Burst", 2);
                                break;
                            case 3:
                                fAnimator1.SetInteger("Burst", 3);
                                fAnimator2.SetInteger("Burst", 3);
                                fAnimator3.SetInteger("Burst", 1);
                                //particles.transform.position = new Vector3(49.3f, .0f, -40f);
                                //emi.enabled = true;
                                //yield return new WaitForSeconds(.2f);
                                BalloonAF03.SetActive(false);
                                print("F3Burst");
                                mAnimator1.SetInteger("Burst", 3);
                                mAnimator2.SetInteger("Burst", 2);
                                mAnimator3.SetInteger("Burst", 2);
                                break;
                            case 4:
                                fAnimator1.SetInteger("Burst", 3);
                                fAnimator2.SetInteger("Burst", 3);
                                fAnimator3.SetInteger("Burst", 2);
                                mAnimator1.SetInteger("Burst", 1);
                                //particles.transform.position = new Vector3(49.3f, .0f, -40f);
                                //emi.enabled = true;
                                //yield return new WaitForSeconds(.2f);
                                BalloonAM01.SetActive(false);
                                print("M1Burst");
                                mAnimator2.SetInteger("Burst", 2);
                                mAnimator3.SetInteger("Burst", 2);
                                break;
                            case 5:
                                fAnimator1.SetInteger("Burst", 3);
                                fAnimator2.SetInteger("Burst", 3);
                                fAnimator3.SetInteger("Burst", 3);
                                mAnimator1.SetInteger("Burst", 3);
                                mAnimator2.SetInteger("Burst", 1);
                                //particles.transform.position = new Vector3(49.3f, .0f, -40f);
                                //emi.enabled = true;
                                //yield return new WaitForSeconds(.2f);
                                BalloonAM02.SetActive(false);
                                print("M2Burst");
                                mAnimator3.SetInteger("Burst", 2);
                                break;
                            case 6:
                                fAnimator1.SetInteger("Burst", 3);
                                fAnimator2.SetInteger("Burst", 3);
                                fAnimator3.SetInteger("Burst", 3);
                                mAnimator1.SetInteger("Burst", 3);
                                mAnimator2.SetInteger("Burst", 3);
                                mAnimator3.SetInteger("Burst", 1);
                                print("M3Burst");
                                //particles.transform.position = new Vector3(49.3f, .0f, -40f);
                                //emi.enabled = true;
                                //yield return new WaitForSeconds(.2f);
                                BalloonAM03.SetActive(false);
                                break;
                        }
                    }
                    else if (testParams[2, trialCounter, AgentCounter] == 2)
                    {
                        switch (testParams[0, trialCounter, AgentCounter])
                        {
                            case 1:
                                print("F1Quit");
                                fAnimator1.SetInteger("Burst", 4);
                                BalloonAF01.SetActive(false);
                                break;
                            case 2:
                                print("F2Quit");
                                fAnimator2.SetInteger("Burst", 4);
                                BalloonAF02.SetActive(false);
                                break;
                            case 3:
                                print("F3Quit");
                                fAnimator3.SetInteger("Burst", 4);
                                BalloonAF03.SetActive(false);
                                break;
                            case 4:
                                print("M1Quit");
                                mAnimator1.SetInteger("Burst", 4);
                                BalloonAM01.SetActive(false);
                                break;
                            case 5:
                                print("M2Quit");
                                mAnimator2.SetInteger("Burst", 4);
                                BalloonAM02.SetActive(false);
                                break;
                            case 6:
                                print("M3Quit");
                                mAnimator3.SetInteger("Burst", 4);
                                BalloonAM03.SetActive(false);
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
                    
                }
            }
        }
        #endregion
        #region quit the experiment when all trials are completed
        if (trialCounter == numberofTrials)
        {
            Application.Quit();
        }
        #endregion
    }
}
