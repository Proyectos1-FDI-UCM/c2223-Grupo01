using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroller : MonoBehaviour
{
    //PARA QUE FUNCIONE BIEN EL SCRIPT, LA IMAGEN DEBE TENER UN MATERIAL QUE SEA "UNLIT/TRANSPARENT", Y QUE EL WRAP MODE SEA "REPEAT"

    #region Parameters
    /// <summary>
    /// Speed used to move the texture
    /// </summary>
    [SerializeField]
    private float _scrollSpeed;
    private Renderer _rend;
    private float _offset;

    #endregion
    #region References
    /// <summar 
    /// Reference to own Sprite Rendere 
    /// </summary>
    private SpriteRenderer _mySpriteRenderer;
    /// <summary>
    /// Reference to own Material
    /// </summary>
    private Material _myMaterial;
    #endregion

    #region methods
    /// <summary>
    /// Disables the component, so the texture movement stops 
    /// </summary>
    private void Stop()
    {
        _scrollSpeed = 0;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _rend = GetComponent<Renderer>();
    }
    // Update is called once per frame
    void Update()
    {
        _offset = _offset - _scrollSpeed * Time.deltaTime;
        _rend.material.SetTextureOffset("_MainTex", new Vector2(-_offset, 0));
    }
}
