using UnityEngine;
using System.Collections.Generic;

public abstract class Basket {
  public List<Item> Items { get; set; }

  public abstract class Item {
    public virtual void Use() { }
  }
}

public class FruitBasket : Basket {
  public Color Color;

  public void Pick() {
    Items.Add(new Fruit(Color));
  }

  public class Fruit : Item {
    public Color Color;

    public Fruit(Color color) {
      Color = color;
    }

    public override void Use() {
      base.Use();
      Eat();
    }

    public void Eat() {
      // Heal health
    }
  }
}