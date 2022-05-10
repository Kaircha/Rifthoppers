using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VipersTongueUpgrade : Upgrade {
  public override string Name => "Viper's Tongue";
  public override string Quote => "Become Venomous.";
  public override string Description => throw new System.NotImplementedException();
  public override int Weight => throw new System.NotImplementedException();

  public VipersTongueUpgrade(int id, Sprite sprite){
    ID = id;
    Sprite = sprite;
  }

  public override void OnAdd(Entity entity) {
    entity.OnDamageDealt += PoisonOnDamageDealt;
    entity.Health.OnDamageTaken += PoisonOnDamageTaken;
    entity.Stats.ProjectileForks += 1;
    // Poison Resistance?
  }

  public override void OnRemove(Entity entity) {
    entity.OnDamageDealt -= PoisonOnDamageDealt;
    entity.Health.OnDamageTaken -= PoisonOnDamageTaken;
    entity.Stats.ProjectileForks -= 1;
  }

  public void PoisonOnDamageDealt(Entity dealer, Entity receiver, float amount, bool isDoT) {
    if (dealer == null || receiver == null || amount == 0 || isDoT) return;
    // 20% + Luck * 5% Chance to apply a Poison stack 
    if (Random.Range(0f, 100f) < 20f + dealer.Stats.PlayerLuck * 5f) return;
    receiver.AddEffect(new PoisonEffect(2, 5));
  }

  // Effect should only occur on touching an enemy
  private void PoisonOnDamageTaken(Entity dealer, Entity receiver, float amount, bool isDoT) {
    if (dealer == null || isDoT) return;
    dealer.AddEffect(new PoisonEffect(2, 5));
  }
}