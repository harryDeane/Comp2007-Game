using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform BulletSpawnPoint;
    public GameObject Bullet;
    public float bulletSpeed = 10;

    void update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            var bullet = Instantiate(Bullet, BulletSpawnPoint.position, BulletSpawnPoint.rotation);
            Bullet.GetComponent<Rigidbody>().velocity = BulletSpawnPoint.forward * bulletSpeed;
        }
    }
}
