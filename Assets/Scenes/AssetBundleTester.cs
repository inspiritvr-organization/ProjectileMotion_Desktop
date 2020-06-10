using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AssetBundleTester : MonoBehaviour
{


    [Header("Set Platform Type")]
    [SerializeField] bool isWindows;
    [SerializeField] bool isAndroid;
    [SerializeField] string selectedPlatformType;


    [Space(20)]
    [SerializeField] string assetBundleName;



    private AssetBundle prevLoadedBundle;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if ( isWindows )
        {
            selectedPlatformType = "Windows";
        }
        if (isAndroid)
        {
            selectedPlatformType = "Android";
        }
    }

    // Start is called before the first frame update
    void Start()
    {






    }

    // Update is called once per frame
    void Update()
    {
        
    }




   public void loadAssetBundle()
    {
        StartCoroutine(loadAssetBundleLevelDataWithScene(assetBundleName));
    }


    public void loadEmptyScene()
    {

        preStart();
    }

    public void preStart()
    {
        Scene newScene = SceneManager.CreateScene("MyNewScene");
        SceneManager.SetActiveScene(newScene);

        foreach (Scene x in SceneManager.GetAllScenes())
        {
            if (x.name != "MyNewScene")
            {
                SceneManager.UnloadSceneAsync(x);
            }
        }

        foreach (var x in AssetBundle.GetAllLoadedAssetBundles())
        {
            Debug.LogError(" Assets in memory :  " + x.name);

            x.Unload(true);
        }

        AssetBundle.UnloadAllAssetBundles(true);


        loadStartScene();
    }

    void loadStartScene()
    {
        StartCoroutine(loadAssetBundleLevelDataWithScene("empty_scene"));
    }




    /// <summary>
    ///  send the asset bundle and platform type to load 
    ///  
    /// level name eg: startmenu, dnareplication 
    /// platform type eg: Windows or Android - Pls note - Case Sensitive text
    /// </summary>
    /// <param name="levelName"></param>
    /// <param name="platformType"></param>
    /// 
    /// <returns></returns>
    IEnumerator loadAssetBundleLevelDataWithScene(string assetBundleName)
    {

        var bundleLoadRequest = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, selectedPlatformType, assetBundleName));
        yield return bundleLoadRequest;

        prevLoadedBundle = bundleLoadRequest.assetBundle;
        Debug.Log("Loaded from file succcessfully");

        AsyncOperation async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(bundleLoadRequest.assetBundle.GetAllScenePaths()[0], LoadSceneMode.Single);
        async.allowSceneActivation = false;
        async.completed += Completed;
        while (!async.isDone)
        {
            if (async.progress >= 0.9f)
            {
                async.allowSceneActivation = true;
            }
            yield return new WaitForSeconds(0.5f);
        }
        yield break;

    }

    public void Completed(AsyncOperation obj)
    {
        Debug.Log("%#Complete" + obj.isDone + ":" + obj.progress);
        Debug.Log("%^&Priority" + obj.isDone + ":" + obj.priority);
        Debug.Log("test" + obj);

        prevLoadedBundle.Unload(false);
    }


    IEnumerator load_AssetBundle_DataOnly(string assetBundleName)
    {
        var bundleLoadRequest = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, selectedPlatformType, assetBundleName));
        yield return bundleLoadRequest;
        yield break;

    }



}
