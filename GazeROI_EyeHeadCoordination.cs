void Start()
{
    combineEyeGazeOriginOffset = Vector3.zero;
    combineEyeGazeVector = Vector3.zero;
    combineEyeGazeOrigin = Vector3.zero;
    originPoseMatrix = Origin.localToWorldMatrix;
    trackingState = (TrackingStateCode)PXR_MotionTracking.WantEyeTrackingService();
    // Query if the current device supports eye tracking
    EyeTrackingMode[] eyeTrackingMode = new EyeTrackingMode[3];
    Array.Fill<EyeTrackingMode>(eyeTrackingMode, EyeTrackingMode.PXR_ETM_NONE);
    int supportedModesCount = 0;
    trackingState = (TrackingStateCode)PXR_MotionTracking.GetEyeTrackingSupported(ref supported, ref supportedModesCount, ref eyeTrackingMode);
}

// Update is called once per frame
void Update()
{
    if (supported)
    {
        PXR_EyeTracking.GetHeadPosMatrix(out headPoseMatrix);

        bool isSuccess = PXR_EyeTracking.GetCombineEyeGazeVector(out combineEyeGazeVector);
        if (!isSuccess) return;

        PXR_EyeTracking.GetCombineEyeGazePoint(out combineEyeGazeOrigin);
        //Translate Eye Gaze point and vector to world space
        combineEyeGazeOrigin += combineEyeGazeOriginOffset;
        combineEyeGazeOriginInWorldSpace = originPoseMatrix.MultiplyPoint(headPoseMatrix.MultiplyPoint(combineEyeGazeOrigin));
        combineEyeGazeVectorInWorldSpace = originPoseMatrix.MultiplyVector(headPoseMatrix.MultiplyVector(combineEyeGazeVector));

        if (SpotLight != null)
        {
            SpotLight.transform.position = combineEyeGazeOriginInWorldSpace;
            SpotLight.transform.rotation = Quaternion.LookRotation(combineEyeGazeVectorInWorldSpace, Vector3.up);
        }

        Gaze_ROI_Method_Control();
        //GazeTargetControl(combineEyeGazeOriginInWorldSpace, combineEyeGazeVectorInWorldSpace);
    }
}

private bool Calculate_Gaze_Head_Distance(out float Angle)
{
    Angle = 0f;
    Vector3 G = combineEyeGazeVectorInWorldSpace.normalized;
    Vector3 H = (Head_Point.transform.position - combineEyeGazeOriginInWorldSpace).normalized;
    float Angle3D = Vector3.Angle(G, H);
    Angle = Angle3D;
    if(Angle3D < Check_Angle)
    {
        return true;
    }
    else
    {
        return false;
    }
}

private void Init_Head_Point(float Size = 2f)
{
    float Gap;
    float Dis = -1f;

    if (Enviroment_Build_Test_1)
    {
        Gap = Test_1_Not_Targets_Recorder[0].transform.localScale.x / 2 + 0.03f;
        Dis = Head_Point_Pos_Calculator(Test_1_Not_Targets_Recorder, Test_1_Targets_Recorder);
    }
    else
    {
        Gap = Test_2_Not_Targets_Recorder[0].transform.localScale.x / 2 + 0.03f;
        Dis = Head_Point_Pos_Calculator(Test_2_Not_Targets_Recorder, Test_2_Targets_Recorder);
    }

    if (Dis <= 0f)
    {
        Head_Point.transform.position = Camera.main.transform.position + Camera.main.transform.forward * Test_1_Item_Distance;
        float scale = Mathf.Tan(Size * 0.5f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;
        Head_Point.transform.localScale = new Vector3(scale, scale, scale * 0.1f);
    }
    else
    {
        Head_Point.transform.position = Camera.main.transform.position + Camera.main.transform.forward * (Dis - Gap);
        float scale = Mathf.Tan(Size * 0.5f * Mathf.Deg2Rad) * (Dis - Gap) * 2f;
        Head_Point.transform.localScale = new Vector3(scale, scale, scale * 0.1f);
    }

    Vector3 dir = Camera.main.transform.position - Head_Point.transform.position;
    Head_Point.transform.rotation = Quaternion.LookRotation(dir);
}

private float Head_Point_Pos_Calculator(List<GameObject> canidates_1, List<GameObject> canidates_2)
{
    float dis = -1f;
    float Best_Ang = Mathf.Infinity;

    foreach (GameObject temp in canidates_1)
    {
        Vector3 to = temp.transform.position - Camera.main.transform.position;
        float depth = Vector3.Dot(to, Camera.main.transform.forward);
        if (depth <= 0f) continue;

        float Cur = temp.GetComponent<Angle_Distance>().V2_return_Angle(Camera.main.transform.position, Camera.main.transform.forward);
        if (Cur <= Check_Head_Depth_Angle)
        {
            if (Cur < Best_Ang)
            {
                dis = depth;
                Best_Ang = Cur;
            }
        }
    }

    foreach (GameObject temp in canidates_2)
    {
        Vector3 to = temp.transform.position - Camera.main.transform.position;
        float depth = Vector3.Dot(to, Camera.main.transform.forward);
        if (depth <= 0f) continue;

        float Cur = temp.GetComponent<Angle_Distance>().V2_return_Angle(Camera.main.transform.position, Camera.main.transform.forward);
        if (Cur <= Check_Head_Depth_Angle)
        {
            if (Cur < Best_Ang)
            {
                dis = depth;
                Best_Ang = Cur;
            }
        }
    }

    return dis;
}


private void Display_Head_Control_Menu(bool CanRemove, bool CanSecondOrder)
{
    Active_Head_Control_Menu = true;
    current_Focus = Head_Button_Focus.None;
    Head_Button_Keep_Time = System.DateTime.Now;

    Vector3 Hpos = Head_Point.transform.position;
    Vector3 Epos = combineEyeGazeOriginInWorldSpace;
    Vector3 DirToTar = (Hpos - Epos).normalized;
    float Dis = Vector3.Distance(Epos, Hpos);

    Vector3 upOnPlane = Vector3.ProjectOnPlane(Camera.main.transform.up, DirToTar).normalized;
    Vector3 rightOnPlane = Vector3.ProjectOnPlane(Camera.main.transform.right, DirToTar).normalized;

    Vector3 diagLeftUp = (upOnPlane - rightOnPlane).normalized;
    Vector3 diagRightUp = (upOnPlane + rightOnPlane).normalized;
    Vector3 diaRightDown = (-upOnPlane + rightOnPlane).normalized;
    Vector3 diaLeftDown = (-upOnPlane - rightOnPlane).normalized;

    float offset = Mathf.Tan(Head_Button_Display_Angle * Mathf.Deg2Rad) * Dis;

    Vector3 HCBpos = Hpos + diagRightUp * offset;
    Vector3 HFBpos = Hpos + diaLeftDown * offset;

    if(HCBpos.z >= Test_1_Item_Distance - 0.5f)
    {
        HCBpos.z = 9.3f;
    }

    if(HFBpos.z >= Test_1_Item_Distance - 0.5f)
    {
        HFBpos.z = 9.3f;
    }

    Current_Head_Virtual_Point = Create_Head_Button(Hpos, 0, "Virtual_Head");
    Head_Confirm_Button = Create_Head_Button(HCBpos, 1, "Confirm_Head");
    Head_Reset_Confirm_Button_LineRender();

    if (Stroke_Points_Recorder.Count >= 3)
    {
        Head_Finish_Button = Create_Head_Button(HFBpos, 2, "Finish_Head");
        Head_Reset_Finish_Button_LineRender();
    }

    if (CanRemove)
    {
        Vector3 HRBpos = Hpos + diaRightDown * offset;
        if (HRBpos.z >= Test_1_Item_Distance - 0.5f)
        {
            HRBpos.z = 9.3f;
        }
        Head_Remove_Button = Create_Head_Button(HRBpos, 3, "Remove_Head");
        Head_Reset_Remove_Button_LineRender();
    }

    if (CanSecondOrder)
    {
        Vector3 HSOBpos = Hpos + diagLeftUp * offset;
        if (HSOBpos.z >= Test_1_Item_Distance - 0.5f)
        {
            HSOBpos.z = 9.3f;
        }
        Head_Second_Order_Button = Create_Head_Button(HSOBpos, 4, "Second_Order_Head");
        Head_Reset_Second_Order_Button_LineRender();
    }
}

private GameObject Create_Head_Button(Vector3 pos, int Color_Index, string name)
{
    GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Cube);
    temp.name = name;
    temp.transform.position = pos;
    temp.transform.localScale = Head_Point.transform.localScale;
    temp.AddComponent<EyeTrackingArea>();
    temp.AddComponent<Angle_Distance>();
    if(name != "Virtual_Head")
        temp.AddComponent<LineRenderer>();
    temp.GetComponent<Renderer>().material = Color_List[Color_Index];
    Vector3 dir = combineEyeGazeOriginInWorldSpace - temp.transform.position;
    temp.transform.rotation = Quaternion.LookRotation(dir);
    return temp;
}

private void Head_Reset_Confirm_Button_LineRender()
{
    LineRenderer line = Head_Confirm_Button.GetComponent<LineRenderer>();
    line.positionCount = 2;
    line.SetPosition(0, Head_Confirm_Button.transform.position);
    line.SetPosition(1, Current_Head_Virtual_Point.transform.position);
    line.startWidth = 0.005f;
    line.endWidth = 0.005f;
    line.material = LineMaterial;
}

private void Head_Reset_Finish_Button_LineRender()
{
    LineRenderer line = Head_Finish_Button.GetComponent<LineRenderer>();
    line.positionCount = 2;
    line.SetPosition(0, Head_Finish_Button.transform.position);
    line.SetPosition(1, Current_Head_Virtual_Point.transform.position);
    line.startWidth = 0.005f;
    line.endWidth = 0.005f;
    line.material = LineMaterial;
}

private void Head_Reset_Remove_Button_LineRender()
{
    LineRenderer line = Head_Remove_Button.GetComponent<LineRenderer>();
    line.positionCount = 2;
    line.SetPosition(0, Head_Remove_Button.transform.position);
    line.SetPosition(1, Current_Head_Virtual_Point.transform.position);
    line.startWidth = 0.005f;
    line.endWidth = 0.005f;
    line.material = LineMaterial;
}

private void Head_Reset_Second_Order_Button_LineRender()
{
    LineRenderer line = Head_Second_Order_Button.GetComponent<LineRenderer>();
    line.positionCount = 2;
    line.SetPosition(0, Head_Second_Order_Button.transform.position);
    line.SetPosition(1, Current_Head_Virtual_Point.transform.position);
    line.startWidth = 0.005f;
    line.endWidth = 0.005f;
    line.material = LineMaterial;
}
