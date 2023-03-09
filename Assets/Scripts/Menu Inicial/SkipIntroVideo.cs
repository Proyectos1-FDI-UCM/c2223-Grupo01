using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipIntroVideo : MonoBehaviour
{
    private float _timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        _timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 5.6f)
        {
            SceneManager.LoadScene(1);
        }
    }
}
