using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBar : MonoBehaviour {
  public RectTransform Fill;
  public RectTransform Underfill;
  private Energy Energy => RiftManager.Instance.Energy;

  private void Update() {
    Fill.transform.localScale = new Vector3(Energy.StaticPercentage, 1, 1);
    Underfill.transform.localScale = new Vector3(Energy.Percentage, 1, 1);
  }
}