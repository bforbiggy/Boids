using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataHub;

public class Boid : MonoBehaviour
{
    public const float defaultSpeed = 0.5f;
    public Rigidbody2D m_rigidbody;

    public float m_angle = 0f; // CURRENT ANGLE IN RADIANS

    [Range(2f, 6f)]
    public float scanRange = 4.7f;
    public GameObject scanArea;

    void Start()
    {
        // Set boid random initial velocity and position
        SetVelocity(Random.Range(0f, 360f) / 180 * Mathf.PI);
        transform.position += new Vector3(Random.Range(-10, 10), Random.Range(-4, 4));
    }

    // Given an angle (in radians), set boid in that direction
    void SetVelocity(float angleRad, float speed = defaultSpeed)
    {
        // Set boid to move in given direction with default speed
        m_rigidbody.velocity = new Vector2(Mathf.Cos(angleRad) * speed, Mathf.Sin(angleRad) * speed);

        // Rotate boid to face direction it is going in
        m_angle = angleRad;
        float rotationAmount = angleRad * 180 / Mathf.PI + 180;
        transform.rotation = Quaternion.Euler(0, 0, rotationAmount);

        // If boid is facing right, we must flip it upside down
        if(transform.rotation.z > 0.5 || transform.rotation.z < -0.5)
            transform.RotateAround(transform.position, transform.right, 180);
    }

    Vector3 SteerForce(Vector3 vector)
    {
        Vector3 direction = vector.normalized * defaultSpeed - (Vector3)m_rigidbody.velocity;
        return Vector3.ClampMagnitude(direction, maxSteerForce);
    }

    void Update()
    {
        // DEBUG: MAKE CYLINDER MATCH SCAN AREA
        scanArea.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        scanArea.transform.localScale = new Vector3(scanRange, 1, scanRange);
        scanArea.transform.position = transform.position - new Vector3(0f, 0f, -10f);

        // Move away from nearby collidable objects
        foreach (Boid boid in boids)
        {
            // Detect collisions and avoid them by steering away
            RaycastHit2D hit = Physics2D.Raycast(transform.position, m_rigidbody.velocity);

            // Seperation
            // Cohesion
            // Alignment
        }
    }
}
