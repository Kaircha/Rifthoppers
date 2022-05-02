using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateImage : MonoBehaviour {
  public Image Image;
  public Sprite On;
  public Sprite Off;

  public void Slider(float value) {
    if (value < 0.3f) Image.sprite = Off;
    else Image.sprite = On;
  }
}
