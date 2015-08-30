using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Animator))]
public class Controller : MonoBehaviour {

	private Animator _animator;
	private AnimatorStateInfo _currentBaseState;
	private AnimatorStateInfo _layer2CurrentState;

	[System.NonSerialized]
	private float animationSpeed = 1f;

	[System.NonSerialized]
	private float playerMovementSpeed = 2.5f;


	void Awake()
	{
		_animator = this.GetComponent<Animator>();

	}

	void Start () 
	{
		if(_animator.layerCount == 2)
			_animator.SetLayerWeight(1,1);
	}

	void OnAnimatorMove()
	{
		if(!_animator) return;
		var newPosition = transform.position;
		newPosition.z += _animator.GetFloat("speed") * playerMovementSpeed * Time.deltaTime;
		newPosition.x += _animator.GetFloat("direction") * playerMovementSpeed * Time.deltaTime;
		transform.position = newPosition;
	}
	
	void FixedUpdate () 
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticleInput = Input.GetAxis("Vertical");

		Debug.Log(string.Format("Horizontal: {0} Verticle {1}", horizontalInput, verticleInput));

		_animator.SetFloat("speed", verticleInput);
		_animator.SetFloat("direction", horizontalInput);

		_animator.speed = animationSpeed;
		_currentBaseState = _animator.GetCurrentAnimatorStateInfo(0);

		playerMovementSpeed = verticleInput <= 0f ? 4f : 6f;
	}
}
