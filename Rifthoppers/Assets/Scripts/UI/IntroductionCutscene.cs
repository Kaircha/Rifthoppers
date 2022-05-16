using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class IntroductionCutscene : MonoBehaviour {
  public List<Frame> Frames = new();

  private void Start() => StartCoroutine(IntroductionRoutine());

  private IEnumerator IntroductionRoutine() {
    foreach (Frame frame in Frames) {
      frame.Card.SetActive(true);
      yield return new WaitForSeconds(frame.Duration);
      frame.Card.SetActive(false);
    }
    SceneManager.LoadScene("Persistent");
  }

  public void Skip() {
    StopAllCoroutines();
    SceneManager.LoadScene("Persistent");
  }
}

[System.Serializable]
public class Frame {
  public GameObject Card;
  public float Duration;
}