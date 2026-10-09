private void Build_Test_2_Environment(float Size, int Num,
    int Size_1_L = 7, int Size_1_W = 10, 
    int Size_2_L = 7, int Size_2_W = 7)
{
    float Item_Size;
    if(Size == 0.5f)
    {
        Item_Size = Test_1_Item_Size * Test_2_ROI_Size[Test_2_ROI_Size_Index];
        Test_2_Structure_Builder(Size_1_L, Size_1_W, Item_Size);
    }
    else
    {
        Item_Size = Test_1_Item_Size * Test_2_ROI_Size[Test_2_ROI_Size_Index];
        Test_2_Structure_Builder(Size_2_L, Size_2_W, Item_Size);
    }
}

private void Test_2_Structure_Builder(int Length, int Width, float Item_Size)
{
    Vector3 Pos = Origin.position;
    Vector3 Center = new Vector3(Pos.x, Pos.y, Pos.z + Test_1_Item_Distance);
    float scale = MathF.Tan(Item_Size / 2f * Mathf.Deg2Rad) * Vector3.Distance(Center, Origin.position) * 2f;
    float Gap = scale;

    for (int i = 0; i < Length; i++)
    {
        if (i == 0)
        {
            for (int j = 0; j < Width; j++)
            {
                if (j == 0)
                {
                    Test_2_Build_Sphere(Center, scale, "Center", 0);
                }
                else
                {
                    Vector3 Left_Pos = new Vector3(Center.x - (Gap + scale / 2) * j, Center.y, Center.z);
                    Vector3 Right_Pos = new Vector3(Center.x + (Gap + scale / 2) * j, Center.y, Center.z);

                    Test_2_Build_Sphere(Left_Pos, scale, "Left " + i.ToString(), j);
                    Test_2_Build_Sphere(Right_Pos, scale, "Right " + i.ToString(), j);
                }
            }
        }
        else
        {
            Vector3 Top_Pos = new Vector3(Center.x, Center.y + (Gap + scale / 2) * i, Center.z);
            Vector3 Buttom_Pos = new Vector3(Center.x, Center.y - (Gap + scale / 2) * i, Center.z);

            for (int j = 0; j < Width; j++)
            {
                if (j == 0)
                {
                    Test_2_Build_Sphere(Top_Pos, scale, "Top", i);
                    Test_2_Build_Sphere(Buttom_Pos, scale, "Button", i);
                }
                else
                {
                    Vector3 Left_Top_Pos = new Vector3(Top_Pos.x - (Gap + scale / 2) * j, Top_Pos.y, Top_Pos.z);
                    Vector3 Left_Buttom_Pos = new Vector3(Buttom_Pos.x - (Gap + scale / 2) * j, Buttom_Pos.y, Buttom_Pos.z);

                    Test_2_Build_Sphere(Left_Top_Pos, scale, "Left_Top " + i.ToString(), j);
                    Test_2_Build_Sphere(Left_Buttom_Pos, scale, "Left_Buttom " + i.ToString(), j);

                    Vector3 Right_Top_Pos = new Vector3(Top_Pos.x + (Gap + scale / 2) * j, Top_Pos.y, Top_Pos.z);
                    Vector3 Right_Buttom_Pos = new Vector3(Buttom_Pos.x + (Gap + scale / 2) * j, Buttom_Pos.y, Buttom_Pos.z);

                    Test_2_Build_Sphere(Right_Top_Pos, scale, "Right_Top " + i.ToString(), j);
                    Test_2_Build_Sphere(Right_Buttom_Pos, scale, "Right_Buttom " + i.ToString(), j);

                }
            }
        }
    }
}

private void Test_2_Small_Random_Builder()
{
    Vector3 Pos = Origin.position;
    Vector3 Center = new Vector3(Pos.x, Pos.y, Pos.z + Test_1_Item_Distance);
    float Item_Size = Test_1_Item_Size * Test_2_ROI_Size[Test_2_ROI_Size_Index];
    float scale = MathF.Tan(Item_Size * 0.5f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;
    float Gap = scale;
    float Vertices = scale * 1.5f;

    Vector2[] List_Of_Center2d = new Vector2[6];
    List_Of_Center2d[0] = new Vector2(Center.x - 6f * Vertices, Center.y + 3f * Vertices);
    List_Of_Center2d[1] = new Vector2(Center.x, Center.y + 3f * Vertices);
    List_Of_Center2d[2] = new Vector2(Center.x + 6f * Vertices, Center.y + 3f * Vertices);
    List_Of_Center2d[3] = new Vector2(Center.x - 6f * Vertices, Center.y - 3f * Vertices);
    List_Of_Center2d[4] = new Vector2(Center.x, Center.y - 3f * Vertices);
    List_Of_Center2d[5] = new Vector2(Center.x + 6f * Vertices, Center.y - 3f * Vertices);

    for (int i = 0; i < List_Of_Center2d.Length; i++)
    {
        int Cur = Test_Random_Order[i];
        if (Cur == 0)
        {
            Test_2_Build_Small_Square(List_Of_Center2d[i], Item_Size);
            Test_Environment += i.ToString() + ". Small Square ";
        }
        else if (Cur == 1)
        {
            Test_2_Build_Small_Circle(List_Of_Center2d[i], Item_Size);
            Test_Environment += i.ToString() + ". Small Circle ";
        }
        else if (Cur == 2)
        {
            Test_2_Build_Small_Triangle(List_Of_Center2d[i], Item_Size);
            Test_Environment += i.ToString() + ". Small Triangle ";
        }
        else if (Cur == 3)
        {
            Test_2_Build_Small_Rhombus(List_Of_Center2d[i], Item_Size);
            Test_Environment += i.ToString() + ". Small Rhombus ";
        }
        else if (Cur == 4)
        {
            Test_2_Build_Small_Hexagon(List_Of_Center2d[i], Item_Size);
            Test_Environment += i.ToString() + ". Small Hexagon ";
        }
        else if (Cur == 5)
        {
            Test_2_Build_Small_HollowArch(List_Of_Center2d[i], Item_Size);
            Test_Environment += i.ToString() + ". Small HollowArch ";
        }
        else if (Cur == 6)
        {
            Test_2_Build_Small_Crosshair(List_Of_Center2d[i], Item_Size);
            Test_Environment += i.ToString() + ". Small Crosshair ";
        }
        else if (Cur == 7)
        {
            Test_2_Build_Small_Moon(List_Of_Center2d[i], Item_Size);
            Test_Environment += i.ToString() + ". Small Moon ";
        }
        else if (Cur == 8)
        {
            Test_2_Build_Small_V(List_Of_Center2d[i], Item_Size);
            Test_Environment += i.ToString() + ". Small V ";
        }
        else if (Cur == 9)
        {
            Test_2_Build_Small_Hourglass(List_Of_Center2d[i], Item_Size);
            Test_Environment += i.ToString() + ". Small Hourglass ";
        }
        else if (Cur == 10)
        {
            Test_2_Build_Small_Arrow(List_Of_Center2d[i], Item_Size);
            Test_Environment += i.ToString() + ". Small Arrow ";
        }
        else
        {
            Test_2_Build_Small_Cup(List_Of_Center2d[i], Item_Size);
            Test_Environment += i.ToString() + ". Normal Cup ";
        }
    }

    Test_ROI_Correct_Precent += "Total Correct Target Nums: " + Test_2_Targets_Recorder.Count + ".\r\n";
}

private void Test_2_Build_Small_Square(Vector2 Center2d, float Item_Size, float Length = 2f)
{
    float scale = MathF.Tan(Item_Size * 0.5f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;
    float Gap = scale;
    float Vertices = scale * 1.5f;

    for (int i = Test_2_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_2_Not_Targets_Recorder[i].transform.position.x, Test_2_Not_Targets_Recorder[i].transform.position.y);
        float dx = Mathf.Abs(Cur.x - Center2d.x);
        float dy = Mathf.Abs(Cur.y - Center2d.y);

        if ((dx <= Length * Vertices + 1e-5f) && (dy <= Length * Vertices + 1e-5f))
        {
            Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
            Test_2_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    for (int i = 0; i < Test_2_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_2_Targets_Recorder[i]);
    }
}

private void Test_2_Build_Small_Circle(Vector2 Center2d, float Item_Size, float Circle_Radius = 9f)
{
    float scale = MathF.Tan(Item_Size * 0.5f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;
    float Gap = scale;
    float Vertices = scale * 1.5f;
    float Radius = MathF.Tan(Circle_Radius * 0.5f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;

    for (int i = Test_2_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_2_Not_Targets_Recorder[i].transform.position.x, Test_2_Not_Targets_Recorder[i].transform.position.y);
        float Distance = Vector2.Distance(Cur, Center2d);

        if (Distance <= Radius)
        {
            Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
            Test_2_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    for (int i = 0; i < Test_2_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_2_Targets_Recorder[i]);
    }
}

private void Test_2_Build_Small_Triangle(Vector2 Center2d, float Item_Size, float Top_Length = 1f, float Button_Length = 1f, float LR_Length = 2f)
{
    float scale = MathF.Tan(Item_Size * 0.5f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;
    float Gap = scale;
    float Vertices = scale * 1.5f;

    Vector2 Top_Vertice = new Vector2(Center2d.x, Center2d.y + Top_Length * Vertices);
    Vector2 Left_Vertice = new Vector2(Center2d.x - LR_Length * Vertices, Center2d.y - Button_Length * Vertices);
    Vector2 Right_Vertice = new Vector2(Center2d.x + LR_Length * Vertices, Left_Vertice.y);

    for (int i = Test_2_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_2_Not_Targets_Recorder[i].transform.position.x, Test_2_Not_Targets_Recorder[i].transform.position.y);
        float c1 = Test_1_Cross(Top_Vertice - Left_Vertice, Cur - Left_Vertice);
        float c2 = Test_1_Cross(Right_Vertice - Top_Vertice, Cur - Top_Vertice);
        float c3 = Test_1_Cross(Left_Vertice - Right_Vertice, Cur - Right_Vertice);

        bool hasNeg = (c1 < 0) || (c2 < 0) || (c3 < 0);
        bool hasPos = (c1 > 0) || (c2 > 0) || (c3 > 0);

        if (hasNeg && hasPos)
        {
            float d1 = Test_1_Triangle_DistPointToSegment2D(Cur, Left_Vertice, Top_Vertice);
            float d2 = Test_1_Triangle_DistPointToSegment2D(Cur, Top_Vertice, Right_Vertice);
            float d3 = Test_1_Triangle_DistPointToSegment2D(Cur, Right_Vertice, Left_Vertice);

            float d = Mathf.Min(d1, Mathf.Min(d2, d3));

            if (d <= scale / 2 + 1e-5f)
            {
                Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
                Test_2_Not_Targets_Recorder.RemoveAt(i);
            }
            else
            {
                continue;
            }
        }
        else
        {
            Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
            Test_2_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    for (int i = 0; i < Test_2_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_2_Targets_Recorder[i]);
    }
}

private void Test_2_Build_Small_Rhombus(Vector2 Center2d, float Item_Size, float Length = 1.5f)
{
    float scale = MathF.Tan(Item_Size * 0.5f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;
    float Gap = scale;
    float Vertices = scale * 1.5f;

    float thetaRad = 45f * Mathf.Deg2Rad;

    for (int i = Test_2_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_2_Not_Targets_Recorder[i].transform.position.x, Test_2_Not_Targets_Recorder[i].transform.position.y);
        float c = Mathf.Cos(thetaRad);
        float s = Mathf.Sin(thetaRad);
        Vector2 d = Cur - Center2d;
        float du = c * d.x + s * d.y;
        float dv = -s * d.x + c * d.y;
        float limit = Vertices * Length + 1e-5f;

        if (Mathf.Abs(du) <= limit && Mathf.Abs(dv) <= limit)
        {
            Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
            Test_2_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    for (int i = 0; i < Test_2_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_2_Targets_Recorder[i]);
    }
}

private void Test_2_Build_Small_Hexagon(Vector2 Center2d, float Item_Size, float Hex_Angle_Deg = 19f)
{
    float scale = MathF.Tan(Item_Size * 0.5f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;
    float Gap = scale;
    float Vertices = scale * 1.5f;

    Vector2[] hex_Verts = new Vector2[6];
    float widthWorld = Mathf.Tan(Hex_Angle_Deg * 0.5f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;
    float R = widthWorld * 0.5f;

    for (int i = 0; i < 6; i++)
    {
        float angleDeg = i * 60f;
        float angleRad = angleDeg * Mathf.Deg2Rad;

        float x = Mathf.Cos(angleRad) * R;
        float y = Mathf.Sin(angleRad) * R;

        hex_Verts[i] = Center2d + new Vector2(x, y);
    }

    for (int i = Test_2_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_2_Not_Targets_Recorder[i].transform.position.x, Test_2_Not_Targets_Recorder[i].transform.position.y);

        if (IsPointInPolygon(Cur, hex_Verts))
        {
            Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
            Test_2_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    for (int i = 0; i < Test_2_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_2_Targets_Recorder[i]);
    }
}

private void Test_2_Build_Small_HollowArch(Vector2 Center2d, float Item_Size, float Top_Half_Circle_Radius = 5f, float Total_Area = 2f)
{
    float scale = MathF.Tan(Item_Size * 0.5f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;
    float Gap = scale;
    float Vertices = scale * 1.5f;

    float Radius = MathF.Tan(Top_Half_Circle_Radius / 2f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;
    Vector2 Circle_Center2D = new Vector2(Center2d.x, Center2d.y + Vertices);
    Vector2 Base_Center2D = new Vector2(Center2d.x, Center2d.y - Vertices);

    for (int i = Test_2_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_2_Not_Targets_Recorder[i].transform.position.x, Test_2_Not_Targets_Recorder[i].transform.position.y);
        float dx = Mathf.Abs(Cur.x - Center2d.x);
        float dy = Mathf.Abs(Cur.y - Center2d.y);

        if ((dx <= Vertices * Total_Area + 1e-5f) && (dy <= Vertices * Total_Area + 1e-5f))
        {
            if (Cur.y >= Circle_Center2D.y)
            {
                float distance = Vector2.Distance(Cur, Center2d);
                if (distance >= Radius)
                {
                    Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
                    Test_2_Not_Targets_Recorder.RemoveAt(i);
                }
            }
            else
            {
                float dx_2 = Mathf.Abs(Cur.x - Base_Center2D.x);
                float dy_2 = Mathf.Abs(Cur.y - Base_Center2D.y);

                if ((dx_2 >= Vertices * 0.25f + 1e-5f) || (dy_2 >= Vertices + 1e-5f))
                {
                    Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
                    Test_2_Not_Targets_Recorder.RemoveAt(i);
                }
            }
        }
    }

    for (int i = 0; i < Test_2_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_2_Targets_Recorder[i]);
    }
}

private void Test_2_Build_Small_Crosshair(Vector2 Center2d, float Item_Size, float Length = 1f, float Around_Lenght = 2f)
{
    float scale = MathF.Tan(Item_Size * 0.5f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;
    float Gap = scale;
    float Vertices = scale * 1.5f;

    for (int i = Test_2_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_2_Not_Targets_Recorder[i].transform.position.x, Test_2_Not_Targets_Recorder[i].transform.position.y);
        
        float dx = Mathf.Abs(Cur.x - Center2d.x);
        float dy = Mathf.Abs(Cur.y - Center2d.y);

        if ((dx <= Length * Vertices + 1e-5f) && (dy <= Length * Vertices + 1e-5f))
        {
            Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
            Test_2_Not_Targets_Recorder.RemoveAt(i);
        }
        else
        {
            if(Mathf.Abs(Cur.x - Center2d.x) <= 0.1f || Mathf.Abs(Cur.y - Center2d.y) <= 0.1f)
            {
                float Distance = Vector2.Distance(Cur, Center2d);

                if(Distance <= Vertices * 2f + 1e-5f)
                {
                    Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
                    Test_2_Not_Targets_Recorder.RemoveAt(i);
                }
            }
        }

    }

    for (int i = 0; i < Test_2_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_2_Targets_Recorder[i]);
    }
}

private void Test_2_Build_Small_Moon(Vector2 Center2d, float Item_Size, float Outside_Circle_Radius = 9f, float Inside_Circle_Radius = 4f)
{
    float scale = MathF.Tan(Item_Size * 0.5f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;
    float Gap = scale;
    float Vertices = scale * 1.5f;
    float ORadius = MathF.Tan(Outside_Circle_Radius * 0.5f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;
    Vector2 Inside_Center2d = new Vector2(Center2d.x + Vertices, Center2d.y);
    float IRadius = MathF.Tan(Inside_Circle_Radius * 0.5f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;
    
    for (int i = Test_2_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_2_Not_Targets_Recorder[i].transform.position.x, Test_2_Not_Targets_Recorder[i].transform.position.y);
        float Distance = Vector2.Distance(Cur, Center2d);

        if (Distance <= ORadius)
        {
            float Distance_2 = Vector2.Distance(Cur, Inside_Center2d);

            if(Distance_2 > IRadius)
            {
                Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
                Test_2_Not_Targets_Recorder.RemoveAt(i);
            }
        }
    }

    for (int i = 0; i < Test_2_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_2_Targets_Recorder[i]);
    }
}

private void Test_2_Build_Small_V(Vector2 Center2d, float Item_Size, 
    float Outside_Buttom_Ver = -2f, float Outside_Top_LR_Ver = 2f, float Outside_Top_Length = 2f,
    float Inside_Button_Ver = 0f, float Inside_Top_LR_Ver = 1f, float Inside_Top_Length = 2f)
{
    float scale = MathF.Tan(Item_Size * 0.5f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;
    float Gap = scale;
    float Vertices = scale * 1.5f;

    Vector2 Top_Vertice = new Vector2(Center2d.x, Center2d.y + Outside_Buttom_Ver * Vertices);
    Vector2 Left_Vertice = new Vector2(Center2d.x - Outside_Top_LR_Ver * Vertices, Center2d.y + Outside_Top_Length * Vertices);
    Vector2 Right_Vertice = new Vector2(Center2d.x + Outside_Top_LR_Ver * Vertices, Left_Vertice.y);

    for (int i = Test_2_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_2_Not_Targets_Recorder[i].transform.position.x, Test_2_Not_Targets_Recorder[i].transform.position.y);
        float c1 = Test_1_Cross(Top_Vertice - Left_Vertice, Cur - Left_Vertice);
        float c2 = Test_1_Cross(Right_Vertice - Top_Vertice, Cur - Top_Vertice);
        float c3 = Test_1_Cross(Left_Vertice - Right_Vertice, Cur - Right_Vertice);

        bool hasNeg = (c1 < 0) || (c2 < 0) || (c3 < 0);
        bool hasPos = (c1 > 0) || (c2 > 0) || (c3 > 0);

        if (hasNeg && hasPos)
        {
            float d1 = Test_1_Triangle_DistPointToSegment2D(Cur, Left_Vertice, Top_Vertice);
            float d2 = Test_1_Triangle_DistPointToSegment2D(Cur, Top_Vertice, Right_Vertice);
            float d3 = Test_1_Triangle_DistPointToSegment2D(Cur, Right_Vertice, Left_Vertice);

            float d = Mathf.Min(d1, Mathf.Min(d2, d3));

            if (d <= scale / 2 + 1e-5f)
            {
                Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
                Test_2_Not_Targets_Recorder.RemoveAt(i);
            }
            else
            {
                continue;
            }
        }
        else
        {
            Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
            Test_2_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    Vector2 Inside_Top_Vertice = new Vector2(Center2d.x, Center2d.y + Inside_Button_Ver * Vertices);
    Vector2 Inside_Left_Vertice = new Vector2(Center2d.x - Inside_Top_LR_Ver * Vertices, Center2d.y + Inside_Top_Length * Vertices);
    Vector2 Inside_Right_Vertice = new Vector2(Center2d.x + Inside_Top_LR_Ver * Vertices, Inside_Left_Vertice.y);

    for(int i =  Test_2_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_2_Targets_Recorder[i].transform.position.x, Test_2_Targets_Recorder[i].transform.position.y);
        float c1 = Test_1_Cross(Inside_Top_Vertice - Inside_Left_Vertice, Cur - Inside_Left_Vertice);
        float c2 = Test_1_Cross(Inside_Right_Vertice - Inside_Top_Vertice, Cur - Inside_Top_Vertice);
        float c3 = Test_1_Cross(Inside_Left_Vertice - Inside_Right_Vertice, Cur - Inside_Right_Vertice);

        bool hasNeg = (c1 < 0) || (c2 < 0) || (c3 < 0);
        bool hasPos = (c1 > 0) || (c2 > 0) || (c3 > 0);

        if (hasNeg && hasPos)
        {
            float d1 = Test_1_Triangle_DistPointToSegment2D(Cur, Inside_Left_Vertice, Inside_Top_Vertice);
            float d2 = Test_1_Triangle_DistPointToSegment2D(Cur, Inside_Top_Vertice, Inside_Right_Vertice);
            float d3 = Test_1_Triangle_DistPointToSegment2D(Cur, Inside_Right_Vertice, Inside_Left_Vertice);

            float d = Mathf.Min(d1, Mathf.Min(d2, d3));

            if (d <= scale / 2 + 1e-5f)
            {
                Test_2_Not_Targets_Recorder.Add(Test_2_Targets_Recorder[i]);
                Test_2_Targets_Recorder.RemoveAt(i);
            }
            else
            {
                continue;
            }
        }
        else
        {
            Test_2_Not_Targets_Recorder.Add(Test_2_Targets_Recorder[i]);
            Test_2_Targets_Recorder.RemoveAt(i);
        }
    }

    for (int i = 0; i < Test_2_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_2_Targets_Recorder[i]);
    }
}

private void Test_2_Build_Small_Hourglass(Vector2 Center2d, float Item_Size, 
    float Height_Angle_Deg = 20f, float Width_Angle_Deg = 20f, float neckRatio = 0.3f)
{
    float scale = MathF.Tan(Item_Size * 0.5f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;
    float Gap = scale;
    float Vertices = scale * 1.5f;

    // 高度、宽度对应的 world 尺寸（和你一贯做法一致）
    float heightWorld = Mathf.Tan(Height_Angle_Deg * 0.5f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;
    float widthWorld = Mathf.Tan(Width_Angle_Deg * 0.5f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;

    float halfH = heightWorld * 0.5f;
    float halfW = widthWorld * 0.5f;

    float neckHalfW = halfW * Mathf.Clamp01(neckRatio); // 腰部一半宽度

    // 构造凹多边形顶点（顺时针）
    // 顶部从左到右 → 右腰 → 底右 → 底左 → 左腰
    Vector2 TL = Center2d + new Vector2(-halfW, halfH);
    Vector2 TR = Center2d + new Vector2(halfW, halfH);
    Vector2 MR = Center2d + new Vector2(neckHalfW, 0f);
    Vector2 BR = Center2d + new Vector2(halfW, -halfH);
    Vector2 BL = Center2d + new Vector2(-halfW, -halfH);
    Vector2 ML = Center2d + new Vector2(-neckHalfW, 0f);

    Vector2[] hourglassVerts = new Vector2[]
    {
    TL, TR, MR, BR, BL, ML
    };

    // 挑选在沙漏内部的小球
    for (int i = Test_2_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_2_Not_Targets_Recorder[i].transform.position.x, Test_2_Not_Targets_Recorder[i].transform.position.y);
        if(IsPointInPolygon(Cur, hourglassVerts))
        {
            Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
            Test_2_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    for (int i = 0; i < Test_2_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_2_Targets_Recorder[i]);
    }
}

private void Test_2_Build_Small_Arrow(Vector2 Center2d, float Item_Size, 
    float Top_Length = 2f, float Top_LR_Length = 2f,
    float Button_Center = -1f, float Button_Length = 1f)
{
    float scale = MathF.Tan(Item_Size * 0.5f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;
    float Gap = scale;
    float Vertices = scale * 1.5f;

    Vector2 T_Top_Vertice = new Vector2(Center2d.x, Center2d.y + Top_Length * Vertices);
    Vector2 T_Left_Vertice = new Vector2(Center2d.x - Top_LR_Length * Vertices, Center2d.y);
    Vector2 T_Right_Vertice = new Vector2(Center2d.x + Top_LR_Length * Vertices, T_Left_Vertice.y);

    for (int i = Test_2_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_2_Not_Targets_Recorder[i].transform.position.x, Test_2_Not_Targets_Recorder[i].transform.position.y);
        float c1 = Test_1_Cross(T_Top_Vertice - T_Left_Vertice, Cur - T_Left_Vertice);
        float c2 = Test_1_Cross(T_Right_Vertice - T_Top_Vertice, Cur - T_Top_Vertice);
        float c3 = Test_1_Cross(T_Left_Vertice - T_Right_Vertice, Cur - T_Right_Vertice);

        bool hasNeg = (c1 < 0) || (c2 < 0) || (c3 < 0);
        bool hasPos = (c1 > 0) || (c2 > 0) || (c3 > 0);

        if (hasNeg && hasPos)
        {
            float d1 = Test_1_Triangle_DistPointToSegment2D(Cur, T_Left_Vertice, T_Top_Vertice);
            float d2 = Test_1_Triangle_DistPointToSegment2D(Cur, T_Top_Vertice, T_Right_Vertice);
            float d3 = Test_1_Triangle_DistPointToSegment2D(Cur, T_Right_Vertice, T_Left_Vertice);

            float d = Mathf.Min(d1, Mathf.Min(d2, d3));

            if (d <= scale / 2 + 1e-5f)
            {
                Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
                Test_2_Not_Targets_Recorder.RemoveAt(i);
            }
            else
            {
                continue;
            }
        }
        else
        {
            Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
            Test_2_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    Vector2 Button_Center_2d = new Vector2(Center2d.x, Center2d.y + Button_Center * Vertices);

    for (int i = Test_2_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_2_Not_Targets_Recorder[i].transform.position.x, Test_2_Not_Targets_Recorder[i].transform.position.y);
        float dx = Mathf.Abs(Cur.x - Button_Center_2d.x);
        float dy = Mathf.Abs(Cur.y - Button_Center_2d.y);

        if ((dx <= Vertices * Button_Length + 1e-5f) && (dy <= Vertices * Button_Length + 1e-5f))
        {
            Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
            Test_2_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    for (int i = 0; i < Test_2_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_2_Targets_Recorder[i]);
    }

}

private void Test_2_Build_Small_Cup(Vector2 Center2d, float Item_Size, 
    float Top_Length = 1f, float Mid_Length = 2f, float Button_Length = -2f)
{
    float scale = MathF.Tan(Item_Size * 0.5f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;
    float Gap = scale;
    float Vertices = scale * 1.5f;

    Vector2 Top_Center = new Vector2(Center2d.x, Center2d.y + Top_Length * Vertices);
    for (int i = Test_2_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_2_Not_Targets_Recorder[i].transform.position.x, Test_2_Not_Targets_Recorder[i].transform.position.y);
        float dx = Mathf.Abs(Cur.x - Top_Center.x);
        float dy = Mathf.Abs(Cur.y - Top_Center.y);

        if ((dx <= Vertices + 1e-5f) && (dy <= Vertices + 1e-5f))
        {
            Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
            Test_2_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    Vector2 Mid_Center = Center2d;
    for (int i = Test_2_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_2_Not_Targets_Recorder[i].transform.position.x, Test_2_Not_Targets_Recorder[i].transform.position.y);
        float dx = Mathf.Abs(Cur.x - Mid_Center.x);
        float dy = Mathf.Abs(Cur.y - Mid_Center.y);

        if (dy <= Mid_Length * Vertices + 1e-5f && dx <= 1e-5f)
        {
            Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
            Test_2_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    Vector2 Button_Center = new Vector2(Center2d.x, Center2d.y + Button_Length * Vertices);
    for (int i = Test_2_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_2_Not_Targets_Recorder[i].transform.position.x, Test_2_Not_Targets_Recorder[i].transform.position.y);
        float dx = Mathf.Abs(Cur.x - Button_Center.x);
        float dy = Mathf.Abs(Cur.y - Button_Center.y);

        if (dx <= Vertices + 1e-5f && dy <= 1e-5f)
        {
            Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
            Test_2_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    for (int i = 0; i < Test_2_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_2_Targets_Recorder[i]);
    }
}

private List<int> Test_2_GetRandomModel(int Limitation)
{
    List<int> nums = new List<int>();
    for (int i = 0; i < Limitation; i++)
        nums.Add(i);
    for(int i = 0; i < nums.Count; i++)
    {
        int r = UnityEngine.Random.Range(i, nums.Count);
        int temp = nums[i];
        nums[i] = nums[r];
        nums[r] = temp;
    }

    return nums;
}

private void Test_2_Large_Random_Builder(int Cur)
{
    Vector3 Pos = Origin.position;
    Vector3 Center = new Vector3(Pos.x, Pos.y, Pos.z + Test_1_Item_Distance);
    Vector2 Center_2D = new Vector2(Center.x, Center.y);
    float Item_Size = Test_1_Item_Size * Test_2_ROI_Size[Test_2_ROI_Size_Index];

    if (Cur == 1)
    {
        Test_2_Large_Square(Center_2D, Item_Size);
        Test_Environment += Cur.ToString() + ". Large Square ";
    }
    else if (Cur == 2)
    {
        Test_2_Large_Hexagon(Center_2D, Item_Size);
        Test_Environment += Cur.ToString() + ". Large Hexagon ";
    }
    else if (Cur == 3)
    {
        Test_2_Large_HollowArch(Center_2D, Item_Size);
        Test_Environment += Cur.ToString() + ". Large HollowArch ";
    }
    else if (Cur == 4)
    {
        Test_2_Large_Moon(Center_2D, Item_Size);
        Test_Environment += Cur.ToString() + ". Large Moon ";
    }
    else if (Cur == 5)
    {
        Test_2_Large_Tree(Center_2D, Item_Size);
        Test_Environment += Cur.ToString() + ". Large Tree ";
    }
    else
    {
        Test_2_Large_IceCream(Center_2D, Item_Size);
        Test_Environment += Cur.ToString() + ". Large IceCream ";
    }

    Test_ROI_Correct_Precent += "Total Correct Target Nums: " + Test_2_Targets_Recorder.Count + ".\r\n";
}

private void Test_2_Large_Square(Vector2 Center2d, float Item_Size, float Length = 5f)
{
    float scale = MathF.Tan(Item_Size * 0.5f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;
    float Gap = scale;
    float Vertices = scale * 1.5f;

    for (int i = Test_2_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_2_Not_Targets_Recorder[i].transform.position.x, Test_2_Not_Targets_Recorder[i].transform.position.y);
        float dx = Mathf.Abs(Cur.x - Center2d.x);
        float dy = Mathf.Abs(Cur.y - Center2d.y);

        if ((dx <= Length * Vertices + 1e-5f) && (dy <= Length * Vertices + 1e-5f))
        {
            Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
            Test_2_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    for (int i = 0; i < Test_2_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_2_Targets_Recorder[i]);
    }
}

private void Test_2_Large_Hexagon(Vector2 Center2d, float Item_Size, float Hex_Angle_Deg = 85f)
{
    float scale = MathF.Tan(Item_Size * 0.5f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;
    float Gap = scale;
    float Vertices = scale * 1.5f;

    Vector2[] hex_Verts = new Vector2[6];
    float widthWorld = Mathf.Tan(Hex_Angle_Deg * 0.5f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;
    float R = widthWorld * 0.5f;

    for (int i = 0; i < 6; i++)
    {
        float angleDeg = i * 60f;
        float angleRad = angleDeg * Mathf.Deg2Rad;

        float x = Mathf.Cos(angleRad) * R;
        float y = Mathf.Sin(angleRad) * R;

        hex_Verts[i] = Center2d + new Vector2(x, y);
    }

    for (int i = Test_2_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_2_Not_Targets_Recorder[i].transform.position.x, Test_2_Not_Targets_Recorder[i].transform.position.y);

        if (IsPointInPolygon(Cur, hex_Verts))
        {
            Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
            Test_2_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    for (int i = 0; i < Test_2_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_2_Targets_Recorder[i]);
    }
}

private void Test_2_Large_HollowArch(Vector2 Center2d, float Item_Size,
    float Top_Half_Circle_Radius = 30f, float Total_Area = 5f)
{
    float scale = MathF.Tan(Item_Size * 0.5f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;
    float Gap = scale;
    float Vertices = scale * 1.5f;

    float Radius = MathF.Tan(Top_Half_Circle_Radius / 2f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;
    Vector2 Circle_Center2D = new Vector2(Center2d.x, Center2d.y + Vertices);
    Vector2 Base_Center2D = new Vector2(Center2d.x, Center2d.y - 2f * Vertices);

    for (int i = Test_2_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_2_Not_Targets_Recorder[i].transform.position.x, Test_2_Not_Targets_Recorder[i].transform.position.y);
        float dx = Mathf.Abs(Cur.x - Center2d.x);
        float dy = Mathf.Abs(Cur.y - Center2d.y);

        if ((dx <= Vertices * Total_Area + 1e-5f) && (dy <= Vertices * Total_Area + 1e-5f))
        {
            if (Cur.y >= Circle_Center2D.y)
            {
                float distance = Vector2.Distance(Cur, Center2d);
                if (distance >= Radius)
                {
                    Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
                    Test_2_Not_Targets_Recorder.RemoveAt(i);
                }
            }
            else
            {
                float dx_2 = Mathf.Abs(Cur.x - Base_Center2D.x);
                float dy_2 = Mathf.Abs(Cur.y - Base_Center2D.y);

                if ((dx_2 >= Vertices * 3f + 1e-5f) || (dy_2 >= 3f * Vertices + 1e-5f))
                {
                    Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
                    Test_2_Not_Targets_Recorder.RemoveAt(i);
                }
            }
        }
    }

    for (int i = 0; i < Test_2_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_2_Targets_Recorder[i]);
    }
}

private void Test_2_Large_Moon(Vector2 Center2d, float Item_Size,
    float Outside_Circle_Radius = 50f, float Inside_Circle_Radius = 30f)
{
    float scale = MathF.Tan(Item_Size * 0.5f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;
    float Gap = scale;
    float Vertices = scale * 1.5f;

    float ORadius = MathF.Tan(Outside_Circle_Radius * 0.5f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;
    Vector2 Inside_Center2d = new Vector2(Center2d.x + 3f * Vertices, Center2d.y);
    float IRadius = MathF.Tan(Inside_Circle_Radius * 0.5f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;

    for (int i = Test_2_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_2_Not_Targets_Recorder[i].transform.position.x, Test_2_Not_Targets_Recorder[i].transform.position.y);
        float Distance = Vector2.Distance(Cur, Center2d);

        if (Distance <= ORadius)
        {
            float Distance_2 = Vector2.Distance(Cur, Inside_Center2d);

            if (Distance_2 > IRadius)
            {
                Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
                Test_2_Not_Targets_Recorder.RemoveAt(i);
            }
        }
    }

    for (int i = 0; i < Test_2_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_2_Targets_Recorder[i]);
    }
}

private void Test_2_Large_Tree(Vector2 Center2d, float Item_Size, 
    float Top_Center2d = 4f, float Top_Vert = 1f, float Top_LR_Vert = 3f, float Top_Button = -3f,
    float Mid_Center2d = 0f, float Mid_Vert = 3f, float Mid_LR_Vert = 5f, float Mid_Button = -2f,
    float Button_Center2d = -4f, float Button_Length = 1f)
{
    float scale = MathF.Tan(Item_Size * 0.5f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;
    float Gap = scale;
    float Vertices = scale * 1.5f;

    Vector2 T_Top_Vertice = new Vector2(Center2d.x, Center2d.y + (Top_Center2d + Top_Vert) * Vertices);
    Vector2 T_Left_Vertice = new Vector2(Center2d.x - Top_LR_Vert * Vertices, T_Top_Vertice.y + Top_Button * Vertices);
    Vector2 T_Right_Vertice = new Vector2(Center2d.x + Top_LR_Vert * Vertices, T_Left_Vertice.y);

    for (int i = Test_2_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_2_Not_Targets_Recorder[i].transform.position.x, Test_2_Not_Targets_Recorder[i].transform.position.y);
        float c1 = Test_1_Cross(T_Top_Vertice - T_Left_Vertice, Cur - T_Left_Vertice);
        float c2 = Test_1_Cross(T_Right_Vertice - T_Top_Vertice, Cur - T_Top_Vertice);
        float c3 = Test_1_Cross(T_Left_Vertice - T_Right_Vertice, Cur - T_Right_Vertice);

        bool hasNeg = (c1 < 0) || (c2 < 0) || (c3 < 0);
        bool hasPos = (c1 > 0) || (c2 > 0) || (c3 > 0);

        if (hasNeg && hasPos)
        {
            float d1 = Test_1_Triangle_DistPointToSegment2D(Cur, T_Left_Vertice, T_Top_Vertice);
            float d2 = Test_1_Triangle_DistPointToSegment2D(Cur, T_Top_Vertice, T_Right_Vertice);
            float d3 = Test_1_Triangle_DistPointToSegment2D(Cur, T_Right_Vertice, T_Left_Vertice);

            float d = Mathf.Min(d1, Mathf.Min(d2, d3));

            if (d <= scale / 2 + 1e-5f)
            {
                Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
                Test_2_Not_Targets_Recorder.RemoveAt(i);
            }
            else
            {
                continue;
            }
        }
        else
        {
            Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
            Test_2_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    Vector2 M_Top_Vertice = new Vector2(Center2d.x, Center2d.y + Mid_Vert * Vertices);
    Vector2 M_Left_Vertice = new Vector2(Center2d.x - Mid_LR_Vert * Vertices, Center2d.y + Mid_Button * Vertices);
    Vector2 M_Right_Vertice = new Vector2(Center2d.x + Mid_LR_Vert * Vertices, M_Left_Vertice.y);

    for (int i = Test_2_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_2_Not_Targets_Recorder[i].transform.position.x, Test_2_Not_Targets_Recorder[i].transform.position.y);
        float c1 = Test_1_Cross(M_Top_Vertice - M_Left_Vertice, Cur - M_Left_Vertice);
        float c2 = Test_1_Cross(M_Right_Vertice - M_Top_Vertice, Cur - M_Top_Vertice);
        float c3 = Test_1_Cross(M_Left_Vertice - M_Right_Vertice, Cur - M_Right_Vertice);

        bool hasNeg = (c1 < 0) || (c2 < 0) || (c3 < 0);
        bool hasPos = (c1 > 0) || (c2 > 0) || (c3 > 0);

        if (hasNeg && hasPos)
        {
            float d1 = Test_1_Triangle_DistPointToSegment2D(Cur, M_Left_Vertice, M_Top_Vertice);
            float d2 = Test_1_Triangle_DistPointToSegment2D(Cur, M_Top_Vertice, M_Right_Vertice);
            float d3 = Test_1_Triangle_DistPointToSegment2D(Cur, M_Right_Vertice, M_Left_Vertice);

            float d = Mathf.Min(d1, Mathf.Min(d2, d3));

            if (d <= scale / 2 + 1e-5f)
            {
                Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
                Test_2_Not_Targets_Recorder.RemoveAt(i);
            }
            else
            {
                continue;
            }
        }
        else
        {
            Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
            Test_2_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    Vector2 Button_Center_2d = new Vector2(Center2d.x, Center2d.y + Button_Center2d * Vertices);

    for (int i = Test_2_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_2_Not_Targets_Recorder[i].transform.position.x, Test_2_Not_Targets_Recorder[i].transform.position.y);
        float dx = Mathf.Abs(Cur.x - Button_Center_2d.x);
        float dy = Mathf.Abs(Cur.y - Button_Center_2d.y);

        if ((dx <= Vertices * Button_Length + 1e-5f) && (dy <= Vertices * Button_Length + 1e-5f))
        {
            Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
            Test_2_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    for (int i = 0; i < Test_2_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_2_Targets_Recorder[i]);
    }
}

private void Test_2_Large_IceCream(Vector2 Center2d, float Item_Size, 
    float Top_Circle_Center2d = 3f, float Top_Circle_Deg = 25f,
    float Mid_Center2d = 0f, float Mid_Vert = -3f, float Mid_LR_Vert = 3f, float Mid_Button = -2f,
    float Button_Center2d = -4f, float Button_Length = 1f)
{
    float scale = MathF.Tan(Item_Size * 0.5f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;
    float Gap = scale;
    float Vertices = scale * 1.5f;

    Vector2 Top_Center2d = new Vector2(Center2d.x, Center2d.y + Top_Circle_Center2d * Vertices);
    float Radius = MathF.Tan(Top_Circle_Deg * 0.5f * Mathf.Deg2Rad) * Test_1_Item_Distance * 2f;

    for (int i = Test_2_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_2_Not_Targets_Recorder[i].transform.position.x, Test_2_Not_Targets_Recorder[i].transform.position.y);
        float Distance = Vector2.Distance(Cur, Top_Center2d);

        if (Distance <= Radius)
        {
            Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
            Test_2_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    Vector2 M_Top_Vertice = new Vector2(Center2d.x, Center2d.y + Mid_Vert * Vertices);
    Vector2 M_Left_Vertice = new Vector2(Center2d.x - Mid_LR_Vert * Vertices, Center2d.y);
    Vector2 M_Right_Vertice = new Vector2(Center2d.x + Mid_LR_Vert * Vertices, M_Left_Vertice.y);

    for (int i = Test_2_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_2_Not_Targets_Recorder[i].transform.position.x, Test_2_Not_Targets_Recorder[i].transform.position.y);
        float c1 = Test_1_Cross(M_Top_Vertice - M_Left_Vertice, Cur - M_Left_Vertice);
        float c2 = Test_1_Cross(M_Right_Vertice - M_Top_Vertice, Cur - M_Top_Vertice);
        float c3 = Test_1_Cross(M_Left_Vertice - M_Right_Vertice, Cur - M_Right_Vertice);

        bool hasNeg = (c1 < 0) || (c2 < 0) || (c3 < 0);
        bool hasPos = (c1 > 0) || (c2 > 0) || (c3 > 0);

        if (hasNeg && hasPos)
        {
            float d1 = Test_1_Triangle_DistPointToSegment2D(Cur, M_Left_Vertice, M_Top_Vertice);
            float d2 = Test_1_Triangle_DistPointToSegment2D(Cur, M_Top_Vertice, M_Right_Vertice);
            float d3 = Test_1_Triangle_DistPointToSegment2D(Cur, M_Right_Vertice, M_Left_Vertice);

            float d = Mathf.Min(d1, Mathf.Min(d2, d3));

            if (d <= scale / 2 + 1e-5f)
            {
                Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
                Test_2_Not_Targets_Recorder.RemoveAt(i);
            }
            else
            {
                continue;
            }
        }
        else
        {
            Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
            Test_2_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    Vector2 Button_Center_2d = new Vector2(Center2d.x, Center2d.y + Button_Center2d * Vertices);

    for (int i = Test_2_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_2_Not_Targets_Recorder[i].transform.position.x, Test_2_Not_Targets_Recorder[i].transform.position.y);
        float dx = Mathf.Abs(Cur.x - Button_Center_2d.x);
        float dy = Mathf.Abs(Cur.y - Button_Center_2d.y);

        if ((dx <= Vertices * Button_Length + 1e-5f) && (dy <= Vertices * Button_Length + 1e-5f))
        {
            Test_2_Targets_Recorder.Add(Test_2_Not_Targets_Recorder[i]);
            Test_2_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    for (int i = 0; i < Test_2_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_2_Targets_Recorder[i]);
    }
}

private void Test_2_Build_Sphere(Vector3 Pos, float scale, string Name, int Num)
{
    GameObject Sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    Sphere.transform.position = Pos;
    Sphere.transform.localScale = new Vector3(scale, scale, scale);
    Sphere.name = "Sphere" + Name + Num.ToString();
    Sphere.tag = "Dwell";
    Sphere.AddComponent<EyeTrackingArea>();
    Sphere.AddComponent<Angle_Distance>();
    Test_2_Not_Targets_Recorder.Add(Sphere);
}
