using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimStateManager : MonoBehaviour
{
     
    [SerializeField] float mouseSensitivity = 1;
    public float xAxis, yAxis;
    [SerializeField] Transform camFollowPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xAxis += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        yAxis = Mathf.Clamp(yAxis, -80, 80);
    }

    private void LateUpdate()
    {
        //Kamera'n�n yukar� a�a�� bakmas� i�in yAxis Value kullan�yoruz.
        camFollowPos.localEulerAngles = new Vector3 (yAxis, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);

        //Burada Karakteri kameran�n bakt��� yere d�nd�r�yoruz. Kamera karakteri takip etti�i i�in o da otomatik olarak onun d�nd��� y�ne d�nm�� oluyor.
        transform.eulerAngles = new Vector3(transform.eulerAngles.z, xAxis, transform.eulerAngles.z);
    }
}
