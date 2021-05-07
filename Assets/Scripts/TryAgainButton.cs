using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TryAgainButton : MonoBehaviour
{
    [SerializeField] StartSimulationEvent startSimulation;
    [SerializeField] Image tryAgainIcon;
    [SerializeField] PlayerMenu raidData;

    void OnMouseOver()
    {
        GetComponent<Transform>().localScale = new Vector3(0.8f, 0.8f, 1);
        tryAgainIcon.GetComponent<Transform>().localScale = new Vector3(2.0f, 2.0f, 1);
    }

    void OnMouseExit()
    {
        GetComponent<Transform>().localScale = new Vector3(0.74f, 0.74f, 1);
        tryAgainIcon.GetComponent<Transform>().localScale = new Vector3(1.8012f, 1.8012f, 1);
    }

    void OnMouseDown()
    {
        startSimulation.StartSim();
    }
}
