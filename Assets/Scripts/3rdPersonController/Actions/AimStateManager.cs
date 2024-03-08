using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class AimStateManager : MonoBehaviour
{
    //AxisState struct'� �zerinden Unity input manager'dan gelen Kamera Axis value'lar� al�n�yor. Bu y�ntemin �al��abilmesi i�in Cinemachine gereklidir. 
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
        //Kamera'n�n yukar� a�a�� bakmas� i�in yAxis Value kullan�yoruz.
        camFollowPos.localEulerAngles = new Vector3 (yAxis.Value, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);

        //Burada Karakteri kameran�n bakt��� yere d�nd�r�yoruz. Kamera karakteri takip etti�i i�in o da otomatik olarak onun d�nd��� y�ne d�nm�� oluyor.
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis.Value, transform.eulerAngles.z);
    }
}
