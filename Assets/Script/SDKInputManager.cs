using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SDKInputManager : MonoBehaviour
{

    public float vertical;
    public float horizontal;
    public float brake;

    // ���
    [HideInInspector] public float accelInput;
    [HideInInspector] public float clutchInput;
    [HideInInspector] public float brakeInput;
    // �ڵ�
    [HideInInspector] public float steerInput;
    // ���
    [HideInInspector] public int gearInput;
    // �ڵ��ư
    [HideInInspector] public bool TInput; // �̷� 
    [HideInInspector] public bool OInput;
    [HideInInspector] public bool SInput;
    // �ڵ��ư
    // �޴� ���� : �� �Ʒ� ������ ����
    [HideInInspector] public bool UpInput;
    [HideInInspector] public bool DownInput;
    [HideInInspector] public bool RightInput;
    [HideInInspector] public bool LeftInput;

    //������
    [HideInInspector] public bool RightBumperInput;
    [HideInInspector] public bool LeftBumperInput;


    public InputCondition inputcondition;

    private void Start()
    {
        print("LogiSteeringInitialize: " + LogitechGSDK.LogiSteeringInitialize(false));
    }

    void Update()
    {
        switch (inputcondition)
        {
            case InputCondition.keyboard:
                vertical = Input.GetAxis("Vertical");
                horizontal = Input.GetAxis("Horizontal");
                break;
            case InputCondition.logitech_wheel:
                if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
                {
                    #region
                    accelInput = LogitechInput.GetAxis("Accel_Vertical");
                    clutchInput = LogitechInput.GetAxis("Clutch_Vertical");
                    brakeInput = LogitechInput.GetAxis("Brake_Vertical");
                    steerInput = LogitechInput.GetAxis("Steering_Horizontal");


                    // ����
                    RightBumperInput = LogitechInput.GetKeyPressed(LogitechKeyCode.FirstIndex, LogitechKeyCode.RightBumper);
                    LeftBumperInput = LogitechInput.GetKeyPressed(LogitechKeyCode.FirstIndex, LogitechKeyCode.LeftBumper);


                    if (LeftBumperInput)
                    {
                        Debug.Log("���ʹ���getkeytriggered");
                    }


                    /*
                    if (LeftBumperInput2)
                    {
                        Debug.Log("���ʹ���GetKeyPressed");
                    }

                    if (LeftBumperInput3)
                    {
                        Debug.Log("���ʹ���GetKeyReleased");
                    }
                    */

                    // �ڵ� ��ư
                    TInput = LogitechInput.GetKeyPressed(LogitechKeyCode.FirstIndex, LogitechKeyCode.Triangle);
                    SInput = LogitechInput.GetKeyPressed(LogitechKeyCode.FirstIndex, LogitechKeyCode.Square);
                    OInput = LogitechInput.GetKeyPressed(LogitechKeyCode.FirstIndex, LogitechKeyCode.Circle);
                    UpInput = LogitechInput.GetKeyPressed(LogitechKeyCode.FirstIndex, LogitechKeyCode.Up);
                    DownInput = LogitechInput.GetKeyPressed(LogitechKeyCode.FirstIndex, LogitechKeyCode.Down);
                    RightInput = LogitechInput.GetKeyPressed(LogitechKeyCode.FirstIndex, LogitechKeyCode.Right);
                    LeftInput = LogitechInput.GetKeyPressed(LogitechKeyCode.FirstIndex, LogitechKeyCode.Left);


                    vertical = accelInput;
                    horizontal = steerInput;
                    brake = brakeInput;
                    #endregion


                    // Gear Setting 
                    // �ǿ��� ��� ��� ��� ���� ����
                    if (clutchInput >= 0.7)
                    {
                        Debug.Log($"clutchInput: {clutchInput}");
                        for (int i = 14; i < 16; i++)
                        {
                            if (LogitechInput.GetKeyPressed(LogitechKeyCode.FirstIndex, (LogitechKeyCode)i))
                            {

                                gearInput = i;
                                //Debug.Log($"gearInput: {gearInput}");
                            }
                            else gearInput = 0;
                        }
                    }

                    switch (gearInput)
                    {
                        // ����
                        case 14:
                            gearInput = 0;
                            break;

                        // ����
                        case 15:
                            gearInput = 2;
                            break;

                        // �߸�
                        case 0:
                            gearInput = 1;
                            break;
                    }
                    Debug.Log($"gearInput: {gearInput}");

                }
                break;
        }
    }

}