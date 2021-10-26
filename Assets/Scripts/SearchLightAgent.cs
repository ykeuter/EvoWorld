using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class SearchLightAgent : Agent
{
    readonly float speed = 1.0f;
    [SerializeField] GameObject good;
    Vector3 startPosPlayer;
    Vector3 startPosObject;
    readonly float bound = .7f;
    readonly float reward = 1f;

    private void Awake()
    {
        startPosPlayer = transform.localPosition;
        startPosObject = good.transform.localPosition;
        //Academy.Instance.OnEnvironmentReset += ResetPlayer;
    }

    public override void OnEpisodeBegin()
    {
        Debug.Log("new episode");
        transform.localPosition = startPosPlayer;
        Spawn();
    }

    //public void ResetPlayer()
    //{
    //    transform.localPosition = startPos;
    //    //transform.localEulerAngles = Vector3.zero;
    //    //int caseId = (int)Academy.Instance.EnvironmentParameters.GetWithDefault("case_id", 0);
    //    //target.transform.localPosition = cases[caseId].pos;
    //    //target.transform.localEulerAngles = cases[caseId].rot;
    //    //idle = false;
    //    target.transform.localPosition = GetRandomPosition();
    //}

    void Spawn()
    {
        startPosObject.x = Random.Range(-bound, bound);
        good.transform.localPosition = startPosObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        AddReward(reward);
        Debug.Log("you win");
        Spawn();

    }

    //private Vector3 GetRandomPosition()
    //{
    //    Vector3 p = new Vector3(Random.Range(-bound, bound), startPos.y, Random.Range(-bound, bound));
    //    while (!ValidatePosition(p)) p = new Vector3(Random.Range(-bound, bound), startPos.y, Random.Range(-bound, bound));
    //    return p;
    //}

    //private bool ValidatePosition(Vector3 p)
    //{
    //    Vector3 p2 = transform.localPosition;
    //    if (Mathf.Abs(p2.x - p.x) < margin && Mathf.Abs(p2.z - p.z) < margin) return false;

    //    foreach (GameObject g in obstacles)
    //    {
    //        p2 = g.transform.localPosition;
    //        if (Mathf.Abs(p2.x - p.x) < margin && Mathf.Abs(p2.z - p.z) < margin) return false;
    //    }
    //    return true;
    //}

    private void FixedUpdate()
    {
        good.transform.position -= Time.fixedDeltaTime * speed * good.transform.forward;
        if (good.transform.localPosition.z < startPosPlayer.z) Spawn();
    }

    public override void OnActionReceived(ActionBuffers vectorAction)
    {   
        //if (idle) return;
        //transform.position += Time.fixedDeltaTime * speed * vectorAction.ContinuousActions[0] * transform.forward;
        //transform.Rotate(0, Time.fixedDeltaTime * rotSpeed * (vectorAction.ContinuousActions[2] - vectorAction.ContinuousActions[1]), 0);
        //transform.position += Time.fixedDeltaTime * speed * (vectorAction.ContinuousActions[0] - vectorAction.ContinuousActions[1]) * transform.forward;
        transform.position += Time.fixedDeltaTime * speed * (vectorAction.ContinuousActions[1] - vectorAction.ContinuousActions[0]) * transform.right;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        //float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        //if (v > 0) continuousActionsOut[0] = v;
        //else continuousActionsOut[0] = 0;
        //else continuousActionsOut[1] = -v;
        if (h > 0) continuousActionsOut[1] = h;
        else continuousActionsOut[0] = -h;
        //continuousActionsOut[0] = Input.GetKey(KeyCode.Space) ? 1.0f : 0.0f;
    }
}
