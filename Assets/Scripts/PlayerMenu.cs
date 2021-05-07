using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class PlayerMenu : MonoBehaviour
{
    [SerializeField] public PlayerData[] players;
    [SerializeField] Image[] playerBars;
    [SerializeField] Image[] playerClass;
    [SerializeField] public Image[] playerRole;
    [SerializeField] public Text[] playerNick;
    [SerializeField] public RaidRoosterCounter raidCounter;
    PlayerData checkPlayer;
    public string roleCheck;
    public string proximityCheck;

    public string howManyHealers;
    public string howManyTanks;

    Image colorTmp;
    Image classTmp;
    Image roleTmp;

    public Sprite hpBarImg;
    public Sprite mageImg;
    public Sprite priestImg;
    public Sprite warlockImg;
    public Sprite druidImg;
    public Sprite demonRetardImg;
    public Sprite monkImg;
    public Sprite rogueImg;
    public Sprite shamanImg;
    public Sprite hunterImg;
    public Sprite deathKnightImg;
    public Sprite warriorImg;
    public Sprite paladinImg;
    public Sprite tankImg;
    public Sprite healerImg;

    public void Start()
    {
        if (playerRole.Length == 0)
        {
            SortPlayersByRole();
        }
        else
        {
            SortPlayers();
        }
        ManagePlayers();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public PlayerData[] ReturnPlayerData()
    {
        return players;
    }

    public void ManagePlayers()
    {
        for (int index = 0; (index < players.Length) && (index <= 31); index++)
        {
            if ((players[index] != null) && (playerRole.Length > 0) && (index <= 31))
            {
                checkPlayer = players[index];
                playerNick[index].text = checkPlayer.GetPlayerName();
                playerNick[index].GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                colorTmp = playerBars[index];
                classTmp = playerClass[index];
                roleTmp = playerRole[index];
                GetRoleIcon();
                colorTmp.GetComponent<Image>().sprite = hpBarImg;
                GetPlayerBarColor();
            }

            else if ((players[index] != null) && (playerRole.Length == 0) && (players[index].GetPlayerRole() == roleCheck) && (players[index].GetPlayerProximity() == proximityCheck) && (index <= 31))
            {
                checkPlayer = players[index];
                playerNick[index].text = checkPlayer.GetPlayerName();
                playerNick[index].GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                colorTmp = playerBars[index];
                classTmp = playerClass[index];
                colorTmp.GetComponent<Image>().sprite = hpBarImg;
                GetPlayerBarColor();
            }

            else
            {
                playerNick[index].text = "";
                colorTmp = playerBars[index];
                colorTmp.GetComponent<Image>().color = new Color32(135, 135, 135, 030);
                colorTmp.GetComponent<Image>().sprite = null;
                classTmp = playerClass[index];
                classTmp.GetComponent<Image>().color = new Color32(135, 135, 135, 030);
                classTmp.GetComponent<Image>().sprite = null;
                if (playerRole.Length > 0)
                {
                    roleTmp = playerRole[index];
                    roleTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
                    roleTmp.GetComponent<Image>().sprite = null;
                }
            }
        }
        if (raidCounter != null)
        {
            raidCounter.healerReq.text = "Healer " + howManyHealers;
            raidCounter.tankReq.text = "Tank " + howManyTanks;

            raidCounter.classCounter();
        }
    }

    public void SortPlayers()
    {
        int activeCheck = 0;
        int index = 0;
        PlayerData tempPlayerPlace;
        string[] classSortingOrder = new string[] { "Death Knight", "Druid", "Hunter", "Mage", "Monk", "Paladin", "Priest", "Rogue", "Shaman", "Warlock", "Warrior", "Demon Retard" };
        string[] roleSortingOrder = new string[] { "Tank", "Dps", "Healer" };

        for (int roleSort = 0; roleSort < roleSortingOrder.Length; roleSort++)
        {
            for (int classSort = 0; classSort < classSortingOrder.Length; classSort++)
            {
                for (int nameSort = 32; nameSort < 255; nameSort++)
                {
                    while (index < players.Length)
                    {
                        while ((players[index] == null) && (index < players.Length))
                        {
                            index++;
                            if (index >= players.Length)
                            {
                                break;
                            }
                        }

                        if (index >= players.Length)
                        {
                            break;
                        }

                        string playerNick = players[index].GetPlayerName();

                        if ((players[index].GetPlayerClass() == classSortingOrder[classSort]) && (playerNick[0] == nameSort) && (players[index].GetPlayerRole() == roleSortingOrder[roleSort]))
                        {
                            checkPlayer = players[index];
                            if (players[activeCheck] != null)
                            {
                                tempPlayerPlace = players[activeCheck];
                                players[activeCheck] = players[index];
                                players[index] = tempPlayerPlace;
                                activeCheck++;
                            }
                            else if (players[activeCheck] == null)
                            {
                                players[activeCheck] = players[index];
                                players[index] = null;
                                activeCheck++;
                            }
                        }
                        index++;
                    }
                    index = 0;
                }
            }
        }
    }

    public void SortPlayersByRole()
    {
        int activeCheck = 0;
        int index = 0;
        PlayerData tempPlayerPlace;
        string[] classSortingOrder = new string[] { "Death Knight", "Druid", "Hunter", "Mage", "Monk", "Paladin", "Priest", "Rogue", "Shaman", "Warlock", "Warrior", "Demon Retard" };

        for (int classSort = 0; classSort < classSortingOrder.Length; classSort++)
        {
            for (int nameSort = 32; nameSort < 255; nameSort++)
            {
                while (index < players.Length)
                {
                    while ((players[index] == null) && (index < players.Length))
                    {
                        index++;
                        if (index >= players.Length)
                        {
                            break;
                        }
                    }

                    if (index >= players.Length)
                    {
                        break;
                    }

                    string playerNick = players[index].GetPlayerName();

                    if ((players[index].GetPlayerClass() == classSortingOrder[classSort]) && (playerNick[0] == nameSort) && (players[index].GetPlayerRole() == roleCheck) && (players[index].GetPlayerProximity() == proximityCheck))
                    {
                        checkPlayer = players[index];
                        if (players[activeCheck] != null)
                        {
                            tempPlayerPlace = players[activeCheck];
                            players[activeCheck] = players[index];
                            players[index] = tempPlayerPlace;
                            activeCheck++;
                        }
                        else if (players[activeCheck] == null)
                        {
                            players[activeCheck] = players[index];
                            players[index] = null;
                            activeCheck++;
                        }
                    }
                    index++;
                }
                index = 0;
            }
        }
    }

    private void GetPlayerBarColor()
    {
        if (checkPlayer.GetPlayerClass() == "Mage")
        {
            colorTmp.GetComponent<Image>().color = new Color32(105, 204, 240, 255);
            classTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            classTmp.GetComponent<Image>().sprite = mageImg;
        }

        else  if (checkPlayer.GetPlayerClass() == "Warlock")
        {
            colorTmp.GetComponent<Image>().color = new Color32(148, 130, 201, 255);
            classTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            classTmp.GetComponent<Image>().sprite = warlockImg;
        }

        else if (checkPlayer.GetPlayerClass() == "Priest")
        {
            colorTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            classTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            classTmp.GetComponent<Image>().sprite = priestImg;
        }

        else if (checkPlayer.GetPlayerClass() == "Druid")
        {
            colorTmp.GetComponent<Image>().color = new Color32(255, 125, 010, 255);
            classTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            classTmp.GetComponent<Image>().sprite = druidImg;
        }

        else if (checkPlayer.GetPlayerClass() == "Rogue")
        {
            colorTmp.GetComponent<Image>().color = new Color32(255, 245, 105, 255);
            classTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            classTmp.GetComponent<Image>().sprite = rogueImg;
        }

        else if (checkPlayer.GetPlayerClass() == "Monk")
        {
            colorTmp.GetComponent<Image>().color = new Color32(000, 255, 150, 255);
            classTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            classTmp.GetComponent<Image>().sprite = monkImg;
        }

        else if (checkPlayer.GetPlayerClass() == "Demon Retard")
        {
            colorTmp.GetComponent<Image>().color = new Color32(163, 048, 201, 255);
            classTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            classTmp.GetComponent<Image>().sprite = demonRetardImg;
        }

        else if (checkPlayer.GetPlayerClass() == "Shaman")
        {
            colorTmp.GetComponent<Image>().color = new Color32(000, 112, 222, 255);
            classTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            classTmp.GetComponent<Image>().sprite = shamanImg;
        }

        else if (checkPlayer.GetPlayerClass() == "Hunter")
        {
            colorTmp.GetComponent<Image>().color = new Color32(171, 212, 115, 255);
            classTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            classTmp.GetComponent<Image>().sprite = hunterImg;
        }

        else if (checkPlayer.GetPlayerClass() == "Death Knight")
        {
            colorTmp.GetComponent<Image>().color = new Color32(196, 031, 059, 255);
            classTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            classTmp.GetComponent<Image>().sprite = deathKnightImg;
        }

        else if (checkPlayer.GetPlayerClass() == "Warrior")
        {
            colorTmp.GetComponent<Image>().color = new Color32(199, 156, 110, 255);
            classTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            classTmp.GetComponent<Image>().sprite = warriorImg;
        }

        else if (checkPlayer.GetPlayerClass() == "Paladin")
        {
            colorTmp.GetComponent<Image>().color = new Color32(245, 140, 186, 255);
            classTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            classTmp.GetComponent<Image>().sprite = paladinImg;
        }
    }

    private void GetRoleIcon()
    {
        if (checkPlayer.GetPlayerRole() == "Healer")
        {
            roleTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            roleTmp.GetComponent<Image>().sprite = healerImg;
        }
        if (checkPlayer.GetPlayerRole() == "Tank")
        {
            roleTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            roleTmp.GetComponent<Image>().sprite = tankImg;
        }
        if (checkPlayer.GetPlayerRole() == "Dps")
        {
            roleTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
            roleTmp.GetComponent<Image>().sprite = null;
        }

    }

}




















/*
 * 
 * public void SortNames()
    {
        int activeCheck = 0;
        int index = 0;
        PlayerData tempPlayerPlace;
        string[] classSortingOrder = new string[] { "Death Knight", "Druid", "Hunter", "Mage", "Monk", "Paladin", "Priest", "Rogue", "Shaman", "Warlock", "Warrior", "Demon Retard" };
        string[] roleSortingOrder = new string[] { "Tank", "Dps", "Healer" };

        for (int nameSort = 32; nameSort < 255; nameSort++)
        {
            while (index < players.Length)
            {
                while ((players[index] == null) && (index < players.Length))
                {
                    Debug.Log("Found empty player, skip " + players[index] + " " + index);
                    index++;
                    if (index >= players.Length)
                    {
                        break;
                    }
                }

                if (index >= players.Length)
                {
                    break;
                }

                string playerNick = players[index].GetPlayerName();

                if (playerNick[0] == nameSort)
                {
                    checkPlayer = players[index];
                    if (players[activeCheck] != null)
                    {
                        Debug.Log("Player change " + players[index] + " " + index + " with " + players[activeCheck] + " " + activeCheck + " " + nameSort);
                        tempPlayerPlace = players[activeCheck];
                        players[activeCheck] = players[index];
                        players[index] = tempPlayerPlace;
                        activeCheck++;
                        Debug.Log("Player changed succesfully " + players[index] + " " + index + " and " + players[activeCheck] + " " + activeCheck + " " + nameSort);
                    }
                    else
                    {
                        Debug.Log("Player erasing " + players[index] + " " + index + " with " + players[activeCheck] + " " + activeCheck + " " + nameSort);
                        players[activeCheck] = players[index];
                        players[index] = null;
                        activeCheck++;
                        Debug.Log("Player erased succesfully " + players[index] + " " + index + " and " + players[activeCheck] + " " + activeCheck + " " + nameSort);
                    }
                }
                index++;
            }
            index = 0;
        }
    }
 * 
 * 
 * public void SortClasses()
    {
        int activeCheck = 0;
        int index = 0;
        PlayerData tempPlayerPlace;
        string[] classSortingOrder = new string[] { "Death Knight", "Druid", "Hunter", "Mage", "Monk", "Paladin", "Priest", "Rogue", "Shaman", "Warlock", "Warrior", "Demon Retard" };

        for (int classSort = 0; classSort < classSortingOrder.Length; classSort++)
        {
            while (index < players.Length)
            {
                while ((players[index] == null) && (index < players.Length))
                {
                    index++;
                    if (index >= players.Length)
                    {
                        break;
                    }
                }

                if (index >= players.Length)
                {
                    break;
                }

                if (players[index].GetPlayerClass() == classSortingOrder[classSort])
                {
                    checkPlayer = players[index];
                    if (players[activeCheck] != null)
                    {
                        tempPlayerPlace = players[activeCheck];
                        players[activeCheck] = players[index];
                        players[index] = tempPlayerPlace;
                        activeCheck++;
                    }
                    else
                    {
                        players[activeCheck] = players[index];
                        players[index] = null;
                        activeCheck++;
                    }
                }
                index++;
            }
            index = 0;
        }
    }

    public void SortRoles()
    {
        int activeCheck = 0;
        int index = 0;
        PlayerData tempPlayerPlace;
        string[] roleSortingOrder = new string[] { "Tank", "Dps", "Healer" };

        for (int roleSort = 0; roleSort < roleSortingOrder.Length; roleSort++)
        {
           while (index < players.Length)
           {
                while ((players[index] == null) && (index < players.Length))
                {
                    index++;
                    if (index >= players.Length)
                    {
                        break;
                    }
                }

                if (index >= players.Length)
                {
                    break;
                }

                if (players[index].GetPlayerRole() == roleSortingOrder[roleSort])
                {
                    checkPlayer = players[index];
                    if (players[activeCheck] != null)
                    {
                        tempPlayerPlace = players[activeCheck];
                        players[activeCheck] = players[index];
                        players[index] = tempPlayerPlace;
                        activeCheck++;
                    }
                    else
                    {
                        players[activeCheck] = players[index];
                        players[index] = null;
                        activeCheck++;
                    }
                }
                index++;
            }
           index = 0;
        }
    }

    */
