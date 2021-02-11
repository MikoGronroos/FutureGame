using UnityEngine;

public abstract class Item : ScriptableObject
{

    public int Id;
    public string ItemName;
    public Sprite Icon;

    public float Weight;

    public int StackSize;
    public bool IsStackable;

    public virtual ItemType Type { get; private set; }

}
