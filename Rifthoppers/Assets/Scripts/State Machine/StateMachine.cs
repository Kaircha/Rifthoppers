using UnityEngine;

public class StateMachine : MonoBehaviour {
  private State _state;
  public State State {
    get => _state;
    set {
      StopAllCoroutines();
      if (State != null) State.Exit();
      _state = value;
      State.Machine = this;
      State.Enter();
      StartCoroutine(State.Execute());
    }
  }
}