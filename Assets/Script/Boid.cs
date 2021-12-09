using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataHub;

public class Boid : MonoBehaviour
{
    public class BoidData
    {
        public Vector3 collision;
    }
    public BoidData data = null;

    [Range(2f, 6f)]
    public float collisionScanRange = 4.7f;

    public Vector2 velocity;
    public float angle;

    void Start()
    {
        // Set boid random initial velocity and position
        SetVelocity(Random.Range(0f, 360f) / 180 * Mathf.PI);
        transform.position += new Vector3(Random.Range(-9, 9), Random.Range(-5, 5));
    }

    // Given an angle (in radians), set boid in that direction
    void SetVelocity(float angleRad, float speed = (minSpeed + maxSpeed)/2)
    {
        // Set boid to move in given direction with default speed
        velocity = AngleSpeedToVector(angleRad, speed);

        // Rotate boid to face direction it is going in
        float rotationAmount = angleRad * 180 / Mathf.PI + 180;
        transform.rotation = Quaternion.Euler(0, 0, rotationAmount);

        // If boid is facing right, we must flip it upside down
        if(transform.rotation.z > 0.5 || transform.rotation.z < -0.5)
            transform.RotateAround(transform.position, transform.right, 180);
    }

    Vector3 SteerForce(Vector3 vector)
    {
        Vector3 direction = vector.normalized * maxSpeed - (Vector3)velocity;
        return Vector3.ClampMagnitude(direction, maxSteerForce);
    }

    void Update()
    {
        Vector3 acceleration = Vector3.zero;

        #region Collision
        

        /*
        Vector3 collisionAvoidDir = ObstacleRays();
        Vector3 collisionAvoidForce = SteerTowards(collisionAvoidDir) * settings.avoidCollisionWeight;
        acceleration += collisionAvoidForce;
        */
        #endregion

        // Seperation
        // Cohesion
        // Alignment

        // After calculating velocity, move the boid
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
