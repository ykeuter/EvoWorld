using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class SearchLightAgent : Agent
{
    [SerializeField] float speed = 2.0f;
    [SerializeField] float angularSpeed = 400.0f;
    [SerializeField] GameObject target;
    float size = 2;
    float minDistance = .75f;
    float height;
    bool idle = true;
    //float punish = -10;

    private void Awake()
    {
        height = transform.localPosition.y;
        Academy.Instance.OnEnvironmentReset += ResetPlayer;
    }

    private void OnTriggerStay(Collider other)
    {
        AddReward(.1f);
    }

    public void ResetPlayer()
    {
        Vector3 t = new Vector3(Random.Range(-size, size), 0, Random.Range(-size, size));
        while (t.magnitude <= minDistance) {
            t = new Vector3(Random.Range(-size, size), 0, Random.Range(-size, size));
        }
        target.transform.localPosition = t;
        transform.localPosition = Vector3.up * height;
        //transform.localEulerAngles = Vector3.up * Random.Range(0, 360);
        idle = false;
    }

    public override void OnActionReceived(ActionBuffers vectorAction)
    {   if (!idle) {
            //transform.Rotate(0, angularSpeed * vectorAction.ContinuousActions[0] * Time.fixedDeltaTime, 0);
            transform.position += transform.right * Time.fixedDeltaTime * speed * vectorAction.ContinuousActions[0];
            transform.position += transform.forward * Time.fixedDeltaTime * speed * vectorAction.ContinuousActions[1];
            //SetReward(-Vector3.Distance(Vector3.ProjectOnPlane(transform.localPosition, Vector3.up), target.transform.localPosition));
        }
        //else {
        //    SetReward(punish);
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        idle = true;
        //SetReward(punish);
        EndEpisode();
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //if (v < 0) v = 0;
        continuousActionsOut[0] = h;
        continuousActionsOut[1] = v;
    }
}
