using System;
using UnityEngine;

public class Walk : MonoBehaviour
{
    int speed = 5;
    public void Move(Vector2 direction, GameObject objectToMove)
    {
        Vector3 actualMovement = direction * Time.deltaTime * speed;
        objectToMove.transform.position += actualMovement;
    }
}
