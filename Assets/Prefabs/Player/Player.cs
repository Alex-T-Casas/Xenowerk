using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private MovementComponent MovmentComp;

    private InputActions inputActions;
    private Animator animator;
    [SerializeField] WeaponScript[] startWeaponPrefabs;
    [SerializeField] Transform weaponSocket;
    [SerializeField] List<WeaponScript> Weapons;
    int CurrentActiveWeaponIndex;
    [SerializeField] WeaponScript currentActiveWeapon;
    int UpperBodyLayerIndex;

    float PlayerHealthCurrent = 100f;
    float PlayerHealthMax = 100f;
    [SerializeField] float HealthBarVal;
    [SerializeField] Material HealthBar;
    
    #region Weapons
    [SerializeField] GameObject Rifle;
    [SerializeField] GameObject Shotgun;
    #endregion

    private void Awake()
    {
        inputActions = new InputActions();
        HealthBarVal = HealthBar.GetFloat("_Progress");
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
        inputActions.Gameplay.WeaponSwitch.performed += SwitchToNextWeapon;

        InitializeWeapons();

    }

    void InitializeWeapons()
    {
        /*foreach(WeaponScript weapon in startWeaponPrefabs)
        {
            WeaponScript newWeapon = Instantiate(weapon);
            newWeapon.transform.position = weaponSocket.position;
            newWeapon.transform.rotation = weaponSocket.rotation;
            newWeapon.transform.parent = weaponSocket;
            newWeapon.SetActive(false);
            Weapons.Add(newWeapon);
        }*/

        Rifle = Instantiate(startWeaponPrefabs[0].gameObject);
        Shotgun = Instantiate(startWeaponPrefabs[1].gameObject);

        Rifle.transform.position = weaponSocket.position;
        Rifle.transform.parent = weaponSocket;
        Rifle.SetActive(false);

        Shotgun.transform.position = weaponSocket.position;
        Shotgun.transform.parent = weaponSocket;
        Shotgun.SetActive(false);


        Weapons.Add(Rifle.GetComponent<WeaponScript>());
        Weapons.Add(Shotgun.GetComponent<WeaponScript>());

        animator.SetBool("useRifle", true);
        animator.SetBool("useShotgun", false);
        EquipWeapon(0);
    }

    void EquipWeapon(int weaponIndex)
    {
        /*if(weaponIndex > Weapons.Count || Weapons[weaponIndex] == currentActiveWeapon)
        {
            return;
        }*/

        if(currentActiveWeapon!=null)
        {
            currentActiveWeapon.SetActive(false);
        }

        CurrentActiveWeaponIndex = weaponIndex;
        currentActiveWeapon = Weapons[weaponIndex];
        currentActiveWeapon.SetActive(true);
    }

    private void SwitchToNextWeapon(InputAction.CallbackContext obj)
    {
        if (CurrentActiveWeaponIndex == 0)
        {
            animator.SetBool("useRifle", false);
            animator.SetBool("useShotgun", true);
            EquipWeapon(1);
        }
        else if (CurrentActiveWeaponIndex == 1)
        {
            animator.SetBool("useRifle", true);
            animator.SetBool("useShotgun", false);
            EquipWeapon(0);
        }
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
        UpdateHealthBar();
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
    }

    public void TakeDamage(float damage)
    {
        PlayerHealthCurrent = PlayerHealthCurrent - damage;

        HealthBarVal = PlayerHealthCurrent % PlayerHealthMax;
    }

    public void UpdateHealthBar()
    {
        HealthBar.SetFloat("_Progress", HealthBarVal);
    }
}