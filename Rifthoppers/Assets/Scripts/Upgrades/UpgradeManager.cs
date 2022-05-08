using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpgradeManager : Singleton<UpgradeManager> {
  public List<Upgrade> Upgrades = new();
  public List<Sprite> Sprites = new();

  public event Action<Upgrade> OnUpgradeTaken;

  public override void Awake() {
    base.Awake();
    Upgrades.Add(new VipersTongueUpgrade(0, Sprites[0]));
    Upgrades.Add(new SuperchargedUpgrade(1, Sprites[1]));
  }

  public void TakeUpgrade(Upgrade upgrade) {
    OnUpgradeTaken?.Invoke(upgrade);
  }
}