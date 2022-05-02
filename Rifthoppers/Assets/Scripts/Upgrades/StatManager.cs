using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : Singleton<StatManager> {
  public override bool Persistent => false;
  public List<Upgrade> Upgrades = new List<Upgrade>();
  public Dictionary<StatType, float> Stats = new Dictionary<StatType, float>();

  public override void Awake() {
    base.Awake();
    Initialize();
  }

  public bool Has(StatType type) => Stats.ContainsKey(type);
  public float Get(StatType type) => Stats.TryGetValue(type, out float value) ? value : default;
  public void Set(StatType type, float value) => Stats[type] = value;
  public void Add(StatType type, float value) => Stats[type] += value;
  public void Initialize() {
    Upgrades = new List<Upgrade>();
    Stats = new Dictionary<StatType, float> {
      { StatType.EnergyTotal, 100 },
      { StatType.PlayerSpeedMulti, 1 },
      { StatType.PlayerFirerate, 3 },
      { StatType.ProjectileCount, 1 },
      { StatType.ProjectileSpeedMulti, 1 },
      { StatType.ProjectileDamage, 6 },
      { StatType.ProjectileDamageMulti, 1 },
      { StatType.ProjectileHoming, 0 },
      { StatType.ProjectileForks, 1 },
      { StatType.ProjectilePierces, 0 },
      { StatType.ProjectileChains, 0 },
      { StatType.ProjectileExplosion, 0 },
      { StatType.ProjectileSizeMulti, 1 },
      { StatType.PowerCostMulti, 1 },
      { StatType.PowerRange, 5 },
      { StatType.PowerStrength, 20 },
    };
  }
}