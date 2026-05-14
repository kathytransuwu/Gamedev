using UnityEngine;
using UnityEngine.UIElements;

public class GunPickup : MonoBehaviour
{
    public GameObject gun;
    public UIDocument hud;
    private VisualElement reticle;
    void Start()
    {
        reticle = hud.rootVisualElement.Q<VisualElement>("Reticle");
        reticle.style.display = DisplayStyle.None; // Hides reticle initially
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gun.SetActive(true);
            gameObject.SetActive(false); //Disables pickup
            reticle.style.display = DisplayStyle.Flex; //Enables reticle
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        
    }
}
