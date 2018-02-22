using UnityEngine;
using UnityEngine.Video;
using MidiJack;

public class VideoMixer : MonoBehaviour
{
    public Material material;

    ChokeLayer[] chokeLayers;

    void Start()
    {
        chokeLayers = GetComponents<ChokeLayer>();
        print(chokeLayers.Length);
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
        material.SetTexture("_Layer1", chokeLayers[0].GetActiveTexture());
        material.SetTexture("_Layer2", chokeLayers[1].GetActiveTexture());

        Graphics.Blit(null, dest, material);
    }
}