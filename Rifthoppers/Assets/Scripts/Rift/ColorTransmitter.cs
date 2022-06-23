using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColorTransmitter : MonoBehaviour {
  public List<ColorReceiver> Receivers = new();
  public SpriteRenderer Background;
  public Color GrassColor = Color.white;
  public Color LeavesColor = Color.white;
  public Color PrimaryColor = Color.white;
  public Color BackgroundColor = Color.white;
  private float Timer = 0f;

  public void Update() {
    Timer += Time.deltaTime;
    if (Timer > 0.5f) {
      Timer = 0f;
      GetReceivers();
      Assign();
    }
  }

  public void GetReceivers() {
    Receivers = GetComponentsInChildren<ColorReceiver>().ToList();
  }

  public void Assign() {
    Background.color = BackgroundColor;
    Receivers.ForEach(receiver => receiver.Assign(GrassColor, LeavesColor, PrimaryColor));    
  }
}