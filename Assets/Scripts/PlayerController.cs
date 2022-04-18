using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 movement;
    private Animator _animator;
    private Rigidbody _rigidBody;

    private AudioSource _audioSource;

    //para que tenga algo por si se ejecuta la animacion antes del update
    private Quaternion rotation=Quaternion.identity;

    [SerializeField]
    private float turnSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement.Set(horizontal, 0, vertical);
        //la suma a veces es más corta o más larga al sumar las resultantes
        movement.Normalize();

        //la animación ya lo traslada

        //calcula si es aproximadamente un valor 
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0);

        bool isWalking = hasHorizontalInput || hasVerticalInput;

        _animator.SetBool("isWalking",isWalking);

		if (isWalking)
		{
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
		}
		else
		{
            _audioSource.Stop();
		}
                                       
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward,// a donde miro
            movement,//a donde quiero mirar
            turnSpeed * Time.fixedDeltaTime,//velocidad al estar en un update se toma en cuenta en delta
            0);//magnitud maxima

        rotation = Quaternion.LookRotation(desiredForward);       
    }

	private void OnAnimatorMove()
	{
        //cuando se tenga que procesar el movimiento de una animacion

        //se mueve porque si solo se reproduce vuelve atrás el paersonaje

        //pos = pos0 + Vt , el "tiempo se saca del animator"
        // mueve el rigid body con una posicion final
        //se usa cuando la animacion ya aporta movimiento
        _rigidBody.MovePosition(_rigidBody.position + movement * _animator.deltaPosition.magnitude);

        _rigidBody.MoveRotation(rotation);
	}
}
