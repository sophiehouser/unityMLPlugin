using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;


public class CubeAgent : Agent
{
    private TrainingArea trainingArea;
    new private Rigidbody rigidbody;

    private bool hasTargetCollision;

    public float moveSpeed = 5f;
    public float turnSpeed = 180f;

    public override void Initialize()
    {
        base.Initialize();
        trainingArea = GetComponentInParent<TrainingArea>();
        rigidbody = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        hasTargetCollision = false;
        trainingArea.ResetArea();
    }  

    /// When the agent collides with something, take action
    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.transform.CompareTag("goal")) {
            hasTargetCollision = true;
            trainingArea.RemoveSpecificGoal(collision.gameObject);
            AddReward(1f);

            if (trainingArea.GoalsRemaining <= 0) {
                EndEpisode();
            }
        }
    }

    /// Perform actions based on a vector of numbers
    public override void OnActionReceived(ActionBuffers actionBuffers) 
    {
        // Convert the first action to forward movement
        float forwardAmount = actionBuffers.DiscreteActions[0];

        // Convert the second action to turning left or right
        float turnAmount = 0f;
        if (actionBuffers.DiscreteActions[1] == 1f) {
            turnAmount = -1f;
        }
        else if (actionBuffers.DiscreteActions[1] == 2f) {
            turnAmount = 1f;
        }

        // Apply movement
        rigidbody.MovePosition(transform.position + transform.forward * forwardAmount * moveSpeed * Time.fixedDeltaTime);
        transform.Rotate(transform.up * turnAmount * turnSpeed * Time.fixedDeltaTime);
    }

    /// Collect all non-Raycast observations
    public override void CollectObservations(VectorSensor sensor) 
    {
        sensor.AddObservation(hasTargetCollision);
    }

    /// Read inputs from the keyboard and converts them to a list of actions
    public override void Heuristic(in ActionBuffers actionsOut) 
    {
        int forwardAction = 0;
        int turnAction = 0;
        if (Input.GetKey(KeyCode.W)) {
            // move forward
            forwardAction = 1;
        }

        if (Input.GetKey(KeyCode.A)) {
            // turn left
            turnAction = 1;
        }
        
        if (Input.GetKey(KeyCode.D)) {
            // turn right
            turnAction = 2;
        }

        // Put the actions into the array
        actionsOut.DiscreteActions.Array[0] = forwardAction;
        actionsOut.DiscreteActions.Array[1] = turnAction;
    }
}
