using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataHub;

public class Boid : MonoBehaviour
{
    public class BoidData
    {
        public Vector3 avoidCollisionDir;
    }
    public BoidData data = null;
    private Collider m_collider;

    [Range(2f, 6f)]
    public float collisionScanRange = 4.7f;

    // Movement fields
    public Vector3 velocity;
    public Vector3 direction;
    public float angle;

    void Start()
    {
        m_collider = GetComponent<Collider>();

        // Set boid random initial velocity and position
        velocity = AngleSpeedToVector(Random.Range(0f, 360f) * angleToRadians, (minSpeed + maxSpeed)/2);
        transform.position += new Vector3(Random.Range(-9, 9), Random.Range(-5, 5));
    }

    Vector3 SteerForce(Vector3 vector)
    {
        Vector3 force = vector.normalized * maxSpeed - (Vector3)velocity;
        return Vector3.ClampMagnitude(force, maxSteerForce);
    }

    void Update()
    {
        Vector3 acceleration = Vector3.zero;

        #region Collision
        //Vector3 collisionAvoidForce = SteerForce(data.avoidCollisionDir) * avoidCollisionWeight;
        //acceleration += collisionAvoidForce;
        #endregion

        // Seperation
        // Cohesion
        // Alignment
        //Orientate();

        // Calculate velocity given acceleration
        velocity += acceleration * Time.deltaTime;

        float speed = velocity.magnitude;
        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
        direction = velocity/speed;
        velocity = direction * speed;

        // Calculate angle 
        float signedAngle = Vector2.SignedAngle(Vector2.up, direction);
        if(signedAngle < 0)
            signedAngle = 360 + signedAngle;
        angle = signedAngle;

        // Move boid
        transform.position += (Vector3)velocity * Time.deltaTime;
    }

    void Orientate()
    {
        // Rotate boid to face direction it is going in
        float rotationAmount = angle * radiansToAngle + 180;
        transform.rotation = Quaternion.Euler(0, 0, rotationAmount);

        // If boid is facing right, flip it right side up
        if (transform.rotation.z > 0.5 || transform.rotation.z < -0.5)
            transform.RotateAround(transform.position, transform.right, 180);
    }
}
