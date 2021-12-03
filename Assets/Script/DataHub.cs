using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataHub
{
    public static List<Boid> boids = new List<Boid>();

    public static float maxSteerForce = 3f;
}
