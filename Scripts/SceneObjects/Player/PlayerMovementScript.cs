using UnityEngine;

public class PlayerMovementScript : MonoBehaviour{

	public float Health = 100f;

	public float moveSpeed = 10.0f;
	public float jumpSpeed = 4.0f;
	public float gravity = 9.8f;
	public float terminalVelocity = 100f;
	public AudioSource source;

	private CharacterController _charCont;
	private Vector3 _moveDirection = Vector3.zero;

	//checkpoint
	public Vector3 checkpoint1 = new Vector3(40,4,-40);

	void Start() {
		_charCont = GetComponent<CharacterController>();
		source = GetComponent<AudioSource>();
	}

	void Update() {
        // STUB: Potentially handle movement if player is active/inactive
		if(true) {
			HandlePlayerMove();
		} else {
			HandlePlayerInactiveMove();
		}
		if (transform.position.y <= 2.3f){
            transform.position = checkpoint1;
        }
	}

	private void HandlePlayerMove() {
		// Move direction directly from axes
		float deltaX = Input.GetAxis("Horizontal") * moveSpeed;
		float deltaZ = Input.GetAxis("Vertical") * moveSpeed;
		_moveDirection = new Vector3(deltaX, _moveDirection.y, deltaZ);
		// Accept jump input if grounded
		if (_charCont.isGrounded) {
			if (Input.GetButton("Jump")) {
				_moveDirection.y = jumpSpeed;
			} else {
				_moveDirection.y = 0f;
			}
			// STUB: Handle movement processes, such as footsteps SFX
			if (deltaX != 0 || deltaZ != 0) {
				// source.Play();
			}
		} else {
			// STUB: Handle movement stop processes, such as footsteps SFX
			// Do handling here...

		}
		ApplyMovement();
	}

	private void HandlePlayerInactiveMove() {
		_moveDirection = Vector3.zero;
		ApplyMovement();
	}

	private void ApplyMovement() {
		_moveDirection = transform.TransformDirection(_moveDirection);
		// Apply gravity. Gravity is multiplied by deltaTime twice (once here, 
		// and once below when the moveDirection is multiplied by deltaTime). 
		// This is because gravity should be applied as an acceleration (ms^-2)
		_moveDirection.y -= this.gravity * Time.deltaTime;
		// Move the controller
		_charCont.Move(_moveDirection * Time.deltaTime);
	}

	void OnDrawGizmos(){
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(checkpoint1,new Vector3(1,1,1));
    }
}
