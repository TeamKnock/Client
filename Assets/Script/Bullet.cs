using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform tr;
    Rigidbody ri;

    Gun gun;

    public float damage = 0f;
    public float speed = 100f;

    void Awake()
    {
        tr = GetComponent<Transform>();
        ri = GetComponent<Rigidbody>();
    }


    public void Shot(Gun _gun, float _damage, float _speed)
    {
        gun = _gun;
        ri.velocity = Vector3.zero;

        damage = _damage;
        speed = _speed;

        ri.AddForce(tr.forward * speed, ForceMode.VelocityChange);
        removeDelay = StartCoroutine(RemoveDelay());
    }

    public void Shot(Vector3 _pos, Quaternion _rot, float _damage, float _speed)
    {
        ri.velocity = Vector3.zero;

        tr.position = _pos;
        tr.rotation = _rot;

        damage = _damage;
        speed = _speed;

        ri.AddForce(tr.forward * speed, ForceMode.VelocityChange);
        removeDelay = StartCoroutine(RemoveDelay());
    }

    void Remove()
    {
        StopCoroutine(removeDelay);
        gun.AddPoolBullet(this);
        gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision col)
    {

        Remove();
    }

    Coroutine removeDelay = null;

    IEnumerator RemoveDelay()
    {
        yield return new WaitForSeconds(5f);
        Remove();
    }

}
