using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gripper2 : MonoBehaviour
{
    public bool isGrip2 = false;
    public GameObject grippedObject2; // �浹�� ��ü�� ������ ����


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "place")
        {
            isGrip2 = true;
            grippedObject2 = collision.gameObject; // �浹�� ��ü�� �׸��ۿ� �Ҵ�

            Material material = collision.gameObject.GetComponent<MeshRenderer>().sharedMaterial;
        }
    }
}
