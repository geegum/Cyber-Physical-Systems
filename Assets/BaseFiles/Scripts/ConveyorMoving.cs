using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorMoving : MonoBehaviour
{
    public Transform tablePoint;

    private void OnCollisionStay(UnityEngine.Collision collision)
    {
        
        Rigidbody rigidbody = collision.gameObject.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            
            collision.gameObject.transform.Translate(Vector3.back * 5f * Time.deltaTime);
        }
    }

    private void OnCollisionExit(UnityEngine.Collision collision)
    {
        Rigidbody rigidbody = collision.gameObject.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.velocity = Vector3.zero;
            collision.gameObject.transform.position = Vector3.MoveTowards(collision.gameObject.transform.position, tablePoint.position, 0.5f * Time.deltaTime);
            
        }
    }
}
