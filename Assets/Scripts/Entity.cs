using UnityEngine;

public class Entity : MonoBehaviour
{
    public GameObject punchHitbox;

    [SerializeField] private float StartingHealth;
    private float health;

    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if (health == 0)
            {
                GameManager.Instance.EnemyDied();
                Destroy(gameObject);
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = StartingHealth;
        punchHitbox.SetActive(false);
    }

    public void EnableHitbox()
    {
        punchHitbox.SetActive(true);
    }

    public void DisableHitbox()
    {
        punchHitbox.SetActive(false);
    }
  

    // Update is called once per frame
    void Update()
    {
        {
            
        }
    }
}
