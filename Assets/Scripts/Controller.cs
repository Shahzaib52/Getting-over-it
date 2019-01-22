using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject Jack;
    public GameObject hammer;
    public float MaxDistance;


    private Rigidbody body_rig;
    private Rigidbody hammer_rig;
    private Transform hammer_anchor;
    private float RelativeDistance;
   


    // Start is called before the first frame update
    void Start()
    {
        body_rig = Jack.GetComponent<Rigidbody>();
        hammer_rig = hammer.GetComponent<Rigidbody>();
        hammer_anchor = hammer.transform.GetChild(2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HammerControl();
    }

    void HammerControl()
    {
        //Vector2 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 MousePosition = GetConfinedPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        hammer_rig.velocity = (new Vector3(MousePosition.x, MousePosition.y, hammer.transform.position.z) - hammer_anchor.position) * 10.0f;


        if (hammer.GetComponent<CollisionDetection>().IsCollision)
        {
            BodyControl(hammer_rig.velocity);
        }

        Vector3 direction = (new Vector3(MousePosition.x, MousePosition.y, hammer_anchor.position.z) - new Vector3(Jack.transform.position.x, Jack.transform.position.y, hammer_anchor.position.z)).normalized;
        hammer.transform.RotateAround(hammer_anchor.position, Vector3.Cross(hammer_anchor.up, direction), Vector3.Angle(hammer_anchor.up, direction));
    }

    void BodyControl(Vector3 velocity)
    {
        body_rig.velocity = -velocity*0.5f;
    }

    Vector2 GetConfinedPosition(Vector2 mouseposition)
    {
        Vector2 confinedMousePosition;
        Vector2 JackPosition = new Vector2(Jack.transform.position.x, Jack.transform.position.y);
        RelativeDistance = Vector2.Distance(mouseposition, JackPosition);
        if (RelativeDistance > MaxDistance)
        {
            confinedMousePosition = (mouseposition - JackPosition).normalized * MaxDistance + JackPosition;
        }
        else
        {
            confinedMousePosition = mouseposition;
        }
        return confinedMousePosition;
    }
}
