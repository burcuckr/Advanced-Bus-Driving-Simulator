using TMPro;
using UnityEngine;

public class BusHUD : MonoBehaviour
{
    [SerializeField] private VehicleController vehicle;
    [SerializeField] private TMP_Text speedText;
    [SerializeField] private TMP_Text gearText;

    void Update()
    {
        float speed = vehicle.CurrentSpeed;

        speedText.text = Mathf.RoundToInt(speed) + " km/h"; //Mathf.RoundToInt -> tam sayıya yuvarlıyor!!

        if (speed > 2)
            gearText.text = "D";
        else if (speed < -2)
            gearText.text = "R";
        else
            gearText.text = "N";

        // Renk değiştirme
        if (speed < 30)
            speedText.color = Color.green;
        else if (speed < 60)
            speedText.color = Color.yellow;
        else
            speedText.color = Color.red;
    }
} 