using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorLevel : MonoBehaviour
{
    [SerializeField] BossMenu bossScript;
    int level;

    void OnMouseOver()
    {
        GetComponent<Transform>().localScale = new Vector3(0.9f, 0.9f, 0);
    }

    void OnMouseExit()
    {
        GetComponent<Transform>().localScale = new Vector3(0.74f, 0.74f, 0);
    }

    void OnMouseDown()
    {
        int level = bossScript.armorValue;
        if ((GetComponent<TextMesh>().text == "-") && (level > 1))
        {
            bossScript.armorValue--;
            Debug.Log("Armor Decremented");
            Debug.Log(bossScript.armorValue);
        }
        else if ((GetComponent<TextMesh>().text == "+") && (level < 15))
        {
            bossScript.armorValue++;
            Debug.Log("Armor Incremented");
            Debug.Log(bossScript.armorValue);
        }
    }
}
