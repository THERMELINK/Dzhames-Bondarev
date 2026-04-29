using System;
using UnityEngine;

public class Walk : MonoBehaviour, IMovement
{
    int speed = 5;
    public void Move(Vector2 direction)
    {
        Vector3 actualMovement = direction * Time.deltaTime * speed;
        gameObject.transform.position += actualMovement;
    }
}
