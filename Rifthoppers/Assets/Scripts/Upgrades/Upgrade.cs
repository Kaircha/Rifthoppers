using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public class Upgrade : ScriptableObject {
  public RarityType Rarity;
  public List<Stat> Stats = new List<Stat>();
  [TextArea] public string Description;
  public Sprite Sprite;

  public virtual void Apply() {
    StatManager.Instance.Upgrades.Add(this);
    foreach (Stat stat in Stats) {
      if (StatManager.Instance.Has(stat.Type)) {
        StatManager.Instance.Add(stat.Type, stat.Value);
      } else {
        StatManager.Instance.Set(stat.Type, stat.Value);
      }
    }
  }
}

public enum RarityType { 
  Common,
  Uncommon,
  Rare,
  Legendary,
}