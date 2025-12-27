using UnityEngine;

public class WeaponBaseClass : ItemBaseClass
{
    public float speed, lifeTime, damage, reloadTime;
    public Transform weaponSpawn;

    protected override void Start()
    {
        if (itemData is WeaponData weaponData)
        {
            damage = weaponData.damage;
            speed = weaponData.speed;
            lifeTime = weaponData.lifeTime;
            reloadTime = weaponData.reloadTime;
        }
    }

    public override void UseItem(ItemData itemData)
    {
        this.itemData = itemData;
        Instantiate(gameObject);
    }

    public override float GetReloadTime()
    {
        return reloadTime;
    }
}
