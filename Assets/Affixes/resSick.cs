using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resSick : MonoBehaviour
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
            simulationFile.Affix[6] = false;

        }
        else if (chooseAffix.activeInHierarchy == false)
        {
            chooseAffix.SetActive(true);
            simulationFile.Affix[6] = true;
            affixtoDisable.SetActive(false);
            simulationFile.Affix[5] = false;
        }
        simulationFile.ResetAffixBar();
    }
}