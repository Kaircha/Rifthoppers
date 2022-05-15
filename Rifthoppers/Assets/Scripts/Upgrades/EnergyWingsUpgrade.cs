using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Energy Wings", menuName = "Upgrades/Energy Wings")]
public class EnergyWingsUpgrade : Upgrade {
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