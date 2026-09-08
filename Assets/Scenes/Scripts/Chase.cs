using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public GameObject Player;
    public float speed;

    private float distance;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

    }
    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, Player.transform.position);
        Vector2 direction = (Vector2)(Player.transform.position - transform.position);
        direction.Normalize();
        

        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
        
        if (direction.x > 0)
             spriteRenderer.flipX = false;
        else if (direction.x < 0)
                spriteRenderer.flipX = true;
    }
}