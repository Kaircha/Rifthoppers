using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Viper's Tongue", menuName = "Upgrades/Viper's Tongue")]
public class VipersTongueUpgrade : UpgradeObject {
  public override Upgrade Upgrade() => new VipersTongue(this);

  public class VipersTongue : Upgrade {
    public VipersTongue(UpgradeObject obj) {
      Object = obj;
    }

    [Min(0)] public float PoisonDamage = 2f;
    [Min(0)] public float PoisonDuration = 5f;

    public override void Add() {
      Brain.Entity.Health.OnDamageTaken += PoisonOnDamageTaken;
      Brain.PlayerStats.PoisonChanceBase += 20f;
      Brain.PlayerStats.PoisonChancePerLuck += 5f;
      Brain.PlayerStats.AmmoStats.AmmoSplits += 1;
    }
    public override IEnumerator UpgradeRoutine() { yield return null; }
    public override void Remove() {
      Brain.Entity.Health.OnDamageTaken -= PoisonOnDamageTaken;
      Brain.PlayerStats.PoisonChanceBase -= 20f;
      Brain.PlayerStats.PoisonChancePerLuck -= 5f;
      Brain.PlayerStats.AmmoStats.AmmoSplits -= 1;
    }

    // Effect should only occur on touching an enemy
    private void PoisonOnDamageTaken(Entity dealer, Entity receiver, float amount, bool isDoT) {
      if (dealer == null || isDoT) return;
      dealer.AddEffect(new PoisonEffect(PoisonDamage, PoisonDuration));
    }
  }
}