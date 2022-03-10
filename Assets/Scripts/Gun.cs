using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    ObjectPooler objectPooler;
    private void Start()
    {
        PlayerController.shootBullet += ShootBullet;    //subscriben
        objectPooler = ObjectPooler.Instance;
    }

    //Hier hole ich die Bullets aus dem BulletPool.
    void ShootBullet(Vector3 spawnPos)
    {
        GameObject bullet = objectPooler.SpawnFromPool("PlayerBullet", spawnPos, Quaternion.identity);
        bullet.transform.rotation = transform.rotation;
        SoundManager.Instance.PlaySound(Sounds.shoot);
    }

    public void ShootBulletTest(Vector3 spawnPos)
    {
        GameObject bullet = objectPooler.SpawnFromPool("PlayerBullet", spawnPos, Quaternion.identity);
    }
}
