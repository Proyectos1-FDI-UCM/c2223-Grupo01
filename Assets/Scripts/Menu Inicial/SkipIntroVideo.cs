using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipIntroVideo : MonoBehaviour
{
    private float _timer = 0.0f;
    [SerializeField] private float _maxTimer;
    [SerializeField] private int _sceneIndex;
    private UniversalInput _newInput;

    // Start is called before the first frame update
    void Start()
    {
        _timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _maxTimer || _newInput.Mighty.Jump.triggered)
        {
            SceneManager.LoadScene(_sceneIndex);
        }
    }
}
