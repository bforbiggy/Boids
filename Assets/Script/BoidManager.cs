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

            Vector2 boidPos = boid.transform.position;
            Vector2 direction = boid.direction != Vector3.zero ? boid.direction.normalized : Vector3.up;

            #region Collision Avoidance
            int signFlip = -1;

            // Pick first direction that doesn't collide with terrain
            for (int i = 0; i < 36; i++)
            {
                // Vary angle with each raycast
                float angleChange = signFlip * (i*10);
                direction = Quaternion.Euler(0f, 0f, angleChange) * direction;

                // Check for collision
                RaycastHit2D hit = Physics2D.Raycast(boidPos, direction, collisionScanRange, envMask);

                // If chosen direction doesn't head to terrain, we pick said direction
                if (hit.collider == null)
                {
                    Debug.DrawLine(boidPos, boidPos + direction, Color.white, 0.03f);
                    data.avoidCollisionDir = direction;
                    break;
                }
                else
                    Debug.DrawLine(boidPos, boidPos + direction, Color.red, 0.03f);

                signFlip *= -1;
            }
            #endregion

            #region Seperation
            float closestDistance = Mathf.Infinity;
            data.closestBoidPos = Vector3.zero;

            // Finds the closest boid
            RaycastHit2D[] hits = Physics2D.CircleCastAll(boid.transform.position, 2f, direction);
            for(int i = 0; i < hits.Length; i++)
            {
                // Make sure hit object is a boid
                GameObject detectedBoid = hits[i].collider.gameObject;
                if(detectedBoid.tag == "Player" && !detectedBoid.Equals(boid.gameObject))
                {
                    float difference = Vector2.Distance(boidPos, detectedBoid.transform.position);
                    if(difference < closestDistance)
                    {
                        closestDistance = difference;
                        data.closestBoidPos = detectedBoid.transform.position;
                    }
                }
            }
            #endregion

            boid.data = data;
        }
    }
}
