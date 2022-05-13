using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyWingsUpgrade : Upgrade {
  public override string Name => "Energy Wings";
  public override string Quote => "Fly over Stuff!";
  public override List<Modifier> Modifiers => new() {
    new(ModifierType.Default, "Flight"),
  };
  public override int Weight => throw new System.NotImplementedException();

  public EnergyWingsUpgrade(int id, Sprite sprite) {
    ID = id;
    Sprite = sprite;
  }

  private GameObject Clone;
  public override void OnAdd() {
    Clone = Entity.UpgradeVFX.ApplyVFX(0);
    Entity.IsFlying = true;
  }

  public override void OnRemove() {
    Entity.UpgradeVFX.DeleteVFX(Clone);
    // Assumes the default IsFlying of Entity is false; Also breaks if there are two flying upgrades!
    Entity.IsFlying = false;
  }
}