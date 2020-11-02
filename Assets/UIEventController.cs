using UnityEngine;
using UnityEngine.Events;

public class TextureEvent : UnityEvent<Texture2D> { }

public static class UIEventController
{
    /* Called from Module.
     * Check if the event is null before invoking.
     * This will be registered from the platform.
     */
    public static UnityEvent OnModuleResume;
    public static TextureEvent OnModulePause;


    /* Called From Platform - Make sure you register your method for callback.
     * Note: You can also register multiple methods.
     */
    public static UnityEvent OnModuleQuit;
}
