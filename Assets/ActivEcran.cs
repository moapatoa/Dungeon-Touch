using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivEcran : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i < Math.Min(Display.displays.Length, 2); i++) // active les écrans, 2 max si on en a plus que 2 (en l'occurence les 2 premiers dans la liste - c'est défini dans windows)
        {
            Display.displays[i].Activate(Screen.currentResolution.width,
            Screen.currentResolution.height,
            Screen.currentResolution.refreshRateRatio);
            // ajuste les paramètres automatiquement à ceux de l'écran considéré
        }
    }
}
