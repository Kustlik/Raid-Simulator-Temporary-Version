using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickMenu : MonoBehaviour
{
    [SerializeField] GameObject hideMenu;
    [SerializeField] GameObject nextMenu;

    void OnMouseOver()
    {
        GetComponent<Transform>().localScale = new Vector3(0.9f, 0.9f, 1);
    }

    void OnMouseExit()
    {
        GetComponent<Transform>().localScale = new Vector3(0.74f, 0.74f, 1);
    }

    void OnMouseDown()
    {
        GetComponent<Transform>().localScale = new Vector3(0.74f, 0.74f, 1);
        hideMenu.SetActive(false);
        nextMenu.SetActive(true);
    }
}
