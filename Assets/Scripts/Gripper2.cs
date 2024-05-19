using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gripper2 : MonoBehaviour
{
    public bool isGrip2 = false;
    public GameObject grippedObject2; // 충돌한 객체를 저장할 변수


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "place")
        {
            isGrip2 = true;
            grippedObject2 = collision.gameObject; // 충돌한 객체를 그리퍼에 할당

            Material material = collision.gameObject.GetComponent<MeshRenderer>().sharedMaterial;
        }
    }
}
