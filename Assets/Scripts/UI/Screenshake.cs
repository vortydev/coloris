/*
 * File:        Screenshake.cs
 * Author:      Étienne Ménard
 * Description: haha camera goes brrr
 */

using UnityEngine;

//https://medium.com/@mattThousand/basic-2d-screen-shake-in-unity-9c27b56b516
public class Screenshake : MonoBehaviour
{
    private Vector3 initialPosition;

    public float shakeDuration;     // Time shaking
    public float shakeMagnitude;    // The intensity of the screenshake
    private float shakeMultiplier = 4;
    public float dampingSpeed;      // How quickly is it slowing down

    private void Awake()
    {
        shakeMagnitude = PlayerPrefsManager.GetFloatPlayerPref(PlayerPrefsManager.shakeMagnitudeKEY, 0.5f);
    }

    private void OnEnable()
    {
        initialPosition = transform.localPosition;  // get the cam's position
    }

    private void FixedUpdate()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * (shakeMagnitude * (shakeMultiplier / 4));
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    public void TriggerScreenshake(float clearedLines = 4)
    {
        shakeDuration = 0.3f;
        shakeMultiplier = clearedLines;
    }

    public void UpdateShakeMagnitude(float m)
    {
        if (shakeMagnitude != m)
        {
            shakeMagnitude = m;
            PlayerPrefsManager.SaveFloatPlayerPref(PlayerPrefsManager.shakeMagnitudeKEY, m);
        }
    }
}