using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour {
  public RectTransform Fill;
  public float Speed = 5f;
  private float Dynamic;
  public bool IsStarted => GameManager.Instance.CurrentWave.Encounter.IsStarted;
  public bool IsFinished => GameManager.Instance.CurrentWave.Encounter.IsFinished;
  public float Progress => GameManager.Instance.CurrentWave.Encounter.Progress;

  // Maybe this should be updated by the Encounter
  private IEnumerator Start() {
    yield return new WaitUntil(() => GameManager.Instance.CurrentWave != null);
    yield return new WaitUntil(() => IsStarted);
    Dynamic = Mathf.Lerp(Dynamic, Progress, Speed * Time.deltaTime);
    Fill.transform.localScale = new Vector3(Dynamic, 1, 1);
  }
}