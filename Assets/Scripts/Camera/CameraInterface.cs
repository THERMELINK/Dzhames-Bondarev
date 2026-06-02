using UnityEngine;

interface CameraInterface
{
    void FollowPlayer(GameObject player);

    void FocusOnObject(GameObject focusObject);

    void ControlZoom(float deltaScrollMovement);
}
