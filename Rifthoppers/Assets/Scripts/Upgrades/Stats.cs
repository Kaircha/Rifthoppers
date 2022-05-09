using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {
  public float PlayerSpeedMulti = 1f;
  public float PlayerFirerate = 3f;

  public int ProjectileCount = 1;
  public float ProjectileSpeed = 15f;
  public float ProjectileSpeedMulti = 1f;
  public float ProjectileDamage = 6f;
  public float ProjectileDamageMulti = 1f;
  public float ProjectileHoming = 0f;
  public int ProjectileForks = 1; 
  public int ProjectileChains = 0;
  public float ProjectileSizeMulti = 1f;

  public float PowerRange = 5f;
  public float PowerStrength = 20f;
  public float PowerCostMulti = 1f;

  public int MaxCharges = 1;

  public int Weapon = 0;
  public bool isFlying = false;

  public Color BulletColor;
}