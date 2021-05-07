using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRaidBg : MonoBehaviour
{
    [SerializeField] GameObject screenToActivate;
    [SerializeField] GameObject screenToDeactivate;

    void OnMouseDown()
    {
        screenToActivate.SetActive(true);
        screenToDeactivate.SetActive(false);
    }
}
