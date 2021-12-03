using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataHub;

public class Generator : MonoBehaviour
{
    public Boid boidBase;

    private int oldBoidCount = 0;
    [Range(0,100)]
    public int boidCount = 40;

    void Update()
    {
        // Create new boids if we need to increase boid count
        if (boidCount > oldBoidCount)
        {
            while(oldBoidCount != boidCount)
            {
                Boid newBoid = Instantiate(boidBase, transform);
                boids.Add(newBoid);
                oldBoidCount++;
            }
        }
        // Destroy boids if we need to decrease boid count
        else if(boidCount < oldBoidCount)
        {
            while(oldBoidCount != boidCount)
            {
                Boid destroyMe = boids[boids.Count - 1];
                boids.RemoveAt(boids.Count - 1);
                GameObject.Destroy(destroyMe.gameObject);
                oldBoidCount--;
            }
        }
    }
}
