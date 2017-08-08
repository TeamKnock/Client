using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{

    public GameObject bullet;

    public Transform shotPos;

    public List<Bullet> bulletPool;

    public float shotSpeed = 50f;

    public void SetShotPos()
    {
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        //print(shotPos.position + "/" + shotPos.localPosition);
        //print(shotPos.parent);
    }

    public void Attack()
    {
        if (bulletPool.Count > 0)
        {
            bulletPool[0].gameObject.SetActive(true);
            bulletPool[0].Shot(shotPos.position, CameraController.instance.transform.rotation, 100f, shotSpeed);
            bulletPool.RemoveAt(0);
        }
        else {
            GameObject g = Instantiate(bullet, shotPos.position, CameraController.instance.transform.rotation);
            g.GetComponent<Bullet>().Shot(this, 100f, shotSpeed);
        }
    }

    public void AddPoolBullet(Bullet _bullet)
    {
        bulletPool.Add(_bullet);
    }

}
