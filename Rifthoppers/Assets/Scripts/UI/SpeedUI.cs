using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeedUI : MonoBehaviour {
  public Image Image;
  public TextMeshProUGUI Text;

  public void SliderUpdate(float value) {
    float speedMultiplier = 0.5f + value * 1.5f;
    RiftManager.Instance.SpeedMultiplier = speedMultiplier;
    Text.text = $"{Mathf.FloorToInt(10f * speedMultiplier)/10f}x";
  }

  public void Update() {
    Image.rectTransform.Rotate(new Vector3(0, 0, -30f * Time.deltaTime));
  }
}