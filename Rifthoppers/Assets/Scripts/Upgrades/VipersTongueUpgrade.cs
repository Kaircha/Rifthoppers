using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Viper's Tongue", menuName = "Upgrades/Viper's Tongue")]
public class VipersTongueUpgrade : Upgrade {
  public override void OnAdd() {
    Entity.OnDamageDealt += PoisonOnDamageDealt;
    Entity.Health.OnDamageTaken += PoisonOnDamageTaken;
    Entity.Stats.ProjectileForks += 1;
  }

  public override void OnRemove() {
    Entity.OnDamageDealt -= PoisonOnDamageDealt;
    Entity.Health.OnDamageTaken -= PoisonOnDamageTaken;
    Entity.Stats.ProjectileForks -= 1;
  }

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
    dealer.AddEffect(new PoisonEffect(2, 5));
  }
}