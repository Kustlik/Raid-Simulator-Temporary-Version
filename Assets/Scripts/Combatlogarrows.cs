using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combatlogarrows : MonoBehaviour
{
    [SerializeField] StartSimulationEvent simulation;
    [SerializeField] bool Upfunctionactive;
    [SerializeField] bool Downfunctionactive;

    void OnMouseOver()
    {
        GetComponent<Transform>().localScale = new Vector3(0.2217508f, 0.1219208f, 0);
    }

    void OnMouseExit()
    {
        GetComponent<Transform>().localScale = new Vector3(0.1903767f, 0.104671f, 0);
    }

    void OnMouseDown()
    {
        if (Upfunctionactive == true)
        {
            simulation.combatlogMinValue--;
            simulation.combatlogMaxValue--;
            simulation.ManageSurviTable();
            Debug.Log(simulation.combatlogMinValue);
            Debug.Log(simulation.combatlogMaxValue);
        }
        else if(Downfunctionactive == true)
        {
            simulation.combatlogMinValue++;
            simulation.combatlogMaxValue++;
            simulation.ManageSurviTable();
            Debug.Log(simulation.combatlogMinValue);
            Debug.Log(simulation.combatlogMaxValue);
        }
    }
}
