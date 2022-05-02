using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeCard : MonoBehaviour {
  public TextMeshProUGUI Title; 
  public TextMeshProUGUI Description;
  public Image Card;
  public Upgrade Upgrade;
  public List<Sprite> Sprites;

  public void Set(Upgrade upgrade) {
    Upgrade = upgrade;
    Title.text = upgrade.name;
    Description.text = upgrade.Description;
    Card.sprite = Sprites[(int)upgrade.Rarity];
  }
  public void Unlock() => Upgrade.Apply();
}
