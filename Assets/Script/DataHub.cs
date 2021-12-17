using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataHub
{
    public static RaycastHit blank = new RaycastHit();

    // Collision Detection Variables
    public static float collisionScanRange = 2.2f;
    public static int envMask = LayerMask.GetMask("Environment");

    // Boid movement calculation values
    public const float minSpeed = 0.3f;
    public const float maxSpeed = 0.9f;
    public static float maxSteerForce = Mathf.Infinity;//0.45f;

    // Boid forces weight
    public static float avoidCollisionWeight = 10;
    public static float seperateWeight = 1;
    public static float alignWeight = 1;
    public static float cohesionWeight = 1;

    // Math calculation values
    public static float angleToRadians = Mathf.PI / 180;
    public static float radiansToAngle = 1/angleToRadians;

    public static Vector2 AngleSpeedToVector(float angle, float speed = 1)
    {
        Vector2 vector = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        vector.x *= speed;
        vector.y *= speed;
        return vector;
    }
}
