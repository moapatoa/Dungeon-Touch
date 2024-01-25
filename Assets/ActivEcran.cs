using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivEcran : MonoBehaviour
{
    void Start()
    {
        for (int i=0; i < Math.Max(Display.displays.Length,2); i++) // active tous les écrans
        {
            Display.displays[i].Activate(Screen.currentResolution.width,
            Screen.currentResolution.height,
            Screen.currentResolution.refreshRateRatio);
            // ajuste les paramètres automatiquement à ceux de l'écran considéré
        }
    }
}
