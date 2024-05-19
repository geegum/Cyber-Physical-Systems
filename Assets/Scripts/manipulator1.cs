using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class manipulator1 : MonoBehaviour
{
    int[] step = new int[] { 1, 2, 1, 3, 2, 1, 1, 2, 3, 1, 2, 1, 1, 3, 2, 1, 2, 1, 3, 1, 4 };
    int move = 0;
    int step_cnt = 0;
    int step_flag = 0;
    int redcube_cnt = 0;
    private GameObject manipulator;
    private Transform startposition;
    public Transform startposition1;
    private SimpleIKSolver ik;
    private Gripper grip;
    private Transform gripposition;
    //Gripper color;
    public GameObject gripper;
    public Transform tablePoint1;
    public Transform tablePoint2;
    public Transform tablePoint3;
    public Transform normaltablePoint;
    public Transform AbnormalBoxPoint;
    public Transform normaltablegrip;
    public Transform AbnormalBoxgrip;

    public GameObject colorsensor;


    // Start is called before the first frame update
    void Start()
    {
        manipulator = GameObject.Find("Base/Sphere/Pivot");
        startposition = transform.GetChild(1);
        ik = manipulator.GetComponent<SimpleIKSolver>();
        ik.target = startposition1.position;
        grip = gripper.GetComponent<Gripper>();
        //color=colorsensor.GetComponent<Gripper>();
        gripposition = gripper.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(step_cnt)
        switch (step[step_cnt])
        {
            case 1:
                MoveTo1();
                break;
            case 2:
                MoveTo2();
                break;
            case 3:
                MoveTo3();
                break;
            case 4:
                Debug.Log("done");
                break;
        }


    }
    void MoveTo1()
    {
        if (tablePoint1.position != transform.position && !grip.isGrip)
        {
            ik.target = Vector3.MoveTowards(ik.target, startposition1.position, 4.0f * Time.deltaTime);

            transform.position = Vector3.MoveTowards(transform.position, tablePoint1.position, 4.0f * Time.deltaTime);
        }
        else
        {
            if (!grip.isGrip)
            {
                Vector3 box1Position = tablePoint1.position;
                box1Position.z += 1f;
                box1Position.y += 0.2f;
                ik.target = Vector3.MoveTowards(ik.target, box1Position, 4.0f * Time.deltaTime);
                if (step_flag == 1)
                {
                    step_flag = 0;
                }
            }
            else
            {
                Transform tr = grip.grippedObject.GetComponent<Transform>();
                Vector3 interpolation = gripposition.position;
                interpolation.y = gripposition.position.y - 0.2f;
                tr.position = interpolation;

                Rigidbody rb = grip.grippedObject.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                grip.grippedObject.transform.SetParent(gripper.transform);
                if (move == 0)
                {
                    if (ik.target.y != startposition.position.y)
                    {
                        ik.target = Vector3.MoveTowards(ik.target, startposition.position, 4.0f * Time.deltaTime);
                    }
                    else
                    {
                        move = 1;
                    }
                }
                if (move == 1)
                {
                    if (grip.color == 1)
                    {
                        if (transform.position != normaltablePoint.position)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, normaltablePoint.position, 4f * Time.deltaTime);
                            ik.target = Vector3.MoveTowards(ik.target, startposition.position, 4.0f * Time.deltaTime);
                        }
                        else
                        {
                            Vector3 normbox1Position = normaltablegrip.position;
                            normbox1Position.z -= 0.1f;
                            normbox1Position.y += 0.5f;
                            if (normbox1Position != ik.target)
                            {
                                ik.target = Vector3.MoveTowards(ik.target, normbox1Position, 4.0f * Time.deltaTime);
                            }
                            else
                            {
                                rb.isKinematic = false;
                                grip.grippedObject.tag = "place";
                                grip.grippedObject.transform.SetParent(null);
                                grip.isGrip = false;
                                if (grip.grippedObject.transform.parent == null)
                                {
                                    Debug.Log("grip.grippedObject.transform.SetParent(null)가 실행되었습니다.");
                                    if (step_flag == 0)
                                    {
                                        if (ik.target.y != startposition.position.y)
                                        {
                                            ik.target = Vector3.MoveTowards(ik.target, startposition.position, 4.0f * Time.deltaTime);
                                        }
                                        step_cnt++;
                                        step_flag = 1;
                                        Debug.Log(step_cnt);
                                    }
                                }
                                else
                                {
                                    Debug.Log("grip.grippedObject.transform.SetParent(null)가 실행되지 않았습니다.");
                                }
                            }
                        }
                    }
                    else if (grip.color == 2)
                    {
                        if (transform.position != AbnormalBoxPoint.position)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, AbnormalBoxPoint.position, 4f * Time.deltaTime);
                            ik.target = Vector3.MoveTowards(ik.target, startposition.position, 4.0f * Time.deltaTime);
                        }
                        else
                        {
                            Vector3 Abnormbox1Position = AbnormalBoxgrip.position;
                            Abnormbox1Position.z -= 0.4f;
                            Abnormbox1Position.y += 0.2f;
                            Abnormbox1Position.x -= redcube_cnt * 0.3f;
                            Abnormbox1Position.z += redcube_cnt * 0.3f;
                            Debug.Log(Abnormbox1Position.x);
                            if (Abnormbox1Position != ik.target)
                            {
                                ik.target = Vector3.MoveTowards(ik.target, Abnormbox1Position, 4.0f * Time.deltaTime);
                            }
                            else
                            {
                                rb.isKinematic = false;
                                grip.grippedObject.tag = "red";
                                grip.grippedObject.transform.SetParent(null);
                                grip.isGrip = false;
                                if (grip.grippedObject.transform.parent == null)
                                {
                                    Debug.Log("grip.grippedObject.transform.SetParent(null)가 실행되었습니다.");
                                    if (step_flag == 0)
                                    {
                                        if (ik.target.y != startposition.position.y)
                                        {
                                            ik.target = Vector3.MoveTowards(ik.target, startposition.position, 4.0f * Time.deltaTime);
                                        }
                                        step_cnt++;
                                        redcube_cnt += 1;
                                        step_flag = 1;
                                        Debug.Log(step_cnt);
                                    }
                                }
                                else
                                {
                                    Debug.Log("grip.grippedObject.transform.SetParent(null)가 실행되지 않았습니다.");
                                }

                            }
                        }
                    }
                }
            }
        }
    }
    void MoveTo2()
    {
        if (tablePoint2.position != transform.position && !grip.isGrip)
        {
            ik.target = Vector3.MoveTowards(ik.target, startposition1.position, 4.0f * Time.deltaTime);

            transform.position = Vector3.MoveTowards(transform.position, tablePoint2.position, 4.0f * Time.deltaTime);
        }
        else
        {
            if (!grip.isGrip)
            {
                Vector3 box1Position = tablePoint2.position;
                box1Position.z += 1f;
                box1Position.y += 0.2f;
                ik.target = Vector3.MoveTowards(ik.target, box1Position, 4.0f * Time.deltaTime);
                if (step_flag == 1)
                {
                    step_flag = 0;
                }
            }
            else
            {
                Transform tr = grip.grippedObject.GetComponent<Transform>();
                Vector3 interpolation = gripposition.position;
                interpolation.y = gripposition.position.y - 0.2f;
                tr.position = interpolation;

                Rigidbody rb = grip.grippedObject.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                grip.grippedObject.transform.SetParent(gripper.transform);
                if (move == 0)
                {
                    if (ik.target.y != startposition.position.y)
                    {
                        ik.target = Vector3.MoveTowards(ik.target, startposition.position, 4.0f * Time.deltaTime);
                    }
                    else
                    {
                        move = 1;
                    }
                }
                if (move == 1)
                {
                    if (grip.color == 1)
                    {
                        if (transform.position != normaltablePoint.position)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, normaltablePoint.position, 4f * Time.deltaTime);
                            ik.target = Vector3.MoveTowards(ik.target, startposition.position, 4.0f * Time.deltaTime);
                        }
                        else
                        {
                            Vector3 normbox1Position = normaltablegrip.position;
                            normbox1Position.z -= 0.1f;
                            normbox1Position.y += 0.5f;
                            if (normbox1Position != ik.target)
                            {
                                ik.target = Vector3.MoveTowards(ik.target, normbox1Position, 4.0f * Time.deltaTime);
                            }
                            else
                            {
                                rb.isKinematic = false;
                                grip.grippedObject.tag = "place";
                                grip.grippedObject.transform.SetParent(null);
                                grip.isGrip = false;
                                if (grip.grippedObject.transform.parent == null)
                                {
                                    Debug.Log("grip.grippedObject.transform.SetParent(null)가 실행되었습니다.");
                                    if (step_flag == 0)
                                    {
                                        if (ik.target.y != startposition.position.y)
                                        {
                                            ik.target = Vector3.MoveTowards(ik.target, startposition.position, 4.0f * Time.deltaTime);
                                        }
                                        step_cnt++;
                                        step_flag = 1;
                                        Debug.Log(step_cnt);
                                    }
                                }
                                else
                                {
                                    Debug.Log("grip.grippedObject.transform.SetParent(null)가 실행되지 않았습니다.");
                                }
                            }
                        }
                    }
                    else if (grip.color == 2)
                    {
                        if (transform.position != AbnormalBoxPoint.position)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, AbnormalBoxPoint.position, 4f * Time.deltaTime);
                            ik.target = Vector3.MoveTowards(ik.target, startposition.position, 4.0f * Time.deltaTime);
                        }
                        else
                        {
                            Vector3 Abnormbox1Position = AbnormalBoxgrip.position;
                            Abnormbox1Position.z -= 0.4f;
                            Abnormbox1Position.y += 0.2f;
                            Abnormbox1Position.x -= redcube_cnt * 0.3f;
                            Abnormbox1Position.z += redcube_cnt * 0.3f;
                            Debug.Log(Abnormbox1Position.x);
                            if (Abnormbox1Position != ik.target)
                            {
                                ik.target = Vector3.MoveTowards(ik.target, Abnormbox1Position, 4.0f * Time.deltaTime);
                            }
                            else
                            {
                                rb.isKinematic = false;
                                grip.grippedObject.tag = "red";
                                grip.grippedObject.transform.SetParent(null);
                                grip.isGrip = false;
                                if (grip.grippedObject.transform.parent == null)
                                {
                                    Debug.Log("grip.grippedObject.transform.SetParent(null)가 실행되었습니다.");
                                    if (step_flag == 0)
                                    {
                                        if (ik.target.y != startposition.position.y)
                                        {
                                            ik.target = Vector3.MoveTowards(ik.target, startposition.position, 4.0f * Time.deltaTime);
                                        }
                                        step_cnt++;
                                        redcube_cnt += 1;
                                        step_flag = 1;
                                        Debug.Log(step_cnt);
                                    }
                                }
                                else
                                {
                                    Debug.Log("grip.grippedObject.transform.SetParent(null)가 실행되지 않았습니다.");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    void MoveTo3()
    {
        if (tablePoint3.position != transform.position && !grip.isGrip)
        {
            ik.target = Vector3.MoveTowards(ik.target, startposition1.position, 4.0f * Time.deltaTime);

            transform.position = Vector3.MoveTowards(transform.position, tablePoint3.position, 4.0f * Time.deltaTime);
        }
        else
        {
            if (!grip.isGrip)
            {
                Vector3 box1Position = tablePoint3.position;
                box1Position.z += 1f;
                box1Position.y += 0.2f;
                ik.target = Vector3.MoveTowards(ik.target, box1Position, 4.0f * Time.deltaTime);
                if (step_flag == 1)
                {
                    step_flag = 0;
                }
            }
            else
            {
                Transform tr = grip.grippedObject.GetComponent<Transform>();
                Vector3 interpolation = gripposition.position;
                interpolation.y = gripposition.position.y - 0.2f;
                tr.position = interpolation;

                Rigidbody rb = grip.grippedObject.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                grip.grippedObject.transform.SetParent(gripper.transform);
                if (move == 0)
                {
                    if (ik.target.y != startposition.position.y)
                    {
                        ik.target = Vector3.MoveTowards(ik.target, startposition.position, 4.0f * Time.deltaTime);
                    }
                    else
                    {
                        move = 1;
                    }
                }
                if (move == 1)
                {
                    if (grip.color == 1)
                    {
                        if (transform.position != normaltablePoint.position)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, normaltablePoint.position, 4f * Time.deltaTime);
                            ik.target = Vector3.MoveTowards(ik.target, startposition.position, 4.0f * Time.deltaTime);
                        }
                        else
                        {
                            Vector3 normbox1Position = normaltablegrip.position;
                            normbox1Position.z -= 0.1f;
                            normbox1Position.y += 0.5f;
                            if (normbox1Position != ik.target)
                            {
                                ik.target = Vector3.MoveTowards(ik.target, normbox1Position, 4.0f * Time.deltaTime);
                            }
                            else
                            {
                                rb.isKinematic = false;
                                grip.grippedObject.tag = "place";
                                grip.grippedObject.transform.SetParent(null);
                                grip.isGrip = false;
                                if (grip.grippedObject.transform.parent == null)
                                {
                                    Debug.Log("grip.grippedObject.transform.SetParent(null)가 실행되었습니다.");
                                    if (step_flag == 0)
                                    {
                                        if (ik.target.y != startposition.position.y)
                                        {
                                            ik.target = Vector3.MoveTowards(ik.target, startposition.position, 4.0f * Time.deltaTime);
                                        }
                                        step_cnt++;
                                        step_flag = 1;
                                        Debug.Log(step_cnt);
                                    }
                                }
                                else
                                {
                                    Debug.Log("grip.grippedObject.transform.SetParent(null)가 실행되지 않았습니다.");
                                }
                            }
                        }
                    }
                    else if (grip.color == 2)
                    {
                        if (transform.position != AbnormalBoxPoint.position)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, AbnormalBoxPoint.position, 4f * Time.deltaTime);
                            ik.target = Vector3.MoveTowards(ik.target, startposition.position, 4.0f * Time.deltaTime);
                        }
                        else
                        {
                            Vector3 Abnormbox1Position = AbnormalBoxgrip.position;
                            Abnormbox1Position.z -= 0.4f;
                            Abnormbox1Position.y += 0.2f;
                            Abnormbox1Position.x -= redcube_cnt * 0.3f;
                            Abnormbox1Position.z += redcube_cnt * 0.3f;
                            Debug.Log(Abnormbox1Position.x);
                            if (Abnormbox1Position != ik.target)
                            {
                                ik.target = Vector3.MoveTowards(ik.target, Abnormbox1Position, 4.0f * Time.deltaTime);
                            }
                            else
                            {
                                rb.isKinematic = false;
                                grip.grippedObject.tag = "red";
                                grip.grippedObject.transform.SetParent(null);
                                grip.isGrip = false;
                                if (grip.grippedObject.transform.parent == null)
                                {
                                    Debug.Log("grip.grippedObject.transform.SetParent(null)가 실행되었습니다.");
                                    if (step_flag == 0)
                                    {
                                        if (ik.target.y != startposition.position.y)
                                        {
                                            ik.target = Vector3.MoveTowards(ik.target, startposition.position, 4.0f * Time.deltaTime);
                                        }
                                        step_cnt++;
                                        redcube_cnt += 1;
                                        step_flag = 1;
                                        Debug.Log(step_cnt);
                                    }
                                }
                                else
                                {
                                    Debug.Log("grip.grippedObject.transform.SetParent(null)가 실행되지 않았습니다.");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}