using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;

    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;

    public float ySpawnPos; // = -4;

    /* ---- TEXT ---- */
    public int pointValue;
    private GameManager gameManager;

    /* ---- PARTICLES ---- */
    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();

        /* ---- TEXT ---- */
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    /* ---- HELPER ---- */
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    // Update is called once per frame
    void Update()
    {
        // if (gameManager.isGameOver())
        // {
        //     Destroy(gameObject);
        // }
    }

    private void OnMouseDown()
    {
        if (!gameManager.isGameOver())
        {
            Destroy(gameObject);

            /* ---- TEXT ---- */
            gameManager.UpdateScore(pointValue);

            /* ---- PARTICLES ---- */
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other) // sensor is the only thing that has Is Trigger applied to it
    {
        if (targetRb.velocity.y < 0) // dennis
        {
            Destroy(gameObject);

            /* ---- LIVES ---- */
            if (tag != "Bad" && !gameManager.isGameOver())
            {
                gameManager.UpdateLives(-1);
                if (gameManager.isGameOver())
                {
                    gameManager.ShowGameOver();
                }
            }
        }
    }
}
