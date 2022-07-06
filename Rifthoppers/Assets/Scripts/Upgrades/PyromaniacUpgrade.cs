using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pyromaniac", menuName = "Upgrades/Pyromaniac")]
public class PyromaniacUpgrade : UpgradeObject {
  public override Upgrade Upgrade() => new Pyromaniac(this);

  public class Pyromaniac : Upgrade {
    [Min(0)] public float Firerate;
    [Min(0)] public float ProjectileDamageMulti;
    [Min(0)] public float IgniteDamage = 2f;
    [Min(0)] public float IgniteDuration = 5f;

    private IgnitedCondition IgnitedCondition;

    public Pyromaniac(UpgradeObject obj) {
      Object = obj;
    }

    public override void Add() {
      Brain.PlayerStats.IgniteChanceBase += 20f;
      Brain.PlayerStats.IgniteChancePerLuck += 5f;
      IgnitedCondition = new IgnitedCondition(Brain.Entity);
    }

    public override IEnumerator UpgradeRoutine() {
      while (true) {
        yield return new WaitUntil(() => IgnitedCondition.Satisfied);
        Brain.PlayerStats.Firerate += Firerate;
        Brain.PlayerStats.AmmoStats.AmmoDamageMulti += ProjectileDamageMulti;
        yield return new WaitUntil(() => !IgnitedCondition.Satisfied);
        Brain.PlayerStats.Firerate -= Firerate;
        Brain.PlayerStats.AmmoStats.AmmoDamageMulti -= ProjectileDamageMulti;
      }
    }

    public override void Remove() {
      Brain.PlayerStats.IgniteChanceBase -= 20f;
      Brain.PlayerStats.IgniteChancePerLuck -= 5f;
    }
  }
}