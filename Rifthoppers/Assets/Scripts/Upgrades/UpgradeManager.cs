using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

public class UpgradeManager : Singleton<UpgradeManager> {

  public List<Upgrade> Upgrades = new();
  public List<Sprite> Sprites = new();

  public event Action<Upgrade> OnUpgradeTaken;

  public override void Awake() {
    base.Awake();
    Upgrades.Add(new VipersTongueUpgrade(0, Sprites[0]));
  }


  public void TakeUpgrade(Upgrade upgrade)
  {
    OnUpgradeTaken?.Invoke(upgrade);
  }
}