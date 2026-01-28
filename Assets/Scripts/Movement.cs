using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Animations.Rigging;
using System;
using UnityEngineInternal;

public class Movement : MonoBehaviour
{

    public CinemachineCamera vCam;
    CinemachineOrbitalFollow orbital;

    public float aimingFov = 20;
    public float idleFov = 40;
    public float currentFov;
    public float FovSpeed = 10;
    
    
    public float idleCameraX;
    public float idleCameraY;
    public float aimingCameraX = 0.9f;
    public float aimingCameraY;
    public float currentCameraX;
    public float currentCameraY;
    public Transform shoulderOffset;
    public Transform neck;
    public float crouchingOffset = 0.7f;
    [SerializeField] float shoulderSwapSpeed = 10;
    
    public AimingBaseState currentAimState;
    public WithWeapon aimIdle = new WithWeapon();
    public AimState aim = new AimState();
    [SerializeField] Transform cameraTransform; 
    [SerializeField] bool faceMoveDirection = false;
    public float currentSpeed;
    public float walkSpeed = 5, walkBackSpeed = 3;
    public float runSpeed = 10, runBackSpeed = 7;
    public float crouchSpeed = 3, crouchBackSpeed = 2;
    public float airSpeed = 1.5f;

    Rigidbody rb;
    public Vector3 direction;

    private CharacterController controller;

    public float xInput, zInput;

    [SerializeField] float GroundOffset;
    [SerializeField] LayerMask GroundLayer;
    Vector3 SpherePos;
    [SerializeField] float Gravity = -9.81f;
    [SerializeField] float jumpForce = 10;
    public bool jumped;
    Vector3 Velocity;

    public BaseState currentState;
    public BaseState previousState;
    public Idle idle = new Idle();
    public Walk walk = new Walk();
    public Run run = new Run();
    public Crouch crouch = new Crouch();
    public JumpState jump = new JumpState();

    [HideInInspector]public Transform aimPos;
    public Vector3 TrueAimPos;
    [SerializeField] float aimSpeed = 20;
    [SerializeField] LayerMask aimMask;

    public Animator animator;
    public GameObject aimPoint;
    [HideInInspector] WeaponManager weapon;
    [SerializeField] float idleLookDistance = 50f;

    [SerializeField] float maxAim = 100f; 
    float lastValidXAxis;
    float lastValidYaw;


    MultiAimConstraint[] multiAims;
    WeightedTransform aimPosWeightedTransform;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        vCam = GetComponentInChildren<CinemachineCamera>();
        weapon = GetComponentInChildren<WeaponManager>();
        
        orbital = vCam.GetComponent<CinemachineOrbitalFollow>();
        idleFov = vCam.Lens.FieldOfView;
        idleCameraX = shoulderOffset.localPosition.x;
        idleCameraY = shoulderOffset.localPosition.y;
        aimingCameraY = idleCameraY;
        ChangeState(idle);
        ChangeAimState(aimIdle);

        aimPos = new GameObject().transform;
        aimPos.name = "AimPosition";

        aimPosWeightedTransform.transform = aimPos;
        aimPosWeightedTransform.weight = 1;

        multiAims = GetComponentsInChildren<MultiAimConstraint>();

        foreach(MultiAimConstraint multiAim in multiAims)
        {
            var data = multiAim.data.sourceObjects;
            data.Clear();
            data.Add(aimPosWeightedTransform);
            multiAim.data.sourceObjects = data; 
        }
    }

    void GetDirectionMove()
    {

        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;


        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical"); 
        Vector3 airDirection =  Vector3.zero;
        if(!IsGrounded()) airDirection = cameraForward * zInput + cameraRight * xInput;
        else direction = cameraForward * zInput + cameraRight * xInput;


        cameraForward.Normalize();
        cameraRight.Normalize();

        controller.Move((direction * currentSpeed + airDirection.normalized * airSpeed) * Time.deltaTime);

        if (faceMoveDirection && direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10 * Time.deltaTime);
        }

    }

    public bool IsGrounded()
    {
        SpherePos = new Vector3(transform.position.x, transform.position.y - GroundOffset, transform.position.z);
        if(Physics.CheckSphere(SpherePos, controller.radius - 0.05f, GroundLayer, QueryTriggerInteraction.Ignore)) return true;
        else return false;
    }

    void GravityForce()
    {
    
    if (IsGrounded())
    {
        if (Velocity.y < 0)
            Velocity.y = -2f; 
    }
    else Velocity.y += Gravity * Time.deltaTime;
    controller.Move(Velocity * Time.deltaTime);
    }

    void Falling()
    {
        animator.SetBool("Falling", !IsGrounded());
    }


    public void JumpForce()
    {
        Velocity.y += jumpForce;
    }

    public void Jumped()
    {
        jumped = true;
    }

    private void OnDrawGizmos()
    {
        if(controller == null) controller = GetComponent<CharacterController>();
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(SpherePos, controller.radius - 0.05f);
    }



    private float GetCameraAngle()
    {
        Vector3 camForward = cameraTransform.forward;
        Vector3 playerForward = transform.forward;

        camForward.y = 0;
        playerForward.y = 0;

        camForward.Normalize();
        playerForward.Normalize();

        return Vector3.SignedAngle(playerForward, camForward, Vector3.up);
    }

    private Vector3 GetAllowedAimDirection(float angle)
    {
        
        float clampedAngle = Mathf.Clamp(angle, -maxAim, maxAim);
        return Quaternion.AngleAxis(clampedAngle, Vector3.up) * transform.forward;
    }
    
    
    private void ClampCameraYaw(float angle)
    {
        
        float yaw = orbital.HorizontalAxis.Value;

    
        if (Mathf.Abs(angle) < maxAim)
            {
                
                lastValidYaw = yaw;
                return;
            }

    
            if ((angle > maxAim && yaw < lastValidYaw) || (angle < -maxAim && yaw > lastValidYaw))
            {
                
                lastValidYaw = yaw;
                return;
            }

        
        orbital.HorizontalAxis.Value = lastValidYaw;
    }


    void Update()
    {
       GetDirectionMove();
       GravityForce();
       Falling();

       animator.SetFloat("xInput", xInput);
       animator.SetFloat("zInput", zInput);


       currentState.UpdateState(this);
       currentAimState.UpdateState(this);
       Vector2 screenCentre = new Vector2(Screen.width/2, Screen.height/2);
       Ray ray = Camera.main.ScreenPointToRay(screenCentre);
       

       if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask) && currentAimState == aim)
        {
            float angle = GetCameraAngle();
            if(Mathf.Abs(angle) > maxAim)
            {
                   
                    Vector3 allowedDir = GetAllowedAimDirection(angle);
                    Vector3 allowedPoint = transform.position + allowedDir * Vector3.Distance(transform.position, hit.point);

                    aimPos.position = Vector3.Lerp(aimPos.position, allowedPoint, aimSpeed * Time.deltaTime);
                    
                    TrueAimPos = allowedPoint;
            }
            else 
                {
                    aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSpeed * Time.deltaTime);
                    TrueAimPos = hit.point;
                }
            ClampCameraYaw(angle);
        }
        else if (currentAimState == aimIdle)
        {
            
            Vector3 forwardPoint = transform.position + transform.forward * idleLookDistance;
            aimPos.position = Vector3.Slerp(aimPos.position, forwardPoint, aimSpeed * Time.deltaTime);
            TrueAimPos = forwardPoint;
        }

        MoveCamera();

    }
    void LateUpdate()
    {
    
       vCam.Lens.FieldOfView = Mathf.Lerp(vCam.Lens.FieldOfView, currentFov, FovSpeed * Time.deltaTime);
       shoulderOffset.localPosition = new Vector3(Mathf.Lerp(shoulderOffset.localPosition.x, currentCameraX, FovSpeed * Time.deltaTime), 
        shoulderOffset.localPosition.y, shoulderOffset.localPosition.z); 
    }

    public void ChangeAimState(AimingBaseState state)
    {
        currentAimState = state;
        currentAimState.EnterState(this);
    }
    public void ChangeState(BaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }
    void MoveCamera()
    {
        if(Input.GetKeyDown(KeyCode.Q)) 
        {
            currentCameraX = -currentCameraX;
            aimingCameraX = -aimingCameraX;
            idleCameraX = -idleCameraX;
        }
        if(currentState == crouch) aimingCameraY = crouchingOffset;
        else aimingCameraY = idleCameraY;

        Vector3 newShoulderOffset = new Vector3(idleCameraX, aimingCameraY, shoulderOffset.localPosition.z);
        shoulderOffset.localPosition = Vector3.Lerp(shoulderOffset.localPosition, newShoulderOffset, shoulderSwapSpeed * Time.deltaTime);
    }
}
