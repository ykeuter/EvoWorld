using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class Alpha : Agent
{
    float maxAge = 50;
    float age = 0.0f;
    [SerializeField] float speed = 2.0f;
    [SerializeField] float angularSpeed = 400.0f;
    [SerializeField] float energyLevel = 10f;
    [SerializeField] float birthThreshold = 15;
    [SerializeField] float birthCost = 10;
    float energyRate = .5f;
    float foodEnergy = 1f;
    BasicManager manager;

    public override void Initialize()
    {
        base.Initialize();
        manager = GameObject.Find("Game Manager").GetComponent<BasicManager>();
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * Time.fixedDeltaTime * speed;
        energyLevel -= Time.deltaTime * energyRate;
        age += Time.deltaTime;
        if (energyLevel < 0 || age > maxAge)
        {
            manager.Eliminate(this);
        }
        if (energyLevel > birthThreshold)
        {
            manager.Conceive(this);
            energyLevel -= birthCost;
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
        manager.Eliminate(this);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        float h = Input.GetAxis("Horizontal");
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = h;
    }
}
