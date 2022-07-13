using System.Collections;
using UnityEngine;

public class StateMachine : MonoBehaviour {
  public State State;

  public void ChangeState(State newState) {
    if (newState == null) return;
    StopAllCoroutines();
    if (State != null) State.Exit();
    State = newState;
    State.Machine = this;
    State.Enter();
    StartCoroutine(State.Execute());
  }
}