using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private MovementComponent MovmentComp;

    private InputActions inputActions;
    private Animator animator;
    [SerializeField] WeaponScript[] startWeaponPrefabs;
    [SerializeField] Transform weaponSocket;
    List<WeaponScript> Weapons;
    [SerializeField] int CurrentActiveWeaponIndex = 0;
    WeaponScript currentActiveWeapon;
    int UpperBodyLayerIndex;

    private void Awake()
    {
        inputActions = new InputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        OnEnable();
        MovmentComp = GetComponent<MovementComponent>();
        animator = GetComponent<Animator>();
        UpperBodyLayerIndex = animator.GetLayerIndex("UpperBody");
        //Set up inputs:
        inputActions.Gameplay.Movement.performed += MovementOnperformed;
        inputActions.Gameplay.Movement.canceled += MovementOncanceled;
        inputActions.Gameplay.MousePosition.performed += MousePositionOnperformed;
        inputActions.Gameplay.Fire.performed += Fire;
        inputActions.Gameplay.Fire.canceled += StopFire;

    }

    void InitializeWeapons()
    {
        foreach(WeaponScript weapon in startWeaponPrefabs)
        {
            WeaponScript newWeapon = Instantiate(weapon);
            newWeapon.transform.position = weaponSocket.position;
            newWeapon.transform.rotation = weaponSocket.rotation;
            newWeapon.transform.parent = weaponSocket;
            newWeapon.SetActive(false);
            Weapons.Add(newWeapon);
        }
        
    }

    void EquipWeapon(int weaponIndex)
    {
        if(weaponIndex > Weapons.Count || Weapons[weaponIndex] == currentActiveWeapon)
        {
            return;
        }

        if(currentActiveWeapon!=null)
        {
            currentActiveWeapon.SetActive(false);
        }

        CurrentActiveWeaponIndex = weaponIndex;
        currentActiveWeapon = Weapons[CurrentActiveWeaponIndex];
        currentActiveWeapon.SetActive(true);
    }
    private void StopFire(InputAction.CallbackContext obj)
    {
        animator.SetLayerWeight(UpperBodyLayerIndex, 0);
    }

    private void Fire(InputAction.CallbackContext obj)
    {
        animator.SetLayerWeight(UpperBodyLayerIndex, 1);
    }

    private void MousePositionOnperformed(InputAction.CallbackContext obj)
    {
        MovmentComp.SetCursorPosition(obj.ReadValue<Vector2>());
    }

    private void MovementOncanceled(InputAction.CallbackContext obj)
    {
        MovmentComp.SetMovementInput(obj.ReadValue<Vector2>());
    }

    private void MovementOnperformed(InputAction.CallbackContext obj)
    {
        MovmentComp.SetMovementInput(obj.ReadValue<Vector2>());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimationParamaters();
    }

    void UpdateAnimationParamaters()
    {
        Vector3 PlayerFacingDir = MovmentComp.GetPlayerDesiredLookDir();
        Vector3 PlayerMoveDir = MovmentComp.GetPlayerDesiredMoveDir();
        Vector3 PlayerRight = transform.right;
        float ForwardDistribution = Vector3.Dot(PlayerFacingDir, PlayerMoveDir);
        float RightDistribution = Vector3.Dot(PlayerRight, PlayerMoveDir);
        animator.SetFloat("moveForward", ForwardDistribution);
        animator.SetFloat("moveLeft", RightDistribution);
    }

    public void FireTimePoint()
    {
        GetComponentInChildren<WeaponScript>().Fire();
        Debug.Log("fire");
    }
}