using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerClassChoose : MonoBehaviour
{
    [SerializeField] PlayerMenu playerTable;
    [SerializeField] Image tank;
    [SerializeField] Image mhealer;
    [SerializeField] Image rhealer;
    [SerializeField] Image mdps;
    [SerializeField] Image rdps;
    [SerializeField] string roleSorting;
    [SerializeField] string proximitySorting;

    void OnMouseOver()
    {
        if(GetComponent<Image>().color != new Color32(255, 255, 255, 255))
        {
            GetComponent<Image>().color = new Color32(255, 255, 255, 180);
        }
    }

    void OnMouseExit()
    {
        if (GetComponent<Image>().color != new Color32(255, 255, 255, 255))
        {
            GetComponent<Image>().color = new Color32(255, 255, 255, 125);
        }
    }

    void OnMouseDown()
    {
        if (GetComponent<Image>().color != new Color32(255, 255, 255, 255))
        {
            tank.GetComponent<Image>().color = new Color32(255, 255, 255, 125);
            mhealer.GetComponent<Image>().color = new Color32(255, 255, 255, 125);
            rhealer.GetComponent<Image>().color = new Color32(255, 255, 255, 125);
            mdps.GetComponent<Image>().color = new Color32(255, 255, 255, 125);
            rdps.GetComponent<Image>().color = new Color32(255, 255, 255, 125);
            GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            playerTable.roleCheck = roleSorting;
            playerTable.proximityCheck = proximitySorting;
            playerTable.SortPlayersByRole();
            playerTable.ManagePlayers();
        }
    }
}
