using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>{

  public List<Upgrade> Upgrades = new();
  public List<Sprite> Sprites = new();

  public override void Awake()
  {
    base.Awake();
    Upgrades.Add(new VipersTongueUpgrade(0, Sprites[0]));
  }
}
