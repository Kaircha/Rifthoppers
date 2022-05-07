using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>{

  public List<Upgrade> Upgrades = new();

  public void Awake()
  {
    Upgrades.Add(new VipersTongueUpgrade());
  }
}
