using UnityEngine;

public class VehicleController : MonoBehaviour
{
    //Serialized -> Private olan değişkeni inspectiordan değişebilmek/erişebilmek için.
    //Görünmez model
    [SerializeField] private WheelCollider frontLeft;
    [SerializeField] private WheelCollider frontRight;
    [SerializeField] private WheelCollider rearLeft;
    [SerializeField] private WheelCollider rearRight;
    
    //Dönen tekerlerklerr oyuncunun gördüğü;
    [SerializeField] private Transform frontLeftVisual; //On lastiğin transformunu (position, rotation, scale) tut.
    [SerializeField] private Transform frontRightVisual;
    [SerializeField] private Transform rearLeftVisual;
    [SerializeField] private Transform rearRightVisual;
    //Aracın jantı lastiklerden ayrı bu yüzden onuda döndürmelyiz. (Not: Rim-> Jant)
    [SerializeField] private Transform frontLeftRim;
    [SerializeField] private Transform frontRightRim;
    [SerializeField] private Transform rearLeftRim;
    [SerializeField] private Transform rearRightRim;

    //Direksiyon(sadece rotation değişecek);
    [SerializeField] private Transform steeringWheel; //direksiyon
    private float steeringWheelRotation = 540f; //direksiyonun max kaç derece döneceği. 1,5 tur

    private Quaternion steeringWheelStartRotation;// Quaternion -> veri tipi dönüş bilgisi tutuyor.

    //Aracın hızı/freni
    private float brakeForce = 7000f; //Fren kuvveti.
    private float motorForce = 3000f; //Gaza basınca motorun tekere uyguladığı kuvvet.
    private float reverseMotorForce = 4500f; //Geri geri giderkenki motora uygulanan kuvvet.
    private float maxSteeringAngle = 40f; //Ön tekerlerin Maks dönüş açısı.
    private float maxSpeed = 65f; //bus max speed!!
    
    //Components
    private Rigidbody rb; //rb değişkenini tanımladık.

    //Speed
    private float currentSpeed; 
    private float forwardSpeed;

    public float CurrentSpeed;

    //Inputs
    private float horizontalInput; // AD
    private float verticalInput; // WS


    private void Awake() //Oyun başlamadan çalışır.
    {
        rb = GetComponent<Rigidbody>(); //Her seferinde GetComponent<Rigidbody>() yazmak yerinbe bir kere aliyoruz.
        rb.centerOfMass = new Vector3(0f, -0.8f, 0f); //Normalde ağırlık merkezi ortadadır ama otobüs kolay devrilmesin diye 0.8 birim aşağıu indirdim

        steeringWheelStartRotation = steeringWheel.localRotation; //Direksiyonun ilk pozisyonunu kaydeder.
    
    }
    private void Update(){
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Toplam hız (km/h)
        currentSpeed = rb.linearVelocity.magnitude * 3.6f; //magnitude -> vektör büyüklüğünü hesaplar.*3.6f -> km/h çevirmek

        // Aracın ileri yöndeki hızı (km/h)
        forwardSpeed = Vector3.Dot(rb.linearVelocity, transform.forward) * 3.6f;

        CurrentSpeed = forwardSpeed;
    }
    private void FixedUpdate() //Fizik işlemleri.
    {
        //SIRASI ÖNEMLİİİ!!!
        HandleMotor(); //Motor kuvveti uygulama -> gaz, fren, geri gitme, max hız.
        HandleSteering(); //Ön tekerleklerin yönünü değiştiriyor.
        HandleSteeringWheel(); //Direksiyonu döndürüyor.
        UpdateWheelVisuals(); //Lastik ve jantı döndürmek
    }


    private void HandleMotor(){ //Motor ve fren sistemi
        // W tuşu - İleri git
        if (verticalInput > 0) //Oyuncu W ye bastı mı?? -> +1
        {
            if (currentSpeed < maxSpeed) //Hız sınırı kontrolü.
            {
                rearLeft.motorTorque = verticalInput * motorForce;
                rearRight.motorTorque = verticalInput * motorForce;
            }
            else //Hız sınırına ulaştırığında
            {
                //Daha fazla güç verme.
                rearLeft.motorTorque = 0f;
                rearRight.motorTorque = 0f;
            }
            //Gaz verirken fren uygulama.
            frontLeft.brakeTorque = 0f;
            frontRight.brakeTorque = 0f;
            rearLeft.brakeTorque = 0f;
            rearRight.brakeTorque = 0f;
        }

        // S tuşu hem ileri hem fren
        else if (verticalInput < 0) //Oyuncu S e bsdtı mı?
        {
            // Araç hâlâ ileri gidiyorsa fren yap
            if (forwardSpeed > 1f) //Araç hala ileri gidiyorsa;
            {
                //Motor kuvvetinni kestik.
                rearLeft.motorTorque = 0f;
                rearRight.motorTorque = 0f;

                //4 tekerede fren uygula!!
                frontLeft.brakeTorque = brakeForce;
                frontRight.brakeTorque = brakeForce;
                rearLeft.brakeTorque = brakeForce;
                rearRight.brakeTorque = brakeForce;
            }
            // Araç durduysa geri git
            else
            {
                //Fren uygulama geri geri gidicez.
                frontLeft.brakeTorque = 0f;
                frontRight.brakeTorque = 0f;
                rearLeft.brakeTorque = 0f;
                rearRight.brakeTorque = 0f;

                // Geri hız limiti
                if (forwardSpeed > -30f)
                {
                    //Geri motor kuvveti uyguka. vertical input -> -1
                    rearLeft.motorTorque = verticalInput * reverseMotorForce;
                    rearRight.motorTorque = verticalInput * reverseMotorForce;
                }
                else //Hız sınırı aşarsa motor kuvvetini kes.
                {
                    rearLeft.motorTorque = 0f;
                    rearRight.motorTorque = 0f;
                }
            }
        }

        // Hiçbir tuşa basılmıyorsa
        else
        {
            rearLeft.motorTorque = 0f;
            rearRight.motorTorque = 0f;

            frontLeft.brakeTorque = 0f;
            frontRight.brakeTorque = 0f;
            rearLeft.brakeTorque = 0f;
            rearRight.brakeTorque = 0f;
        }
    }
    
    private void HandleSteering() //Ön tekerlek dönüş açısı
    {
        float steerAngle = horizontalInput * maxSteeringAngle; //steerAngle ön tekerin kaç derece döneceğini tutar.

        //Açıyı ön tekerleklere uygular.
        frontLeft.steerAngle = steerAngle;
        frontRight.steerAngle = steerAngle;
    }

    //Direksiyonu döndüren metod;
    private void HandleSteeringWheel()
    {
        //Direksiyon kaç derece dönecek; (Tekerin dönüşüne göre belli oranla dönüyor)
        float angle = (frontLeft.steerAngle / maxSteeringAngle) * steeringWheelRotation;

        steeringWheel.localRotation = steeringWheelStartRotation * Quaternion.Euler(0f, 0f, angle); //Quaternion.Euler(0f, 0f, angle) -> direksiyonu Z ekseni etrafında angle kadar döndürür.
    }
    //WheelCollider'ın hareketimni jant ve lastiğe aktarmak;
    private void UpdateWheelVisuals(){
        UpdateSingleWheel(frontLeft, frontLeftVisual); //Ön sol WheelCollider'ın konumunu ve dönüşünü al, ön sol lastik modeline uygula.
        UpdateSingleWheel(frontLeft, frontLeftRim); //Aynı işlemi janta uygula.
        UpdateSingleWheel(frontRight, frontRightVisual);
        UpdateSingleWheel(frontRight, frontRightRim);
        UpdateSingleWheel(rearLeft, rearLeftVisual);
        UpdateSingleWheel(rearLeft, rearLeftRim);
        UpdateSingleWheel(rearRight, rearRightVisual);
        UpdateSingleWheel(rearRight, rearRightRim);
    }
    //Bu metod tek tekeri günceller 8 kere yazmamak için;
    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelVisual){
        wheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation); 
        //GetWorldPose -> Tekerin o anki komum ve dönüş açısı.

        //Görsel lasitiği wheel colliderla aynı konum ve dönüş açısına götürüyor
        wheelVisual.position = position;
        wheelVisual.rotation = rotation;
    }

}
