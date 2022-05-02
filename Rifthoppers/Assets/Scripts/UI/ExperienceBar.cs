using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceBar : MonoBehaviour {
  public RectTransform Fill;
  private float progress;
  private Experience Experience => RiftManager.Instance.Experience;

  private void Start() {
    progress = Experience.Progress;
  }

  private void Update() {
    progress = Mathf.Lerp(progress, Experience.Progress, 5f * Time.deltaTime);
    Fill.transform.localScale = new Vector3(progress, 1, 1);
  }

  //private void Start() {
  //  UpdateBar();
  //}

  //private void OnEnable() {
  //  Experience.OnLearn += UpdateBar;
  //}

  //private void OnDisable() {
  //  Experience.OnLearn -= UpdateBar;
  //}

  //private void UpdateBar() {
  //  Fill.transform.localScale = new Vector3(Experience.Progress, 1, 1);
  //}
}