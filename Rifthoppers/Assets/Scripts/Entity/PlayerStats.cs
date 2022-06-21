  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
  public int Luck = 0;
  public float Speed => SpeedBase * SpeedMulti;
  public float SpeedBase = 8f;
  public float SpeedMulti = 1f;

  public float Firerate = 6f;

  public int AmmoCount = 1;
  public float AmmoSpeed => AmmoBaseSpeed * AmmoSpeedMulti;
  public float AmmoBaseSpeed = 15f;
  public float AmmoSpeedMulti = 1f;
  public float AmmoDamage => AmmoBaseDamage * AmmoDamageMulti;
  public float AmmoBaseDamage = 6f;
  public float AmmoDamageMulti = 1f;
  public float AmmoHoming = 0f;
  public int AmmoSplits = 1;
  public int AmmoReflects = 0;
  public int AmmoPierces = 0;
  public float AmmoSize = 1f;
  public Color AmmoColor = Color.white;

  public float IgniteChance => IgniteChanceBase + IgniteChancePerLuck * Luck;
  public float IgniteChanceBase = 0f;
  public float IgniteChancePerLuck = 0f;
  public float PoisonChance => PoisonChanceBase + PoisonChancePerLuck * Luck;
  public float PoisonChanceBase = 0f;
  public float PoisonChancePerLuck = 0f;

  public float PowerRange = 5f;
  public float PowerStrength = 20f;
  public float PowerCostMulti = 1f;

  public WeaponType WeaponType;

  public FakeBullet FakeBullet; // This shouldn't be here
}