using UnityEngine;

public class GunDamage : MonoBehaviour
{

    public float Damage;
    public float BulletRange;
    private Transform PlayerCamera;
    public int maxAmmo;
    private int currentAmmo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created//
    void Start()
    {
        PlayerCamera = Camera.main.transform;
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame//
    void Update()
    {
        
    }

    public void Shoot()
    {
        if(currentAmmo <= 0)
        {
            return;
        }

        Ray gunRay = new Ray(PlayerCamera.position, PlayerCamera.forward); //Uses the playercamera to check. Not where the gun is pointed.//
        currentAmmo--;
        Debug.DrawRay(gunRay.origin, gunRay.direction * BulletRange, Color.red, 1f);
        Debug.Log("Shoot called");
        if (Physics.Raycast(gunRay, out RaycastHit hit, BulletRange))
        {
            Debug.Log("Hit: " + hit.collider.gameObject.name);
            if (hit.collider.gameObject.TryGetComponent(out Entity Enemy))
            {
                Debug.Log("Hit enemy, dealing damage");
                Enemy.Health -= Damage;
            }
            if(currentAmmo <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

}
