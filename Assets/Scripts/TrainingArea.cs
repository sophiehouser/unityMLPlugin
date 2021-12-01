using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class TrainingArea : MonoBehaviour
{
    public GameObject goal;
    private List<GameObject> goalList;
    
    public static Vector3 GetRandomPointInTrainingArea() {
        // TODO: dynamically get bounds of TrainingArea, or make hard coded numbers more readable
        return new Vector3(
            Random.Range(-6.6f, 11.3f),
            1.48f,
            Random.Range( -7.2f, 10.7f)
        );
    }

    private void SpawnGoals(int count)
    {
        for (int i = 0; i < count; i++)
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

    private void Start()
    {
        goalList = new List<GameObject>();
        SpawnGoals(Random.Range(0, 20));
    }
}
