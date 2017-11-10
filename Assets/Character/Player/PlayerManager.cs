using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class PlayerManager : MonoBehaviour
{

    public GameObject playerToCreate;

    private Player[] playerArray = new Player[6];

    [DllImport("kinectGetBody")]
    private static extern IntPtr getStream();

    [DllImport("kinectGetBody")]
    private static extern void getBodyJoint(IntPtr stream, float[] positionX, float[] positionY, float[] positionZ);

    [DllImport("kinectGetBody")]
    private static extern void releaseStream(IntPtr stream);

    [DllImport("kinectGetBody")]
    private static extern void getIsPlayer(IntPtr stream, int[] exist);

    [DllImport("kinectGetBody")]
    private static extern void upDateStreamData(IntPtr stream);

    [DllImport("kinectGetBody")]
    private static extern void getJointType(IntPtr stream, int[] type);

    private IntPtr stream;



    // Use this for initialization
    void Start()
    {
        this.stream = getStream();
        for (int i = 0; i < 6; i++)
        {
            this.playerArray[i] = Instantiate(playerToCreate).GetComponent<Player>();
        }
    }


    void SetJointPositionAndType(float[] positionX, float[] positionY, float[] positionZ, int[] type)
    {
        for (int i = 0; i < 6; i++)
        {
            this.playerArray[i].SetJointPositionAndType(positionX, positionY, positionZ, type, i);
        }
    }

    void SetIsPlayer(int[] exist)
    {
        for (int i = 0; i < 6; i++)
        {
            this.playerArray[i].IsPlayer = (exist[i] == 0 ? false : true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float[] positionX = new float[25 * 6];
        float[] positionY = new float[25 * 6];
        float[] positionZ = new float[25 * 6];
        int[] exist = new int[6];
        int[] type = new int[25 * 6];
        upDateStreamData(this.stream);
        getBodyJoint(this.stream, positionX, positionY, positionZ);
        getIsPlayer(this.stream, exist);
        getJointType(this.stream, type);
        SetIsPlayer(exist);
        SetJointPositionAndType(positionX, positionY, positionZ, type);
    }

    private void OnApplicationQuit()
    {
        releaseStream(this.stream);
    }
}
