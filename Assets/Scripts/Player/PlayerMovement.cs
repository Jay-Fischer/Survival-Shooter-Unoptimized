using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	[Header("Input System")]
	IA_Player _inputs;
	[SerializeField] CharacterController cc;
    Vector2 moveInput;
    Vector3 moveDirection;
    Vector2 mouseScreenPosition;



    public float speed = 6f;

	private Vector3 movement;
	private Animator _anim;
	private Rigidbody playerRigidbody;
	private int floorMask;
	private float camRayLength = 100f;

	void Awake()
	{
		floorMask = LayerMask.GetMask("Floor");
        _inputs = new IA_Player();
        cc = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
		playerRigidbody = GetComponent<Rigidbody>();
	}
    private void OnEnable()
    {
        _inputs.Enable();
    }

    private void OnDisable()
    {
        _inputs.Disable();
    }

    void FixedUpdate()
	{
        moveInput = _inputs.Player.Movement.ReadValue<Vector2>().normalized;

        Move();
		Turning();
		//Animating();
	}
    public void OnAim(InputAction.CallbackContext value)
    {
        mouseScreenPosition = value.ReadValue<Vector2>();
    }

    void Move()
	{
        moveDirection = new Vector3(moveInput.x, 0, moveInput.y);

        if (moveDirection.magnitude > 0.1f)
        {

            cc.Move(moveDirection * speed * Time.deltaTime);
        }
    }

	void Turning()
	{
		Ray camRay = Camera.main.ScreenPointToRay(mouseScreenPosition);	
		RaycastHit floorHit;

		if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			transform.rotation = newRotation;

        }
	}

	/*void Animating()
	{
		bool walking = h != 0f || v != 0f;

		anim.SetBool("IsWalking", walking);
	}*/
}
