using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataHub
{
    public static RaycastHit blank = new RaycastHit();

    public static int collisionDensity = 20;

    public const float minSpeed = 0.3f;
    public const float maxSpeed = 0.9f;

    public static float maxSteerForce = 0.45f;

    public static float avoidCollisionWeight = 10;
    public static float seperateWeight = 1;
    public static float alignWeight = 1;
    public static float cohesionWeight = 1;

    public static Vector2 AngleSpeedToVector(float angle, float speed = 1)
    {
        Vector2 vector = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        vector.x *= speed;
        vector.y *= speed;
        return vector;
    }
}
