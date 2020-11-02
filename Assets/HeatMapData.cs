using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.XR;

namespace HeatMap
{
    public class HeatMapData : MonoBehaviour
    {

        public static HeatMapData Instance;
        [SerializeField] string moduleName = "default";
        [SerializeField] string version = "0";
        List<Vector3> playerPosition = new List<Vector3>();
        List<Quaternion> playerOrientation = new List<Quaternion>();
        List<float> timeStamp = new List<float>();
        float time = 0f;
        float deltatime = 0.5f;
        DateTime startTime;
        DateTime endTime;
        Session newSession;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            UIEventController.OnModuleQuit?.AddListener(OnModuleQuit);
        }

        private void Start()
        {
            CreateJSON();
            startTime = DateTime.Now.ToUniversalTime();
            newSession.startTime = startTime.ToString();
            newSession.moduleName = moduleName;
            newSession.version = version;
            if (XRSettings.enabled)
                newSession.platform = "VR";
            else
                newSession.platform = "Desktop";

            newSession.userName = PlayerPrefs.HasKey("username") ? PlayerPrefs.GetString("username") : "admin";
            CreateDirectory();
        }

        private void CreateDirectory()                                              // Creates Directory if required
        {
            string path = Application.persistentDataPath + "/" + newSession.userName;
            if (Directory.Exists(path))
            {
                path += "/" + moduleName;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
            else
            {
                path += "/" + moduleName;
                Directory.CreateDirectory(path);
            }
        }

        private void CreateJSON()
        {
            newSession = new Session();
        }

        void FixedUpdate()
        {
            if (deltatime < 0f)                                                  // Stores player positions every 
            {
                playerPosition.Add(Camera.main.transform.position);
                playerOrientation.Add(Camera.main.transform.rotation);
                timeStamp.Add(time);
                deltatime = 0.5f;
            }
            time = time + Time.deltaTime;
            deltatime -= Time.deltaTime;
        }

        public void clickCountIncrement()               // Increments click count
        {
            newSession.clickCount++;
        }

        //private void OnApplicationQuit()                // Stores Session end details and calls BuildJson
        //{
        //    EndSession();
        //}

        private void OnModuleQuit()
        {
            EndSession();
        }

        public void EndSession()
        {
            endTime = DateTime.Now.ToUniversalTime();
            newSession.endTime = endTime.ToString();
            newSession.sessionTime = (endTime - startTime).ToString();
            BuildJSON();
        }

        private void BuildJSON()        //Creates the json string compiling together the heatmap data
        {
            newSession.heatMap = new List<HeatMap>();
            for (int i = 0; i < timeStamp.Count; i++)
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

        public void WriteDataToFile(string jsonString)              //Writes json string to the jsonfile
        {
            string path = Application.persistentDataPath + "/" + newSession.userName + "/" + moduleName + "/HeatMap_Session_" + startTime.ToString("yyyy-MM-dd-HH-mm-ss") + ".json";
            File.WriteAllText(path, jsonString);
        }

    }


    [Serializable]
    public class Session
    {
        public string userName;
        public string moduleName;
        public string version;
        public string platform;
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
}
