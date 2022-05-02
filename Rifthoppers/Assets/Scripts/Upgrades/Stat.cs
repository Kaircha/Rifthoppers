using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat {
  public StatType Type;
  public float Value;
}

public enum StatType {
  EnergyTotal,
  PlayerSpeedMulti,
  PlayerFirerate,
  ProjectileCount, // TO-DO
  ProjectileSpeedMulti,
  ProjectileDamage,
  ProjectileDamageMulti,
  ProjectileHoming,
  ProjectileForks,
  ProjectilePierces,
  ProjectileChains,
  ProjectileExplosion, // TO-DO
  ProjectileSizeMulti,
  PowerCostMulti,
  PowerRange,
  PowerStrength,
}