using System.Collections;
using Unity.VisualScripting;
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

    //this controls the camera zoom
    float OrtographicCameraSize;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OrtographicCameraSize = gameObject.GetComponent<Camera>().orthographicSize;
        //subscribes to the player onscroll
        Player.OnScroll += ControlZoom;
    }


    /// <summary>
    /// follows the player object and sets the player as the parent
    /// </summary>
    public void FollowPlayer(GameObject player)
    {
        currentLockedObject = player;
        gameObject.transform.SetParent(player.transform);
        gameObject.transform.localPosition = new Vector3(0,0,-10);
    }

    /// <summary>
    /// focuses on a selected object
    /// this would get used during a cutscene / level view at the beginning
    /// </summary>
    public void FocusOnObject(GameObject objectToFocus)
    {

    }

    /// <summary>
    /// this method controls the zoom from the camera according to the subscribed event
    /// </summary>
    public void ControlZoom(float deltaZoomInput)
    {
        OrtographicCameraSize -= deltaZoomInput;
        OrtographicCameraSize = Mathf.Clamp(OrtographicCameraSize, camMinZoom, camMaxZoom);
        gameObject.GetComponent<Camera>().orthographicSize = OrtographicCameraSize;
    }

    /// <summary>
    /// this focuses on a selected object for a certain amount of time
    /// this would get used during a cutscene / level view at the beginning
    /// </summary>
    IEnumerator StayLockedOnObjectFor(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
    private void OnDisable()
    {
        Player.OnScroll -= ControlZoom;
    }
}
