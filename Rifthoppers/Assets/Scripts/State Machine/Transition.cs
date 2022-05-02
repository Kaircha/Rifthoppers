using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition {
  public State From;
  public State To;
  public Condition Condition; // Cannot be shared between Transitions

  public Transition(State to, Condition condition) {
    From = null;
    To = to;
    Condition = condition;
  }

  public Transition(State from, State to, Condition condition) {
    From = from;
    To = to;
    Condition = condition;
  }
}