using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshake : MonoBehaviour
{
    Transform transform;
    Vector3 initialPosition;

    public float shakeDuration = 0f; // Time shaking
    public float shakeMagnitude = 0.7f; // The intensity of the screenshake
    public float dampingSpeed = 1.0f; // How quickly is it slowing down

    private void Awake()
    {
        if (transform == null)
        {
            transform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    private void OnEnable()
    {
        initialPosition = transform.localPosition;
    }

    private void FixedUpdate()
    {

        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    public void TriggerScreenshake()
    {
        shakeDuration = 0.3f;
    }

    // https://medium.com/@mattThousand/basic-2d-screen-shake-in-unity-9c27b56b516
}
