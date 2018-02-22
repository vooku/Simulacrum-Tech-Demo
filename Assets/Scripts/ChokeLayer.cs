using UnityEngine;
using UnityEngine.Video;
using MidiJack;

public class ChokeLayer : MonoBehaviour {
    public VideoClip[] clips;
    public int[] notes;

    RenderTexture[] textures;
    VideoPlayer[] players;
    
    void Start()
    {
        if (notes.Length != clips.Length)
        {
            Debug.Log("Error: Clip and note counts mismatch.");
        }

        textures = new RenderTexture[clips.Length];
        textures[0] = new RenderTexture(Screen.width, Screen.height, 16, RenderTextureFormat.ARGB32);
        textures[0].Create();

        players = new VideoPlayer[clips.Length];
        players[0] = gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();
        players[0].playOnAwake = true;
        players[0].isLooping = true;
        players[0].renderMode = VideoRenderMode.RenderTexture;
        players[0].targetTexture = textures[0];
        players[0].clip = clips[0];
        players[0].Prepare();
    }

    public RenderTexture GetActiveTexture()
    {
        return textures[0];
    }
}
