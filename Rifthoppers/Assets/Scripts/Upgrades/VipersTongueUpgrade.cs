using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Viper's Tongue", menuName = "Upgrades/Viper's Tongue")]
public class VipersTongueUpgrade : Upgrade {
  [Min(0)] public float PoisonDamage = 2f;
  [Min(0)] public float PoisonDuration = 5f;

  public override void Add() {
    Entity.OnDamageDealt += PoisonOnDamageDealt;
    Entity.Health.OnDamageTaken += PoisonOnDamageTaken;
    Entity.Stats.ProjectileForks += 1;
  }
  public override IEnumerator UpgradeRoutine() { yield return null; }
  public override void Remove() {
    Entity.OnDamageDealt -= PoisonOnDamageDealt;
    Entity.Health.OnDamageTaken -= PoisonOnDamageTaken;
    Entity.Stats.ProjectileForks -= 1;
  }

  // Technically doesn't stack with other sources of poison chance
  public void PoisonOnDamageDealt(Entity dealer, Entity receiver, float amount, bool isDoT) {
    if (dealer == null || receiver == null || amount == 0 || isDoT) return;
    // 20% + Luck * 5% Chance to apply a Poison stack 
    if (Random.Range(0f, 100f) < 20f + 5f * Entity.Stats.PlayerLuck) {
      receiver.AddEffect(new PoisonEffect(2, 5));
    }
  }

  // Effect should only occur on touching an enemy
  private void PoisonOnDamageTaken(Entity dealer, Entity receiver, float amount, bool isDoT) {
    if (dealer == null || isDoT) return;
    dealer.AddEffect(new PoisonEffect(PoisonDamage, PoisonDuration));
  }
}