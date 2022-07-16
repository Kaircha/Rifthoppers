using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour {
  public RectTransform Fill;
  //public float Speed = 5f;
  public void UpdateBar(float progress) => Fill.transform.localScale = new Vector3(Mathf.Clamp01(progress), 1, 1);
}