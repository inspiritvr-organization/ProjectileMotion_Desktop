using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class HeatmapDataDesktop : MonoBehaviour
{
    [SerializeField] string ModuleName;
    [SerializeField] string Version;
    List<Vector3> playerPosition = new List<Vector3>();
    List<Quaternion> playerOrientation = new List<Quaternion>();
    List<float> timeStamp = new List<float>();
    float time = 0f;
    float deltatime = 0.5f;
    DateTime startTime;
    DateTime endTime;
    Session newSession;

    private void Start()
    {
        CreateJSON();
        startTime = DateTime.Now.ToUniversalTime();
        newSession.startTime = startTime.ToString();
        newSession.moduleName = ModuleName;
        newSession.version = Version;
        newSession.platform = "Desktop";
        newSession.userName = PlayerPrefs.GetString("username");
        CreateDirectory();
    }

    private void CreateDirectory()                                              // Creates Directory if required
    {
        string path = Application.persistentDataPath + "/" + newSession.userName;
        if (Directory.Exists(path))
        {
            path += "/" + ModuleName;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
        else
        {
            path += "/" + ModuleName;
            Directory.CreateDirectory(path);
        }
    }

    private void CreateJSON()
    {
        newSession = new Session();
    }

    // Update is called once per frame
    void FixedUpdate()                                                                  //Adds position, orientation at a time difference
    {
        if(deltatime < 0f)
        {
            playerPosition.Add(Camera.main.transform.position);
            playerOrientation.Add(Camera.main.transform.rotation);
            timeStamp.Add(time);
            deltatime = 0.5f;
        }
        time = time + Time.deltaTime;
        deltatime -= Time.deltaTime;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))                                    // Counts left clicks
            newSession.clickCount++;
    }

    private void OnApplicationQuit()
    {
        endTime = DateTime.Now.ToUniversalTime();
        newSession.endTime = endTime.ToString();
        newSession.sessionTime = (endTime - startTime).ToString();
        BuildJSON();
    }

    private void BuildJSON()                // stores player position, orientation and timestamps in json string
    {
        newSession.heatMap = new List<HeatMap>();
        for(int i = 0; i < timeStamp.Count; i++)
        {
            HeatMap newEntry = new HeatMap();
            newEntry.playerPosition = playerPosition[i];                            
            newEntry.playerOrientation = playerOrientation[i];
            newEntry.timeStamp = timeStamp[i];
            newSession.heatMap.Add(newEntry);
        }
        string jsonString = JsonUtility.ToJson(newSession);
        WriteDataToFile(jsonString);
    }

    public void WriteDataToFile(string jsonString)
    {
        string path = Application.persistentDataPath + "/" + newSession.userName +  "/" + ModuleName + "/HeatMap_Session_" + startTime.ToString("yyyy-MM-dd-HH-mm-ss") + ".json";
        File.WriteAllText(path, jsonString);
        //#if UNITY_EDITOR
        //    UnityEditor.AssetDatabase.Refresh();
        //#endif
    }

}

[Serializable]
public class Session
{
    public string userName;
    public string moduleName;
    public string platform;
    public string version;
    public string startTime;
    public string endTime;
    public string sessionTime;
    public int clickCount = 0;
    public List<HeatMap> heatMap;
}

[Serializable]
public class HeatMap
{
    public Vector3 playerPosition;
    public Quaternion playerOrientation;
    public float timeStamp;
}
