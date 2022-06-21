using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //

public class Coins : MonoBehaviour
{
    public GameObject coin;
    public GameObject Box001;
    // Start is called before the first frame update
    void Start()
    {
        //Physics.gravity = new Vector3(0.0f, -1.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //Physics.gravity = new Vector3(0.0f, -Mathf.Pow(2, Time.time/3), 0.0f);
    }
}
