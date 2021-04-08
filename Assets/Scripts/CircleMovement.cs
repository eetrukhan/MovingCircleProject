using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class CircleMovement : MonoBehaviour
{
    private float speed;
    private Vector3 point;
    private Vector3 mouseTap;
    private Queue<Vector3> Points = new Queue<Vector3>(); 
    
    [SerializeField]
    private Camera main_camera;
    
    [SerializeField]
    private GameObject moveObj;

    [SerializeField] 
    private Slider slider;
    
    private void Start()
    {
        speed = slider.value;
    }

    void Update()
    {
        if (Points.Any())
        {
           moveObj.transform.position = Vector3.MoveTowards(moveObj.transform.position,
               Points.Peek(), speed * Time.deltaTime);
           
           if(moveObj.transform.position == Points.Peek())
               Points.Dequeue();
        }
    }

    public void SpeedChange()
    {
        speed = slider.value;   
    }
 
    void OnMouseDown()
    {
        mouseTap = Input.mousePosition;
        point = main_camera.ScreenToWorldPoint(new Vector3(mouseTap.x, mouseTap.y, 0));
        point.z = 0;
        Points.Enqueue(point);
    }
    
}
