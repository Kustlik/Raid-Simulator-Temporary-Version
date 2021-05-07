using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickOnPlayer : MonoBehaviour
{
    [SerializeField] PlayerMenu currentDatabase;
    [SerializeField] PlayerMenu exportDatabase;
    [SerializeField] GameObject lightenBar;
    [SerializeField] int playerPosition;
    int exportposition = 0;
    public PlayerData[] players;
    public PlayerData playerToExport;
    RaidRoosterCounter raidRoosterCounter;

    void OnMouseOver()
    {
        players = currentDatabase.ReturnPlayerData();
        if ((players[playerPosition] != null) && ((currentDatabase.playerNick[playerPosition].text != "")))
        {
            lightenBar.GetComponent<Image>().color = new Color32(255, 255, 255, 80);
        }
    }

    void OnMouseExit()
    {
        players = currentDatabase.ReturnPlayerData();
        if (players[playerPosition] != null)
        {
            lightenBar.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        }
    }

    void OnMouseDown()
    {
        if((exportDatabase.players[exportDatabase.players.Length - 1] == null) && (currentDatabase.playerNick[playerPosition].text != ""))
        {
            players = currentDatabase.ReturnPlayerData();
            playerToExport = players[playerPosition];
            players[playerPosition] = null;
            if (currentDatabase.playerRole.Length == 0)
            {
                currentDatabase.SortPlayersByRole();
            }
            else
            {
                currentDatabase.SortPlayers();
            }
            currentDatabase.ManagePlayers();

            while((exportDatabase.players[exportposition] != null) && (exportposition < exportDatabase.players.Length))
            {
                exportposition++;
            }
            exportDatabase.players[exportposition] = playerToExport;
            exportposition = 0;

            if (exportDatabase.playerRole.Length == 0)
            {
                exportDatabase.SortPlayersByRole();
            }
            else
            {
                exportDatabase.SortPlayers();
            }
            exportDatabase.ManagePlayers();
            lightenBar.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        }
        //    GetComponent<Transform>().localScale = new Vector3(0.74f, 0.74f, 1);
    }
}
