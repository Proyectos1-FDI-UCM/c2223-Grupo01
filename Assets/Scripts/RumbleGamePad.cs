using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RumbleGamePad : MonoBehaviour
{
    private Gamepad pad;
    private Coroutine _stopRumble;
    [SerializeField] private float _lowfrequency;
    [SerializeField] private float _highfrequency;
    [SerializeField] private float _rumbleduration;
    public void ControllerRumble ()
    {   
        pad = Gamepad.current;
        if (pad != null)
        pad.SetMotorSpeeds(_lowfrequency, _highfrequency);
        StartCoroutine("RumbleDuration");
    }

    private IEnumerator RumbleDuration()
    {
        float elapsedtime = 0f;

        while(elapsedtime < _rumbleduration)
        {
            elapsedtime+=Time.deltaTime;
            yield return null;
        }
        pad.SetMotorSpeeds(0f, 0f);
    }
}
