using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Finark/Weapon")]

public class Weapon : Equipment
{

    public WeaponType WeaponType;

    public float damage;
    public float range;
    public float speed;

}
