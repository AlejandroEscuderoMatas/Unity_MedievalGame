using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class CameraManager : TemporalSingleton<CameraManager>
{
    //Variable que determina quien tiene el control del FOV: 0 cuando el PlayerControl; 1 cuando el arco
    private int   m_FOVcontrol = 0;

    public int getFOVcontrol()
    {
        return m_FOVcontrol;
    }
    public void setFOVcontrol(int control)
    {
        m_FOVcontrol = control;
    }
}
