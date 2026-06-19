using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerInputManager))]
public class Player : MonoBehaviour
{
    public static event Action<float> OnScroll;
    PlayerInputManager inputManager;
    IMovement movement;
    Ijumpable jumpable;
    Ilookable lookable;
    Ishootable shootable;
    Vector2 walkInput;
    IIinteractable[] interactables;
    //lists interactableItems

    List<GameObject> gameObjectsInRange = new();
    GameStateManager gameStateManager;
    [SerializeField] GameObject equippedGun; private bool isSomethingInTrigger = false;

    //delegate for keeping track of gun inputs 
    delegate void GunInputs();
    GunInputs gunInputs;

    private void Start()
    {
        inputManager = GetComponent<PlayerInputManager>();

        //adding methods to delegate for keeping track of gun inputs
        gunInputs += DropGun;
        gunInputs += CheckReloadInput;
        gunInputs += CheckShootInput;

        shootable = GetComponentInChildren<Ishootable>();
        movement = GetComponent<IMovement>();
        jumpable = GetComponent<Ijumpable>();
        lookable = GetComponent<Ilookable>();
        gameStateManager = GameStateManager.instance;
    }
    void Update()
    {
        walkInput = inputManager.WalkInput;
        lookable?.RotatePlayerToPosition(inputManager.MousePosition);
        CheckJumpInput();
        CheckInteractInput();

        if (gunInputs != null && equippedGun != null)
        {
            gunInputs();
        }
        CheckScrollInput();
    }
    //-10 > 40

    private void FixedUpdate()
    {
        //gets moves the player with th new walk inputs
        movement?.Move(walkInput);
        //checks for inrange interactables
        FindClosestInteractable();
    }

    /// <summary>
    //  first turns gun to face a position (mouse position)
    //  then checks if a gun input has been pressed
    /// </summary>
    void CheckShootInput()
    {
        shootable?.LookAtTarget(inputManager.MousePosition);
        if (inputManager.ShootPressed)
        {
            if (equippedGun != null)
            {
                shootable = equippedGun.GetComponent<Ishootable>();
                print("shootInput");
                shootable?.ShootBullet(inputManager.MousePosition);
            }
        }
    }

    /// <summary>
    /// checks if a jump input has been pressed
    /// calls the right Ijumpable componenent (interchangable -> jump/doublejump)
    /// </summary>
    void CheckJumpInput()
    {
        if (inputManager.JumpPressed)
        {
            //gets most recent jumpable interface
            jumpable = GetComponent<Ijumpable>();
            //jumps (either normal or double jump)
            jumpable?.JumpNow();
        }
    }


    //Checks if reload button has been pressed, if pressed activates the reload magazine function from the shootable interface
    void CheckReloadInput()
    {
        if (inputManager.ReloadPressed)
        {
            shootable?.ReloadMagazine();
        }
    }


    /// <summary>
    /// checks if the interact button has been pressed
    /// </summary>
    void CheckInteractInput()
    {
        if (inputManager.Interact)
        {

        }
    }


    /// <summary>
    /// checks if drop input has been pressed
    /// </summary>
    void CheckDropGunInput()
    {
        if (inputManager.DropGunPressed)
        {

        }
    }


    /// <summary>
    /// checks if there is a delta in the scroll input (scrolling)
    /// </summary>
    void CheckScrollInput()
    {
        float scroll = inputManager.Scroll;
        if (scroll != 0)
        {
            //starts an event, passes the scroll amount
            OnScroll?.Invoke(scroll);
        }
    }


    /// <summary>
    /// equips an interactable gun
    /// </summary>
    void EquipGun()
    {

    }

    /// <summary>
    /// drops an equipped gun and adds interactable instead of shootable
    /// </summary>
    void DropGun()
    {

    }


    //finds the closes interactable in a range
    void FindClosestInteractable()
    {
        if (isSomethingInTrigger)
        {
            GameObject closestObj;
            float distanceClosest = float.MaxValue;
            //instead of looping trough ALL the objects, only picks objects in range
            foreach (GameObject obj in gameObjectsInRange)
            {
                float currentDistance = Vector3.Distance(gameObject.transform.position, obj.transform.position);
                if (currentDistance < distanceClosest)
                {
                    //if the current distance is smaller than the closest noted distance, then the current object is the closest
                    distanceClosest = currentDistance;
                    closestObj = obj;
                }
            }
        }
    }



    //checks for a trigger collission
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //sets a bool to prevent double activation if multiple gameobjects are in the trigger
        isSomethingInTrigger = true;
        if (collision.gameObject.TryGetComponent(out IIinteractable test))
        {
            gameObjectsInRange.Add(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isSomethingInTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isSomethingInTrigger = false;
        if (collision.gameObject.TryGetComponent(out IIinteractable test))
        {
            if (gameObjectsInRange.Contains(collision.gameObject))
            {
                gameObjectsInRange.Remove(collision.gameObject);
            }
        }
    }
}
