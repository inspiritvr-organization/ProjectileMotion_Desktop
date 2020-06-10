using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class CsvReadWrite : MonoBehaviour
{
    public int NumberOfClicks = 0;
    private DateTime startTime;
    private DateTime endTime;
    private float time;
    public List<string[]> rowData = new List<string[]>();
    public List<Vector3> heatMap = new List<Vector3>();
    public static CsvReadWrite Instance;
    private string[] rowDataTemp = new string[5];
    private int FrameCount = 0;
    private string temp = "", s = "", n = "", w = "";
    private String tt;
    public string timestamp = "";
    private string nowtime = "";
    // Use this for initialization

    public void saveintwoseconds()
    {
         SimpleSave2("1", "Save1C");
    }

    private void Start()
    {
        //SimpleSave2("1", "Save1C");

        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        RecordHeatMap();
    }


    public void SimpleSave2(string expNo, string expName)
    {
        rowDataTemp = new string[4];
        rowDataTemp[0] = expName;
        rowDataTemp[1] = expNo;
        rowDataTemp[2] = Time.time.ToString("F2");
        rowDataTemp[3] = NumberOfClicks.ToString();

        StringBuilder sb = new StringBuilder();
        sb.Append(into());


        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            string path2 = Application.dataPath;

            string n3 = string.Format("{1:}-{0:yyyy-MM-dd_hh-mm-ss-tt}-{2:}-{3:}.csv",
            DateTime.Now, expName, "Total Clicks_" + NumberOfClicks.ToString(), "Time Spent_" + rowDataTemp[2] + "s");


            var fullResultPath3 = Path.Combine(path2, n3);
            File.WriteAllText(fullResultPath3, sb.ToString());

            Debug.LogError("fullResultPath3: " + fullResultPath3);

        }

        if (Application.platform == RuntimePlatform.Android)
        {
            string path2 = Application.persistentDataPath.Substring(0, Application.persistentDataPath.Length - 5);

            string n = string.Format("{1:}-{0:yyyy-MM-dd_hh-mm-ss-tt}-{2:}-{3:}.csv",
            DateTime.Now, expName, "Total Clicks: " + NumberOfClicks.ToString(), "Time Spent: " + rowDataTemp[2] + "s");

            var fullResultPath3 = Path.Combine(path2, n);
            File.WriteAllText(fullResultPath3, "Data");
        }



    }



    public float recordTimer;
    public float recordSaveTime;

    public void RecordHeatMap()
    {
        recordTimer += Time.deltaTime;

        if (recordTimer > 2)
        {
            heatMap.Add(Camera.main.transform.position);
            heatMap.Add(Camera.main.transform.rotation.eulerAngles);

            recordTimer = 0;
        }
    }
    public string into()
    {
        foreach (Vector3 str in heatMap)
        {
            temp += str + "\n"; ; //maybe also + '\n' to put them on their own line.
            s = temp;

            n = s.Replace(",", "_");

            //print(n);

        }
        return n;

    }
}
