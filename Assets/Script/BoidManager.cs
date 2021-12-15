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
            int signFlip = -1;

            // Pick first direction that doesn't collide with terrain
            for (int i = 0; i < collisionDensity; i++)
            {
                // Vary angle with each raycast
                float angleChange = signFlip * (i*10);
                Vector3 direction = boid.direction;
                direction = Quaternion.Euler(0f, 0f, angleChange) * direction;

                // Check for collision
                RaycastHit2D hit = Physics2D.Raycast(boid.transform.position, direction, Mathf.Infinity, envMask);

                // If chosen direction doesn't head to terrain, we pick said direction
                if (hit.collider == null)
                {
                    Debug.DrawLine(boid.transform.position, boid.transform.position + direction, Color.white, 0.1f);
                    data.avoidCollisionDir = boid.direction;
                    break;
                }
                else
                    Debug.DrawLine(boid.transform.position, boid.transform.position + direction, Color.red, 0.1f);

                signFlip *= -1;
            }
            #endregion

            boid.data = data;
        }
    }
}
