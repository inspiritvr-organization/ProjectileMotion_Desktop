using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class HeatmapData : MonoBehaviour
{
    [SerializeField] string ModuleName;
    [SerializeField] string Version;
    [SerializeField] string dataPath;
    List<Vector3> playerPosition = new List<Vector3>();
    List<Quaternion> playerOrientation = new List<Quaternion>();
    List<float> timeStamp = new List<float>();
    float time = 0f;
    float deltatime = 0.5f;
    DateTime startTime;
    DateTime endTime;
    Data data;
    Session newSession;

    private void Start()
    {
        if (FileExists())
            ReadJSON();
        else CreateJSON();
        //newSession = new Session();
        startTime = DateTime.Now;
        newSession.startTime = startTime.ToString();
        newSession.ModuleName = ModuleName;
        newSession.Version = Version;
        
    }

    private bool FileExists()
    {
        string path = Application.dataPath + "/StreamingAssets/JSONFiles/" + ModuleName + "_HeatMap.json";
        if (!File.Exists(path))
            return false;
        else return true;
    }

    private void CreateJSON()
    {
        data = new Data();
        data.session = new List<Session>();
        newSession = new Session();
        newSession.sessionId = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
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
        if (Input.GetMouseButtonDown(0))
            newSession.clickCount++;
    }

    private void OnApplicationQuit()
    {
        endTime = DateTime.Now;
        newSession.endTime = endTime.ToString();
        newSession.sessionTime = (endTime - startTime).ToString();
        BuildJSON();
    }

    private bool ReadJSON()
    {
        string path = Application.dataPath + "/StreamingAssets/JSONFiles/" + ModuleName + "_HeatMap.json";
        string jsonString = File.ReadAllText(path);
        data = JsonUtility.FromJson<Data>(jsonString);
        newSession = new Session();
        newSession.sessionId = data.session[data.session.Count - 1].sessionId + 1;
        return true;
    }

    private void BuildJSON()
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
        data.session.Add(newSession);
        string jsonString = JsonUtility.ToJson(data);
        WriteDataToFile(jsonString);
    }

    public void WriteDataToFile(string jsonString)
    {
        string path = Application.dataPath + "/StreamingAssets/JSONFiles/" + ModuleName + "_HeatMap.json";
        Debug.Log("AssetPath:" + path);
        File.WriteAllText(path, jsonString);
        #if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
        #endif
    }

}
[Serializable]
public class Data
{
    public List<Session> session;
}

[Serializable]
public class Session
{
    public string ModuleName;
    public string Version;
    public int sessionId;
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
