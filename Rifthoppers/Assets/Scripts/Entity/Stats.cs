  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {
  public int Luck = 0;
  public float PlayerSpeedMulti = 1f;
  public float PlayerFirerate = 3f;

  public int ProjectileCount = 1;
  public float ProjectileSpeed = 15f;
  public float ProjectileSpeedMulti = 1f;
  public float ProjectileDamage = 6f;
  public float ProjectileDamageMulti = 1f;
  public float ProjectileHoming = 0f;
  public int ProjectileForks = 1;
  public int ProjectileBounces = 0;
  public int ProjectileChains = 0;
  public int ProjectilePierces = 0;
  public float ProjectileSizeMulti = 1f;
  public Color ProjectileColor;

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