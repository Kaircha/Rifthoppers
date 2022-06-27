using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pyromaniac", menuName = "Upgrades/Pyromaniac")]
public class PyromaniacUpgrade : Upgrade {
  [Min(0)] public float Firerate;
  [Min(0)] public float ProjectileDamageMulti;
  [Min(0)] public float IgniteDamage = 2f;
  [Min(0)] public float IgniteDuration = 5f;

  private IgnitedCondition IgnitedCondition;

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

  // Technically doesn't stack with other sources of ignite chance
  //public void IgniteOnDamageDealt(Entity dealer, Entity receiver, float amount, bool isDoT) {
  //  if (dealer == null || receiver == null || amount == 0 || isDoT) return;
  //  // 20% + Luck * 5% Chance to apply a Poison stack 
  //  if (Random.Range(0f, 100f) < 20f + 5f * Entity.Stats.PlayerLuck) {
  //    receiver.AddEffect(new IgniteEffect(IgniteDamage, IgniteDuration));
  //  }
  //}
}