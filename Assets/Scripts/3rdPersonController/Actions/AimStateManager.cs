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
        //Kamera'nýn yukarý aþaðý bakmasý için yAxis Value kullanýyoruz.
        camFollowPos.localEulerAngles = new Vector3 (yAxis, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);

        //Burada Karakteri kameranýn baktýðý yere döndürüyoruz. Kamera karakteri takip ettiði için o da otomatik olarak onun döndüðü yöne dönmüþ oluyor.
        transform.eulerAngles = new Vector3(transform.eulerAngles.z, xAxis, transform.eulerAngles.z);
    }
}
