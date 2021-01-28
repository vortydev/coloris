using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshake : MonoBehaviour
{
    Vector3 initialPosition;

    public float shakeDuration; // Time shaking
    public float shakeMagnitude; // The intensity of the screenshake
    public float dampingSpeed; // How quickly is it slowing down

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("shakeMagnitude"))
        {
            PlayerPrefs.SetFloat("shakeMagnitude", 0.5f);
        }

        shakeMagnitude = PlayerPrefs.GetFloat("shakeMagnitude");
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

    public void UpdateShakeMagnitude(float m)
    {
        if (shakeMagnitude != m)
        {
            shakeMagnitude = m;
            SettingsManager.SaveFloatPlayerPrefs("shakeMagnitude", m);
        }
    }
}