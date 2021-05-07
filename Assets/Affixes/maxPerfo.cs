﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class maxPerfo : MonoBehaviour
{
    [SerializeField] StartSimulationEvent simulationFile;
    [SerializeField] GameObject lightenBar;
    [SerializeField] GameObject chooseAffix;
    [SerializeField] GameObject affixtoDisable;

    void OnMouseOver()
    {
        lightenBar.GetComponent<Image>().color = new Color32(255, 255, 255, 80);
    }

    void OnMouseExit()
    {
        lightenBar.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }

    void OnMouseDown()
    {
        lightenBar.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        if (chooseAffix.activeInHierarchy == true)
        {
            chooseAffix.SetActive(false);
            simulationFile.Affix[0] = false;

        }
        else if (chooseAffix.activeInHierarchy == false)
        {
            chooseAffix.SetActive(true);
            simulationFile.Affix[0] = true;
            affixtoDisable.SetActive(false);
            simulationFile.Affix[1] = false;
        }
        simulationFile.ResetAffixBar();
    }
}
