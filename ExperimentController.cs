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
    float[,,] testParams=new float[,,]{{{1,2,5,6,3,4},{ 1, 2, 4,5, 3, 6 },{ 2,4,3, 6, 5, 1 }, { 1, 2, 5, 6, 3, 4 }, { 1, 2, 4, 5, 3, 6 }, { 2, 4, 3, 6, 5, 1 } , { 1, 2, 5, 6, 3, 4 }, { 1, 2, 4, 5, 3, 6 }, { 2, 4, 3, 6, 5, 1 } , { 1, 2, 5, 6, 3, 4 }, { 1, 2, 4, 5, 3, 6 }, { 2, 4, 3, 6, 5, 1 } }, { { 0, 0, 1.05f, 1.2f, 1.21f, 2f }, { 0, 0, 0, 0, 1.21f, 2f }, { 0, 1f, 1.05f, 1.12f, 1.22f, 2.1f }, { 0, 0, 1.05f, 1.2f, 1.21f, 2f }, { 0, 0, 0, 0, 1.21f, 2f }, { 0, 1f, 1.05f, 1.12f, 1.22f, 2.1f }, { 0, 0, 1.05f, 1.2f, 1.21f, 2f }, { 0, 0, 0, 0, 1.21f, 2f }, { 0, 1f, 1.05f, 1.12f, 1.22f, 2.1f }, { 0, 0, 1.05f, 1.2f, 1.21f, 2f }, { 0, 0, 0, 0, 1.21f, 2f }, { 0, 1f, 1.05f, 1.12f, 1.22f, 2.1f } },{ { 0, 0, 1, 1, 1, 2 }, { 0, 0, 0, 0, 1, 2 }, { 0, 2,2, 1, 1, 2 }, { 0, 0, 1, 1, 1, 2 }, { 0, 0, 0, 0, 1, 2 }, { 0, 2, 2, 1, 1, 2 }, { 0, 0, 1, 1, 1, 2 }, { 0, 0, 0, 0, 1, 2 }, { 0, 2, 2, 1, 1, 2 }, { 0, 0, 1, 1, 1, 2 }, { 0, 0, 0, 0, 1, 2 }, { 0, 2, 2, 1, 1, 2 } } };
    public float startTime;
    public float refTime;
    public int trialTrigger;




    // Start is called before the first frame update................................................................................
    void Start()
    {
        Rb = coin.GetComponent<Rigidbody>();
        Coroutine a = StartCoroutine(delayBetweenTrials());
    }

    
    void Update()
    {
        #region control the coin loop
        //Physics.gravity = new Vector3(0.0f, -.1f, 0.0f);
        Rb.AddForce(new Vector3(0, -(Time.time-startTime) * Rb.mass/100, 0));
        if (coin.transform.position[1] < 1.5)   // if the coin reached the box, start over..........................................
        {
            //Physics.gravity = new Vector3(0.0f, -Mathf.Pow(2, (Time.time-startTime)) * 0.01f, 0.0f);
            coin.transform.position = new Vector3(48.7f, 3f, -37.7f);   // change the vertical position of the coin to the first point
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
        fAnimator1 = F01.GetComponent<Animator>();
        fAnimator2 = F02.GetComponent<Animator>();
        fAnimator3 = F03.GetComponent<Animator>();
        mAnimator1 = M01.GetComponent<Animator>();
        mAnimator2 = M02.GetComponent<Animator>();
        mAnimator3 = M03.GetComponent<Animator>();
        #region active agents
        print("burstFactor: "+ testParams[2, TN, AN]);
        if (testParams[2, TN, AN] == 1)
        {
            switch (testParams[0, TN, AN])
            {
                case 1:
                    print("fAnimator1B");
                    fAnimator1.SetInteger("Burst", trialTrigger);
                    fAnimator2.SetInteger("Look", trialTrigger);
                    fAnimator3.SetInteger("Look", trialTrigger);
                    mAnimator1.SetInteger("Look", trialTrigger);
                    mAnimator2.SetInteger("Look", trialTrigger);
                    mAnimator3.SetInteger("Look", trialTrigger);
                    break;
                case 2:
                    print("fAnimator2B");
                    fAnimator1.SetInteger("Look", trialTrigger);
                    fAnimator2.SetInteger("burst", trialTrigger);
                    fAnimator3.SetInteger("Look", trialTrigger);
                    mAnimator1.SetInteger("Look", trialTrigger);
                    mAnimator2.SetInteger("Look", trialTrigger);
                    mAnimator3.SetInteger("Look", trialTrigger);
                    break;
                case 3:
                    print("fAnimator3B");
                    fAnimator1.SetInteger("Look", trialTrigger);
                    fAnimator2.SetInteger("Look", trialTrigger);
                    fAnimator3.SetInteger("Burst", trialTrigger);
                    mAnimator1.SetInteger("Look", trialTrigger);
                    mAnimator2.SetInteger("Look", trialTrigger);
                    mAnimator3.SetInteger("Look", trialTrigger);
                    break;
                case 4:
                    print("mAnimator1B");
                    fAnimator1.SetInteger("Look", trialTrigger);
                    fAnimator2.SetInteger("Look", trialTrigger);
                    fAnimator3.SetInteger("Look", trialTrigger);
                    mAnimator1.SetInteger("Burst", trialTrigger);
                    mAnimator2.SetInteger("Look", trialTrigger);
                    mAnimator3.SetInteger("Look", trialTrigger);
                    break;
                case 5:
                    print("mAnimator2B");
                    fAnimator1.SetInteger("Look", trialTrigger);
                    fAnimator2.SetInteger("Look", trialTrigger);
                    fAnimator3.SetInteger("Look", trialTrigger);
                    mAnimator1.SetInteger("Look", trialTrigger);
                    mAnimator2.SetInteger("Burst", trialTrigger);
                    mAnimator3.SetInteger("Look", trialTrigger);
                    break;
                case 6:
                    print("mAnimator3B");
                    fAnimator1.SetInteger("Look", trialTrigger);
                    fAnimator2.SetInteger("Look", trialTrigger);
                    fAnimator3.SetInteger("Look", trialTrigger);
                    mAnimator1.SetInteger("Look", trialTrigger);
                    mAnimator2.SetInteger("Look", trialTrigger);
                    mAnimator3.SetInteger("Burst", trialTrigger);
                    break;
            }
        }
        else if (testParams[2, TN, AN] == 2)
        {
            switch (testParams[0, TN, AN])
            {
                case 1:
                    print("fAnimator1Q");
                    fAnimator1.SetInteger("Quit", trialTrigger);
                    break;
                case 2:
                    print("fAnimator2Q");
                    fAnimator2.SetInteger("Quit", trialTrigger);
                    break;
                case 3:
                    print("fAnimator3Q");
                    fAnimator3.SetInteger("Quit", trialTrigger);
                    break;
                case 4:
                    print("mAnimator1Q");
                    mAnimator1.SetInteger("Quit", trialTrigger);
                    break;
                case 5:
                    print("mAnimator2Q");
                    mAnimator2.SetInteger("Quit", trialTrigger);
                    break;
                case 6:
                    print("mAnimator3Q");
                    mAnimator3.SetInteger("Quit", trialTrigger);
                    break;
            }
        }
        #endregion
        #endregion
    }
    #endregion
    IEnumerator delayBetweenTrials()
    {
        int trialCounter;
        int numberofTrials = 10;
        int AgentCounter;
        int terminateFactor = 0;

        for (trialCounter = 0; trialCounter < numberofTrials; trialCounter++)
        {
            FixImage.SetActive(true);
            yield return new WaitForSeconds(1.5f);      // show the fixation image for 1.5 seconds..................................
            FixImage.SetActive(false);
            while (!(Input.GetKey("space")))
            {
                yield return null;
            }
            refTime = Time.time;                        // save the time after idle phase at the beginning of each trial
            for (AgentCounter = 0;AgentCounter<6;AgentCounter++)   // this loop is on agents to determine the role of each agent in each trial
            {
                //print("Hoooooooooooooooooooooooooooooooooray" + testParams[1, 0, AgentCounter]);
                print("trialCounter: " + trialCounter + " , agentCounter: " + AgentCounter);
                //while (((Time.time - refTime) <= testParams[1, trialCounter, AgentCounter]) && !(Input.GetKeyDown("space")))
                while ((Time.time - refTime) <= testParams[1, trialCounter, AgentCounter])
                {
                    if (!(Input.GetKey("space")))
                    {
                        print("user terminated: "+ testParams[1, trialCounter, AgentCounter]);
                        terminateFactor = 1;
                        break;
                    }
                    print("Time: " + (Time.time - refTime));
                    yield return new WaitForSeconds(0.00001f);
                }
                if (terminateFactor==0)
                {
                    TrialsLoop(trialCounter, AgentCounter);
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
