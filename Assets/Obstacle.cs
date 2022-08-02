using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float moveSpeed;
    private float speed;
    private int side;

    private SpriteRenderer sR;

    void Start()
    {
        speed = Random.Range(3, moveSpeed);
        sR = GetComponent<SpriteRenderer>();

        side = Random.Range(0, 2);
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if(transform.position.x <= -11)
        {
            Destroy(this.gameObject);
        }

        if(transform.position.y == -2.25f && transform.position.x <= -4)
        {
            if(side == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, -3.25f), 5);
            }
            else if(side == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, -1.25f), 5);
            }
        }

        SpriteOrder();
    }

    void SpriteOrder()
    {
        if (transform.position.y == -1.25f)
        {
            sR.sortingOrder = 3;
        }
        else if (transform.position.y == -2.25f)
        {
            sR.sortingOrder = 6;
        }
        else if (transform.position.y == -3.25f)
        {
            sR.sortingOrder = 9;
        }
        else if (transform.position.y == -4.25f)
        {
            sR.sortingOrder = 12;
            speed = GameManager.instance.roadSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && GameManager.instance.canHit == true)
        {
            GameManager.instance.canHit = false;
            Debug.Log("Player Hit");
            GameManager.instance.playerAnim.Play("PlayerHurt");
            GameManager.instance.health--;
            GameManager.instance.canHit = true;
        }
        else if(other.CompareTag("Obstacle"))
        {
            speed = other.GetComponent<Obstacle>().speed;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            speed = other.GetComponent<Obstacle>().speed;
        }
    }
}
