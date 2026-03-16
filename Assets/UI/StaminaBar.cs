using UnityEngine;
using UnityEngine.UIElements;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float drainRate = 20f;
    [SerializeField] private float regenRate = 10f;

    private VisualElement _fill;
    private float _currentStamina;

    private void OnEnable()
    {
        var root = uiDocument.rootVisualElement;
        _fill = root.Q<VisualElement>("stamina-bar-fill");
        _currentStamina = maxStamina;
    }

    private void Update()
    {
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && _currentStamina > 0;

        if (isSprinting)
            _currentStamina -= drainRate * Time.deltaTime;
        else
            _currentStamina += regenRate * Time.deltaTime;

        _currentStamina = Mathf.Clamp(_currentStamina, 0f, maxStamina);
        SetStamina(_currentStamina / maxStamina);
    }

    public void SetStamina(float normalizedValue)
    {
        // Clamp between 0 and 1
        normalizedValue = Mathf.Clamp01(normalizedValue);

        // Drive the fill width as a percentage
        _fill.style.width = Length.Percent(normalizedValue * 100f);

        // Optional: tint red when low
        _fill.style.backgroundColor = normalizedValue < 0.25f
            ? new Color(0.9f, 0.2f, 0.2f)
            : new Color(0.31f, 0.86f, 0.39f);
    }
}