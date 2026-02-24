using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostingDash : MonoBehaviour
{
    [SerializeField] private GameObject shadow;
    private float shadowCoolDown;
    [SerializeField] private float shadowInitialTime;
    // Start is called before the first frame update
    void Start()
    {
        shadowCoolDown = shadowInitialTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (shadowCoolDown > 0.0f)
        {
            shadowCoolDown -= Time.deltaTime;
        }
        else
        {
            InstantiateShadow();
            shadowCoolDown = shadowInitialTime;
        }
    }

    public void InstantiateShadow()
    {
        GameObject currentShadow = Instantiate(shadow, transform.position, Quaternion.identity);
        currentShadow.transform.rotation = transform.rotation;
        currentShadow.GetComponent<SpriteRenderer>().sprite = transform.GetComponent<SpriteRenderer>().sprite;
        Destroy(currentShadow, 0.75f);
    }
}
