using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint : MonoBehaviour
{

    public int Type { get; set; }
    private int prevType = -1;

    private MeshRenderer meshRender;
    public Material headMaterial;
    public Material handMaterial;
    public Material spineMaterial;
    public Material othersMaterial;

    // Use this for initialization
    void Start()
    {
        this.meshRender = gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (prevType != this.Type)
        {
            if (this.Type == (int)JointType.JointType_HandTipLeft || this.Type == (int)JointType.JointType_HandTipRight)
            {
                this.meshRender.material = this.handMaterial;
            }
            else if (this.Type == (int)JointType.JointType_Head)
            {
                this.meshRender.material = this.headMaterial;
            }
            else if (this.Type == (int)JointType.JointType_SpineMid)
            {
                this.meshRender.material = this.spineMaterial;
            }
        }
        this.prevType = this.Type;
    }
}
