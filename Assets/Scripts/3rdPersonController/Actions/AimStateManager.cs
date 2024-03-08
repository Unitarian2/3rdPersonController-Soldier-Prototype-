using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class AimStateManager : MonoBehaviour
{
    //AxisState struct'ý üzerinden Unity input manager'dan gelen Kamera Axis value'larý alýnýyor. Bu yöntemin çalýþabilmesi için Cinemachine gereklidir. 
    public AxisState xAxis, yAxis;
    [SerializeField] Transform camFollowPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xAxis.Update(Time.deltaTime);
        yAxis.Update(Time.deltaTime);
    }

    private void LateUpdate()
    {
        //Kamera'nýn yukarý aþaðý bakmasý için yAxis Value kullanýyoruz.
        camFollowPos.localEulerAngles = new Vector3 (yAxis.Value, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);

        //Burada Karakteri kameranýn baktýðý yere döndürüyoruz. Kamera karakteri takip ettiði için o da otomatik olarak onun döndüðü yöne dönmüþ oluyor.
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis.Value, transform.eulerAngles.z);
    }
}
