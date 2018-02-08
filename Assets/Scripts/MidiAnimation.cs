using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;

public class MidiAnimation : MonoBehaviour {

    void Update()
    {
        if (MidiMaster.GetKey(36) > 0)
        {
            GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 0, 0));
        }
        else if (MidiMaster.GetKey(38) > 0)
        {
            GetComponent<Renderer>().material.SetColor("_Color", new Color(0, 1, 0));
        }
        else
        {
            GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 1, 1));
        }
    }
}
