using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateMachine))]
public abstract class Brain : MonoBehaviour {
  [HideInInspector] public Entity Entity;
  private StateMachine Machine;
  public abstract State EntryState { get; }
  public List<Transition> Transitions = new();

  public abstract void Initialize();

  private void OnEnable() {
    Entity = GetComponent<Entity>();
    Machine = GetComponent<StateMachine>();
    Initialize();
    // When Disabled and Re-Enabled, the Brain will re-enter into its EntryState
    Machine.State = EntryState;
    foreach (Transition transition in Transitions) {
      transition.Condition.Initialize();
    }
    StartCoroutine(TransitionRoutine());
  }

  private IEnumerator TransitionRoutine() {
    yield return null; // yield point
    while (true) {
      foreach (Transition transition in Transitions) {
        if (transition.To == null || transition.To == Machine.State || !transition.To.CanEnter) continue;
        if (!transition.Condition.Satisfied) continue;
        if (transition.From == null || (transition.From == Machine.State && transition.From.CanExit)) {
          Debug.Log($"{Entity.name} transitioned from {Machine.State} to {transition.To}.");
          Machine.State = transition.To;
          transition.Condition.Reset();
          break;
        }
      }
      //yield return new WaitForSeconds(1f);
      yield return null;
    }
  }

  private void OnDisable() {
    foreach (Transition transition in Transitions) {
      transition.Condition.Terminate();
    }
    StopAllCoroutines();
  }
}