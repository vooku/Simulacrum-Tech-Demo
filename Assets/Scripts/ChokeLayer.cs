using UnityEngine;
using UnityEngine.Video;
using MidiJack;

public class ChokeLayer : MonoBehaviour {
    public VideoClip[] clips;
    public int[] notes;

    RenderTexture[] textures;
    VideoPlayer[] players;

    RenderTexture activeTexture;
    bool noteActive;
    
    void Start()
    {
        if (notes.Length != clips.Length)
        {
            Debug.Log("Error: Clip and note counts mismatch.");
        }

        textures = new RenderTexture[clips.Length];
        players = new VideoPlayer[clips.Length];
        noteActive = false;

        for (int i = 0; i < clips.Length; i++)
        {
            textures[i] = new RenderTexture((int)(0.5 * Screen.width), (int)(0.5 * Screen.height), 16, RenderTextureFormat.ARGB32);
            textures[i].Create();

            players[i] = gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();
            players[i].playOnAwake = true;
            players[i].isLooping = true;
            players[i].renderMode = VideoRenderMode.RenderTexture;
            players[i].targetTexture = textures[0];
            players[i].clip = clips[0];
            players[i].Prepare();
        }
    }

    void Update()
    {
        for (int i = 0; i < notes.Length; i++)
        {
            if (MidiMaster.GetKeyUp(notes[i]))
            {
                noteActive = false;
            }
        }

        for (int i=0; i<notes.Length; i++)
        {
            if (MidiMaster.GetKeyDown(notes[i]))
            {
                noteActive = true;
                activeTexture = textures[i];
            }
        }
        
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

    public RenderTexture GetActiveTexture(ref bool active)
    {
        active = noteActive;

        return textures[0];
    }
}
