using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gripper : MonoBehaviour
{
    public bool isGrip = false;
    public GameObject grippedObject; // 충돌한 객체를 저장할 변수
    public int color;

    
    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag == "TargetObject") 
        {
            isGrip = true;
            grippedObject = collision.gameObject; // 충돌한 객체를 그리퍼에 할당

            Material material = collision.gameObject.GetComponent<MeshRenderer>().sharedMaterial;

            if (material.color == Color.blue)
            {
                Debug.Log("It's blue");
                color =1;
            }
            else if (material.color == Color.red)
            {
                Debug.Log("It's Red!");
                color=2;
            }
        }
    }
}