using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class State {
  public StateMachine Machine;
  public bool CanEnter = true;
  public bool CanExit = true;
  public virtual float ExecutionTime => 0f; 
  public virtual void Enter() { }
  public virtual IEnumerator Execute() { yield return null; }
  public virtual void Exit() { }
}