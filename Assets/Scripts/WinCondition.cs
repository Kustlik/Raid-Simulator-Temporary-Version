using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCondition : MonoBehaviour
{
    [SerializeField] StartSimulationEvent simulationScript;
    [SerializeField] ScoreDataBase scoreData;
    [SerializeField] Text zoneName;
    [SerializeField] Text fetchBossName;
    bool isBossDead = false;
    bool isSkipActive = false;
    bool doItOnce = false;
    [SerializeField] GameObject backgroundBlack1;
    [SerializeField] GameObject backgroundBlack2;
    [SerializeField] GameObject backgroundWhite;
    [SerializeField] GameObject medal;
    [SerializeField] GameObject glow;
    [SerializeField] GameObject darkBg;
    [SerializeField] Text medalScore;
    [SerializeField] Text bossName;
    [SerializeField] Text hasBeenDefeated;
    [SerializeField] Text newRecord;
    public int armorlevel = 0;
    public int record = 0;
    public int bossOrder = 0;

    static float t = 0.0f;

    [SerializeField] GameObject tryAgainButton;
    [SerializeField] GameObject raidCompositionButton;

    public void ShowWinScreen()
    {
        t = 0.0f;

        backgroundWhite.GetComponent<RectTransform>().anchoredPosition = new Vector3(-6.85f, 5.63f, 0);
        hasBeenDefeated.GetComponent<RectTransform>().anchoredPosition = new Vector3(-10.257f, 3.93f, 0);

        backgroundBlack1.GetComponent<RectTransform>().anchoredPosition = new Vector3(13.08f, 5.63f, 0);
        bossName.GetComponent<RectTransform>().anchoredPosition = new Vector3(13.466f, 4.398f, 0);

        backgroundBlack2.GetComponent<RectTransform>().anchoredPosition = new Vector3(13.08f, 5.63f, 0);

        darkBg.GetComponent<Image>().color = new Color32(0, 0, 0, 0);

        isBossDead = true;
        doItOnce = false;

        if ((armorlevel > record) && (
            (simulationScript.Affix[0] == false) &&
            (simulationScript.Affix[1] == false) &&
            (simulationScript.Affix[2] == false) &&
            (simulationScript.Affix[3] == false) &&
            (simulationScript.Affix[4] == false) &&
            (simulationScript.Affix[5] == false) &&
            (simulationScript.Affix[6] == false) &&
            (simulationScript.Affix[7] == false) &&
            (simulationScript.Affix[8] == false) &&
            (simulationScript.Affix[9] == false) && (isBossDead == true)
            ))
        {
            tryAgainButton.GetComponent<BoxCollider2D>().enabled = false;
            raidCompositionButton.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void Update()
    {
        if ((armorlevel <= record) || (
            (simulationScript.Affix[0] == true) ||
            (simulationScript.Affix[1] == true) ||
            (simulationScript.Affix[2] == true) ||
            (simulationScript.Affix[3] == true) ||
            (simulationScript.Affix[4] == true) ||
            (simulationScript.Affix[5] == true) ||
            (simulationScript.Affix[6] == true) ||
            (simulationScript.Affix[7] == true) ||
            (simulationScript.Affix[8] == true) ||
            (simulationScript.Affix[9] == true)
            ))
        {
            if (isBossDead == true)
            {
                backgroundWhite.GetComponent<RectTransform>().anchoredPosition = new Vector3(Mathf.SmoothStep(-6.85f, 3.427f, t), 5.63f, 0);
                backgroundBlack1.GetComponent<RectTransform>().anchoredPosition = new Vector3(Mathf.SmoothStep(13.08f, 3.427f, t), 5.63f, 0);
                backgroundBlack2.GetComponent<RectTransform>().anchoredPosition = new Vector3(Mathf.SmoothStep(13.08f, 3.427f, t), 5.63f, 0);

                hasBeenDefeated.GetComponent<RectTransform>().anchoredPosition = new Vector3(Mathf.SmoothStep(-10.257f, 0.02f, t), 3.93f, 0);
                bossName.GetComponent<RectTransform>().anchoredPosition = new Vector3(Mathf.SmoothStep(13.466f, 3.813f, t), 4.398f, 0);

                t += 1 * Time.deltaTime;
            }

            if ((t >= 2) || (isSkipActive == true))
            {
                if (doItOnce == false)
                {
                    isBossDead = false;
                    doItOnce = true;
                    isSkipActive = true;
                    t = 0;
                }

                backgroundWhite.GetComponent<RectTransform>().anchoredPosition = new Vector3(Mathf.SmoothStep(3.427f, 13.08f, t), 5.63f, 0);
                backgroundBlack1.GetComponent<RectTransform>().anchoredPosition = new Vector3(Mathf.SmoothStep(3.427f, -6.85f, t), 5.63f, 0);
                backgroundBlack2.GetComponent<RectTransform>().anchoredPosition = new Vector3(Mathf.SmoothStep(3.427f, -6.85f, t), 5.63f, 0);

                hasBeenDefeated.GetComponent<RectTransform>().anchoredPosition = new Vector3(Mathf.SmoothStep(0.02f, 13.466f, t), 3.93f, 0);
                bossName.GetComponent<RectTransform>().anchoredPosition = new Vector3(Mathf.SmoothStep(3.813f, -10.257f, t), 4.398f, 0);

                if (t <= 1.5f)
                {
                    t += 0.75f * Time.deltaTime;
                }
                if (t > 1)
                {
                    isSkipActive = false;
                }
            }
        }
       else if ((armorlevel > record) && (
            (simulationScript.Affix[0] == false) &&
            (simulationScript.Affix[1] == false) &&
            (simulationScript.Affix[2] == false) &&
            (simulationScript.Affix[3] == false) &&
            (simulationScript.Affix[4] == false) &&
            (simulationScript.Affix[5] == false) &&
            (simulationScript.Affix[6] == false) &&
            (simulationScript.Affix[7] == false) &&
            (simulationScript.Affix[8] == false) &&
            (simulationScript.Affix[9] == false)
            ))
        {
            if (isBossDead == true)
            {
                bossName.text = fetchBossName.text;

                if (armorlevel == 1)
                {
                    medalScore.text = "";
                }
                if (armorlevel > 1)
                {
                    medalScore.text = armorlevel.ToString();
                }

                backgroundWhite.GetComponent<RectTransform>().anchoredPosition = new Vector3(Mathf.SmoothStep(-6.85f, 3.427f, t), 3.61f, 0);
                backgroundBlack1.GetComponent<RectTransform>().anchoredPosition = new Vector3(Mathf.SmoothStep(13.08f, 3.427f, t), 3.609999f, 0);
                backgroundBlack2.GetComponent<RectTransform>().anchoredPosition = new Vector3(Mathf.SmoothStep(13.08f, 3.427f, t), 3.609999f, 0);

                hasBeenDefeated.GetComponent<RectTransform>().anchoredPosition = new Vector3(Mathf.SmoothStep(-10.257f, 0.02f, t), 1.91f, 0);
                bossName.GetComponent<RectTransform>().anchoredPosition = new Vector3(Mathf.SmoothStep(13.466f, 3.09f, t), 2.378f, 0);

                darkBg.GetComponent<Image>().color = new Color(0f, 0f, 0f, Mathf.SmoothStep(0, 0.4f, t));

                t += 1 * Time.deltaTime;

                RectTransform rectTransform = glow.GetComponent<RectTransform>();
                rectTransform.Rotate(new Vector3(0, 0, 0.2f));

                glow.GetComponent<RectTransform>().anchoredPosition = new Vector3(3.09f, 4.98f, 0);
                medal.GetComponent<RectTransform>().anchoredPosition = new Vector3(3.1f, 4.3f, 0);
                medalScore.GetComponent<RectTransform>().anchoredPosition = new Vector3(3.08f, 4.729f, 0);

                glow.transform.localScale = new Vector3(Mathf.SmoothStep(1f, 0.05240074f, t * 3), Mathf.SmoothStep(1f, 0.05240074f, t*3), 0);
                medal.transform.localScale = new Vector3(Mathf.SmoothStep(1f, 0.03362686f, t * 3), Mathf.SmoothStep(1f, 0.03362686f, t*3), 0);
                medalScore.transform.localScale = new Vector3(Mathf.SmoothStep(1f, 0.01736481f, t * 3), Mathf.SmoothStep(1f, 0.01736481f, t*3), 0);

                newRecord.GetComponent<RectTransform>().anchoredPosition = new Vector3(5.11f, 2.97f, 0);
                newRecord.transform.localScale = new Vector3(Mathf.SmoothStep(1f, 0.01736481f, t * 3), Mathf.SmoothStep(1f, 0.01736481f, t * 3), 0);
            }

            if (isSkipActive == true)
            {
                if (doItOnce == false)
                {
                    isBossDead = false;
                    doItOnce = true;
                    isSkipActive = true;
                    t = 0;
                }

                backgroundWhite.GetComponent<RectTransform>().anchoredPosition = new Vector3(Mathf.SmoothStep(3.427f, 13.08f, t), 3.61f, 0);
                backgroundBlack1.GetComponent<RectTransform>().anchoredPosition = new Vector3(Mathf.SmoothStep(3.427f, -6.85f, t), 3.609999f, 0);
                backgroundBlack2.GetComponent<RectTransform>().anchoredPosition = new Vector3(Mathf.SmoothStep(3.427f, -6.85f, t), 3.609999f, 0);

                hasBeenDefeated.GetComponent<RectTransform>().anchoredPosition = new Vector3(Mathf.SmoothStep(0.02f, 13.466f, t), 1.91f, 0);
                bossName.GetComponent<RectTransform>().anchoredPosition = new Vector3(Mathf.SmoothStep(3.09f, -10.257f, t), 2.378f, 0);

                darkBg.GetComponent<Image>().color = new Color(0f, 0f, 0f, Mathf.SmoothStep(0.4f, 0, t));

                RectTransform rectTransform = glow.GetComponent<RectTransform>();
                rectTransform.Rotate(new Vector3(0, 0, 0.2f));

                glow.transform.localScale = new Vector3(Mathf.SmoothStep(0.05240074f, 0, t * 2), Mathf.SmoothStep(0.05240074f, 0, t * 2), 0);
                medal.transform.localScale = new Vector3(Mathf.SmoothStep(0.03362686f, 0, t * 2), Mathf.SmoothStep(0.03362686f, 0, t * 2), 0);
                medalScore.transform.localScale = new Vector3(Mathf.SmoothStep(0.01736481f, 0, t * 2), Mathf.SmoothStep(0.01736481f, 0, t * 2), 0);

                glow.GetComponent<RectTransform>().anchoredPosition = new Vector3(3.09f, Mathf.SmoothStep(4.98f, 4.3f, t * 2), 0);
                medalScore.GetComponent<RectTransform>().anchoredPosition = new Vector3(3.08f, Mathf.SmoothStep(4.729f, 4.3f, t * 2), 0);

                newRecord.transform.localScale = new Vector3(Mathf.SmoothStep(0.01736481f, 0, t * 2), Mathf.SmoothStep(0.01736481f, 0, t * 2), 0);
                newRecord.GetComponent<RectTransform>().anchoredPosition = new Vector3(Mathf.SmoothStep(5.11f, 3.1f, t * 2), Mathf.SmoothStep(2.97f, 4.3f, t * 2), 0);

                if (t <= 1.5f)
                {
                    t += 0.75f * Time.deltaTime;
                }
                if (t > 1)
                {
                    glow.GetComponent<RectTransform>().anchoredPosition = new Vector3(3.09f, 15, 0);
                    medal.GetComponent<RectTransform>().anchoredPosition = new Vector3(3.1f, 15, 0);
                    medalScore.GetComponent<RectTransform>().anchoredPosition = new Vector3(3.08f, 15, 0);
                    newRecord.GetComponent<RectTransform>().anchoredPosition = new Vector3(5.11f, 15, 0);

                    record = armorlevel;
                    isSkipActive = false;

                    tryAgainButton.GetComponent<BoxCollider2D>().enabled = true;
                    raidCompositionButton.GetComponent<BoxCollider2D>().enabled = true;
                }
            }

            if (zoneName.text == "ULDIR")
            {
                if (armorlevel == 1)
                {
                    scoreData.uldir[bossOrder].text = "DEAD";
                }
                else if (armorlevel > 1)
                {
                    scoreData.uldir[bossOrder].text = "+" + armorlevel;
                }
            }
            else if (zoneName.text == "BATTLE OF DAZAR'ALOR")
            {
                if (armorlevel == 1)
                {
                    scoreData.bod[bossOrder].text = "DEAD";
                }
                else if (armorlevel > 1)
                {
                    scoreData.bod[bossOrder].text = "+" + armorlevel;
                }
            }
            else if (zoneName.text == "THE ETERNAL PALACE")
            {
                if (armorlevel == 1)
                {
                    scoreData.ep[bossOrder].text = "DEAD";
                }
                else if (armorlevel > 1)
                {
                    scoreData.ep[bossOrder].text = "+" + armorlevel;
                }
            }
            else if (zoneName.text == "NY'ALOTHA, THE WAKING CITY")
            {
                if (armorlevel == 1)
                {
                    scoreData.nya[bossOrder].text = "DEAD";
                }
                else if (armorlevel > 1)
                {
                    scoreData.nya[bossOrder].text = "+" + armorlevel;
                }
            }
        }
    }

    void OnMouseDown()
    {
        if (isBossDead == true)
        {
            isBossDead = false;
            isSkipActive = true;
        }
    }

    public void ResetWinScreen()
    {
        t = 0.0f;

        backgroundWhite.GetComponent<RectTransform>().anchoredPosition = new Vector3(-6.85f, 5.63f, 0);
        hasBeenDefeated.GetComponent<RectTransform>().anchoredPosition = new Vector3(-10.257f, 3.93f, 0);

        backgroundBlack1.GetComponent<RectTransform>().anchoredPosition = new Vector3(13.08f, 5.63f, 0);
        bossName.GetComponent<RectTransform>().anchoredPosition = new Vector3(13.466f, 4.398f, 0);

        backgroundBlack2.GetComponent<RectTransform>().anchoredPosition = new Vector3(13.08f, 5.63f, 0);
        doItOnce = true;
        isBossDead = false;
        isSkipActive = false;
    }
}
