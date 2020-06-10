using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FollowHeatMap : MonoBehaviour
{
    Data data;
    Session newSession;
    [SerializeField] string ModuleName;
    [SerializeField] string dataPath;
    [SerializeField] int SessionID;
    public GameObject trackerPrefab;
    void RecreateSession(int sessionId)
    {
        if( SessionID >= data.session.Count)
        {
            print("Invalid Session ID");
            return;
        }
        newSession = data.session[sessionId];
        for (int i = 0; i < newSession.heatMap.Count - 1; i++) {
            GameObject tracker = Instantiate(trackerPrefab);
            tracker.transform.position = newSession.heatMap[i].playerPosition;
            tracker.transform.rotation = newSession.heatMap[i].playerOrientation;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        if (FileExists())
        {
            ReadJSON();
            RecreateSession(SessionID);
        }
    }
    private bool FileExists()
    {
        string path = Application.dataPath + dataPath + ModuleName + "_HeatMap.json";
        if (!File.Exists(path))
            return false;
        else return true;
    }

    private bool ReadJSON()
    {
        string path = Application.dataPath + dataPath + ModuleName + "_HeatMap.json";
        string jsonString = File.ReadAllText(path);
        data = JsonUtility.FromJson<Data>(jsonString);
        int sessionId = data.session[data.session.Count - 1].sessionId + 1;
        return true;
    }

}


