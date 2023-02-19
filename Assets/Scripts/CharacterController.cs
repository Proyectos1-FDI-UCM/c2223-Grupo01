using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    #region Parameters
    // esta variable puede ser leída desde otros scripts pero no cambiada, por el private set.
    public bool _isgrounded { get; private set; }
    public bool _isOnStairs;
    public bool _doublejump { get; private set; }
    [SerializeField] private float _MovementSmoothing;
    private Vector3 _velocity = Vector3.zero;
    [SerializeField] private float _jumpForce;
    private Vector3 _scale;
    private bool _facingRight = true;
    [SerializeField] private float _dashForce;
    private bool _dash = false;
    #endregion

    #region References
    //Referencia al Rigid Body
    private Rigidbody2D _myRigidBody2D;
    // referencia al colider
    private Collider2D _myCollider2D;
    //el número debe coincide con el layer de Ground
    [SerializeField] private LayerMask _groundLayer;
    private InputComponent _myInputComponent;
    #endregion

    #region Methods
    private bool IsGrounded()
    {
        // cada vez que tocamos el suelo reactivamos el doble salt0
        if (!_doublejump && _isgrounded)
        {
            _doublejump = true;
        }
        // detecto si estoy en el suelo
        return Physics2D.BoxCast(_myCollider2D.bounds.center, _myCollider2D.bounds.size, 0f, Vector2.down, .1f, _groundLayer);
    }

    public void MoveXAxis(float XAxismove)
    {
        // Muevo al personaje
        Vector3 targetVelocity = new Vector2(XAxismove * 10f, _myRigidBody2D.velocity.y);
        _myRigidBody2D.velocity = Vector3.SmoothDamp(_myRigidBody2D.velocity, targetVelocity,ref _velocity, _MovementSmoothing);
        if (XAxismove > 0 && !_facingRight)
        {
            Flip();
        }
        else if (XAxismove < 0 && _facingRight)
        {
            Flip();
        }
    }
    public void MoveYAxis(float YAxismove)
    {
        // Muevo al personaje
        Vector3 targetVelocity = new Vector2(_myRigidBody2D.velocity.x, YAxismove * 10f);
        _myRigidBody2D.velocity = Vector3.SmoothDamp(_myRigidBody2D.velocity, targetVelocity, ref _velocity, _MovementSmoothing);
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        _facingRight = !_facingRight;

        // Multiply the player's x local scale by -1.
        transform.Rotate(0f, 180f, 0f);
    }

    public void Jump()
    {
        if (!_isgrounded)
        {
            _doublejump = false;
        }
        else
        {
            _isgrounded = false;
        }

        _myRigidBody2D.AddForce(new Vector2(0f, _jumpForce));
    }

    public void Dash()
    {
        transform.Rotate(new Vector3(0, 0, 90));
        _myRigidBody2D.AddForce(_dashForce * Vector2.right*transform.localScale.x);//Impulsa al jugador si está agachado
        _myInputComponent.enabled = false;
        _dash = true;
    }
    #endregion

    private void Start()
    {
        //inicializo el Rigid Body y el collider
        _myRigidBody2D = GetComponent<Rigidbody2D>();
        _myCollider2D = GetComponent<Collider2D>();
        _myInputComponent = GetComponent<InputComponent>();
        _isOnStairs = false;
    }

    private void Update()
    {
        //detecta que estemos tocando tierra (no seais informáticos y tocad césped)
        _isgrounded = IsGrounded();

        if (_dash && _myRigidBody2D.velocity.x == 0)
        {
            transform.Rotate(new Vector3(0, 0, 270));
            _myInputComponent.enabled = true;
            _dash = false;
        }
    }
}