using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Boid;
using static DataHub;

public class BoidManager : MonoBehaviour
{
    public Boid boidBase;

    private int oldBoidCount = 0;
    [Range(0,100)]
    public int boidCount = 40;
    private static List<Boid> boids = new List<Boid>();

    void Update()
    {
        #region Boid Creation/Destruction
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
        #endregion

        foreach(Boid boid in boids)
        {
            BoidData data = new BoidData();

            #region Collision Avoidance
            RaycastHit closest = blank;

            // Detect closest collidable terrain
            closest.distance = Mathf.Infinity;
            for (int i = 0; i < collisionDensity; i++)
            {
                Vector3 vectorDirection = AngleSpeedToVector(boid.angle);
                RaycastHit hitData;
                if (Physics.Raycast(transform.position, vectorDirection, out hitData) && hitData.distance < closest.distance)
                {
                    closest = hitData;
                }
            }
            #endregion

            boid.data = data;
        }
    }
}
