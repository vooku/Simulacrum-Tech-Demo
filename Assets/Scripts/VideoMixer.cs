using UnityEngine;

public class VideoMixer : MonoBehaviour
{
    public Material material;

    ChokeLayer[] chokeLayers;
    bool[] active;

    void Start()
    {
        chokeLayers = GetComponents<ChokeLayer>();
        active = new bool[chokeLayers.Length];
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        int activity = 0;
        for (int i=0; i<chokeLayers.Length; i++)
        {
            material.SetTexture("_Layer" + i.ToString(), chokeLayers[i].GetActiveTexture(ref active[i]));
            activity += active[i] ? (1 << i) : 0;
        }
        material.SetInt("_Activity", activity);

        Graphics.Blit(null, dest, material);
    }
}