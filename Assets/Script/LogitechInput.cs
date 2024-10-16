using System;

public class LogitechInput
{
    static LogitechGSDK.DIJOYSTATE2ENGINES rec;
    #region
    public static float GetAxis(string axisName)
    {
        rec = LogitechGSDK.LogiGetStateUnity(0);
        switch (axisName)
        {
            // x-axis position: �ڵ� 
            // ���������� �ڵ� �ٵ����� 32767f, �������� �ڵ� �� ������ -32767f
            case "Steering_Horizontal": return rec.lX / 32767f;
            // y-axis position: �ǿ�����'
            // ����Ʈ 32767f, �� ������ -32767f
            case "Accel_Vertical": return rec.lY / (-32767f);
            // extra axes positions
            case "Clutch_Vertical": return rec.rglSlider[0] / -32767f;
            // z-axis rotation: ��� 
            // ����Ʈ 32767f
            case "Brake_Vertical": return rec.lRz / -32767f;
        }
        return 0f;
    }
    #endregion

    public static bool GetKeyTriggered(LogitechKeyCode gamecontroller, LogitechKeyCode keyCode)
    {
        if (LogitechGSDK.LogiButtonTriggered((int)gamecontroller, (int)keyCode))
        {
            return true;
        }
        return false;
    }

    public static bool GetKeyPressed(LogitechKeyCode gamecontroller, LogitechKeyCode keyCode)
    {
        if (LogitechGSDK.LogiButtonIsPressed((int)gamecontroller, (int)keyCode))
        {
            return true;
        }
        return false;
    }
    public static bool GetKeyReleased(LogitechKeyCode gamecontroller, LogitechKeyCode keyCode)
    {
        if (LogitechGSDK.LogiButtonReleased((int)gamecontroller, (int)keyCode))
        {
            return true;
        }
        return false;
    }

    public static uint GetKeyDirectional()
    {
        return rec.rgdwPOV[0];
    }
}