using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModifierUI : MonoBehaviour {
  public TextMeshProUGUI Description;
  public Image Image;
  public List<Sprite> Sprites;

  public void DisplayModifier(Modifier modifier) {
    Description.text = modifier.Description;
    Image.sprite = Sprites[(int)modifier.Type];
  }
}