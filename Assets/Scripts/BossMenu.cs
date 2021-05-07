using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMenu : MonoBehaviour
{
    [SerializeField] public TextMesh zoneName;
    [SerializeField] public Text bossName;
    [SerializeField] public Text bossNumericHp;
    [SerializeField] public Text bossPercentHp;
    [SerializeField] public Text armorLevel;
    [SerializeField] public Image armorTextureLayer1;
    [SerializeField] public Image armorTextureLayer2;
    [SerializeField] public Image armorTextureLayer3;
    
    public int armorValue;
    public float activeArmorLayer;
    string abbreviationMark = "";
    
    [SerializeField] public float bossHpValue;
    [SerializeField] public int berserkInSeconds;
    [SerializeField] public string[] causeOfDeath;
    [SerializeField] public string[] benchEvent;
    [SerializeField] public int record;
    [SerializeField] public int bossOrder;
    public float bosscalculatedValue;
    public float roundedbosscalculatedValue;

    void Start()
    {
        GetArmorLayer();
    }

    void Update()
    {
        GetArmorLayer();

        if ((armorTextureLayer1.fillAmount != 1) && (armorTextureLayer2.fillAmount == 0) && (armorTextureLayer3.fillAmount == 0))
        {
            armorTextureLayer1.fillAmount = Mathf.Lerp(armorTextureLayer1.fillAmount, (activeArmorLayer - 1f), Time.deltaTime * 5);
        }
        else if ((armorTextureLayer1.fillAmount == 1) && (armorTextureLayer2.fillAmount != 1) && (armorTextureLayer3.fillAmount == 0))
        {
            armorTextureLayer2.fillAmount = Mathf.Lerp(armorTextureLayer2.fillAmount, (activeArmorLayer - 2f), Time.deltaTime * 5);
            if ((armorTextureLayer1.fillAmount == 1) && (armorTextureLayer2.fillAmount == 0) && (armorTextureLayer3.fillAmount == 0))
            {
                armorTextureLayer1.fillAmount = Mathf.Lerp(armorTextureLayer1.fillAmount, (activeArmorLayer - 1f), Time.deltaTime * 5);
            }
        }
        else if ((armorTextureLayer1.fillAmount == 1) && (armorTextureLayer2.fillAmount == 1) && (armorTextureLayer3.fillAmount != 1))
        {
            armorTextureLayer3.fillAmount = Mathf.Lerp(armorTextureLayer3.fillAmount, (activeArmorLayer - 3f), Time.deltaTime * 5);
            if ((armorTextureLayer1.fillAmount == 1) && (armorTextureLayer2.fillAmount == 1) && (armorTextureLayer3.fillAmount == 0))
            {
                armorTextureLayer2.fillAmount = Mathf.Lerp(armorTextureLayer2.fillAmount, (activeArmorLayer - 2f), Time.deltaTime * 5);
            }
        }
    }

    public void GetArmorLayer()
    {
        int index = 0;
        float[] hpScalingValues = new float[] { 1f, 1.05f, 1.1f, 1.21f, 1.33f, 1.46f, 1.61f, 1.77f, 1.95f, 2.14f, 2.36f, 2.59f, 2.85f, 3.14f, 3.45f };
        int[] armorScalingValues = new int[] { 0, 5, 10, 21, 33, 46, 61, 77, 95, 114, 136, 159, 185, 214, 245 };
        armorLevel.text = armorValue.ToString();

        while (armorValue != (index + 1))
        {
            index++;
        }

        if(index == 0)
        {
            bossPercentHp.text = "100% Hp";
        }
        else
        {
            bossPercentHp.text = "100% Hp + " + armorScalingValues[index] + "% Armor";
        }

        bosscalculatedValue = bossHpValue * hpScalingValues[index];

        RoundBossHp();

        bossNumericHp.text = System.Math.Round(roundedbosscalculatedValue, 2).ToString() + " " + abbreviationMark;

        activeArmorLayer = hpScalingValues[index];
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
            roundedbosscalculatedValue =  bosscalculatedValue / 1000000000;
            abbreviationMark = "B";
        }
        else
        {
            abbreviationMark = "";
        }
    }
}
