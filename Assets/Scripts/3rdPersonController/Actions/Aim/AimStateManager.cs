using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimStateManager : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 1;
    float xAxis, yAxis;
    [SerializeField] Transform camFollowPos;

    //States
    AimBaseState currentState;
    public HipfireState Hip = new();
    public AimState Aim = new();

    [HideInInspector] public Animator animator;

    [Header(">ZOOM SETTINGS<")]
    public CinemachineVirtualCamera virtualCamera;
    public float adsFov = 40;
    [HideInInspector] public float hipFov;
    [HideInInspector] public float currentFov;
    public float fovTransitionSpeed = 10;

    [Header(">AIMING SETTINGS<")]
    public Transform aimPos;
    [HideInInspector] public Vector3 actualAimPos;
    [SerializeField] float aimTransitionSpeed = 20;
    [SerializeField] LayerMask aimMask;

    // Start is called before the first frame update
    void Start()
    {
        hipFov = virtualCamera.m_Lens.FieldOfView;
        animator = GetComponent<Animator>();
        SwitchState(Hip);
    }

    // Update is called once per frame
    void Update()
    {
        xAxis += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        yAxis = Mathf.Clamp(yAxis, -80, 80);

        virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, currentFov, fovTransitionSpeed * Time.deltaTime);

        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        if(Physics.Raycast(ray,out RaycastHit hit, Mathf.Infinity, aimMask))
        {
            aimPos.position = Vector3.Lerp(aimPos.position,hit.point,aimTransitionSpeed * Time.deltaTime);
            actualAimPos = hit.point;
        }

        currentState.UpdateState(this);
    }

    private void LateUpdate()
    {
        //Kamera'nýn yukarý aþaðý bakmasý için yAxis Value kullanýyoruz.
        camFollowPos.localEulerAngles = new Vector3 (yAxis, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);

        //Burada Karakteri kameranýn baktýðý yere döndürüyoruz. Kamera karakteri takip ettiði için o da otomatik olarak onun döndüðü yöne dönmüþ oluyor.
        transform.eulerAngles = new Vector3(transform.eulerAngles.z, xAxis, transform.eulerAngles.z);
    }

    public void SwitchState(AimBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
    
}
