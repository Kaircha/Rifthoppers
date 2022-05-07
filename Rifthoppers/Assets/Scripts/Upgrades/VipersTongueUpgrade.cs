using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VipersTongueUpgrade : Upgrade {
  public override string Name => "Viper's Tongue";
  public override int ID => 0;
  public override Sprite Sprite => throw new System.NotImplementedException();
  public override string Quote => throw new System.NotImplementedException();
  public override string Description => throw new System.NotImplementedException();
  public override int Weight => throw new System.NotImplementedException();

  public override void OnAdd(Entity entity) {
    entity.OnDamageDealt += PoisonOnDamageDealt;
    entity.Health.OnDamageTaken += PoisonOnDamageTaken;
    // Find a better solution for the StatManager
    StatManager.Instance.Set(StatType.ProjectileForks, 2);
    // Poison Resistance?
  }

  public override void OnRemove(Entity entity) {
    entity.OnDamageDealt -= PoisonOnDamageDealt;
    entity.Health.OnDamageTaken -= PoisonOnDamageTaken;
  }

  public void PoisonOnDamageDealt(Entity dealer, Entity receiver, float amount, bool isDoT) {
    if (amount == 0 || isDoT) return;
    receiver.AddEffect(new PoisonEffect());
  }

  // Effect should only occur on touching an enemy
  private void PoisonOnDamageTaken(Entity dealer, Entity receiver, float amount, bool isDoT) {
    if (amount == 0 || isDoT) return;
    dealer.AddEffect(new PoisonEffect());
  }
}