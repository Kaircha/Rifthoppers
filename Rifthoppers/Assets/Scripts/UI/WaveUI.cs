using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour {
  public TextMeshProUGUI WaveText;

  public void OnEnable() {
    RiftManager.Instance.OnWaveStarted += ShowWaveUI;
    //RiftManager.Instance.OnWaveEnded += HideWaveUI;
  }

  public void OnDisable() {
    RiftManager.Instance.OnWaveStarted -= ShowWaveUI;
    //RiftManager.Instance.OnWaveEnded -= HideWaveUI;
  }

  public void ShowWaveUI() => StartCoroutine(ShowWaveUIRoutine());
  public IEnumerator ShowWaveUIRoutine() {
    WaveText.color = Color.white; // replace with animation
    WaveText.text = $"Wave {RiftManager.Instance.Experience.Level}";
    yield return new WaitForSeconds(2f);
    WaveText.color = Color.clear;
  }

  //public void HideWaveUI() {
  //  WaveText.color = Color.clear; // replace with animation
  //}
}