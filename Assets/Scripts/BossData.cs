using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Boss Data")]
public class BossData : ScriptableObject
{
    [SerializeField] public PlayerData[] players;
    [SerializeField] public string zoneName;
    [SerializeField] public string bossName;
    [SerializeField] public int bossOrder;
    [SerializeField] public float bossHpValue;
    [SerializeField] public int berserkInSeconds;
    [SerializeField] public string[] causeOfDeath;
    [SerializeField] public string[] benchEvent;
    [SerializeField] public string howManyHealers;
    [SerializeField] public string howManyTanks;
}