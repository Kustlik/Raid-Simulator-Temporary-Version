using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mirrorImg : MonoBehaviour
{
    [SerializeField] StartSimulationEvent simulationFile;
    [SerializeField] GameObject lightenBar;
    [SerializeField] GameObject chooseAffix;
    [SerializeField] PlayerMenu playerbase;

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
            simulationFile.Affix[9] = false;

        }
        else if (chooseAffix.activeInHierarchy == false)
        {
            chooseAffix.SetActive(true);
            simulationFile.Affix[9] = true;
        }
        simulationFile.ResetAffixBar();
    }
}
