using UnityEngine;
using UnityEngine.Video;
using MidiJack;

public class VideoMixer : MonoBehaviour
{
    public ChokeLayer chLayer = new ChokeLayer();

    public VideoClip bgClip;
    public VideoClip[] clips;
    public int[] notes;
    public Material material;
    public Material emptyMaterial;

    VideoPlayer[] players;
    VideoPlayer bgPlayer;

    RenderTexture renderTexture;
    RenderTexture bgTexture;

    void Start()
    {
        if (notes.Length != clips.Length)
        {
            Debug.Log("Error: Clip and note counts mismatch.");
        }

        players = new VideoPlayer[clips.Length];
        renderTexture = new RenderTexture(Screen.width, Screen.height, 16, RenderTextureFormat.ARGB32);
        renderTexture.Create();
        material.SetTexture("_Layer2", renderTexture);


        bgPlayer = new VideoPlayer();
        bgTexture = new RenderTexture(Screen.width, Screen.height, 16, RenderTextureFormat.ARGB32);
        bgTexture.Create();
        material.SetTexture("_Layer1", bgTexture);

        for (int i = 0; i < 1/*clips.Length*/; i++)
        {
            players[i] = gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();
            players[i].playOnAwake = true;
            players[i].isLooping = true;
            players[i].renderMode = VideoRenderMode.RenderTexture;
            players[i].targetTexture = renderTexture;
            players[i].clip = clips[i];
            players[i].Prepare();
        }

        bgPlayer = gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();
        bgPlayer.playOnAwake = true;
        bgPlayer.isLooping = true;
        bgPlayer.renderMode = VideoRenderMode.RenderTexture;
        bgPlayer.targetTexture = bgTexture;
        bgPlayer.clip = bgClip;
        bgPlayer.Prepare();
    }

    void Update()
    {
        //Graphics.Blit(null, renderTexture, emptyMaterial);

        //for (int i = 0; i < clips.Length; i++)
        //{
        //    if (MidiMaster.GetKeyDown(notes[i]))
        //    {
        //        Debug.Log(Time.frameCount + " " + notes[i]);
        //        players[i].Play();
        //        players[i].targetCameraAlpha = 1;
        //    }
        //    else if (MidiMaster.GetKeyUp(notes[i]))
        //    {
        //        players[i].Pause();
        //        players[i].targetCameraAlpha = 0;
        //    }
        //}
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(null, dest, material);
    }
}