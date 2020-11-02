using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
/// <summary>
/// Used for setting skybox material and light environment. Attach this script to any gameobject.
/// </summary>
public class SkyboxSettings : MonoBehaviour
{
    /// <summary>
    /// The skybox material
    /// </summary>
    public Material skyboxMaterial;
    /// <summary>
    /// Is skybox the source of light?
    /// </summary>
    public bool isLightSourceSkybox;
    /// <summary>
    /// The light source color, set this if the source isn't a skybox
    /// </summary>
    public Color lightSourceColor;
    /// <summary>
    /// The light source (direction light of the scene)
    /// </summary>
    public Light lightSource;

    /// <summary>
    /// Should set texture for the skybox material
    /// </summary>
    public bool setTexture;
    /// <summary>
    /// Should set shader for the skybox material
    /// </summary>
    public bool setShader = true;
    /// <summary>
    /// The render texture reference, if this is a video skybox
    /// </summary>
    public RenderTexture renderTexture;
    /// <summary>
    /// The shader path. Make sure the shader for skybox is included in the platform app.
    /// </summary>
    public string SkyboxShader = "Skybox/Panoramic";
    /// <summary>
    /// default shader if SkyboxShader is empty or not found
    /// </summary>
    private readonly string defaultFallbackShader = "Skybox/Procedural";

    /// <summary>
    /// Set the skybox material and light environment for the scene
    /// </summary>
    public void Awake()
    {
        if (skyboxMaterial != null)
        {
            SetSkyboxShader();
        }
        if (lightSource != null)
        {
            RenderSettings.sun = lightSource;
        }
        if (!isLightSourceSkybox)
        {
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
            RenderSettings.ambientLight = lightSourceColor;
        }
        else
        {
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Skybox;
        }
    }
    /// <summary>
    /// Set the skybox shader to the given shader.
    /// </summary>
    public void SetSkyboxShader()
    {
        Shader shader = Shader.Find(SkyboxShader) == null ? Shader.Find(defaultFallbackShader) : Shader.Find(SkyboxShader);
        skyboxMaterial.shader = shader;
        Debug.Log(SkyboxShader+":"+ Shader.Find(SkyboxShader));
        Debug.Log("shader " + shader+":"+ skyboxMaterial.shader);

        if (Application.platform == RuntimePlatform.Android)
        {
            if (setTexture && renderTexture != null)
            {
                Debug.Log("material " + renderTexture.name);
                skyboxMaterial.SetTexture("_MainTex", renderTexture);
            }
        }
        RenderSettings.skybox = skyboxMaterial;
    }
}
