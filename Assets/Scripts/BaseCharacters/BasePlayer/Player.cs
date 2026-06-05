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

    bool PlayerMovementEnabled;

    //lists interactableItems
    IIinteractable[] interactables;

    [SerializeField] GameObject equippedGun;
    delegate void GunInputs();
    GunInputs gunInputs;
    GameStateManager gameStateManager;
    private void Start()
    {
        inputManager = GetComponent<PlayerInputManager>();

        //gun Inputs
        gunInputs += DropGun;
        gunInputs += CheckReloadInput;
        gunInputs += CheckShootInput;

        shootable = GetComponentInChildren<Ishootable>();
        movement = GetComponent<IMovement>();
        jumpable = GetComponent<Ijumpable>();
        lookable = GetComponent<Ilookable>();
    }
    void Update()
    {
        walkInput = inputManager.WalkInput;
        lookable?.RotatePlayerToPosition();
        if (gunInputs != null && equippedGun != null)
        {
            gunInputs();
        }
        CheckJumpInput();
        CheckInteractInput();
        CheckScrollInput();
    }
    //-10 > 40

    private void FixedUpdate()
    {
        movement?.Move(walkInput);
        FindClosestInteractable();
    }

    void CheckShootInput()
    {

        if (inputManager.ShootPressed)
        {
            if (equippedGun != null)
            {

                shootable = equippedGun.GetComponent<Ishootable>();
                print("shootInput");
                shootable?.ShootBullet();
            }
        }
    }
    void CheckJumpInput()
    {
        if (inputManager.JumpPressed)
        {
            jumpable?.JumpNow();
        }
    }

    void CheckReloadInput()
    {
        if (inputManager.ReloadPressed)
        {
            shootable.ReloadMagazine();
        }
    }

    void CheckInteractInput()
    {
        if (inputManager.Interact)
        {

        }
    }

    void CheckDropGunInput()
    {
        if (inputManager.DropGunPressed)
        {

        }
    }

    void CheckScrollInput()
    {
        float scroll = inputManager.Scroll;
        if (scroll != 0)
        {
            OnScroll?.Invoke(scroll);
        }
    }

    void EquipGun()
    {

    }

    void DropGun()
    {

    }

    void FindClosestInteractable()
    {
        if (isSomethingInTrigger)
        {
            GameObject closestObj;
            float distanceClosest = float.MaxValue;
            foreach (GameObject obj in gameObjectsInRange)
            {
                float currentDistance = Vector3.Distance(gameObject.transform.position, obj.transform.position);
                if (currentDistance < distanceClosest)
                {
                    distanceClosest = currentDistance;
                    closestObj = obj;
                }
            }
        }
    }


    private bool isSomethingInTrigger = false;
    List<GameObject> gameObjectsInRange = new();
    private void OnTriggerEnter2D(Collider2D collision)
    {
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
