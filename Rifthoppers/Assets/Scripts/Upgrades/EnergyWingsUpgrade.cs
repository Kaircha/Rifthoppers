using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyWingsUpgrade : Upgrade {
  public override string Name => "Energy Wings";
  public override string Quote => "Fly over Stuff!";
  public override string Description => throw new System.NotImplementedException();
  public override int Weight => throw new System.NotImplementedException();

  public EnergyWingsUpgrade(int id, Sprite sprite) {
    ID = id;
    Sprite = sprite;
  }

  public override void OnAdd(Entity entity) {
    entity.UpgradeVFX.ApplyVFX(0);
  }

  public override void OnRemove(Entity entity) {
    entity.UpgradeVFX.DeleteVFX(0);
  }
}