using UnityEngine;
using UnityEngine.UIElements;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float drainRate = 20f;
    [SerializeField] private float regenRate = 10f;

    private VisualElement _fill;
    private float _currentStamina;
    private bool _isExhausted = false;

    private void OnEnable()
    {
        var root = uiDocument.rootVisualElement;
        _fill = root.Q<VisualElement>("stamina-bar-fill");
        _currentStamina = maxStamina;
    }

    private void Update()
    {
        bool IsSprinting = playerMovement.IsSprinting;

        if (IsSprinting)
        {
            _currentStamina -= drainRate * Time.deltaTime;
        }
        else
        {
            _currentStamina += regenRate * Time.deltaTime;
        }
        _currentStamina = Mathf.Clamp(_currentStamina, 0f, maxStamina);

        //The stamina bar when empty means the user can't sprint until its full again.
        if (_currentStamina <= 0f && !_isExhausted)
        {
            _isExhausted = true;
            playerMovement.canSprint = false;
        }
        //Code to allow stamina again when refilled
        if (_isExhausted && _currentStamina >= maxStamina)
        {
            _isExhausted = false;
            playerMovement.canSprint = true;
        }
        
        SetStamina(_currentStamina / maxStamina);
    }


    public void SetStamina(float normalizedValue)
    {
        //Clamp between 0 and 1
        normalizedValue = Mathf.Clamp01(normalizedValue);

        //Drive the fill width as a percentage
        _fill.style.width = Length.Percent(normalizedValue * 100f);

        //tint red when low, green normally
        _fill.style.backgroundColor = normalizedValue < 0.25f
            ? new Color(0.9f, 0.2f, 0.2f)
            : new Color(0.31f, 0.86f, 0.39f);
    }
}