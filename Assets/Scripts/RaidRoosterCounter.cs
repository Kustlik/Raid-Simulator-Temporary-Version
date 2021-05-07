using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RaidRoosterCounter : MonoBehaviour
{
    [SerializeField] public Text mageCounter;
    [SerializeField] public Text priestCounter;
    [SerializeField] public Text warlockCounter;
    [SerializeField] public Text rogueCounter;
    [SerializeField] public Text demonRetardCounter;
    [SerializeField] public Text druidCounter;
    [SerializeField] public Text monkCounter;
    [SerializeField] public Text shamanCounter;
    [SerializeField] public Text hunterCounter;
    [SerializeField] public Text deathKnightCounter;
    [SerializeField] public Text warriorCounter;
    [SerializeField] public Text paladinCounter;

    [SerializeField] public Text tankCounter;
    [SerializeField] public Text mhealerCounter;
    [SerializeField] public Text rhealerCounter;
    [SerializeField] public Text mdpsCounter;
    [SerializeField] public Text rdpsCounter;

    [SerializeField] public Image staminaCounter;
    [SerializeField] public Image apCounter;
    [SerializeField] public Image intCounter;
    [SerializeField] public Image magicdCounter;
    [SerializeField] public Image physdCounter;
    [SerializeField] public Image heroCounter;

    [SerializeField] public Text healerReq;
    [SerializeField] public Text tankReq;

    [SerializeField] PlayerMenu currentDatabase;
    public PlayerData[] players;
    int tankAmountNumber;
    int healerAmountNumber;

    public void classCounter()
    {
        players = currentDatabase.ReturnPlayerData();
        int index = 0;
        int classCount = 0;
        int tankCount = 0;
        int healerMeleeCount = 0;
        int healerRangedCount = 0;
        int dpsMeleeCount = 0;
        int dpsRangedCount = 0;
        string[] classSortingOrder = new string[] { "Death Knight", "Druid", "Hunter", "Mage", "Monk", "Paladin", "Priest", "Rogue", "Shaman", "Warlock", "Warrior", "Demon Retard" };
        string[] roleSortingOrder = new string[] { "Tank", "Dps", "Healer" };
        string[] proximitySortingOrder = new string[] { "Melee", "Ranged"};
        int[] classReturn = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] roleReturn = new int[] { 0, 0, 0 };
        int[] proximityReturn = new int[] { 0, 0 };

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
                    classCount++;

                    if (players[index].GetPlayerRole() == "Tank")
                    {
                        tankCount++;
                    }

                    if ((players[index].GetPlayerProximity() == "Melee") && (players[index].GetPlayerRole() == "Healer"))
                    {
                        healerMeleeCount++;
                    }

                    if ((players[index].GetPlayerProximity() == "Ranged") && (players[index].GetPlayerRole() == "Healer"))
                    {
                        healerRangedCount++;
                    }

                    if ((players[index].GetPlayerProximity() == "Melee") && (players[index].GetPlayerRole() == "Dps"))
                    {
                        dpsMeleeCount++;
                    }

                    if ((players[index].GetPlayerProximity() == "Ranged") && (players[index].GetPlayerRole() == "Dps"))
                    {
                        dpsRangedCount++;
                    }
                }

                classReturn[classSort] = classCount;
                index++;
            }
            index = 0;
            classCount = 0;
        }

        deathKnightCounter.text = classReturn[0].ToString();
        druidCounter.text = classReturn[1].ToString();
        hunterCounter.text = classReturn[2].ToString();
        mageCounter.text = classReturn[3].ToString();
        monkCounter.text = classReturn[4].ToString();
        paladinCounter.text = classReturn[5].ToString();
        priestCounter.text = classReturn[6].ToString();
        rogueCounter.text = classReturn[7].ToString();
        shamanCounter.text = classReturn[8].ToString();
        warlockCounter.text = classReturn[9].ToString();
        warriorCounter.text = classReturn[10].ToString();
        demonRetardCounter.text = classReturn[11].ToString();

        tankCounter.text = tankCount.ToString();
        mhealerCounter.text = healerMeleeCount.ToString();
        rhealerCounter.text = healerRangedCount.ToString();
        mdpsCounter.text = dpsMeleeCount.ToString();
        rdpsCounter.text = dpsRangedCount.ToString();

        staminaCounter.GetComponent<Image>().color = new Color32(135, 135, 135, 030);
        apCounter.GetComponent<Image>().color = new Color32(135, 135, 135, 030);
        intCounter.GetComponent<Image>().color = new Color32(135, 135, 135, 030);
        magicdCounter.GetComponent<Image>().color = new Color32(135, 135, 135, 030);
        physdCounter.GetComponent<Image>().color = new Color32(135, 135, 135, 030);
        heroCounter.GetComponent<Image>().color = new Color32(135, 135, 135, 030);

        GetHealerRequirement();
        GetTankRequirement();

        if (healerAmountNumber <= (healerRangedCount + healerMeleeCount))
        {
            healerReq.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            healerReq.GetComponent<Text>().color = new Color32(255, 0, 0, 255);
        }

        if (tankAmountNumber <= tankCount)
        {
            tankReq.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            tankReq.GetComponent<Text>().color = new Color32(255, 0, 0, 255);
        }

        if (classReturn[3] >= 1)
        {
            intCounter.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        if (classReturn[4] >= 1)
        {
            physdCounter.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        if (classReturn[6] >= 1)
        {
            staminaCounter.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        if (classReturn[10] >= 1)
        {
            apCounter.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        if (classReturn[11] >= 1)
        {
            magicdCounter.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        if ((classReturn[2] >= 1) || (classReturn[3] >= 1) || (classReturn[8] >= 1))
        {
            heroCounter.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    public void GetHealerRequirement()
    {
        string healerAmountReq = healerReq.text;
        Debug.Log(healerAmountReq);
        healerAmountNumber = healerAmountReq[7];
        if ((healerAmountNumber >= 48) && (healerAmountNumber <= 57))
        {
            healerAmountNumber = healerAmountNumber - 48;
        }
    }

    public void GetTankRequirement()
    {
        string tankAmountReq = tankReq.text;
        Debug.Log(tankAmountReq);
        tankAmountNumber = tankAmountReq[5];
        if ((tankAmountNumber >= 48) && (tankAmountNumber <= 57))
        {
            tankAmountNumber = tankAmountNumber - 48;
        }
    }

    public int ReturnHealerRequirement()
    {
        return healerAmountNumber;
    }

    public int ReturnTankRequirement()
    {
        return tankAmountNumber;
    }
}
