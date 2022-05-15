[System.Serializable]
public class Modifier {
  public ModifierType Type;
  public string Description;

  public Modifier(ModifierType type, string description) {
    Type = type;
    Description = description;
  }
}

public enum ModifierType {
  Increase,
  Decrease,
  Default,
}