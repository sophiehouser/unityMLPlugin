# Phantasma Labs Unity MLPlugin Challenge
An agent moves around a training area and gets rewarded for colliding with goals. Built with Unity, using the MLAgents plugin.

I ran into a bug, specified in **Known Bugs** below, and I didn't have time to finish working through it. This meant I was not able to run my program in heuristic mode or to run my tests. I also ran out of time to write an additional test, but I wrote in **Tests** about the one I would have written with more time.

This is the coding challenge for Phantasma Labs as specified [here](https://gist.github.com/meiemari/30d273573b0ef33aa42faeb8ac3fb47b).

## Classes
- **TrainingArea** - The script associated with the floor of the training area. Takes care of spawning, removing, placing and keeping track of goals and the agent. 

- **CubeAgent** - The script associated with the blue cube agent. Includes all logic for the agent. 

## Known Bugs
- It seems like Unity cannot find the TrainingArea file. I think something might have gone wrong when I moved the *TrainingArea* file from the */Assets* folder into a sub */Scripts* folder. This is causing a *NullReferenceError* when the *CubeAgent* tries to refer to the *TrainingArea* and an error running tests as well. 

## Tests
With more time, I would have written a test for the *GetClosestGoal* method as well. The test would have looked like this:

1. Place the *CubeAgent* at a specified point
2. Add three goals to a list and pass that to the *GetClosestGoal* method
3. Assert that the method returned the expected goal
4. Additionaly write a test for the edge case in which the *GoalList* is empty

*Note: The tests are commented out right now*
