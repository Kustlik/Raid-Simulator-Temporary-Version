using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintWindow : MonoBehaviour
{
    [SerializeField] string hint;
    [SerializeField] Text hintLocation;
    string customHint = "Choose your Raid group, remember to fullfill minimum requirements. Choosing an affix, invalidates estabilishing new record. Remember that Damage is randomized through different parses, so different simulations gives different results.";

    void Start()
    {
        hintLocation.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
        hintLocation.text = customHint;
    }

    void OnMouseOver()
    {
        hintLocation.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
        hintLocation.text = hint;
    }

    void OnMouseExit()
    {
        hintLocation.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
        hintLocation.text = customHint;
    }
}
