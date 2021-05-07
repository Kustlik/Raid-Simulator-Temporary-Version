using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSimulationEvent : MonoBehaviour
{
    [SerializeField] PlayerMenu raidData;
    [SerializeField] BossMenu bossData;
    [SerializeField] SimBoss simulateBossData;
    [SerializeField] PlayerData[] raidRooster;
    [SerializeField] PlayerData[] raidRoosterDps;
    [SerializeField] PlayerData[] raidRoosterHps;

    [SerializeField] Image[] playerBarsDps;
    [SerializeField] Image[] playerClassDps;
    [SerializeField] public Image[] playerRoleDps;
    [SerializeField] public Text[] playerNickDps;
    [SerializeField] public Text[] dpsPlayerWindow;
    [SerializeField] public Text[] overallDpsPlayerWindow;

    [SerializeField] Image[] playerBarsHps;
    [SerializeField] Image[] playerClassHps;
    [SerializeField] public Image[] playerRoleHps;
    [SerializeField] public Text[] playerNickHps;
    [SerializeField] public Text[] hpsPlayerWindow;
    [SerializeField] public Text[] overallHpsPlayerWindow;

    [SerializeField] public Text[] ripWindow;
    [SerializeField] public Text ripCounter;
    [SerializeField] public Text encounterTimer;
    [SerializeField] public Text berserkTimer;

    [SerializeField] Image minReqDamage;
    [SerializeField] Image totalOverallDamageBg;
    [SerializeField] Image totalDpsBg;

    [SerializeField] Image minReqHealing;
    [SerializeField] Image totalOverallHealingBg;
    [SerializeField] Image totalHpsBg;

    [SerializeField] GameObject CombatlogUp;
    [SerializeField] GameObject CombatlogDown;
    PlayerData playerImport;

    Image colorTmp;
    Image classTmp;
    Image roleTmp;
    int fillindex = 0;
    string abbreviationMark = "";

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
    public Sprite dpsImg;

    public bool[] Affix = new bool[10];
    public Sprite[] AffixIcon = new Sprite[10];
    public GameObject[] AffixBuff = new GameObject[6];
    public GameObject[] AffixBuffCopy = new GameObject[6];
    [SerializeField] GameObject affixInfoBar;
    bool MyFunctionCalled = false;

    int ripindex = 0;
    string[] combatLog;
    int[] combatLogTime;

    int affixIndex;
    float benchValue;
    float benchTime;
    bool[] benchDead = new bool[20];

    float[] surviData;
    float[] avrgSurvi = new float[20];
    float[] randomSurvi = new float[20];
    bool[] isAlive = new bool[20];
    float[] deathTime = new float[20];
    float[] benchdeathTime = new float[20];
    float[] deathValue = new float[20];
    bool[] deadcheck = new bool[20];
    bool[] randomizeDead = new bool[20];
    float[] dpsData;
    float[] hpsData;
    int encounterTime = 0;
    int calculatedEncounterTime = 0;
    int resurrectionCharges = 1;
    int[] ressTime = new int[20];
    string[] resstext = new string[20];

    public int combatlogMinValue = 0;
    public int combatlogMaxValue = 19;

    public float[] randomDps = new float[21];
    public float[] overallDamageData = new float[21];

    public float[] randomHps = new float[21];
    public float[] overallHealingData = new float[21];

    float roundedNumber;

    float reqDamageProgressBar = 0;
    float totalDps = 0;

    float reqHealingProgressBar = 0;
    float totalHps = 0;

    float[] DpsPlaceCopy = new float[20];
    float[] OverallDpsPlaceCopy = new float[20];
    float[] HpsPlaceCopy = new float[20];
    float[] OverallHpsPlaceCopy = new float[20];
    float[] DeathTimerPlaceCopy = new float[20];
    float[] benchDeathTimerPlaceCopy = new float[20];
    bool[] StatusCopy = new bool[20];
    string[] textCopy = new string[20];
    int[] ressTimeCopy = new int[20];
    bool[] deadcheckCopy = new bool[20];
    float[] deathValueCopy = new float[20];
    float[] avrgSurviCopy = new float[20];
    float[] randomSurviCopy = new float[20];
    bool[] randomizeDeadCopy = new bool[20];

    public void StartSim()
    {
        CombatlogDown.SetActive(false);
        CombatlogUp.SetActive(false);

        combatlogMinValue = 0;
        combatlogMaxValue = 19;

        ImportRaidGroup();

        if (Affix[7] == true)
        {
            ToxicHealingAffix();
            ToxicDamageAffix();

            ResetHpsTable();
            ResetDpsTable();

            playerBarsDps[fillindex].fillAmount = 0;
            reqHealingProgressBar += OverallHpsPlaceCopy[fillindex];

            playerBarsHps[fillindex].fillAmount = 0;
            reqDamageProgressBar += overallDamageData[fillindex];

            simulateBossData.bossHpDamaged = bossData.bosscalculatedValue;
            simulateBossData.bossHpDamaged -= OverallHpsPlaceCopy[fillindex];
            totalHps += HpsPlaceCopy[fillindex];
            totalDps += randomDps[fillindex];
            MyFunctionCalled = true;
            simulateBossData.CopyBossData();
            simulateBossData.ManageBoss();
            simulateBossData.ManageHpBar();
        }
        else
        {
            CalculateDps();
            CalculateHps();

            ResetDpsTable();
            ResetHpsTable();

            playerBarsDps[fillindex].fillAmount = 0;
            reqDamageProgressBar += OverallDpsPlaceCopy[fillindex];

            playerBarsHps[fillindex].fillAmount = 0;
            reqHealingProgressBar += overallHealingData[fillindex];

            simulateBossData.bossHpDamaged = bossData.bosscalculatedValue;
            simulateBossData.bossHpDamaged -= OverallDpsPlaceCopy[fillindex];
            totalDps += DpsPlaceCopy[fillindex];
            totalHps += randomHps[fillindex];
            MyFunctionCalled = true;
            simulateBossData.CopyBossData();
            simulateBossData.ManageBoss();
            simulateBossData.ManageHpBar();
        }
    }

    public void ImportRaidGroup()
    {
        for(int index = 0; index < 20; index++)
        {
            playerImport = raidData.players[index];
            raidRooster[index] = playerImport;
        }
    }

    public void CalculateDps()
    {
        System.Array.Clear(overallDamageData, 0, overallDamageData.Length);
        float bossActuallHp = bossData.bosscalculatedValue;
        encounterTime = 0;
        randomDps[20] = 0;
        ripindex = 0;

        combatLog = new string[1];
        combatLogTime = new int[1];
        System.Array.Clear(combatLog, 0, combatLog.Length);
        System.Array.Clear(combatLogTime, 0, combatLogTime.Length);

        for (int index = 0; index < 20; index++)
        {
            if(raidRooster[index] != null)
            {
                dpsData = raidRooster[index].GetDps();
                if ((Affix[0] == false) && (Affix[1] == false))
                {
                    randomDps[index] = dpsData[Random.Range(0, dpsData.Length)];
                }
                else if (Affix[0] == true)
                {
                    randomDps[index] = maxDpsPerfoAffix();
                }
                else if (Affix[1] == true)
                {
                    randomDps[index] = minDpsPerfoAffix();
                }
                randomDps[20] += randomDps[index];
            }
        }

        while ((encounterTime <= bossData.berserkInSeconds) && (bossActuallHp > 0))
        {
            for (int index = 0; index < 20; index++)
            {
                if (raidRooster[index] != null)
                {
                    overallDamageData[index] += randomDps[index];
                    overallDamageData[20] += randomDps[index];
                    bossActuallHp -= randomDps[index];
                }
            }
            if (bossActuallHp <= 0)
            {
                bossActuallHp = 0;
            }
            encounterTime++;
        }

        CalculateSurvi();
        if ((CalculateSurviCheck() == true) || (isBenchActive() == true))
        {
            CalculateDpsAfterSurvi();

            for (int index = 0; index < 20; index++)
            {
                if (isAlive[index] != true)
                {
                      randomDps[index] = overallDamageData[index] / calculatedEncounterTime;
                }
            }
        }

        calculatedEncounterTime = encounterTime;
        encounterTimer.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
        if (calculatedEncounterTime >= 60)
        {
            encounterTimer.text = (calculatedEncounterTime / 60).ToString() + ":" + (calculatedEncounterTime % 60).ToString("00");
            if (encounterTime >= bossData.berserkInSeconds)
            {
                encounterTimer.GetComponent<Text>().color = new Color32(255, 0, 0, 255);
            }
        }
        else
        {
            encounterTimer.text = "0:" + (calculatedEncounterTime % 60).ToString("00");
        }
    
        berserkTimer.text = (bossData.berserkInSeconds / 60).ToString() + ":" + (bossData.berserkInSeconds % 60).ToString("00");

        ManageSurviTable();  
        SortByDps();
    }

    void CalculateDpsAfterSurvi()
    {
        System.Array.Clear(deadcheck, 0, deadcheck.Length);
        System.Array.Clear(benchDead, 0, benchDead.Length);
        System.Array.Clear(benchdeathTime, 0, benchdeathTime.Length);
        System.Array.Clear(randomizeDead, 0, randomizeDead.Length);
        System.Array.Clear(overallDamageData, 0, overallDamageData.Length);
        System.Array.Clear(ressTime, 0, ressTime.Length);
        float bossActuallHp = bossData.bosscalculatedValue;
        calculatedEncounterTime = encounterTime;
        randomDps[20] = 0;
        ripindex = 1;
        int combatLogIndex = 0;
        int benchBreak = 0;
        float benchRandom = 5;
        int tryNumber = HowManyDead() - 1;

        if (tryNumber < 0)
        {
            tryNumber = 0;
            ripindex = 0;
        }

        for (int retryDead = 0; retryDead <= tryNumber; retryDead++)
        {
            Debug.Log("Try number: " + retryDead);
            bossActuallHp = bossData.bosscalculatedValue;
            System.Array.Clear(resstext, 0, resstext.Length);
            System.Array.Clear(ressTime, 0, ressTime.Length);
            System.Array.Clear(overallDamageData, 0, overallDamageData.Length);
            bool encounterDeath = false;
            bool playerDied = false;
            int intDeathTime;
            int intBenchTime = 0;

            if(Affix[5] == true)
            {
                resurrectionCharges = 999;
            }
            else if(Affix[6] == true)
            {
                resurrectionCharges = 0;
            }
            else
            {
                resurrectionCharges = 1;
            }

            if ((Affix[8] == true) && (isBenchActive() == true) && (retryDead == tryNumber))
            {
                benchTime = benchValue * calculatedEncounterTime;
                if (benchTime >= (calculatedEncounterTime - 5))
                {
                    benchTime = calculatedEncounterTime - 6;
                }
                intBenchTime = Mathf.RoundToInt(benchTime);

                if ((combatLogIndex + 1) == combatLog.Length)
                {
                    System.Array.Resize<string>(ref combatLog, combatLog.Length + 1);
                    System.Array.Resize<int>(ref combatLogTime, combatLogTime.Length + 1);
                }
                combatLogTime[combatLogIndex] = intBenchTime;
                combatLog[combatLogIndex] = RandomBenchEvent();
                combatLogIndex++;
            }

            encounterTime = 0;
            while ((encounterTime <= bossData.berserkInSeconds) && (bossActuallHp > 0))
            {
                if (Affix[5] == false && Affix[6] == false)
                {
                    if (((encounterTime % 300) == 0) && (encounterTime != 0))
                    {
                        resurrectionCharges++;
                        Debug.Log("Amount of res charges increased: " + resurrectionCharges + " time " + encounterTime);
                    }
                }

                for (int index = 0; index < 20; index++)
                {
                    if ((encounterDeath == false) && (isAlive[index] == false) && (randomizeDead[index] == false) && (retryDead != 0) && (deadcheck[index] == false))
                    {
                        deathTime[index] = deathValue[index] * calculatedEncounterTime;
                        Debug.Log("death time for player " + index + " will occure" + deathTime[index]);
                        intDeathTime = Mathf.RoundToInt(deathTime[index]);
                        if ((combatLogIndex + 1) == combatLog.Length)
                        {
                            System.Array.Resize<string>(ref combatLog, combatLog.Length + 1);
                            System.Array.Resize<int>(ref combatLogTime, combatLogTime.Length + 1);
                        }
                        combatLogTime[combatLogIndex] = intDeathTime;
                        combatLog[combatLogIndex] = "Player " + raidRooster[index].GetPlayerName() + " died at " + (intDeathTime / 60).ToString() + ":" + (intDeathTime % 60).ToString("00") + ". " + RandomCoD();
                        encounterDeath = true;
                        randomizeDead[index] = true;
                        combatLogIndex++;
                        ripindex++;
                    }
                    if ((raidRooster[index] != null))
                    {
                        if ((isAlive[index] == false) && (deadcheck[index] == false) && (playerDied == false))
                        {
                            deadcheck[index] = true;
                            playerDied = true;
                        }
                        else if ((deadcheck[index] == false) || ((encounterTime >= ressTime[index]) && (ressTime[index] != 0) && (benchdeathTime[index] == 0)))
                        {
                            overallDamageData[index] += randomDps[index];
                            overallDamageData[20] += randomDps[index];
                            bossActuallHp -= randomDps[index];
                        }
                        else if ((encounterTime >= ressTime[index]) && (ressTime[index] != 0) && (benchdeathTime[index] != 0) && (encounterTime <= deathTime[index]))
                        {
                            overallDamageData[index] += randomDps[index];
                            overallDamageData[20] += randomDps[index];
                            bossActuallHp -= randomDps[index];
                        }
                        if ((isAlive[index] == false) && (deadcheck[index] == true) && (encounterTime >= deathTime[index]) && (ressTime[index] == 0) && (benchdeathTime[index] == 0))
                        {
                            if  ((retryDead == 0) && (deadcheck[index] == true) && (encounterTime < (deathTime[index] + 1)))
                            {
                                Debug.Log("death time for player " + index + " will occure" + deathTime[index]);
                                intDeathTime = Mathf.RoundToInt(deathTime[index]);
                                if ((combatLogIndex + 1) == combatLog.Length)
                                {
                                    System.Array.Resize<string>(ref combatLog, combatLog.Length + 1);
                                    System.Array.Resize<int>(ref combatLogTime, combatLogTime.Length + 1);
                                }
                                combatLogTime[combatLogIndex] = intDeathTime;
                                combatLog[combatLogIndex] = "Player " + raidRooster[index].GetPlayerName() + " died at " + (intDeathTime / 60).ToString() + ":" + (intDeathTime % 60).ToString("00") + ". " + RandomCoD();
                                combatLogIndex++;
                            }
                            if ((Affix[8] == false) || (retryDead != tryNumber) || ((Affix[8] == true) && (retryDead == tryNumber) && (encounterTime < (intBenchTime - 15))))
                            {
                                if ((raidRooster[index].GetPlayerClass() != "Shaman") && (resurrectionCharges > 0) && (ressTime[index] == 0))
                                {
                                    ressTime[index] = encounterTime + randomRess();
                                    Debug.Log("ressurection time for player " + index + " will occure" + ressTime[index]);
                                    Debug.Log(encounterTime);
                                    if ((combatLogIndex + 1) == combatLog.Length)
                                    {
                                        System.Array.Resize<string>(ref combatLog, combatLog.Length + 1);
                                        System.Array.Resize<int>(ref combatLogTime, combatLogTime.Length + 1);
                                    }
                                    if (retryDead == tryNumber)
                                    {
                                        combatLogTime[combatLogIndex] = ressTime[index];
                                        combatLog[combatLogIndex] = "Player " + raidRooster[index].GetPlayerName() + " resurrected at " + (ressTime[index] / 60).ToString() + ":" + (ressTime[index] % 60).ToString("00") + ". ";
                                        combatLogIndex++;
                                    }
                                    resurrectionCharges--;
                                }
                                else if ((raidRooster[index].GetPlayerClass() == "Shaman") && (ressTime[index] == 0))
                                {
                                    ressTime[index] = encounterTime + randomRess();
                                    if ((combatLogIndex + 1) == combatLog.Length)
                                    {
                                        System.Array.Resize<string>(ref combatLog, combatLog.Length + 1);
                                        System.Array.Resize<int>(ref combatLogTime, combatLogTime.Length + 1);
                                    }
                                    if (retryDead == tryNumber)
                                    {
                                        combatLogTime[combatLogIndex] = ressTime[index];
                                        combatLog[combatLogIndex] = "Player " + raidRooster[index].GetPlayerName() + " used Reincarnation at " + (ressTime[index] / 60).ToString() + ":" + (ressTime[index] % 60).ToString("00") + ".";
                                        combatLogIndex++;
                                    }
                                }
                            }
                        }
                        if ((encounterTime <= deathTime[index]) && (deadcheck[index] == true) && (benchdeathTime[index] == 0))
                        {
                            overallDamageData[index] += randomDps[index];
                            overallDamageData[20] += randomDps[index];
                            bossActuallHp -= randomDps[index];
                        }
                    }
                }

                if ((Affix[8] == true) && (isBenchActive() == true) && (retryDead == tryNumber) && (intBenchTime <= encounterTime))
                {
                    for (int index = 0; index < 20; index++)
                    {
                        if ((((randomSurvi[index] < benchRandom) && (isAlive[index] == true)) && (benchDead[index] == false)) || ((deathTime[index] >= (benchTime + 5)) && (isAlive[index] == false) && (benchDead[index] == false)) || ((isAlive[index] == false) && (deadcheck[index] == true) && (benchDead[index] == false) && ((ressTime[index] != 0) && (ressTime[index] <= benchTime))))
                        {
                            if (ressTime[index] != 0)
                            {
                                benchdeathTime[index] = deathTime[index];
                            }

                            deathTime[index] = encounterTime;

                            if ((deadcheck[index] == false) || (((deadcheck[index] == true) && (ressTime[index] < benchTime))))
                            {
                                Debug.Log("death time for player " + index + " will occure" + deathTime[index]);
                                intDeathTime = Mathf.RoundToInt(deathTime[index]);
                                if ((combatLogIndex + 1) == combatLog.Length)
                                {
                                    System.Array.Resize<string>(ref combatLog, combatLog.Length + 1);
                                    System.Array.Resize<int>(ref combatLogTime, combatLogTime.Length + 1);
                                }
                                combatLogTime[combatLogIndex] = intDeathTime;   
                                combatLog[combatLogIndex] = "Player " + raidRooster[index].GetPlayerName() + " killed by Bench Lord at " + (intDeathTime / 60).ToString() + ":" + (intDeathTime % 60).ToString("00") + ". ";
                                combatLogIndex++;
                                ripindex++;
                            }

                            randomizeDead[index] = true;
                            benchDead[index] = true;
                            isAlive[index] = false;
                            deadcheck[index] = true;
                            benchBreak++;
                            Debug.Log("Triggered benchBreak for player " + index + " total amount: " + benchBreak);
                        }
                        else if ((deathTime[index] < (benchTime + 5)) && (isAlive[index] == false) && (benchDead[index] == false) && ((ressTime[index] == 0) || (ressTime[index] > benchTime)))
                        {
                            benchDead[index] = true;
                            benchBreak++;
                            Debug.Log("Triggered benchBreak for player " + index + " total amount: " + benchBreak);
                        }
                    }
                    benchRandom = benchRandom * 2;
                }

                if ((bossActuallHp <= 0) && (retryDead == tryNumber))
                {
                    bossActuallHp = 0;
                }

                encounterTime++;

                if (benchBreak == 20)
                {
                    Debug.Log("triggered break");
                    break;
                }
            }
            calculatedEncounterTime = encounterTime;
            Debug.Log("set new encounter time: " + calculatedEncounterTime);
        }

        for (int index = 0; index < 20; index++)
        {
            if (raidRooster[index] != null)
            {
                randomDps[20] += (overallDamageData[index] / calculatedEncounterTime);
            }
        }

        ressCompatibility();
    }

    void SortByDps()
    {
        PlayerData tempPlayerPlace;
        float tempDpsPlace;
        float tempHpsPlace;
        float tempOverallPlace;
        float tempDeathTimerPlace;
        float tempbenchDeathTimerPlace;
        bool tempStatus;
        string temptext;
        int tempress;
        bool tempdeadcheck;
        float tempdeathValue;
        float tempavrgSurvi;
        float temprandomSurvi;
        bool temprandomizeDead;


        for (int index = 0; index < 19; index++)
        {
            while (index < 19)
            {
                string playerNickDps = raidRooster[index].GetPlayerName();

                if (randomDps[index] < randomDps[index+1])
                {
                    tempPlayerPlace = raidRooster[index];
                    raidRooster[index] = raidRooster[index+1];
                    raidRooster[index+1] = tempPlayerPlace;

                    tempDpsPlace = randomDps[index];
                    randomDps[index] = randomDps[index + 1];
                    randomDps[index + 1] = tempDpsPlace;

                    tempOverallPlace = overallDamageData[index];
                    overallDamageData[index] = overallDamageData[index + 1];
                    overallDamageData[index + 1] = tempOverallPlace;

                    tempHpsPlace = randomHps[index];
                    randomHps[index] = randomHps[index + 1];
                    randomHps[index + 1] = tempHpsPlace;

                    tempOverallPlace = overallHealingData[index];
                    overallHealingData[index] = overallHealingData[index + 1];
                    overallHealingData[index + 1] = tempOverallPlace;

                    tempDeathTimerPlace = deathTime[index];
                    deathTime[index] = deathTime[index + 1];
                    deathTime[index + 1] = tempDeathTimerPlace;

                    tempbenchDeathTimerPlace = benchdeathTime[index];
                    benchdeathTime[index] = benchdeathTime[index + 1];
                    benchdeathTime[index + 1] = tempbenchDeathTimerPlace;

                    tempStatus = isAlive[index];
                    isAlive[index] = isAlive[index + 1];
                    isAlive[index + 1] = tempStatus;

                    temptext = resstext[index];
                    resstext[index] = resstext[index + 1];
                    resstext[index + 1] = temptext;

                    tempress = ressTime[index];
                    ressTime[index] = ressTime[index + 1];
                    ressTime[index + 1] = tempress;

                    tempdeadcheck = deadcheck[index];
                    deadcheck[index] = deadcheck[index + 1];
                    deadcheck[index + 1] = tempdeadcheck;

                    tempdeathValue = deathValue[index];
                    deathValue[index] = deathValue[index + 1];
                    deathValue[index + 1] = tempdeathValue;

                    tempavrgSurvi = avrgSurvi[index];
                    avrgSurvi[index] = avrgSurvi[index + 1];
                    avrgSurvi[index + 1] = tempavrgSurvi;

                    temprandomSurvi = randomSurvi[index];
                    randomSurvi[index] = randomSurvi[index + 1];
                    randomSurvi[index + 1] = temprandomSurvi;

                    temprandomizeDead = randomizeDead[index];
                    randomizeDead[index] = randomizeDead[index + 1];
                    randomizeDead[index + 1] = temprandomizeDead;



                    index =-1;         
                }
                index++;                   // Wszystko przekopiowac
            }
        }

        if (Affix[7] == false)
        {
            for (int index = 0; index < 20; index++)
            {
                raidRoosterDps[index] = raidRooster[index];
                DpsPlaceCopy[index] = randomDps[index];
                OverallDpsPlaceCopy[index] = overallDamageData[index]; ;
                HpsPlaceCopy[index] = randomHps[index];
                OverallHpsPlaceCopy[index] = overallHealingData[index];
                DeathTimerPlaceCopy[index] = deathTime[index];
                benchDeathTimerPlaceCopy[index] = benchdeathTime[index];
                StatusCopy[index] = isAlive[index];
                textCopy[index] = resstext[index];
                ressTimeCopy[index] = ressTime[index];
                deadcheckCopy[index] = deadcheck[index];
                deathValueCopy[index] = deathValueCopy[index];
                avrgSurviCopy[index] = avrgSurvi[index];
                randomSurviCopy[index] = randomSurvi[index];
                randomizeDeadCopy[index] = randomizeDead[index];
            }
        }
    }

    void ResetDpsTable()
    {
        for (int index = 0; index <20; index++)
        {
            playerNickDps[index].text = "";
            colorTmp = playerBarsDps[index];
            colorTmp.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            classTmp = playerClassDps[index];
            classTmp.GetComponent<Image>().color = new Color32(135, 135, 135, 0);
            classTmp.GetComponent<Image>().sprite = null;
            roleTmp = playerRoleDps[index];
            roleTmp.GetComponent<Image>().color = new Color32(135, 135, 135, 0);
            roleTmp.GetComponent<Image>().sprite = null;
            colorTmp.GetComponent<Image>().sprite = null;

            dpsPlayerWindow[index].text = "";
            overallDpsPlayerWindow[index].text = "";
        }
        dpsPlayerWindow[20].text = "";
        overallDpsPlayerWindow[20].text = "";
        fillindex = 0;
        totalDps = 0;
        reqDamageProgressBar = 0;
        totalDpsBg.GetComponent<Image>().color = new Color32(135, 135, 135, 30);
        totalOverallDamageBg.GetComponent<Image>().color = new Color32(135, 135, 135, 30);
    }

    void ManageDpsTable()
    {
        playerImport = raidRoosterDps[fillindex];
        playerNickDps[fillindex].text = playerImport.GetPlayerName();
        playerNickDps[fillindex].GetComponent<Text>().color = new Color32(255, 255, 255, 255);
        colorTmp = playerBarsDps[fillindex];
        classTmp = playerClassDps[fillindex];
        roleTmp = playerRoleDps[fillindex];
        GetRoleIcon();
        colorTmp.GetComponent<Image>().sprite = hpBarImg;
        GetPlayerBarColor();

        roundedNumber = DpsPlaceCopy[fillindex];
        RoundedNumber();
        dpsPlayerWindow[fillindex].text = string.Format("{0:F1} " + abbreviationMark, roundedNumber);

        roundedNumber = OverallDpsPlaceCopy[fillindex];
        RoundedNumber();
        overallDpsPlayerWindow[fillindex].text = string.Format("{0:F2} " + abbreviationMark, roundedNumber);
    }

    void Update()
    {
        ManageAffixBar();
        if (MyFunctionCalled == true)
        {
            if (fillindex >= 20)
            {
                if (Affix[7] == true)
                {
                    if (simulateBossData.bossActuallHp > 0)
                    {
                        totalHpsBg.GetComponent<Image>().color = new Color32(255, 0, 0, 90);
                        totalOverallHealingBg.GetComponent<Image>().color = new Color32(255, 0, 0, 90);
                    }

                    totalHps = 0;
                    reqHealingProgressBar = 0;

                    totalDps = 0;
                    reqDamageProgressBar = 0;
                    fillindex = 0;

                    MyFunctionCalled = false;
                }
                else
                {
                    if (simulateBossData.bossActuallHp > 0)
                    {
                        totalDpsBg.GetComponent<Image>().color = new Color32(255, 0, 0, 90);
                        totalOverallDamageBg.GetComponent<Image>().color = new Color32(255, 0, 0, 90);
                    }

                    totalDps = 0;
                    reqDamageProgressBar = 0;

                    totalHps = 0;
                    reqHealingProgressBar = 0;
                    fillindex = 0;

                    MyFunctionCalled = false;
                }
            }
            else if ((((playerBarsDps[fillindex].fillAmount >= ((DpsPlaceCopy[fillindex] / DpsPlaceCopy[0]) - 0.00001f)) && (playerBarsHps[fillindex].fillAmount >= ((randomHps[fillindex] / randomHps[0]) - 0.00001f)) && (fillindex < 19))) && Affix[7] == false)
            {
                    fillindex++;
                    reqDamageProgressBar += OverallDpsPlaceCopy[fillindex];
                    simulateBossData.ManageHpBar();
                    simulateBossData.bossHpDamaged -= OverallDpsPlaceCopy[fillindex];
                    totalDps += DpsPlaceCopy[fillindex];

                    reqHealingProgressBar += overallHealingData[fillindex];
                    totalHps += randomHps[fillindex];

                    playerBarsDps[fillindex].fillAmount = 0;

                    playerBarsHps[fillindex].fillAmount = 0;
            }
            else if ((((playerBarsDps[fillindex].fillAmount >= ((DpsPlaceCopy[fillindex] / DpsPlaceCopy[0]) - 0.00001f)) && (playerBarsHps[fillindex].fillAmount >= ((randomHps[fillindex] / randomHps[0]) - 0.00001f)) && (fillindex == 19))) && Affix[7] == false)
            {
                    fillindex++;
                    reqDamageProgressBar += overallDamageData[fillindex];
                    simulateBossData.ManageHpBar();
                    simulateBossData.bossHpDamaged -= overallDamageData[fillindex];
                    totalDps += randomDps[fillindex];

                    reqHealingProgressBar += overallHealingData[fillindex];
                    totalHps += randomHps[fillindex];
            }
            else if (((playerBarsDps[fillindex].fillAmount != (DpsPlaceCopy[fillindex] / DpsPlaceCopy[0])) && (playerBarsHps[fillindex].fillAmount != (randomHps[fillindex] / randomHps[0])) && (fillindex < 20) && (playerBarsDps[fillindex].fillAmount >= 0) && (playerBarsHps[fillindex].fillAmount >= 0)) && Affix[7] == false)
            {
                ManageDpsTable();
                playerBarsDps[fillindex].fillAmount = Mathf.Lerp(playerBarsDps[fillindex].fillAmount, (DpsPlaceCopy[fillindex] / DpsPlaceCopy[0]), Time.deltaTime * 60);

                ManageHpsTable();
                playerBarsHps[fillindex].fillAmount = Mathf.Lerp(playerBarsHps[fillindex].fillAmount, (randomHps[fillindex] / randomHps[0]), Time.deltaTime * 60);

                minReqDamage.fillAmount = Mathf.Lerp(minReqDamage.fillAmount, (reqDamageProgressBar / bossData.bosscalculatedValue), Time.deltaTime * 60);
                roundedNumber = reqDamageProgressBar;
                RoundedNumber();
                overallDpsPlayerWindow[20].text = string.Format("{0:F2} " + abbreviationMark, roundedNumber);
                roundedNumber = totalDps;
                RoundedNumber();
                dpsPlayerWindow[20].text = string.Format("{0:F1} " + abbreviationMark, roundedNumber);

                minReqHealing.fillAmount = Mathf.Lerp(minReqHealing.fillAmount, (reqHealingProgressBar / overallHealingData[20]), Time.deltaTime * 60);
                roundedNumber = reqHealingProgressBar;
                RoundedNumber();
                overallHpsPlayerWindow[20].text = string.Format("{0:F2} " + abbreviationMark, roundedNumber);
                roundedNumber = totalHps;
                RoundedNumber();
                hpsPlayerWindow[20].text = string.Format("{0:F1} " + abbreviationMark, roundedNumber);

                simulateBossData.bossActuallHp = bossData.bosscalculatedValue - reqDamageProgressBar;
                simulateBossData.ManageBoss();
            }
            else if ((((playerBarsHps[fillindex].fillAmount >= ((HpsPlaceCopy[fillindex] / HpsPlaceCopy[0]) - 0.00001f)) && (playerBarsDps[fillindex].fillAmount >= ((randomDps[fillindex] / randomDps[0]) - 0.00001f)) && (fillindex < 19))) && Affix[7] == true)
            {
                    fillindex++;
                    reqHealingProgressBar += OverallHpsPlaceCopy[fillindex];
                    simulateBossData.ManageHpBar();
                    simulateBossData.bossHpDamaged -= OverallHpsPlaceCopy[fillindex];
                    totalHps += HpsPlaceCopy[fillindex];

                    reqDamageProgressBar += overallDamageData[fillindex];
                    totalDps += randomDps[fillindex];

                    playerBarsHps[fillindex].fillAmount = 0;

                    playerBarsDps[fillindex].fillAmount = 0;
            }
            else if ((((playerBarsHps[fillindex].fillAmount >= ((HpsPlaceCopy[fillindex] / HpsPlaceCopy[0]) - 0.00001f)) && (playerBarsDps[fillindex].fillAmount >= ((randomDps[fillindex] / randomDps[0]) - 0.00001f)) && (fillindex == 19))) && Affix[7] == true)
            {
                    fillindex++;
                    reqHealingProgressBar += overallHealingData[fillindex];
                    simulateBossData.ManageHpBar();
                    simulateBossData.bossHpDamaged -= overallHealingData[fillindex];
                    totalHps += randomHps[fillindex];

                    reqDamageProgressBar += overallDamageData[fillindex];
                    totalDps += randomDps[fillindex];
            }
            else if (((playerBarsHps[fillindex].fillAmount != (HpsPlaceCopy[fillindex] / HpsPlaceCopy[0])) && (playerBarsDps[fillindex].fillAmount != (randomDps[fillindex] / randomDps[0])) && (fillindex < 20) && (playerBarsHps[fillindex].fillAmount >= 0) && (playerBarsDps[fillindex].fillAmount >= 0)) && Affix[7] == true)
            {
                ToxicManageHpsTable();
                playerBarsHps[fillindex].fillAmount = Mathf.Lerp(playerBarsHps[fillindex].fillAmount, (HpsPlaceCopy[fillindex] / HpsPlaceCopy[0]), Time.deltaTime * 60);

                ToxicManageDpsTable();
                playerBarsDps[fillindex].fillAmount = Mathf.Lerp(playerBarsDps[fillindex].fillAmount, (randomDps[fillindex] / randomDps[0]), Time.deltaTime * 60);

                minReqHealing.fillAmount = Mathf.Lerp(minReqHealing.fillAmount, (reqHealingProgressBar / bossData.bosscalculatedValue), Time.deltaTime * 60);
                roundedNumber = reqHealingProgressBar;
                RoundedNumber();
                overallHpsPlayerWindow[20].text = string.Format("{0:F2} " + abbreviationMark, roundedNumber);
                roundedNumber = totalHps;
                RoundedNumber();
                hpsPlayerWindow[20].text = string.Format("{0:F1} " + abbreviationMark, roundedNumber);

                minReqDamage.fillAmount = Mathf.Lerp(minReqDamage.fillAmount, (reqDamageProgressBar / overallDamageData[20]), Time.deltaTime * 60);
                roundedNumber = reqDamageProgressBar;
                RoundedNumber();
                overallDpsPlayerWindow[20].text = string.Format("{0:F2} " + abbreviationMark, roundedNumber);
                roundedNumber = totalDps;
                RoundedNumber();
                dpsPlayerWindow[20].text = string.Format("{0:F1} " + abbreviationMark, roundedNumber);

                simulateBossData.bossActuallHp = bossData.bosscalculatedValue - reqHealingProgressBar;
                simulateBossData.ManageBoss();
            }
        }
    }

    void CalculateSurvi()
    {
        ripindex = 0;
        for (int index = 0; index < 20; index++)
        {
            ripWindow[index].text = "";
        }

        for (int index = 0; index < 20; index++)
        {
            if (raidRooster[index] != null)
            {
                surviData = raidRooster[index].GetSurvi();
                if(Affix[3] == true)
                {
                    avrgSurvi[index] = maxSurviAffix();
                }
                else if(Affix[4] == true)
                {
                    avrgSurvi[index] = minSurviAffix();
                }
                else
                {
                    for (int sum = 0; sum < surviData.Length; sum++)
                    {
                        avrgSurvi[index] += surviData[sum];
                    }
                    avrgSurvi[index] = (avrgSurvi[index] / surviData.Length);
                }
            }

            randomSurvi[index] = Random.Range(0f, 100f);
            if (avrgSurvi[index] >= randomSurvi[index])
            {
                isAlive[index] = true;
            }
            else
            {
                isAlive[index] = false;
                deathTime[index] = Random.Range(1, encounterTime);
                deathValue[index] = deathTime[index] / encounterTime;
                deathTime[index] = deathValue[index] * encounterTime;
                randomizeDead[index] = true;
            }
        }

        if ((Affix[8] == true) && (isBenchActive() == true))
        {
            for (int index = 0; index < 20; index++)
            {
                if (raidRooster[index].GetPlayerName() == "Ikes")
                {
                    benchValue = Random.Range(0.00f, 1f);
                    break;
                }
            }
        }

    }

    void SortBySurvi()
    {
        PlayerData tempPlayerPlace;
        float tempDpsPlace;
        float tempHpsPlace;
        float tempOverallPlace;
        float tempDeathTimerPlace;
        float tempbenchDeathTimerPlace;
        bool tempStatus;
        string temptext;
        int tempress;
        bool tempdeadcheck;
        float tempdeathValue;
        float tempavrgSurvi;
        float temprandomSurvi;
        bool temprandomizeDead;

        for (int index = 0; index < 19; index++)
        {
            while (index < 19)
            {
                if ((deathTime[index] > deathTime[index + 1]))
                {
                    tempPlayerPlace = raidRooster[index];
                    raidRooster[index] = raidRooster[index + 1];
                    raidRooster[index + 1] = tempPlayerPlace;

                    tempDpsPlace = randomDps[index];
                    randomDps[index] = randomDps[index + 1];
                    randomDps[index + 1] = tempDpsPlace;

                    tempOverallPlace = overallDamageData[index];
                    overallDamageData[index] = overallDamageData[index + 1];
                    overallDamageData[index + 1] = tempOverallPlace;

                    tempHpsPlace = randomHps[index];
                    randomHps[index] = randomHps[index + 1];
                    randomHps[index + 1] = tempHpsPlace;

                    tempOverallPlace = overallHealingData[index];
                    overallHealingData[index] = overallHealingData[index + 1];
                    overallHealingData[index + 1] = tempOverallPlace;

                    tempDeathTimerPlace = deathTime[index];
                    deathTime[index] = deathTime[index + 1];
                    deathTime[index + 1] = tempDeathTimerPlace;

                    tempbenchDeathTimerPlace = benchdeathTime[index];
                    benchdeathTime[index] = benchdeathTime[index + 1];
                    benchdeathTime[index + 1] = tempbenchDeathTimerPlace;

                    tempStatus = isAlive[index];
                    isAlive[index] = isAlive[index + 1];
                    isAlive[index + 1] = tempStatus;

                    temptext = resstext[index];
                    resstext[index] = resstext[index + 1];
                    resstext[index + 1] = temptext;

                    tempress = ressTime[index];
                    ressTime[index] = ressTime[index + 1];
                    ressTime[index + 1] = tempress;

                    tempdeadcheck = deadcheck[index];
                    deadcheck[index] = deadcheck[index + 1];
                    deadcheck[index + 1] = tempdeadcheck;

                    tempdeathValue = deathValue[index];
                    deathValue[index] = deathValue[index + 1];
                    deathValue[index + 1] = tempdeathValue;

                    tempavrgSurvi = avrgSurvi[index];
                    avrgSurvi[index] = avrgSurvi[index + 1];
                    avrgSurvi[index + 1] = tempavrgSurvi;

                    temprandomSurvi = randomSurvi[index];
                    randomSurvi[index] = randomSurvi[index + 1];
                    randomSurvi[index + 1] = temprandomSurvi;

                    temprandomizeDead = randomizeDead[index];
                    randomizeDead[index] = randomizeDead[index + 1];
                    randomizeDead[index + 1] = temprandomizeDead;

                    index = -1;
                }
                index++;
            }
        }
    }

    public void ManageSurviTable()
    {
        int reduceLength = 0;

        for (int index = 0; index < 20; index++)
        {
            ripWindow[index].text = "";
        }

        int tempTime;
        string tempIndex;

        for (int index = 0; index < combatLog.Length; index++)
        {
            while (index < (combatLog.Length - 2))
            {
                if (combatLogTime[index] > combatLogTime[index + 1])
                {
                    tempTime = combatLogTime[index];
                    combatLogTime[index] = combatLogTime[index + 1];
                    combatLogTime[index + 1] = tempTime;

                    tempIndex = combatLog[index];
                    combatLog[index] = combatLog[index + 1];
                    combatLog[index + 1] = tempIndex;

                    index = -1;
                }
                index++;
            }
        }

        for (int index = 0; index < (combatLog.Length - 1); index++)
        {
            while (index < (combatLog.Length - 1))
            {
                if (combatLogTime[index] >= (calculatedEncounterTime))
                {
                    if (combatLog[index].Contains("died"))
                    {
                        ripindex--;
                    }
                    reduceLength++;
                }
                index++;
            }
        }


        System.Array.Resize<string>(ref combatLog, combatLog.Length - reduceLength);
        System.Array.Resize<int>(ref combatLogTime, combatLogTime.Length - reduceLength);

        

        for (int index = 0; ((index < 20) && (index < (combatLog.Length-1))); index++)
        {
            ripWindow[index].text = combatLog[index + combatlogMinValue];
        }
        ripCounter.text = ripindex.ToString();

        if ((combatLog.Length > 20) && (combatlogMaxValue != (combatLog.Length - 2)))
        {
            CombatlogDown.SetActive(true);
        }
        else
        {
            CombatlogDown.GetComponent<Transform>().localScale = new Vector3(0.1903767f, 0.104671f, 0);
            CombatlogDown.SetActive(false);
        }

        if (combatlogMinValue != 0)
        {
            CombatlogUp.SetActive(true);
        }
        else
        {
            CombatlogUp.GetComponent<Transform>().localScale = new Vector3(0.1903767f, 0.104671f, 0);
            CombatlogUp.SetActive(false);
        }
    }

    public void CalculateHps()
    {
        System.Array.Clear(overallHealingData, 0, overallHealingData.Length);
        randomHps[20] = 0;
        encounterTime = 0;

        for (int index = 0; index < 20; index++)
        {
            if (raidRooster[index] != null)
            {
                hpsData = raidRooster[index].GetHps();
                if ((Affix[0] == false) && (Affix[1] == false))
                {
                    randomHps[index] = hpsData[Random.Range(0, hpsData.Length)];
                }
                else if (Affix[0] == true)
                {
                    randomHps[index] = maxHpsPerfoAffix();
                }
                else if (Affix[1] == true)
                {
                    randomHps[index] = minHpsPerfoAffix();
                }
                randomHps[20] += randomHps[index];
            }
        }

        while (encounterTime < calculatedEncounterTime)
        {
            for (int index = 0; index < 20; index++)
            {
                if (raidRooster[index] != null)
                {
                    overallHealingData[index] += randomHps[index];
                    overallHealingData[20] += randomHps[index];
                }
            }
            encounterTime++;
        }

        if (CalculateSurviCheck() == true)
        {
            CalculateHpsAfterSurvi();

            for (int index = 0; index < 20; index++)
            {
                if (isAlive[index] != true)
                {
                    randomHps[index] = overallHealingData[index] / calculatedEncounterTime;
                }
            }
        }

        SortByHps();
    }

    void CalculateHpsAfterSurvi()
    {
        randomHps[20] = 0;
        System.Array.Clear(overallHealingData, 0, overallHealingData.Length);

        encounterTime = 0;
        while (encounterTime < calculatedEncounterTime)
        {
            for (int index = 0; index < 20; index++)
            {
                if ((raidRooster[index] != null))
                {
                    if ((deadcheck[index] == false) || ((encounterTime >= ressTime[index]) && (ressTime[index] != 0) && (benchdeathTime[index] == 0)))
                    {
                        overallHealingData[index] += randomHps[index];
                        overallHealingData[20] += randomHps[index];
                    }
                    else if ((benchdeathTime[index] != 0) && (encounterTime <= benchdeathTime[index])) 
                    {
                        overallHealingData[index] += randomHps[index];
                        overallHealingData[20] += randomHps[index];
                    }
                    if ((encounterTime <= deathTime[index]) && (deadcheck[index] == true) && (benchdeathTime[index] == 0))
                    {
                        overallHealingData[index] += randomHps[index];
                        overallHealingData[20] += randomHps[index];
                    }
                    else if ((encounterTime <= deathTime[index]) && (benchdeathTime[index] != 0) && (encounterTime >= ressTime[index]))
                    {
                        overallHealingData[index] += randomHps[index];
                        overallHealingData[20] += randomHps[index];
                    }
                }
            }
            encounterTime++;
        }

        for (int index = 0; index < 20; index++)
        {
            if (raidRooster[index] != null)
            {
                randomHps[20] += (overallHealingData[index] / calculatedEncounterTime);
            }
        }
    }

    void SortByHps()
    {
        PlayerData tempPlayerPlace;
        float tempHpsPlace;
        float tempDpsPlace;
        float tempOverallPlace;
        float tempDeathTimerPlace;
        float tempbenchDeathTimerPlace;
        bool tempStatus;
        string temptext;
        int tempress;
        bool tempdeadcheck;
        float tempdeathValue;
        float tempavrgSurvi;
        float temprandomSurvi;
        bool temprandomizeDead;

        for (int index = 0; index < 19; index++)
        {
            while (index < 19)
            {
                string playerNickDps = raidRooster[index].GetPlayerName();

                if (randomHps[index] < randomHps[index + 1])
                {
                    tempPlayerPlace = raidRooster[index];
                    raidRooster[index] = raidRooster[index + 1];
                    raidRooster[index + 1] = tempPlayerPlace;

                    tempHpsPlace = randomHps[index];
                    randomHps[index] = randomHps[index + 1];
                    randomHps[index + 1] = tempHpsPlace;

                    tempOverallPlace = overallHealingData[index];
                    overallHealingData[index] = overallHealingData[index + 1];
                    overallHealingData[index + 1] = tempOverallPlace;

                    tempDpsPlace = randomDps[index];
                    randomDps[index] = randomDps[index + 1];
                    randomDps[index + 1] = tempDpsPlace;

                    tempOverallPlace = overallDamageData[index];
                    overallDamageData[index] = overallDamageData[index + 1];
                    overallDamageData[index + 1] = tempOverallPlace;

                    tempDeathTimerPlace = deathTime[index];
                    deathTime[index] = deathTime[index + 1];
                    deathTime[index + 1] = tempDeathTimerPlace;

                    tempbenchDeathTimerPlace = benchdeathTime[index];
                    benchdeathTime[index] = benchdeathTime[index + 1];
                    benchdeathTime[index + 1] = tempbenchDeathTimerPlace;

                    tempStatus = isAlive[index];
                    isAlive[index] = isAlive[index + 1];
                    isAlive[index + 1] = tempStatus;

                    tempress = ressTime[index];
                    ressTime[index] = ressTime[index + 1];
                    ressTime[index + 1] = tempress;

                    tempdeadcheck = deadcheck[index];
                    deadcheck[index] = deadcheck[index + 1];
                    deadcheck[index + 1] = tempdeadcheck;

                    tempdeathValue = deathValue[index];
                    deathValue[index] = deathValue[index + 1];
                    deathValue[index + 1] = tempdeathValue;

                    tempavrgSurvi = avrgSurvi[index];
                    avrgSurvi[index] = avrgSurvi[index + 1];
                    avrgSurvi[index + 1] = tempavrgSurvi;

                    temprandomSurvi = randomSurvi[index];
                    randomSurvi[index] = randomSurvi[index + 1];
                    randomSurvi[index + 1] = temprandomSurvi;

                    temprandomizeDead = randomizeDead[index];
                    randomizeDead[index] = randomizeDead[index + 1];
                    randomizeDead[index + 1] = temprandomizeDead;

                    index = -1;
                }
                index++;
            }
        }

        if (Affix[7] == true)
        {
            for (int index = 0; index < 20; index++)
            {
                raidRoosterHps[index] = raidRooster[index];
                DpsPlaceCopy[index] = randomDps[index];
                OverallDpsPlaceCopy[index] = overallDamageData[index]; ;
                HpsPlaceCopy[index] = randomHps[index];
                OverallHpsPlaceCopy[index] = overallHealingData[index];
                DeathTimerPlaceCopy[index] = deathTime[index];
                benchDeathTimerPlaceCopy[index] = benchdeathTime[index];
                StatusCopy[index] = isAlive[index];
                textCopy[index] = resstext[index];
                ressTimeCopy[index] = ressTime[index];
                deadcheckCopy[index] = deadcheck[index];
                deathValueCopy[index] = deathValueCopy[index];
                avrgSurviCopy[index] = avrgSurvi[index];
                randomSurviCopy[index] = randomSurvi[index];
                randomizeDeadCopy[index] = randomizeDead[index];
            }
        }
    }

    void ResetHpsTable()
    {
        for (int index = 0; index < 20; index++)
        {
            playerNickHps[index].text = "";
            colorTmp = playerBarsHps[index];
            colorTmp.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            classTmp = playerClassHps[index];
            classTmp.GetComponent<Image>().color = new Color32(135, 135, 135, 0);
            classTmp.GetComponent<Image>().sprite = null;
            roleTmp = playerRoleHps[index];
            roleTmp.GetComponent<Image>().color = new Color32(135, 135, 135, 0);
            roleTmp.GetComponent<Image>().sprite = null;
            colorTmp.GetComponent<Image>().sprite = null;

            hpsPlayerWindow[index].text = "";
            overallHpsPlayerWindow[index].text = "";
        }
        hpsPlayerWindow[20].text = "";
        overallHpsPlayerWindow[20].text = "";
        fillindex = 0;
        totalHps = 0;
        reqHealingProgressBar = 0;
        totalHpsBg.GetComponent<Image>().color = new Color32(135, 135, 135, 30);
        totalOverallHealingBg.GetComponent<Image>().color = new Color32(135, 135, 135, 30);
    }

    void ManageHpsTable()
    {
        playerImport = raidRooster[fillindex];
        playerNickHps[fillindex].text = playerImport.GetPlayerName();
        playerNickHps[fillindex].GetComponent<Text>().color = new Color32(255, 255, 255, 255);
        colorTmp = playerBarsHps[fillindex];
        classTmp = playerClassHps[fillindex];
        roleTmp = playerRoleHps[fillindex];
        GetRoleIcon();
        colorTmp.GetComponent<Image>().sprite = hpBarImg;
        GetPlayerBarColor();

        roundedNumber = randomHps[fillindex];
        RoundedNumber();
        hpsPlayerWindow[fillindex].text = string.Format("{0:F1} " + abbreviationMark, roundedNumber);

        roundedNumber = overallHealingData[fillindex];
        RoundedNumber();
        overallHpsPlayerWindow[fillindex].text = string.Format("{0:F2} " + abbreviationMark, roundedNumber);
    }

    private void GetPlayerBarColor()
    {
        if (playerImport.GetPlayerClass() == "Mage")
        {
            colorTmp.GetComponent<Image>().color = new Color32(105, 204, 240, 255);
            classTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            classTmp.GetComponent<Image>().sprite = mageImg;
        }

        else if (playerImport.GetPlayerClass() == "Warlock")
        {
            colorTmp.GetComponent<Image>().color = new Color32(148, 130, 201, 255);
            classTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            classTmp.GetComponent<Image>().sprite = warlockImg;
        }

        else if (playerImport.GetPlayerClass() == "Priest")
        {
            colorTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            classTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            classTmp.GetComponent<Image>().sprite = priestImg;
        }

        else if (playerImport.GetPlayerClass() == "Druid")
        {
            colorTmp.GetComponent<Image>().color = new Color32(255, 125, 010, 255);
            classTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            classTmp.GetComponent<Image>().sprite = druidImg;
        }

        else if (playerImport.GetPlayerClass() == "Rogue")
        {
            colorTmp.GetComponent<Image>().color = new Color32(255, 245, 105, 255);
            classTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            classTmp.GetComponent<Image>().sprite = rogueImg;
        }

        else if (playerImport.GetPlayerClass() == "Monk")
        {
            colorTmp.GetComponent<Image>().color = new Color32(000, 255, 150, 255);
            classTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            classTmp.GetComponent<Image>().sprite = monkImg;
        }

        else if (playerImport.GetPlayerClass() == "Demon Retard")
        {
            colorTmp.GetComponent<Image>().color = new Color32(163, 048, 201, 255);
            classTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            classTmp.GetComponent<Image>().sprite = demonRetardImg;
        }

        else if (playerImport.GetPlayerClass() == "Shaman")
        {
            colorTmp.GetComponent<Image>().color = new Color32(000, 112, 222, 255);
            classTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            classTmp.GetComponent<Image>().sprite = shamanImg;
        }

        else if (playerImport.GetPlayerClass() == "Hunter")
        {
            colorTmp.GetComponent<Image>().color = new Color32(171, 212, 115, 255);
            classTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            classTmp.GetComponent<Image>().sprite = hunterImg;
        }

        else if (playerImport.GetPlayerClass() == "Death Knight")
        {
            colorTmp.GetComponent<Image>().color = new Color32(196, 031, 059, 255);
            classTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            classTmp.GetComponent<Image>().sprite = deathKnightImg;
        }

        else if (playerImport.GetPlayerClass() == "Warrior")
        {
            colorTmp.GetComponent<Image>().color = new Color32(199, 156, 110, 255);
            classTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            classTmp.GetComponent<Image>().sprite = warriorImg;
        }

        else if (playerImport.GetPlayerClass() == "Paladin")
        {
            colorTmp.GetComponent<Image>().color = new Color32(245, 140, 186, 255);
            classTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            classTmp.GetComponent<Image>().sprite = paladinImg;
        }
    }

    private void GetRoleIcon()
    {
        if (playerImport.GetPlayerRole() == "Healer")
        {
            roleTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            roleTmp.GetComponent<Image>().sprite = healerImg;
        }
        if (playerImport.GetPlayerRole() == "Tank")
        {
            roleTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            roleTmp.GetComponent<Image>().sprite = tankImg;
        }
        if (playerImport.GetPlayerRole() == "Dps")
        {
            roleTmp.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            roleTmp.GetComponent<Image>().sprite = dpsImg;
        }
    }

    void RoundedNumber()
    {
        if ((roundedNumber >= 1000) && (roundedNumber < 1000000))
        {
            roundedNumber = roundedNumber / 1000;
            abbreviationMark = "K";
        }
        else if ((roundedNumber >= 1000000) && (roundedNumber < 1000000000))
        {
            roundedNumber = roundedNumber / 1000000;
            abbreviationMark = "Mln";
        }
        else if ((roundedNumber >= 1000000000) && (roundedNumber < 1000000000000))
        {
            roundedNumber = roundedNumber / 1000000000;
            abbreviationMark = "B";
        }
        else
        {
            abbreviationMark = "";
        }
    }

    bool CalculateSurviCheck()
    {
        bool surviFunctionBool = false;

        for(int index = 0; index<20; index++)
        {
            if(isAlive[index] == false)
            {
                surviFunctionBool = true;
                break;
            }
            else
            {
                surviFunctionBool = false;
            }
        }

        return surviFunctionBool;
    }

    int randomRess()
    {
        int ressvalue;
        ressvalue = Random.Range(5, 16);
        return ressvalue;
    }

    string RandomCoD()
    {
        string tempCoD = bossData.causeOfDeath[Random.Range(0, bossData.causeOfDeath.Length)];
        return tempCoD;
    }

    string RandomBenchEvent()
    {
        string tempBE = bossData.benchEvent[Random.Range(0, bossData.benchEvent.Length)];
        return tempBE;
    }

    void ressCompatibility()
    {
        for (int index = 0; index < 20; index++)
        {
            if ((ressTime[index] > calculatedEncounterTime) || (ressTime[index] > bossData.berserkInSeconds))
            {
                resstext[index] = null;
            }
        }
    }

    float maxDpsPerfoAffix()
    {
        float maxDps = dpsData[0];

        for (int index=0; index < dpsData.Length; index++)
        {
            if (dpsData[index] > maxDps)
            {
                maxDps = dpsData[index];
            }
        }
        return maxDps;
    }

    float minDpsPerfoAffix()
    {
        float minDps = dpsData[0];

        for (int index = 0; index < dpsData.Length; index++)
        {
            if (dpsData[index] < minDps)
            {
                minDps = dpsData[index];
            }
        }
        return minDps;
    }

    float maxHpsPerfoAffix()
    {
        float maxHps = hpsData[0];

        for (int index = 0; index < hpsData.Length; index++)
        {
            if (hpsData[index] > maxHps)
            {
                maxHps = hpsData[index];
            }
        }
        return maxHps;
    }

    float minHpsPerfoAffix()
    {
        float minHps = hpsData[0];

        for (int index = 0; index < hpsData.Length; index++)
        {
            if (hpsData[index] < minHps)
            {
                minHps = hpsData[index];
            }
        }
        return minHps;
    }

    float maxSurviAffix()
    {
        float maxSurvi = surviData[0];

        for (int index = 0; index < surviData.Length; index++)
        {
            if (surviData[index] > maxSurvi)
            {
                maxSurvi = surviData[index];
            }
        }
        return maxSurvi;
    }

    float minSurviAffix()
    {
        float minSurvi = surviData[0];

        for (int index = 0; index < surviData.Length; index++)
        {
            if (surviData[index] < minSurvi)
            {
                minSurvi = surviData[index];
            }
        }
        return minSurvi;
    }

    public void ToxicHealingAffix()
    {
        System.Array.Clear(overallHealingData, 0, overallHealingData.Length);
        float bossActuallHp = bossData.bosscalculatedValue;
        encounterTime = 0;
        randomHps[20] = 0;
        ripindex = 0;

        combatLog = new string[1];
        combatLogTime = new int[1];
        System.Array.Clear(combatLog, 0, combatLog.Length);
        System.Array.Clear(combatLogTime, 0, combatLogTime.Length);

        for (int index = 0; index < 20; index++)
        {
            if (raidRooster[index] != null)
            {
                hpsData = raidRooster[index].GetHps();
                if ((Affix[0] == false) && (Affix[1] == false))
                {
                    randomHps[index] = hpsData[Random.Range(0, hpsData.Length)];
                }
                else if (Affix[0] == true)
                {
                    randomHps[index] = maxHpsPerfoAffix();
                }
                else if (Affix[1] == true)
                {
                    randomHps[index] = minHpsPerfoAffix();
                }
                randomHps[20] += randomHps[index];
            }
        }

        while ((encounterTime <= bossData.berserkInSeconds) && (bossActuallHp > 0))
        {
            for (int index = 0; index < 20; index++)
            {
                if (raidRooster[index] != null)
                {
                    overallHealingData[index] += randomHps[index];
                    overallHealingData[20] += randomHps[index];
                    bossActuallHp -= randomHps[index];
                }
            }
            if (bossActuallHp <= 0)
            {
                bossActuallHp = 0;
            }
            encounterTime++;
        }

        CalculateSurvi();
        if ((CalculateSurviCheck() == true) || (isBenchActive() == true))
        {
            ToxicHealingAfterSurvi();

            for (int index = 0; index < 20; index++)
            {
                if (isAlive[index] != true)
                {
                    randomHps[index] = overallHealingData[index] / calculatedEncounterTime;
                }
            }
        }

        calculatedEncounterTime = encounterTime;
        encounterTimer.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
        if (calculatedEncounterTime >= 60)
        {
            encounterTimer.text = (calculatedEncounterTime / 60).ToString() + ":" + (calculatedEncounterTime % 60).ToString("00");
            if (encounterTime >= bossData.berserkInSeconds)
            {
                encounterTimer.GetComponent<Text>().color = new Color32(255, 0, 0, 255);
            }
        }
        else
        {
            encounterTimer.text = "0:" + (calculatedEncounterTime % 60).ToString("00");
        }

        berserkTimer.text = (bossData.berserkInSeconds / 60).ToString() + ":" + (bossData.berserkInSeconds % 60).ToString("00");

        ManageSurviTable();
        SortByHps();
    }

    void ToxicHealingAfterSurvi()
    {
        System.Array.Clear(deadcheck, 0, deadcheck.Length);
        System.Array.Clear(benchDead, 0, benchDead.Length);
        System.Array.Clear(benchdeathTime, 0, benchdeathTime.Length);
        System.Array.Clear(randomizeDead, 0, randomizeDead.Length);
        System.Array.Clear(overallHealingData, 0, overallHealingData.Length);
        System.Array.Clear(ressTime, 0, ressTime.Length);
        float bossActuallHp = bossData.bosscalculatedValue;
        calculatedEncounterTime = encounterTime;
        randomHps[20] = 0;
        ripindex = 1;
        int combatLogIndex = 0;
        int benchBreak = 0;
        float benchRandom = 5;
        int tryNumber = HowManyDead() - 1;

        if (tryNumber < 0)
        {
            tryNumber = 0;
            ripindex = 0;
        }

        for (int retryDead = 0; retryDead <= tryNumber; retryDead++)
        {
            Debug.Log("Try number: " + retryDead);
            bossActuallHp = bossData.bosscalculatedValue;
            System.Array.Clear(resstext, 0, resstext.Length);
            System.Array.Clear(ressTime, 0, ressTime.Length);
            System.Array.Clear(overallHealingData, 0, overallHealingData.Length);
            bool encounterDeath = false;
            bool playerDied = false;
            int intDeathTime;
            int intBenchTime = 0;

            if (Affix[5] == true)
            {
                resurrectionCharges = 999;
            }
            else if (Affix[6] == true)
            {
                resurrectionCharges = 0;
            }
            else
            {
                resurrectionCharges = 1;
            }

            if ((Affix[8] == true) && (isBenchActive() == true) && (retryDead == tryNumber))
            {
                benchTime = benchValue * calculatedEncounterTime;
                if (benchTime >= (calculatedEncounterTime - 5))
                {
                    benchTime = calculatedEncounterTime - 6;
                }
                intBenchTime = Mathf.RoundToInt(benchTime);

                if ((combatLogIndex + 1) == combatLog.Length)
                {
                    System.Array.Resize<string>(ref combatLog, combatLog.Length + 1);
                    System.Array.Resize<int>(ref combatLogTime, combatLogTime.Length + 1);
                }
                combatLogTime[combatLogIndex] = intBenchTime;
                combatLog[combatLogIndex] = RandomBenchEvent();
                combatLogIndex++;
            }

            encounterTime = 0;
            while ((encounterTime <= bossData.berserkInSeconds) && (bossActuallHp > 0))
            {
                if (Affix[5] == false && Affix[6] == false)
                {
                    if (((encounterTime % 300) == 0) && (encounterTime != 0))
                    {
                        resurrectionCharges++;
                        Debug.Log("Amount of res charges increased: " + resurrectionCharges + " time " + encounterTime);
                    }
                }

                for (int index = 0; index < 20; index++)
                {
                    if ((encounterDeath == false) && (isAlive[index] == false) && (randomizeDead[index] == false) && (retryDead != 0) && (deadcheck[index] == false))
                    {
                        deathTime[index] = deathValue[index] * calculatedEncounterTime;
                        intDeathTime = Mathf.RoundToInt(deathTime[index]);
                        if ((combatLogIndex + 1) == combatLog.Length)
                        {
                            System.Array.Resize<string>(ref combatLog, combatLog.Length + 1);
                            System.Array.Resize<int>(ref combatLogTime, combatLogTime.Length + 1);
                        }
                        combatLogTime[combatLogIndex] = intDeathTime;
                        combatLog[combatLogIndex] = "Player " + raidRooster[index].GetPlayerName() + " died at " + (intDeathTime / 60).ToString() + ":" + (intDeathTime % 60).ToString("00") + ". " + RandomCoD();
                        encounterDeath = true;
                        randomizeDead[index] = true;
                        combatLogIndex++;
                        ripindex++;
                    }
                    if ((raidRooster[index] != null))
                    {
                        if ((isAlive[index] == false) && (deadcheck[index] == false) && (playerDied == false))
                        {
                            deadcheck[index] = true;
                            playerDied = true;
                        }
                        else if ((deadcheck[index] == false) || ((encounterTime >= ressTime[index]) && (ressTime[index] != 0) && (benchdeathTime[index] == 0)))
                        {
                            overallHealingData[index] += randomHps[index];
                            overallHealingData[20] += randomHps[index];
                            bossActuallHp -= randomHps[index];
                        }
                        if ((isAlive[index] == false) && (deadcheck[index] == true) && (encounterTime >= deathTime[index]) && (ressTime[index] == 0) && (benchdeathTime[index] == 0))
                        {
                            if ((retryDead == 0) && (deadcheck[index] == true) && (encounterTime < (deathTime[index] + 1)))
                            {
                                intDeathTime = Mathf.RoundToInt(deathTime[index]);
                                if ((combatLogIndex + 1) == combatLog.Length)
                                {
                                    System.Array.Resize<string>(ref combatLog, combatLog.Length + 1);
                                    System.Array.Resize<int>(ref combatLogTime, combatLogTime.Length + 1);
                                }
                                combatLogTime[combatLogIndex] = intDeathTime;
                                combatLog[combatLogIndex] = "Player " + raidRooster[index].GetPlayerName() + " died at " + (intDeathTime / 60).ToString() + ":" + (intDeathTime % 60).ToString("00") + ". " + RandomCoD();
                                combatLogIndex++;
                            }
                            if ((Affix[8] == false) || (retryDead != tryNumber) || ((Affix[8] == true) && (retryDead == tryNumber) && (encounterTime < (intBenchTime - 15))))
                            {
                                if ((raidRooster[index].GetPlayerClass() != "Shaman") && (resurrectionCharges > 0) && (ressTime[index] == 0))
                                {
                                    ressTime[index] = encounterTime + randomRess();
                                    if ((combatLogIndex + 1) == combatLog.Length)
                                    {
                                        System.Array.Resize<string>(ref combatLog, combatLog.Length + 1);
                                        System.Array.Resize<int>(ref combatLogTime, combatLogTime.Length + 1);
                                    }
                                    if (retryDead == tryNumber)
                                    {
                                        combatLogTime[combatLogIndex] = ressTime[index];
                                        combatLog[combatLogIndex] = "Player " + raidRooster[index].GetPlayerName() + " resurrected at " + (ressTime[index] / 60).ToString() + ":" + (ressTime[index] % 60).ToString("00") + ". ";
                                        combatLogIndex++;
                                    }
                                    resurrectionCharges--;
                                }
                                else if ((raidRooster[index].GetPlayerClass() == "Shaman") && (ressTime[index] == 0))
                                {
                                    ressTime[index] = encounterTime + randomRess();
                                    if ((combatLogIndex + 1) == combatLog.Length)
                                    {
                                        System.Array.Resize<string>(ref combatLog, combatLog.Length + 1);
                                        System.Array.Resize<int>(ref combatLogTime, combatLogTime.Length + 1);
                                    }
                                    if (retryDead == tryNumber)
                                    {
                                        combatLogTime[combatLogIndex] = ressTime[index];
                                        combatLog[combatLogIndex] = "Player " + raidRooster[index].GetPlayerName() + " used Reincarnation at " + (ressTime[index] / 60).ToString() + ":" + (ressTime[index] % 60).ToString("00") + ".";
                                        combatLogIndex++;
                                    }
                                }
                            }
                        }
                        if ((encounterTime <= deathTime[index]) && (deadcheck[index] == true) && (benchdeathTime[index] == 0))
                        {
                            overallHealingData[index] += randomHps[index];
                            overallHealingData[20] += randomHps[index];
                            bossActuallHp -= randomHps[index];
                        }
                    }
                }

                if ((Affix[8] == true) && (isBenchActive() == true) && (retryDead == tryNumber) && (intBenchTime <= encounterTime))
                {
                    for (int index = 0; index < 20; index++)
                    {
                        if ((((randomSurvi[index] < benchRandom) && (isAlive[index] == true)) && (benchDead[index] == false)) || ((deathTime[index] >= (benchTime + 5)) && (isAlive[index] == false) && (benchDead[index] == false)) || ((isAlive[index] == false) && (deadcheck[index] == true) && (benchDead[index] == false) && ((ressTime[index] != 0) && (ressTime[index] <= benchTime))))
                        {
                            if (ressTime[index] != 0)
                            {
                                benchdeathTime[index] = deathTime[index];
                            }

                            deathTime[index] = encounterTime;

                            if ((deadcheck[index] == false) || (((deadcheck[index] == true) && (ressTime[index] < benchTime))))
                            {
                                Debug.Log("death time for player " + index + " will occure" + deathTime[index]);
                                intDeathTime = Mathf.RoundToInt(deathTime[index]);
                                if ((combatLogIndex + 1) == combatLog.Length)
                                {
                                    System.Array.Resize<string>(ref combatLog, combatLog.Length + 1);
                                    System.Array.Resize<int>(ref combatLogTime, combatLogTime.Length + 1);
                                }
                                combatLogTime[combatLogIndex] = intDeathTime;
                                combatLog[combatLogIndex] = "Player " + raidRooster[index].GetPlayerName() + " killed by Bench Lord at " + (intDeathTime / 60).ToString() + ":" + (intDeathTime % 60).ToString("00") + ". ";
                                combatLogIndex++;
                                ripindex++;
                            }

                            randomizeDead[index] = true;
                            benchDead[index] = true;
                            isAlive[index] = false;
                            deadcheck[index] = true;
                            benchBreak++;
                            Debug.Log("Triggered benchBreak for player " + index + " total amount: " + benchBreak);
                        }
                        else if ((deathTime[index] < (benchTime + 5)) && (isAlive[index] == false) && (benchDead[index] == false) && ((ressTime[index] == 0) || (ressTime[index] > benchTime)))
                        {
                            benchDead[index] = true;
                            benchBreak++;
                            Debug.Log("Triggered benchBreak for player " + index + " total amount: " + benchBreak);
                        }
                    }
                    benchRandom = benchRandom * 2;
                }

                if ((bossActuallHp <= 0) && (retryDead == tryNumber))
                {
                    bossActuallHp = 0;
                }

                encounterTime++;

                if (benchBreak == 20)
                {
                    Debug.Log("triggered break");
                    break;
                }
            }
            calculatedEncounterTime = encounterTime;
            Debug.Log("set new encounter time: " + calculatedEncounterTime);
        }

        for (int index = 0; index < 20; index++)
        {
            if (raidRooster[index] != null)
            {
                randomHps[20] += (overallHealingData[index] / calculatedEncounterTime);
            }
        }

        ressCompatibility();
    }

    public void ToxicDamageAffix()
    {
        System.Array.Clear(overallDamageData, 0, overallDamageData.Length);
        randomDps[20] = 0;
        encounterTime = 0;

        for (int index = 0; index < 20; index++)
        {
            if (raidRooster[index] != null)
            {
                dpsData = raidRooster[index].GetDps();
                if ((Affix[0] == false) && (Affix[1] == false))
                {
                    randomDps[index] = dpsData[Random.Range(0, dpsData.Length)];
                }
                else if (Affix[0] == true)
                {
                    randomDps[index] = maxDpsPerfoAffix();
                }
                else if (Affix[1] == true)
                {
                    randomDps[index] = minDpsPerfoAffix();
                }
                randomDps[20] += randomDps[index];
            }
        }

        while (encounterTime < calculatedEncounterTime)
        {
            for (int index = 0; index < 20; index++)
            {
                if (raidRooster[index] != null)
                {
                    overallDamageData[index] += randomDps[index];
                    overallDamageData[20] += randomDps[index];
                }
            }
            encounterTime++;
        }

        if (CalculateSurviCheck() == true)
        {
            ToxicDamageAfterSurvi();

            for (int index = 0; index < 20; index++)
            {
                if (isAlive[index] != true)
                {
                    randomDps[index] = overallDamageData[index] / calculatedEncounterTime;
                }
            }
        }

        SortByDps();
    }

    void ToxicDamageAfterSurvi()
    {
        randomDps[20] = 0;
        System.Array.Clear(overallDamageData, 0, overallDamageData.Length);

        encounterTime = 0;
        while (encounterTime < calculatedEncounterTime)
        {
            for (int index = 0; index < 20; index++)
            {
                if ((raidRooster[index] != null))
                {
                    if ((deadcheck[index] == false) || ((encounterTime >= ressTime[index]) && (ressTime[index] != 0) && (benchdeathTime[index] == 0)))
                    {
                        overallDamageData[index] += randomDps[index];
                        overallDamageData[20] += randomDps[index];
                    }
                    else if ((benchdeathTime[index] != 0) && (encounterTime <= benchdeathTime[index]))
                    {
                        overallDamageData[index] += randomDps[index];
                        overallDamageData[20] += randomDps[index];
                    }
                    if ((encounterTime <= deathTime[index]) && (deadcheck[index] == true) && (benchdeathTime[index] == 0))
                    {
                        overallDamageData[index] += randomDps[index];
                        overallDamageData[20] += randomDps[index];
                    }
                    else if ((encounterTime <= deathTime[index]) && (benchdeathTime[index] != 0) && (encounterTime >= ressTime[index]))
                    {
                        overallDamageData[index] += randomDps[index];
                        overallDamageData[20] += randomDps[index];
                    }
                }
            }
            encounterTime++;
        }

        for (int index = 0; index < 20; index++)
        {
            if (raidRooster[index] != null)
            {
                randomDps[20] += (overallDamageData[index] / calculatedEncounterTime);
            }
        }
    }

    void ToxicManageHpsTable()
    {
        playerImport = raidRoosterHps[fillindex];
        playerNickHps[fillindex].text = playerImport.GetPlayerName();
        playerNickHps[fillindex].GetComponent<Text>().color = new Color32(255, 255, 255, 255);
        colorTmp = playerBarsHps[fillindex];
        classTmp = playerClassHps[fillindex];
        roleTmp = playerRoleHps[fillindex];
        GetRoleIcon();
        colorTmp.GetComponent<Image>().sprite = hpBarImg;
        GetPlayerBarColor();

        roundedNumber = HpsPlaceCopy[fillindex];
        RoundedNumber();
        hpsPlayerWindow[fillindex].text = string.Format("{0:F1} " + abbreviationMark, roundedNumber);

        roundedNumber = OverallHpsPlaceCopy[fillindex];
        RoundedNumber();
        overallHpsPlayerWindow[fillindex].text = string.Format("{0:F2} " + abbreviationMark, roundedNumber);
    }

    void ToxicManageDpsTable()
    {
        playerImport = raidRooster[fillindex];
        playerNickDps[fillindex].text = playerImport.GetPlayerName();
        playerNickDps[fillindex].GetComponent<Text>().color = new Color32(255, 255, 255, 255);
        colorTmp = playerBarsDps[fillindex];
        classTmp = playerClassDps[fillindex];
        roleTmp = playerRoleDps[fillindex];
        GetRoleIcon();
        colorTmp.GetComponent<Image>().sprite = hpBarImg;
        GetPlayerBarColor();

        roundedNumber = randomDps[fillindex];
        RoundedNumber();
        dpsPlayerWindow[fillindex].text = string.Format("{0:F1} " + abbreviationMark, roundedNumber);

        roundedNumber = overallDamageData[fillindex];
        RoundedNumber();
        overallDpsPlayerWindow[fillindex].text = string.Format("{0:F2} " + abbreviationMark, roundedNumber);
    }

    void ManageAffixBar()
    {
        affixIndex = 0;

        if (((Affix[0] == true) || (Affix[1] == true) || (Affix[3] == true) || (Affix[4] == true) || (Affix[5] == true) || (Affix[6] == true) || (Affix[7] == true) || (Affix[8] == true) || (Affix[9] == true)))
        {
            for (int index = affixIndex; (index <= 9); index++)
            {
                if (Affix[index] == true)
                {
                    AffixBuff[0].SetActive(true);
                    AffixBuff[0].GetComponent<Image>().sprite = AffixIcon[index];
                    affixIndex = index + 1;
                    affixInfoBar.SetActive(true);
                    break;
                }

                if (index == 1)
                {
                    index = 2;
                }
            }
        }

        if (((Affix[3] == true) || (Affix[4] == true) || (Affix[5] == true) || (Affix[6] == true) || (Affix[7] == true) || (Affix[8] == true) || (Affix[9] == true)))
        {
            for (int index = affixIndex; (index <= 9); index++)
            {
                if (Affix[index] == true)
                {
                    AffixBuff[1].SetActive(true);
                    AffixBuff[1].GetComponent<Image>().sprite = AffixIcon[index];
                    affixIndex = index + 1;
                    break;
                }
            }
        }

        if (((Affix[5] == true) || (Affix[6] == true) || (Affix[7] == true) || (Affix[8] == true) || (Affix[9] == true)))
        {
            for (int index = affixIndex; (index <= 9); index++)
            {
                if (Affix[index] == true)
                {
                    AffixBuff[2].SetActive(true);
                    AffixBuff[2].GetComponent<Image>().sprite = AffixIcon[index];
                    affixIndex = index + 1;
                    break;
                }
            }
        }

        if (((Affix[7] == true) || (Affix[8] == true) || (Affix[9] == true)))
        {
            for (int index = affixIndex; (index <= 9); index++)
            {
                if (Affix[index] == true)
                {
                    AffixBuff[3].SetActive(true);
                    AffixBuff[3].GetComponent<Image>().sprite = AffixIcon[index];
                    affixIndex = index + 1;
                    break;
                }
            }
        }

        if (((Affix[8] == true) || (Affix[9] == true)))
        {
            for (int index = affixIndex; (index <= 9); index++)
            {
                if (Affix[index] == true)
                {
                    AffixBuff[4].SetActive(true);
                    AffixBuff[4].GetComponent<Image>().sprite = AffixIcon[index];
                    affixIndex = index + 1;
                    break;
                }
            }
        }

        for (int index = affixIndex; (index <= 9); index++)
        {
            if (Affix[index] == true)
            {
                AffixBuff[5].SetActive(true);
                AffixBuff[5].GetComponent<Image>().sprite = AffixIcon[index];
                break;
            }
        }

        CopyAffixFrame();
    }

    public void ResetAffixBar()
    {
        AffixBuff[0].GetComponent<Image>().sprite = null;
        AffixBuff[1].GetComponent<Image>().sprite = null;
        AffixBuff[2].GetComponent<Image>().sprite = null;
        AffixBuff[3].GetComponent<Image>().sprite = null;
        AffixBuff[4].GetComponent<Image>().sprite = null;
        AffixBuff[5].GetComponent<Image>().sprite = null;

        AffixBuff[0].SetActive(false);
        AffixBuff[1].SetActive(false);
        AffixBuff[2].SetActive(false);
        AffixBuff[3].SetActive(false);
        AffixBuff[4].SetActive(false);
        AffixBuff[5].SetActive(false);

        affixInfoBar.SetActive(false);
    }

    public bool isBenchActive()
    {
        bool bench = false;

        for (int index = 0; index < 20; index++)
        {
            if (raidRooster[index].GetPlayerName() == "Ikes")
            {
                if(isAlive[index] == true)
                {
                    bench = true;
                    break;
                }
            }

            else
            {
                bench = false;
            }
        }
        return bench;
    }

    public int HowManyDead()
    {
        int playersDead = 0;

        for (int index = 0; index < 20; index++)
        {
            if (isAlive[index] == false)
            {
                playersDead++;
            }
        }

        return playersDead;
    }

    void CopyAffixFrame()
    {
        if(AffixBuff[0].GetComponent<Image>().sprite != null)
        {
            AffixBuffCopy[0].SetActive(true);
            AffixBuffCopy[0].GetComponent<Image>().sprite = AffixBuff[0].GetComponent<Image>().sprite;
        }
        else
        {
            AffixBuffCopy[0].SetActive(false);
        }

        if (AffixBuff[1].GetComponent<Image>().sprite != null)
        {
            AffixBuffCopy[1].SetActive(true);
            AffixBuffCopy[1].GetComponent<Image>().sprite = AffixBuff[1].GetComponent<Image>().sprite;
        }
        else
        {
            AffixBuffCopy[1].SetActive(false);
        }

        if (AffixBuff[2].GetComponent<Image>().sprite != null)
        {
            AffixBuffCopy[2].SetActive(true);
            AffixBuffCopy[2].GetComponent<Image>().sprite = AffixBuff[2].GetComponent<Image>().sprite;
        }
        else
        {
            AffixBuffCopy[2].SetActive(false);
        }

        if (AffixBuff[3].GetComponent<Image>().sprite != null)
        {
            AffixBuffCopy[3].SetActive(true);
            AffixBuffCopy[3].GetComponent<Image>().sprite = AffixBuff[3].GetComponent<Image>().sprite;
        }
        else
        {
            AffixBuffCopy[3].SetActive(false);
        }

        if (AffixBuff[4].GetComponent<Image>().sprite != null)
        {
            AffixBuffCopy[4].SetActive(true);
            AffixBuffCopy[4].GetComponent<Image>().sprite = AffixBuff[4].GetComponent<Image>().sprite;
        }
        else
        {
            AffixBuffCopy[4].SetActive(false);
        }

        if (AffixBuff[5].GetComponent<Image>().sprite != null)
        {
            AffixBuffCopy[5].SetActive(true);
            AffixBuffCopy[5].GetComponent<Image>().sprite = AffixBuff[5].GetComponent<Image>().sprite;
        }
        else
        {
            AffixBuffCopy[5].SetActive(false);
        }
    }
}


/*
   System.Array.Clear(deadcheck, 0, deadcheck.Length);
        System.Array.Clear(randomizeDead, 0, randomizeDead.Length);
        System.Array.Clear(overallHealingData, 0, overallHealingData.Length);
        System.Array.Clear(ressTime, 0, ressTime.Length);
        float bossActuallHp = bossData.bosscalculatedValue;
        calculatedEncounterTime = encounterTime;
        randomHps[20] = 0;
        ripindex = 1;
        int combatLogIndex = 0;

        for (int retryDead = 0; retryDead <= ripindex; retryDead++)
        {
            Debug.Log("Try number: " + retryDead);
            bossActuallHp = bossData.bosscalculatedValue;
            System.Array.Clear(resstext, 0, resstext.Length);
            System.Array.Clear(ressTime, 0, ressTime.Length);
            System.Array.Clear(overallHealingData, 0, overallHealingData.Length);
            bool encounterDeath = false;
            bool playerDied = false;
            int intDeathTime;

            if (Affix[5] == true)
            {
                resurrectionCharges = 999;
            }
            else if (Affix[6] == true)
            {
                resurrectionCharges = 0;
            }
            else
            {
                resurrectionCharges = 1;
            }

            encounterTime = 0;
            while ((encounterTime <= bossData.berserkInSeconds) && (bossActuallHp > 0))
            {
                if (Affix[5] == false && Affix[6] == false)
                {
                    if (((encounterTime % 300) == 0) && (encounterTime != 0))
                    {
                        resurrectionCharges++;
                        Debug.Log("Amount of res charges increased: " + resurrectionCharges + " time " + encounterTime);
                    }
                }

                for (int index = 0; index < 20; index++)
                {
                    if ((encounterDeath == false) && (isAlive[index] == false) && (randomizeDead[index] == false) && (retryDead != 0) && (deadcheck[index] == false))
                    {
                        deathTime[index] = deathValue[index] * calculatedEncounterTime;
                        intDeathTime = Mathf.RoundToInt(deathTime[index]);
                        if ((combatLogIndex + 1) == combatLog.Length)
                        {
                            System.Array.Resize<string>(ref combatLog, combatLog.Length + 1);
                            System.Array.Resize<int>(ref combatLogTime, combatLogTime.Length + 1);
                        }
                        combatLogTime[combatLogIndex] = intDeathTime;
                        combatLog[combatLogIndex] = "Player " + raidRooster[index].GetPlayerName() + " died at " + (intDeathTime / 60).ToString() + ":" + (intDeathTime % 60).ToString("00") + ". " + RandomCoD();
                        encounterDeath = true;
                        randomizeDead[index] = true;
                        combatLogIndex++;
                        ripindex++;
                    }
                    if ((raidRooster[index] != null))
                    {
                        if ((isAlive[index] == false) && (deadcheck[index] == false) && (playerDied == false))
                        {
                            deadcheck[index] = true;
                            playerDied = true;
                        }
                        else if ((deadcheck[index] == false) || ((encounterTime >= ressTime[index]) && (ressTime[index] != 0)))
                        {
                            overallHealingData[index] += randomHps[index];
                            overallHealingData[20] += randomHps[index];
                            bossActuallHp -= randomHps[index];
                        }
                        if ((isAlive[index] == false) && (deadcheck[index] == true) && (encounterTime >= deathTime[index]) && (ressTime[index] == 0))
                        {
                            if ((retryDead == 0) && (deadcheck[index] == true) && (encounterTime < (deathTime[index] + 1)))
                            {
                                intDeathTime = Mathf.RoundToInt(deathTime[index]);
                                if ((combatLogIndex + 1) == combatLog.Length)
                                {
                                    System.Array.Resize<string>(ref combatLog, combatLog.Length + 1);
                                    System.Array.Resize<int>(ref combatLogTime, combatLogTime.Length + 1);
                                }
                                combatLogTime[combatLogIndex] = intDeathTime;
                                combatLog[combatLogIndex] = "Player " + raidRooster[index].GetPlayerName() + " died at " + (intDeathTime / 60).ToString() + ":" + (intDeathTime % 60).ToString("00") + ". " + RandomCoD();
                                combatLogIndex++;
                            }
                            if ((raidRooster[index].GetPlayerClass() != "Shaman") && (resurrectionCharges > 0) && (ressTime[index] == 0))
                            {
                                ressTime[index] = encounterTime + randomRess();
                                if ((combatLogIndex + 1) == combatLog.Length)
                                {
                                    System.Array.Resize<string>(ref combatLog, combatLog.Length + 1);
                                    System.Array.Resize<int>(ref combatLogTime, combatLogTime.Length + 1);
                                }
                                if (retryDead == ripindex)
                                {
                                    combatLogTime[combatLogIndex] = ressTime[index];
                                    combatLog[combatLogIndex] = "Player " + raidRooster[index].GetPlayerName() + " resurrected at " + (ressTime[index] / 60).ToString() + ":" + (ressTime[index] % 60).ToString("00") + ". ";
                                    combatLogIndex++;
                                }
                                resurrectionCharges--;
                            }
                            else if ((raidRooster[index].GetPlayerClass() == "Shaman") && (ressTime[index] == 0))
                            {
                                ressTime[index] = encounterTime + randomRess();
                                if ((combatLogIndex + 1) == combatLog.Length)
                                {
                                    System.Array.Resize<string>(ref combatLog, combatLog.Length + 1);
                                    System.Array.Resize<int>(ref combatLogTime, combatLogTime.Length + 1);
                                }
                                if (retryDead == ripindex)
                                {
                                    combatLogTime[combatLogIndex] = ressTime[index];
                                    combatLog[combatLogIndex] = "Player " + raidRooster[index].GetPlayerName() + " used Reincarnation at " + (ressTime[index] / 60).ToString() + ":" + (ressTime[index] % 60).ToString("00") + ".";
                                    combatLogIndex++;
                                }
                            }
                        }
                        if ((encounterTime <= deathTime[index]) && (deadcheck[index] == true))
                        {
                            overallHealingData[index] += randomHps[index];
                            overallHealingData[20] += randomHps[index];
                            bossActuallHp -= randomHps[index];
                        }
                    }
                }
                if (bossActuallHp <= 0)
                {
                    bossActuallHp = 0;
                }
                encounterTime++;
            }
            calculatedEncounterTime = encounterTime;
            Debug.Log("set new encounter time: " + calculatedEncounterTime);
        }

        for (int index = 0; index < 20; index++)
        {
            if (raidRooster[index] != null)
            {
                randomHps[20] += (overallHealingData[index] / calculatedEncounterTime);
            }
        }

        ressCompatibility();
 */