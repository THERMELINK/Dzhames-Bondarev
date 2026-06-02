using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;


/// <summary>
/// this script is put on the camera
/// </summary>
public class CameraManager : MonoBehaviour, CameraInterface
{
    [SerializeField] GameObject currentLockedObject;
    float camMinZoom = 1;
    float camMaxZoom = 8;

    //cameraZoom
    float OrtographicCameraSize;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OrtographicCameraSize = gameObject.GetComponent<Camera>().orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FollowPlayer(GameObject player)
    {
        currentLockedObject = player;
    }
    public void FocusOnObject(GameObject objectToFocus)
    {

    }

    public void ControlZoom(float deltaZoomInput)
    {
        OrtographicCameraSize += deltaZoomInput;
        OrtographicCameraSize = Mathf.Clamp(OrtographicCameraSize, camMinZoom, camMaxZoom);
        gameObject.GetComponent<Camera>().orthographicSize = OrtographicCameraSize;
    }

    IEnumerator StayLockedOnObjectFor(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

}
