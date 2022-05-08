using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioUI : MonoBehaviour {
  public Image Image;
  public Sprite On;
  public Sprite Off;
  public TextMeshProUGUI Text;

  public void SliderUpdated(float value) {
    if (value < 0.3f) Image.sprite = Off;
    else Image.sprite = On;
    Text.text = $"{Mathf.FloorToInt(value * 100f)}%";
  }
}