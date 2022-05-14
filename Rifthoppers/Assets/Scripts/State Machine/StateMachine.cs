using System.Collections;
using UnityEngine;

public class StateMachine : MonoBehaviour {
  public State State;

  public void ChangeState(State newState, float delay = 0f) {
    if (newState == null) return;
    StopAllCoroutines();

    if (delay > 0f) {
      StartCoroutine(ChangeStateRoutine(newState, delay));
      return;
    }

    if (State != null) State.Exit();
    State = newState;
    State.Machine = this;
    State.Enter();
    StartCoroutine(State.Execute());
  }

  public IEnumerator ChangeStateRoutine(State newState, float delay) {
    if (State != null) State.Exit();
    yield return new WaitForSeconds(delay);
    State = newState;
    State.Machine = this;
    State.Enter();
    StartCoroutine(State.Execute());
  }
}