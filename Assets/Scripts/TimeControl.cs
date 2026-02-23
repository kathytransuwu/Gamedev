using UnityEngine;

public class TimeControl : MonoBehaviour
{

    public float SlowTime = 0.3f; //30% speed
    public float NormalTime = 1f; //100% speed
    public float TransitionSpeed = 5f; //Speed between them, used for lerp

    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float TargetTimeScale = rb.linearVelocity.magnitude > 0.1f ? SlowTime : NormalTime;
        Time.timeScale = Mathf.Lerp(Time.timeScale, TargetTimeScale, TransitionSpeed * Time.unscaledDeltaTime);
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
}
