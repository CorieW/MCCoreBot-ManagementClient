using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using WebSocketSharp;
using Minecraft;

public class AppController : MonoBehaviour
{
    public static AppController Instance;
    // Each of these will represent which Minecraft versions the management client support.
    public static Dictionary<string, IMinecraftClient> VersionToMCClientDict { get; } = new Dictionary<string, IMinecraftClient>();
    
    public Account Account { get; set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance) 
        {
            Destroy(gameObject);
            // Return as to not load the data back in.
            return;
        }
        else Instance = this;

        Screen.SetResolution(400, 600, FullScreenMode.Windowed);
    }

    private void Start()
    {
        RetrieveMinecraftData();
    }

    private void RetrieveMinecraftData()
    {
        VersionToMCClientDict.Add("1.18.2", new Minecraft.v1_18_2.MinecraftClient());
    }
}
