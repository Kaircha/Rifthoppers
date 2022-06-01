using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour {
  public RectTransform Fill;
  public float Speed = 5f;
  private float Dynamic;
  public float Progress => RiftManager.Instance.Encounter.Progress;

  private void Update() {
    Dynamic = Mathf.Lerp(Dynamic, Progress, Speed * Time.deltaTime);
    Fill.transform.localScale = new Vector3(Dynamic, 1, 1);
  }
}