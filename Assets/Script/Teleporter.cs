using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public bool flipX;
    public bool flipY;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        Transform boidTransform = other.gameObject.transform;
        boidTransform.position += new Vector3(flipX ? -2*boidTransform.position.x : 0f, flipY ? -2*boidTransform.position.y : 0f, 0f);
    }
}
