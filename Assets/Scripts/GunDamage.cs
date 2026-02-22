using UnityEngine;

public class GunDamage : MonoBehaviour
{

    public float Damage;
    public float BulletRange;
    private Transform PlayerCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        Debug.Log("Shoot called"); // Is GunDamage.Shoot being reached?
        Ray gunRay = new Ray(PlayerCamera.position, PlayerCamera.forward);
        if(Physics.Raycast(gunRay, out RaycastHit hit, BulletRange))
        {
            Debug.Log($"Hit: {hit.collider.gameObject.name}"); // Are we hitting anything?
            if (hit.collider.gameObject.TryGetComponent(out Entity Enemy))
            {
                Debug.Log($"Hit entity, dealing {Damage} damage"); // Is it an Entity?
                Enemy.Health -= Damage;
            }
        }
    }

}
