using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class Alpha : Agent
{
    float maxAge = 10f;
    float age = 0.0f;
    [SerializeField] float speed = 2.0f;
    [SerializeField] float angularSpeed = 400.0f;
    [SerializeField] float energyLevel = 20f;
    float energyRate = 1f;
    float foodEnergy = 1f;

    private void Awake()
    {
    }

    public override void OnEpisodeBegin()
    {
    }

    public void ResetPlayer()
    {
    }

    new void EndEpisode()
    {
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * Time.fixedDeltaTime * speed;
        energyLevel -= Time.deltaTime * energyRate;
        age += Time.deltaTime;
        if (energyLevel < 0 || age > maxAge)
        {
            Destroy(gameObject);
        }
        
    }

    public override void OnActionReceived(ActionBuffers vectorAction)
    {
        transform.Rotate(0, angularSpeed * vectorAction.ContinuousActions[0] * Time.fixedDeltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        energyLevel += foodEnergy;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        float h = Input.GetAxis("Horizontal");
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = h;
    }
}
