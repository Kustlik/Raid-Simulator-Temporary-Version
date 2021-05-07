using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    [SerializeField] string playerName;
    [SerializeField] string playerRole;
    [SerializeField] string playerClass;
    [SerializeField] string playerProximity;
    [SerializeField] float[] dps;
    [SerializeField] float[] hps;
    [SerializeField] float[] survivability;

    public string GetPlayerName()
    {
        return playerName;
    }

    public string GetPlayerRole()
    {
        return playerRole;
    }

    public string GetPlayerClass()
    {
        return playerClass;
    }

    public string GetPlayerProximity()
    {
        return playerProximity;
    }

    public float[] GetDps()
    {
        return dps;
    }

    public float[] GetHps()
    {
        return hps;
    }

    public float[] GetSurvi()
    {
        return survivability;
    }
}