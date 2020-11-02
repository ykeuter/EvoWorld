using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class WallFollower : Agent
{
    float chargeTime = 5.0f;
    float timeLeft;
    float age = 0.0f;
    float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = chargeTime;
    }




    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            GameOver();
        }
        age += Time.deltaTime;

        transform.position += transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.position += transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        timeLeft = chargeTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision!");
        GameOver();
    }

    void GameOver()
    {
        //Debug.Log("Game Over!");
    }
}
