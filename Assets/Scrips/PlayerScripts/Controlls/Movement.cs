using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    public float walkingSpeed = 6.0f;
    // public float runningSpeed = 25.0f;
    public float gravity = -9.8f;

    private CharacterController _characterController;

    private Animator _animator;
    
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Speed = Animator.StringToHash("speed");

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        if (_characterController == null)
        {
            Debug.LogError("CharacterController is NULL");
        }
        
        _animator = GetComponentInChildren<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Animator is NULL");
        }
    }

    private void Update()
    {
        var deltaX = Input.GetAxis("Horizontal");
        var deltaZ = Input.GetAxis("Vertical");

        _animator.SetFloat(Horizontal, deltaX);
        _animator.SetFloat(Vertical, deltaZ);

        deltaX *= walkingSpeed;
        deltaZ *= walkingSpeed;



        var movement = new Vector3(deltaX, 0, deltaZ);
          movement = Vector3.ClampMagnitude(movement, walkingSpeed);


        if (movement == Vector3.zero)
        {
            _animator.SetFloat(Speed, 0);
        }
        else
        {
            _animator.SetFloat(Speed, walkingSpeed);
        }

        movement.y = gravity;

          movement *= Time.deltaTime;

          movement = transform.TransformDirection(movement);
          _characterController.Move(movement);
    }
}
