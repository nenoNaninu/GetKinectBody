using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum JointType
{
    JointType_SpineBase = 0,
    JointType_SpineMid = 1,
    JointType_Neck = 2,
    JointType_Head = 3,
    JointType_ShoulderLeft = 4,
    JointType_ElbowLeft = 5,
    JointType_WristLeft = 6,
    JointType_HandLeft = 7,
    JointType_ShoulderRight = 8,
    JointType_ElbowRight = 9,
    JointType_WristRight = 10,
    JointType_HandRight = 11,
    JointType_HipLeft = 12,
    JointType_KneeLeft = 13,
    JointType_AnkleLeft = 14,
    JointType_FootLeft = 15,
    JointType_HipRight = 16,
    JointType_KneeRight = 17,
    JointType_AnkleRight = 18,
    JointType_FootRight = 19,
    JointType_SpineShoulder = 20,
    JointType_HandTipLeft = 21,
    JointType_ThumbLeft = 22,
    JointType_HandTipRight = 23,
    JointType_ThumbRight = 24,
    JointType_Count = (JointType_ThumbRight + 1)
};

public class Player : MonoBehaviour
{
    public bool IsPlayer { get; set; }
    private bool prevIsPlayer = false;

    public GameObject jointToCreate;
    //public GameObject impactToCreate;

    private int[] jointType = new int[25];
    private Joint[] jointArray = new Joint[25];

    private int leftHandIndex;
    private int rightHandIndex;
    private int spineIndex;
    private int headIndex;
    private int leftFootIndex;
    private int rightFootIndex;

    private float prepareArmShakeTime;
    private bool prepareArmShakeFlag = false;

    private Vector3 prepareArmShakePosition;

    private float impactSpan = 3.0f;
    private float impactShootTime = 0;

    public void SetJointPositionAndType(float[] positionX, float[] positionY, float[] positionZ, int[] argType, int index)
    {
        for (int i = 0; i < 25; i++)
        {
            Vector3 vec = new Vector3(positionX[i + index * 25], positionY[i + index * 25], positionZ[i + index * 25]);
            this.jointArray[i].transform.position = vec;
            int type = this.jointArray[i].Type = argType[i + index * 25];
            switch (type)
            {
                case (int)JointType.JointType_HandTipRight:
                    this.rightHandIndex = i;
                    break;
                case (int)JointType.JointType_HandTipLeft:
                    this.leftHandIndex = i;
                    break;
                case (int)JointType.JointType_Head:
                    this.headIndex = i;
                    break;
                case (int)JointType.JointType_SpineMid:
                    this.spineIndex = i;
                    break;
                case (int)JointType.JointType_FootLeft:
                    this.leftFootIndex = i;
                    break;
                case (int)JointType.JointType_FootRight:
                    this.rightFootIndex = i;
                    break;
                default:
                    break;
            }

        }
    }

    // Use this for initialization
    void Start()
    {
        this.IsPlayer = false;
        for (int i = 0; i < 25; i++)
        {
            this.jointArray[i] = Instantiate(this.jointToCreate).GetComponent<Joint>() ;
            this.jointArray[i].Type = -1;
            this.jointArray[i].transform.position = Vector3.zero;
            this.jointArray[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.IsPlayer)
        {
            if (!this.prevIsPlayer)
            {
                for (int i = 0; i < 25; i++)
                {
                    this.jointArray[i].gameObject.SetActive(true);
                }
            }

            float disSpineToRight = Vector3.Distance(jointArray[this.spineIndex].gameObject.transform.position, jointArray[rightHandIndex].gameObject.transform.position);
            Debug.Log("disSpineToRight" + disSpineToRight);
            if (disSpineToRight > 0.70 && Time.time > this.impactSpan + this.impactShootTime)
            {
                if (!this.prepareArmShakeFlag)
                {
                    this.prepareArmShakePosition = this.jointArray[this.rightHandIndex].gameObject.transform.position;
                }
                this.prepareArmShakeFlag = true;
                this.prepareArmShakeTime = Time.time;
            }

            if (this.prepareArmShakeFlag)
            {
                if (Time.time > this.prepareArmShakeTime + 0.5f)
                {
                    this.prepareArmShakeFlag = false;
                }
                else
                {
                    float disPrepareRightToCurrentRight = Vector3.Distance(this.prepareArmShakePosition, this.jointArray[rightHandIndex].gameObject.transform.position);
                    float disSpineToRightHand = Vector3.Distance(jointArray[this.rightHandIndex].gameObject.transform.position, jointArray[spineIndex].gameObject.transform.position);
                    Debug.Log("disPrepareRightToCurrentRight" + disPrepareRightToCurrentRight);
                    if (disPrepareRightToCurrentRight > 0.7f && disSpineToRightHand > 0.4f)
                    {
                        //ここに入ったら引火
                        this.prepareArmShakeFlag = false;
                        this.impactShootTime = Time.time;
                        //GameObject impact = Instantiate(this.impactToCreate, this.jointArray[rightHandIndex].gameObject.transform.position, Quaternion.identity);
                        //Destroy(impact, 1);
                    }
                }
            }
        }
        else
        {
            if (this.prevIsPlayer)
            {
                for (int i = 0; i < 25; i++)
                {
                    this.jointArray[i].gameObject.SetActive(false);
                }
            }
        }
        this.prevIsPlayer = this.IsPlayer;
    }
}
