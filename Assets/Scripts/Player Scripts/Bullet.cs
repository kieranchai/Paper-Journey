using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 15f;
    private Rigidbody2D rb;
    public int bulletDamage = 1;
    public Collider2D[] bulletCollider;
    public bool IsPlatformOrNot = false;
    public bool hasExited = false;
    private Shoot playerShoot;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        bulletCollider = GetComponents<Collider2D>();
        bulletCollider[0].isTrigger = true;
        bulletCollider[1].enabled = false;
        playerShoot = GameObject.FindWithTag("Player").GetComponent<Shoot>();
    }

    void Update()
    {
        StartCoroutine(DestroyBullet());
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (playerShoot.platformsSpawned > 0)
            {
                int counter = playerShoot.platformsSpawned;
                for(int i = counter - 1; i >= 0; i--)
                {
                    Destroy(playerShoot.bullets[i]);
                    playerShoot.bullets.RemoveAt(i);
                    playerShoot.platformsSpawned--;
                    break;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            if (collision.tag == "Wall")
            {
                if (playerShoot.platformsSpawned == 0 || playerShoot.platformsSpawned == 1)
                {
                    bulletCollider[0].isTrigger = false;
                    IsPlatformOrNot = true;
                    playerShoot.platformsSpawned++;
                    playerShoot.bullets.Add(gameObject);
                    rb.bodyType = RigidbodyType2D.Static;
                    return;
                } else
                {
                    Destroy(playerShoot.bullets[0]);
                    playerShoot.bullets.RemoveAt(0);
                    playerShoot.platformsSpawned--;
                    bulletCollider[0].isTrigger = false;
                    IsPlatformOrNot = true;
                    playerShoot.platformsSpawned++;
                    playerShoot.bullets.Add(gameObject);
                    rb.bodyType = RigidbodyType2D.Static;
                    return;
                }
            }
            else if (collision.tag == "Hinge_Wall")
            {
                collision.attachedRigidbody.bodyType = RigidbodyType2D.Dynamic;
                collision.attachedRigidbody.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
            } else if (collision.tag == "Checkpoint" || collision.tag == "ZoomOut" || collision.tag == "ZoomIn" || collision.tag == "Goal" || collision.tag == "Sinking")
            {
                return;
            }
            else if (collision.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyHealth>().takeDamage(bulletDamage);
            }
            else if (collision.tag == "Bullet")
            {
                if (collision.GetComponent<Bullet>().IsPlatformOrNot)
                {
                    Destroy(gameObject);
                }
                return;
            }
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(1);
        if(!IsPlatformOrNot)
        {
            Destroy(gameObject);
        }
    }
}
