using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVwriter : MonoBehaviour
{
    string filename = "";

    [System.Serializable]
    public class Player
    {
        public string name;
        public int health; 
        public int damage;
    }
    [System.Serializable]
    public class PlayerList
    {
        public Player[] Player;
    }

    public PlayerList myPlayerList = new PlayerList();
    // Start is called before the first frame update
    void Start()
    {
        filename = Application.dataPath + "/test.csv";
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            WriteCSV();
        
    }
    public void WriteCSV()
    {
        if(myPlayerList.Player.Length>0)
        {
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine("Name, Health, Damage");
            tw.Close();

            tw = new StreamWriter(filename, true);

            //for(int i=0;i<myPlayerList.Player.Length;i++)
            //{
            //    tw.WriteLine(myPlayerList[i].name + "," + myPlayerList.Player[i].health + "," + myPlayerList.Player[i].damage);
            //}
            tw.Close();
        }
    }
}
