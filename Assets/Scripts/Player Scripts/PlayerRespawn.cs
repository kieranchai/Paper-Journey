using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //temp

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 respawnPoint;
    public GameObject fallDetector;
    public SceneLoader crossfade;
    PlayerHealth playerHealth;
    public int fallDamage = 1;

    void Start()
    {
        respawnPoint = transform.position;
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fall Detector")
        {
            transform.position = respawnPoint;
            playerHealth.takeDamage(fallDamage);
            
        }

        if (collision.tag == "Checkpoint")
        {
            if (collision.gameObject.GetComponent<Checkpoint>().passed != true)
            {
                collision.gameObject.GetComponent<Checkpoint>().passed = true;
                AudioManager.instance.PlaySFX("checkpoint");
                collision.gameObject.GetComponent<Animator>().SetBool("passed", true);
                respawnPoint = collision.transform.position;
            }
        }

        //temp
        if (collision.tag == "Goal")
        {
            crossfade.LoadNextLevel("Level 2");
        }

        if (collision.tag == "Ending")
        {
            crossfade.LoadNextLevel("Ending Cutscene");
        }
    }
}
