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
        Ray gunRay = new Ray(PlayerCamera.position, PlayerCamera.forward); //Uses the playercamera to check. Not where the gun is pointed. 
        
        if(Physics.Raycast(gunRay, out RaycastHit hit, BulletRange))
        {
            if (hit.collider.gameObject.TryGetComponent(out Entity Enemy))
            {
                Enemy.Health -= Damage;
            }
        }
    }

}
