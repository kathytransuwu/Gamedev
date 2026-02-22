using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public UnityEvent OnShoot;
    public float shootCooldown;

    public bool Automatic;

    private float CurrentCooldown;

    public InputAction GunShoot;

    private void OnEnable()
    {
        GunShoot.Enable();
    }
    private void OnDisable()
    {
        GunShoot.Disable();
    }
    void Start()
    {
        CurrentCooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentCooldown -= Time.deltaTime;

        bool Shooting = Automatic ? GunShoot.IsPressed() : GunShoot.WasPressedThisFrame();

        UnityEngine.Debug.Log($"Shooting: {Shooting}, Cooldown: {CurrentCooldown}"); // Is input even registering?


        if (Shooting && CurrentCooldown <= 0)
        {
            UnityEngine.Debug.Log("OnShoot Invoked"); // Is the event firing?
            OnShoot.Invoke();
            CurrentCooldown = shootCooldown;
        }
    }
}
