using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Game/Weapon")]
public class WeaponData : ItemData
{
    public float damage;
    public float speed;
    public float lifeTime;
    public float reloadTime;
}
