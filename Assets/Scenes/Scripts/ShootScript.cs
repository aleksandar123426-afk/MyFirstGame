using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour {

    public Transform Gun;

    Vector2 direction;

    public GameObject Bullet;

    public float BulletSpeed;

    public int maxDamagePerShot = 1;

    public Transform ShootPoint;

    public float fireRate;
    float ReadyForNextShot;
    void Start () {

    }

    // Update is called once per frame
    void Update ()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - (Vector2)Gun.position;
        FaceMouse();

        if (Input.GetMouseButtonDown(0))
        {
            if(Time.time > ReadyForNextShot)
            {
                ReadyForNextShot = Time.time + 1/fireRate;
                shoot();
            }
        }


    }
        
    void FaceMouse()
    {
        Gun.right = direction;
    }
    
    
    void shoot()
    {
        GameObject BulletIns = Instantiate(Bullet, ShootPoint.position, ShootPoint.rotation);

        Bullet bullet = BulletIns.GetComponent<Bullet>();
        if (bullet == null)
        {
            bullet = BulletIns.AddComponent<Bullet>();
        }

        bullet.damagePerShot = Mathf.Clamp(maxDamagePerShot, 1, 1);

        Rigidbody2D rb = BulletIns.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = BulletIns.transform.right * BulletSpeed;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }

        Collider2D bulletCollider = BulletIns.GetComponent<Collider2D>();
        if (bulletCollider != null)
        {
            bulletCollider.isTrigger = true;
        }

        Destroy(BulletIns, 3f);
    }
}
