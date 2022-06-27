using System;
using UnityEngine;

[Serializable]
public class AmmoStats{
  public WeaponType WeaponType;
  public int AmmoCount = 1;
  public float AmmoSpeed => AmmoBaseSpeed * AmmoSpeedMulti;
  public float AmmoBaseSpeed = 15f;
  public float AmmoSpeedMulti = 1f;
  public float AmmoDamage => AmmoBaseDamage * AmmoDamageMulti;
  public float AmmoBaseDamage = 6f;
  public float AmmoDamageMulti = 1f;
  public float AmmoHoming = 0f;
  public int AmmoSplits = 0;
  public int AmmoReflects = 0;
  public int AmmoPierces = 0;
  public float AmmoSize = 1f;
  public Color AmmoColor = Color.white;

  public AmmoStats(AmmoStats stats){

    WeaponType = stats.WeaponType;
    AmmoCount = stats.AmmoCount;
    AmmoBaseSpeed = stats.AmmoBaseSpeed;
    AmmoSpeedMulti = stats.AmmoSpeedMulti;
    AmmoBaseDamage = stats.AmmoBaseDamage;
    AmmoDamageMulti = stats.AmmoDamageMulti;
    AmmoHoming = stats.AmmoHoming;
    AmmoSplits = stats.AmmoSplits;
    AmmoReflects = stats.AmmoReflects;
    AmmoPierces = stats.AmmoPierces;
    AmmoSize = stats.AmmoSize;
    AmmoColor = stats.AmmoColor;
  }
}