using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MovementStateManager : MonoBehaviour
{
    [Header("-Movement Settings-")]
    public float currentMoveSpeed = 3f;
    public float walkSpeed = 3, walkBackSpeed = 2;
    public float runSpeed = 7, runBackSpeed = 5;
    public float crouchSpeed = 2, crouchBackSpeed = 1;

    [Header("-Gravity Settings-")]
    [SerializeField] private float groundYOffset;
    [SerializeField] private LayerMask groundMask;
    Vector3 groundCheckSpherePos;
    [SerializeField] private float gravity = -9.81f;
    Vector3 velocity;

    //Input
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public float inputX, inputZ;

    //References
    CharacterController characterController;
    [HideInInspector] public Animator animator;

    //States
    MovementBaseState currentState;
    public IdleState Idle = new();
    public WalkState Walk = new();
    public CrouchState Crouch = new();
    public RunState Run = new();


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        SwitchState(Idle);
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionAndMove();
        HandleGravity();

        currentState.UpdateState(this);

        animator.SetFloat("hzInput", inputX, 0.1f, Time.deltaTime);
        animator.SetFloat("vInput", inputZ, 0.1f, Time.deltaTime);
        
    }


    public void SwitchState(MovementBaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }
    void GetDirectionAndMove()
    {
        //Old input system'dan hareket deðerlerini alýyoruz. Player X ve Z'de hareket edecek.
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        //Hareket için direction hesaplamasý
        Vector3 xDirection = transform.right * inputX;
        Vector3 zDirection = transform.forward * inputZ;
        direction = xDirection + zDirection;

        //Karakteri hareket ettiriyoruz. 
        characterController.Move(direction.normalized * currentMoveSpeed * Time.deltaTime);
    }

    bool IsGrounded()
    {
        //Önce sphere cast'in baþlangýç noktasýný belirliyoruz. groundYOffset ile yukarý aþaðý yönde baþlangýç noktasý deðiþtirilebilir.
        groundCheckSpherePos = new Vector3(transform.position.x,transform.position.y - groundYOffset, transform.position.z);

        if(Physics.CheckSphere(groundCheckSpherePos,characterController.radius - 0.05f,groundMask)) return true;
        return false;
    }

    void HandleGravity()
    {
        if (!IsGrounded()) velocity.y += gravity * Time.deltaTime;
        else if(velocity.y<0)velocity.y = -2;

        characterController.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(characterController != null) Gizmos.DrawWireSphere(groundCheckSpherePos, characterController.radius - 0.05f);

    }
}
