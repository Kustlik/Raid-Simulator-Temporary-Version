using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimBoss : MonoBehaviour
{
    [SerializeField] BossMenu bossCustomData;
    [SerializeField] Text zoneName;
    [SerializeField] Text bossName;
    [SerializeField] Text bossNumericHp;
    [SerializeField] Text bossPercentHp;
    [SerializeField] Text armorLevel;
    [SerializeField] Image hpTextureLayer;
    [SerializeField] Image armorTextureLayer1;
    [SerializeField] Image armorTextureLayer2;
    [SerializeField] Image armorTextureLayer3;
    [SerializeField] WinCondition winScript;

    string abbreviationMark = "";
    float[] hpScalingValues = new float[] { 1f, 1.05f, 1.1f, 1.21f, 1.33f, 1.46f, 1.61f, 1.77f, 1.95f, 2.14f, 2.36f, 2.59f, 2.85f, 3.14f, 3.45f };

    public float bosscalculatedValue;
    public float bossFullHp;
    float roundedbosscalculatedValue;
    float activeArmorLayer;
    float bossHpValue;
    public float bossHpDamaged;

    public float bossActuallHp;
    float calculatedProgressBar;


    bool MyFunctionCalled = false;

    public void StartSim()
    {
        CopyBossData();
        ManageBoss();
        ManageHpBar();
    }

    public void ManageBoss()
    {
        if (bossActuallHp <= 0)
        {
            bossNumericHp.text = "";
            bossPercentHp.text = "Dead";
        }
        else
        {
            bosscalculatedValue = bossActuallHp;
            roundedbosscalculatedValue = bosscalculatedValue;
            RoundBossHp();
            bossNumericHp.text = string.Format("{0:F2} " + abbreviationMark, roundedbosscalculatedValue);
        }
    }

    public void ManageHpBar()
    {
        if (bossHpDamaged <= 0)
        {
            calculatedProgressBar = 0;
            winScript.ShowWinScreen();
        }
        if (bossHpDamaged > 0)
        {
            calculatedProgressBar = bossHpDamaged / bossHpValue;

            if (calculatedProgressBar <= 1)
            {
                bossPercentHp.text = string.Format("{0:F1}% Hp", (calculatedProgressBar * 100));
            }
            else if (calculatedProgressBar == 2)
            {
                bossPercentHp.text = "100% Hp + " + ((calculatedProgressBar * 100) - 100) + "% Armor";
            }
            else if (calculatedProgressBar == 3)
            {
                bossPercentHp.text = "100% Hp + " + ((calculatedProgressBar * 100) - 100) + "% Armor";
            }
            else
            {
                bossPercentHp.text = string.Format("100% Hp + {0:F1}% Armor", (calculatedProgressBar * 100) - 100);
            }
        }

        MyFunctionCalled = true;
    }

    void Update()
    {
        if (MyFunctionCalled == true)
        {
            if ((armorTextureLayer1.fillAmount == 0) && (armorTextureLayer2.fillAmount == 0) && (armorTextureLayer3.fillAmount == 0))
            {
                hpTextureLayer.fillAmount = Mathf.Lerp(hpTextureLayer.fillAmount, calculatedProgressBar, Time.deltaTime * 60);
                if (hpTextureLayer.fillAmount <= (calculatedProgressBar + 0.001f))
                {
                    MyFunctionCalled = false;
                }
            }
            else if ((hpTextureLayer.fillAmount == 1) && (armorTextureLayer1.fillAmount != 0) && (armorTextureLayer2.fillAmount == 0) && (armorTextureLayer3.fillAmount == 0))
            {
                armorTextureLayer1.fillAmount = Mathf.Lerp(armorTextureLayer1.fillAmount, (calculatedProgressBar - 1), Time.deltaTime * 60);
                if (armorTextureLayer1.fillAmount <= ((calculatedProgressBar - 1) + 0.001f))
                {
                    MyFunctionCalled = false;
                }
            }
            else if ((hpTextureLayer.fillAmount == 1) && (armorTextureLayer1.fillAmount == 1) && (armorTextureLayer2.fillAmount != 0) && (armorTextureLayer3.fillAmount == 0))
            {
                armorTextureLayer2.fillAmount = Mathf.Lerp(armorTextureLayer2.fillAmount, (calculatedProgressBar - 2), Time.deltaTime * 60);
                if (armorTextureLayer2.fillAmount <= ((calculatedProgressBar - 2) + 0.001f))
                {
                    MyFunctionCalled = false;
                }
            }
            else if ((hpTextureLayer.fillAmount == 1) && (armorTextureLayer1.fillAmount == 1) && (armorTextureLayer2.fillAmount == 1) && (armorTextureLayer3.fillAmount != 0))
            {
                armorTextureLayer3.fillAmount = Mathf.Lerp(armorTextureLayer3.fillAmount, (calculatedProgressBar - 3), Time.deltaTime * 60);
                if (armorTextureLayer3.fillAmount <= ((calculatedProgressBar - 3) + 0.001f))
                {
                    MyFunctionCalled = false;
                }
            }
        }
    }

    void RoundBossHp()
    {
        if ((bosscalculatedValue >= 1000) && (bosscalculatedValue < 1000000))
        {
            roundedbosscalculatedValue = bosscalculatedValue / 1000;
            abbreviationMark = "K";
        }
        else if ((bosscalculatedValue >= 1000000) && (bosscalculatedValue < 1000000000))
        {
            roundedbosscalculatedValue = bosscalculatedValue / 1000000;
            abbreviationMark = "Mln";
        }
        else if ((bosscalculatedValue >= 1000000000) && (bosscalculatedValue < 1000000000000))
        {
            roundedbosscalculatedValue = bosscalculatedValue / 1000000000;
            abbreviationMark = "B";
        }
        else
        {
            abbreviationMark = "";
        }
    }

    public void CopyBossData()
    {
        winScript.ResetWinScreen();
        winScript.bossOrder = bossCustomData.bossOrder;
        if (winScript.record < bossCustomData.record)
        {
            winScript.record = bossCustomData.record;
        }
        winScript.armorlevel = bossCustomData.armorValue;

        zoneName.text = bossCustomData.zoneName.text;
        bossName.text = bossCustomData.bossName.text;
        bossNumericHp.text = bossCustomData.bossNumericHp.text;
        bossPercentHp.text = bossCustomData.bossPercentHp.text;
        armorLevel.text = bossCustomData.armorLevel.text;
        int armorValue = bossCustomData.armorValue;

        roundedbosscalculatedValue = bossCustomData.roundedbosscalculatedValue;
        activeArmorLayer = bossCustomData.activeArmorLayer;

        bossHpValue = bossCustomData.bossHpValue;
        hpTextureLayer.fillAmount = 1;
        armorTextureLayer1.fillAmount = bossCustomData.armorTextureLayer1.fillAmount;
        armorTextureLayer2.fillAmount = bossCustomData.armorTextureLayer2.fillAmount;
        armorTextureLayer3.fillAmount = bossCustomData.armorTextureLayer3.fillAmount;
        bossFullHp = bossCustomData.bosscalculatedValue;
}
}
