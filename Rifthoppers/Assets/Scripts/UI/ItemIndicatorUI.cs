using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemIndicatorUI : MonoBehaviour {
  public Image Image;
  public TextMeshProUGUI Name;
  public TextMeshProUGUI Quote;
  public Transform Holder;
  public ModifierUI ModifierPrefab;

  public void ShowItemIndicator(Upgrade upgrade) {
    foreach (Transform child in Holder) {
      Destroy(child.gameObject);
    }

    Image.sprite = upgrade.Object.Sprite;
    Name.text = upgrade.Object.name;
    Quote.text = upgrade.Object.Quote;
    foreach (Modifier modifier in upgrade.Object.Modifiers) {
      Instantiate(ModifierPrefab, Holder).DisplayModifier(modifier);
    }
    gameObject.SetActive(true);
    Canvas.ForceUpdateCanvases();
  }
  public void HideItemIndicator() {
    gameObject.SetActive(false);
  }
}