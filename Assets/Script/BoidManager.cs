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
    public int boidCount = 10;
    private static List<Boid> boids = new List<Boid>();

    void Start()
    {

    }

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
            float variation = 135/(collisionDensity/2f);

            // Pick first direction that doesn't collide with terrain
            for (int i = 0; i < collisionDensity; i++)
            {
                // Vary angle with each raycast
                float angle = boid.angle * (i*variation);
                Vector3 ccwDirection = AngleSpeedToVector(boid.angle);
                Vector3 cwDirection = AngleSpeedToVector(-boid.angle);

                // If chosen direction doesn't head to terrain, we pick said direction
                if (!Physics2D.Raycast(boid.transform.position, ccwDirection, collisionScanRange))
                {
                    data.avoidCollisionDir = ccwDirection;
                    break;
                }
                if(!Physics2D.Raycast(boid.transform.position, cwDirection, collisionScanRange))
                {
                    data.avoidCollisionDir = cwDirection;
                    break;
                }
            }
            #endregion

            boid.data = data;
        }
    }
}
