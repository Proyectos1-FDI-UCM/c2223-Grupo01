using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtravesarPlataforma : MonoBehaviour
    //Este componente est� creado para a�adirlo al hijo de una plataforma
    //que pueda ser atravesables desde abajo.
    //La plataforma madre: Collider con trigger y Tag de "Atravesable"
    //La plataforma hija: Lo mismo con el collider en el tope, este script y layer "Ground"
{
    #region Parameters & References
    //cc significa collider
    private GameObject _player;
    private BoxCollider2D _ccPlayer;
    private BoxCollider2D _ccPlataforma;
    private Bounds _ccPlataformaBounds;
    private Vector2 _ccPlayerSize;

    private float _topPlataforma, _piePlayer;
    #endregion

    // Start is called before the first frame update
    void Start()
    // Inicializo referencias y guardo parametros.
    {
        _player = GameManager.instance._player;
        _ccPlayer = _player.GetComponent<BoxCollider2D>();
        _ccPlataforma = GetComponent<BoxCollider2D>();
        _ccPlataformaBounds = _ccPlataforma.bounds;
        _ccPlayerSize = _ccPlayer.size;
        _topPlataforma = _ccPlataformaBounds.center.y + _ccPlataformaBounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    // Cuando se detecte que los pies del player est�n por debajo de la cima de
    // la plataforma atravesable, se podr� atravesar por debajo.
    {
        _piePlayer = _player.transform.position.y - _ccPlayerSize.y/4;
        if(_piePlayer > _topPlataforma)
        {
            _ccPlataforma.isTrigger = false;
            gameObject.tag = "Atravesable";
            gameObject.layer = LayerMask.NameToLayer("Ground");
        }
        if(!_ccPlataforma.isTrigger && (_piePlayer < _topPlataforma + 0.1f))
        {
            _ccPlataforma.isTrigger = true;
            gameObject.tag = "Untagged";
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
}
