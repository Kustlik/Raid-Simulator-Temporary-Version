using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickStart : MonoBehaviour
{
    [SerializeField] StartSimulationEvent startSimulation;
    [SerializeField] PlayerMenu raidData;
    [SerializeField] GameObject hideMenu;
    [SerializeField] GameObject nextMenu;
    [SerializeField] Text hintWarning;
    [SerializeField] RaidRoosterCounter requirementsData;

    void Update()
    {
        RequirementCheck();
    }

    void OnMouseOver()
    {
        GetComponent<Transform>().localScale = new Vector3(0.9f, 0.9f, 1);
    }

    void OnMouseExit()
    {
        GetComponent<Transform>().localScale = new Vector3(0.74f, 0.74f, 1);
    }

    void OnMouseDown()
    {
        if(FullRaidCheck())
        {
            GetComponent<Transform>().localScale = new Vector3(0.74f, 0.74f, 1);
            hideMenu.SetActive(false);
            nextMenu.SetActive(true);
            startSimulation.StartSim();
        }
        else
        {
            hintWarning.GetComponent<Text>().color = new Color32(255, 0, 0, 255);
            hintWarning.text = "Raid Rooster does not meet the requirements. Check your players.";
        }
    }

    void RequirementCheck()
    {
        if (FullRaidCheck() && TanksCheck() && HealersCheck())
        {
            GetComponent<TextMesh>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            GetComponent<TextMesh>().color = new Color32(135, 135, 135, 135);
        }
    }

    private bool FullRaidCheck()
    {
        if (raidData.players[19] != null)
        {
            return (true);
        }
        else
        {
            return (false);
        }
    }

    private bool HealersCheck()
    {
        if (requirementsData.healerReq.GetComponent<Text>().color == new Color32(255, 255, 255, 255))
        {
            return (true);
        }
        else
        {
            return (false);
        }
    }

    private bool TanksCheck()
    {
        if (requirementsData.tankReq.GetComponent<Text>().color == new Color32(255, 255, 255, 255))
        {
            return (true);
        }
        else
        {
            return (false);
        }
    }
}
