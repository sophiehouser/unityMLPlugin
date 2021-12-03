using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class TrainingArea : MonoBehaviour
{
    public CubeAgent cubeAgent;
    public GameObject goal;
    private List<GameObject> goalList;
    
    private GameObject GetClosestGoal(List<GameObject> goalList)
    {
        GameObject closestGoal = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = cubeAgent.transform.position;
        foreach(GameObject potentialGoal in goalList)
        {
            Vector3 directionToGoal = potentialGoal.transform.position - currentPosition;
            float dSqrToGoal = directionToGoal.sqrMagnitude;
            if(dSqrToGoal < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToGoal;
                closestGoal = potentialGoal;
            }
        }
     
        return closestGoal;
    }  

    public void RemoveSpecificGoal(GameObject goalObject)
    {
        goalList.Remove(goalObject);
        Destroy(goalObject);
    }

    public static Vector3 GetRandomPointInTrainingArea() {
        // TODO: dynamically get bounds of TrainingArea, or make hard coded numbers more readable
        return new Vector3(
            Random.Range(-6.6f, 11.3f),
            1.48f,
            Random.Range( -7.2f, 10.7f)
        );
    }

    private void SpawnGoals()
    {
        var goalCount = Random.Range(0, 20);

        for (int i = 0; i < goalCount; i++)
        {
            // Spawn and place the goal
            GameObject goalObject = Instantiate<GameObject>(goal.gameObject);
            goalObject.transform.position = GetRandomPointInTrainingArea();
            
            // Set the goal's parent to this area's transform
            goalObject.transform.SetParent(transform);

            // Keep track of the goals
            goalList.Add(goalObject);
        }
    }

    private void PlaceCubeAgent()
    {
        cubeAgent.transform.position = GetRandomPointInTrainingArea();
    }

    public void ResetArea()
    {
        PlaceCubeAgent();
        SpawnGoals();
    }

    public int GoalsRemaining
    {
        get { return goalList.Count; }
    }

    private void Start()
    {
        goalList = new List<GameObject>();
        ResetArea();

        var closestGoal = GetClosestGoal(goalList);
        Debug.DrawLine(cubeAgent.transform.position, closestGoal.transform.position, Color.white, 2.5f);
    }
}
