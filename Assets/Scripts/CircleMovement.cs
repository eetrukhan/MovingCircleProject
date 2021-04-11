using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

/// <summary>
/// Moves circle by taking coordinates of clicking on plane
/// </summary>
public class CircleMovement : MonoBehaviour
{
    // Speed of circle
    private float speed;

    // Coordinates in world
    private Vector3 point; 
    
    // Coordinates of mouseTap or touch of the screen
    private Vector3 mouseTap; 
    
    // Queue of coordinates
    private Queue<Vector3> Points = new Queue<Vector3>(); 
    
    // Main camera
    [SerializeField]
    private Camera main_camera; 
    
    // Object which is going to move
    [SerializeField]
    private GameObject moveObj; 

    // Speed controlling slider
    [SerializeField] 
    private Slider slider;
    
    /// <summary>
    /// Assigns the starting speed value
    /// </summary>
    private void Start()
    {
        speed = slider.value;
    }

    /// <summary>
    /// Checks each frame if there are any points in queue
    /// If yes, moves the circle towards earliest point
    /// until circle reaches it. 
    /// Then throws out reached point and repeat
    /// </summary>
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

    /// <summary>
    /// When the position of the slider changes,
    /// the variable speed changes
    /// </summary>
    public void SpeedChange()
    {
        speed = slider.value;   
    }
 
    /// <summary>
    /// Takes coordinates of tap,
    /// Translates them to local system
    /// Assigns axle z equal to 0, because we are in 2d
    /// Places the point in the queue
    /// </summary>
    void OnMouseDown()
    {
        mouseTap = Input.mousePosition;
        point = main_camera.ScreenToWorldPoint(new Vector3(mouseTap.x, mouseTap.y, 0));
        point.z = 0;
        Points.Enqueue(point);
    }
    
}
