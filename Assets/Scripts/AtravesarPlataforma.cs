using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtravesarPlataforma : MonoBehaviour
    //Este componente está creado para añadirlo al hijo de una plataforma
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
        _player = GameManager.instance._player; // Cogemos al player
        _ccPlayer = _player.GetComponent<BoxCollider2D>(); // Cogemos el colider del jugador
        _ccPlataforma = GetComponent<BoxCollider2D>(); // Cogemos el colider de la plataforma
        _ccPlataformaBounds = _ccPlataforma.bounds; // Guardamos en la variable plataforma bound los bounds de la plataforma (los bound son parametros
                                                    // Como por ejemplo los tamaños)
        _ccPlayerSize = _ccPlayer.size; // Guardamos el tamaño del player en su variable
        _topPlataforma = _ccPlataformaBounds.center.y + _ccPlataformaBounds.extents.y; // A partir del centro buscamos el borde superior de la plataforma
    }

    // Update is called once per frame
    void Update()
    // Cuando se detecte que los pies del player est�n por debajo de la cima de
    // la plataforma atravesable, se podr� atravesar por debajo.
    {
        _piePlayer = _player.transform.position.y - _ccPlayerSize.y / 4; // Buscamos el pie del jugador, se toma como un float a una cierta altura 
                                                                         // por debajo de su centro
        if (_piePlayer > _topPlataforma) 
        {
            _ccPlataforma.isTrigger = false; // se quita el Trigger para poder pisar sobre el
            gameObject.tag = "Atravesable"; //String typing
            gameObject.layer = LayerMask.NameToLayer("Ground"); // string typing
        }
        if(!_ccPlataforma.isTrigger && (_piePlayer < _topPlataforma + 0.1f))
        {
            _ccPlataforma.isTrigger = true; // Activas el trigger para poder traspasar la `plataforma
            gameObject.tag = "Untagged"; // string typing
            gameObject.layer = LayerMask.NameToLayer("Default"); // string typing
        }
    }
}
