using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    public float shootRange = 12f;

    private float timer;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");

            if (player == null)
                return;
        }

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < shootRange)
        {
            timer += Time.deltaTime;

            if (timer > 2f)
            {
                timer = 0f;
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        if (bullet == null || bulletPos == null)
            return;

        Instantiate(bullet, bulletPos.position, bulletPos.rotation);
    }
}
    
    