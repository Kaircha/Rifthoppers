  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {
  public int Luck = 0;
  public float Speed => SpeedBase * SpeedMulti;
  public float SpeedBase = 8f;
  public float SpeedMulti = 1f;

  public float Firerate = 6f;

  public float IgniteChance => IgniteChanceBase + IgniteChancePerLuck * Luck;
  public float IgniteChanceBase = 0f;
  public float IgniteChancePerLuck = 0f;
  public float PoisonChance => PoisonChanceBase + PoisonChancePerLuck * Luck;
  public float PoisonChanceBase = 0f;
  public float PoisonChancePerLuck = 0f;

  public float PowerRange = 5f;
  public float PowerStrength = 20f;
  public float PowerCostMulti = 1f;

  public FakeBullet FakeBullet; // This shouldn't be here

  public AmmoStats AmmoStats;



  // Enemy Stats
  public float Health;
  public float Damage;
  public int MinOrbs;
  public int MaxOrbs;
}