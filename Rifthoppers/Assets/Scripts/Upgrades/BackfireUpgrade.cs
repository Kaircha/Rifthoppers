using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackfireUpgrade : Upgrade {
  public override string Name => "Backfire";
  public override string Quote => "Backward Combustion";
  public override List<Modifier> Modifiers => new() { 
    new(ModifierType.Default, ""),
  };
  public override int Weight => throw new System.NotImplementedException();

  public override void OnAdd() {
    GameObject backBlaster = Entity.UpgradeVFX.ApplyVFX(1);
    // Add backwards shooting

    Blaster blaster = Entity.GetComponentInChildren<Blaster>(true);
    blaster.OnShoot += SelfIgnite;
  }

  public override void OnRemove() {
    Entity.UpgradeVFX.DeleteVFX(1);
    // Remove backwards shooting

    Blaster blaster = Entity.GetComponentInChildren<Blaster>(true);
    blaster.OnShoot -= SelfIgnite;
  }

  private void SelfIgnite() {
    if (Random.Range(0f, 100f) < 100f - 20f * Entity.Stats.PlayerLuck)
    Entity.AddEffect(new IgniteEffect(1, 5));
  }
}