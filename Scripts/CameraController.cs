using UnityEngine;

public class CameraController : MonoBehaviour //Tek kamera pozisyon değiştirecek.
{
    [SerializeField] private Transform driverPoint;
    [SerializeField] private Transform topPoint;
    [SerializeField] private Transform wheelPoint;
    [SerializeField] private Transform birdPoint;

    private Transform currentPoint; //Kamera driver veya top kısmında olabilir, bunu tutacak!!
 
    void Start()
    {
        currentPoint = driverPoint; //Oyun başladığında kamera driver konumundan başlasın.
    }


    void LateUpdate()
    {
        transform.position = currentPoint.position;
        transform.rotation = currentPoint.rotation;
        //main camerayı = currentpointin konumu yap, yönü yap.
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.C)){
            if(currentPoint == driverPoint){
                currentPoint = topPoint;
            }else if(currentPoint == topPoint){
                currentPoint = birdPoint;
            }else if(currentPoint == birdPoint){
                currentPoint  = wheelPoint;
            }else{
                currentPoint = driverPoint;
            }
        }
    }
}
