using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickBoss : MonoBehaviour
{
    [SerializeField] GameObject hideMenu;
    [SerializeField] GameObject nextMenu;
    [SerializeField] BossData bossData;
    [SerializeField] PlayerMenu playerSelectWindow;
    [SerializeField] PlayerMenu playerRoosterWindow;
    [SerializeField] BossMenu exportTo;
    [SerializeField] PlayerMenu exportToPlayers;
    [SerializeField] public TextMesh record;

    void OnMouseOver()
    {
        GetComponent<Transform>().localScale = new Vector3(0.011f, 0.011f, 1);
    }

    void OnMouseExit()
    {
        GetComponent<Transform>().localScale = new Vector3(0.01f, 0.01f, 1);
    }

    void OnMouseDown()
    {
        GetComponent<Transform>().localScale = new Vector3(0.01f, 0.01f, 1);
        System.Array.Clear(playerSelectWindow.players, 0, playerSelectWindow.players.Length);
        System.Array.Clear(playerRoosterWindow.players, 0, playerRoosterWindow.players.Length);

        for (int index = 0; index < bossData.players.Length; index++)
        {
            playerSelectWindow.players[index] = bossData.players[index];
        }
        exportTo.zoneName.text = bossData.zoneName;
        exportTo.bossName.text = bossData.bossName;
        exportTo.bossHpValue = bossData.bossHpValue;
        exportTo.berserkInSeconds = bossData.berserkInSeconds;
        exportTo.causeOfDeath = bossData.causeOfDeath;
        exportTo.benchEvent = bossData.benchEvent;
        exportTo.bossOrder = bossData.bossOrder;
        exportToPlayers.howManyHealers = bossData.howManyHealers;
        exportToPlayers.howManyTanks = bossData.howManyTanks;

        if (record.text == "ALIVE")
        {
            exportTo.record = 0;
        }
        else if (record.text == "DEAD")
        {
            exportTo.record = 1;
        }
        else
        {
            int.TryParse(record.text, out exportTo.record);
        }
        Debug.Log(exportTo.record);

        playerSelectWindow.Start();
        playerRoosterWindow.Start();

        hideMenu.SetActive(false);
        nextMenu.SetActive(true);
    }
}
