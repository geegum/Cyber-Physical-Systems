using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class manipulator2 : MonoBehaviour
{
    //samplemoving.cs와 거의 동일

    private GameObject manipulator;
    private Transform gripposition;
    private Transform startposition2;
    private SimpleIKSolver2 ik;
    private Gripper2 grip;

    int move = 0;
    int step = 0;
    bool stepic = false;
    public bool grab = true;


    public GameObject gripper2;
    public Transform boxPoint;
    public Transform endtablePoint;
    public Transform endtablegrip;

    // Start is called before the first frame update
    void Start()
    {
        manipulator = GameObject.Find("Base2/Sphere2/Pivot2");
        startposition2 = transform.GetChild(1);
        ik = manipulator.GetComponent<SimpleIKSolver2>();//target이 겹치므로 스크립트 1개 더 생성
        ik.target2 = startposition2.position;
        grip = gripper2.GetComponent<Gripper2>();
        gripposition = gripper2.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToEnd();
    }


    void MoveToEnd()
    {
        if (boxPoint.position != transform.position && !grip.isGrip2)
        {
            if (move == 3)
            {
                if (ik.target2 != startposition2.position)
                {
                    ik.target2 = Vector3.MoveTowards(ik.target2, startposition2.position, 4.0f * Time.deltaTime);
                }
                else
                {
                    Debug.Log("move 3 완료");
                    move = 1;
                }
            }
            else if (move == 1 || move == 0)
            {
                ik.target2 = Vector3.MoveTowards(ik.target2, startposition2.position, 4.0f * Time.deltaTime);

                transform.position = Vector3.MoveTowards(transform.position, boxPoint.position, 4.0f * Time.deltaTime);
            }
        }
        else
        {
            if (!grip.isGrip2)
            {
                
                Vector3 box1Position = boxPoint.position;
                box1Position.z += 1.1f;
                box1Position.y += 0.2f;
                ik.target2 = Vector3.MoveTowards(ik.target2, box1Position, 4.0f * Time.deltaTime);
                grab = true;
             
                
            }
            else
            {
                if (grab)
                {
                    Transform tr = grip.grippedObject2.GetComponent<Transform>();
                    Vector3 interpolation = gripposition.position;
                    interpolation.y = gripposition.position.y - 0.4f;
                    tr.position = interpolation;
                }
                Rigidbody rb = grip.grippedObject2.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                grip.grippedObject2.transform.SetParent(gripper2.transform);
                Vector3 endposition = endtablePoint.position;
                endposition.z += 0.1f;
                Vector3 normalbox1Position = endtablegrip.position;
                normalbox1Position.x += 0.4f;
                normalbox1Position.z -= 0.8f;
                normalbox1Position.x -= 0.35f * (step % 5);
                normalbox1Position.z += 0.5f * (step / 5);
                stepic = true;
                if (move == 0)
                {
                    if (ik.target2.y != startposition2.position.y)
                    {
                        ik.target2 = Vector3.MoveTowards(ik.target2, startposition2.position, 4.0f * Time.deltaTime);
                    }
                    else
                    {
                        move = 1;
                    }
                }
                if (move == 1)
                {
                    
                    if (transform.position != endposition)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, endposition, 4f * Time.deltaTime);
                        ik.target2 = Vector3.MoveTowards(ik.target2, startposition2.position, 4.0f * Time.deltaTime);

                    }
                    else
                    {
                        if (ik.target2 != normalbox1Position)
                        {
                            ik.target2 = Vector3.MoveTowards(ik.target2, normalbox1Position, 4.0f * Time.deltaTime);
                        }
                        else
                        {
                            Debug.Log("move 1 완료");
                            move = 2;
                        }
                    }
                }
                else if (move == 2)
                {
                    Vector3 normalbox2Position = normalbox1Position;
                    normalbox2Position.y -= 1.35f;
                    if (ik.target2 != normalbox2Position)
                    {
                        ik.target2 = Vector3.MoveTowards(ik.target2, normalbox2Position, 4.0f * Time.deltaTime);

                    }
                    else
                    {
                        rb.isKinematic = false;
                        grip.grippedObject2.tag = "done";
                        grip.isGrip2 = false;
                        
                        grip.grippedObject2.transform.SetParent(null);
                        move = 3;

                            

                        Debug.Log("move 2 완료");
                        Debug.Log("step:");
                        Debug.Log(step);
                        if (stepic) //normal box로 매니퓰레이터가 이동중에 step increase가 활성화 된 경우 step 1번 증가
                        {
                            step += 1;
                            stepic = false;
                        }
                    }
                }
                

            }
        }
    }
}
