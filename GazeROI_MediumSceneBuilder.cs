private void Test_1_Structure_Builder(int Length, int Width)
{
    Vector3 Pos = Origin.position;
    Vector3 Center = new Vector3(Pos.x, Pos.y, Pos.z + Test_1_Item_Distance);
    float scale = MathF.Tan(Test_1_Item_Size / 2f * Mathf.Deg2Rad) * Vector3.Distance(Center, Origin.position) * 2f;
    float Gap = scale;

    for (int i = 0; i < Length; i++)
    {
        if (i == 0)
        {
            for (int j = 0; j < Width; j++)
            {
                if (j == 0)
                {
                    Test_1_Build_Sphere(Center, scale, "Center", 0);
                }
                else
                {
                    Vector3 Left_Pos = new Vector3(Center.x - (Gap + scale / 2) * j, Center.y, Center.z);
                    Vector3 Right_Pos = new Vector3(Center.x + (Gap + scale / 2) * j, Center.y, Center.z);

                    Test_1_Build_Sphere(Left_Pos, scale, "Left " + i.ToString(), j);
                    Test_1_Build_Sphere(Right_Pos, scale, "Right " + i.ToString(), j);
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
                    Test_1_Build_Sphere(Top_Pos, scale, "Top", i);
                    Test_1_Build_Sphere(Buttom_Pos, scale, "Button", i);
                }
                else
                {
                    Vector3 Left_Top_Pos = new Vector3(Top_Pos.x - (Gap + scale / 2) * j, Top_Pos.y, Top_Pos.z);
                    Vector3 Left_Buttom_Pos = new Vector3(Buttom_Pos.x - (Gap + scale / 2) * j, Buttom_Pos.y, Buttom_Pos.z);

                    Test_1_Build_Sphere(Left_Top_Pos, scale, "Left_Top " + i.ToString(), j);
                    Test_1_Build_Sphere(Left_Buttom_Pos, scale, "Left_Buttom " + i.ToString(), j);

                    Vector3 Right_Top_Pos = new Vector3(Top_Pos.x + (Gap + scale / 2) * j, Top_Pos.y, Top_Pos.z);
                    Vector3 Right_Buttom_Pos = new Vector3(Buttom_Pos.x + (Gap + scale / 2) * j, Buttom_Pos.y, Buttom_Pos.z);

                    Test_1_Build_Sphere(Right_Top_Pos, scale, "Right_Top " + i.ToString(), j);
                    Test_1_Build_Sphere(Right_Buttom_Pos, scale, "Right_Buttom " + i.ToString(), j);

                }
            }
        }
    }
}

private void Test_1_Basic_Model_Circle(float Circle_Radius = 33f)
{
    Vector3 Pos = Origin.position;
    Vector3 Center = new Vector3(Pos.x, Pos.y, Pos.z + Test_1_Item_Distance);

    Vector2 Center2D = new Vector2(Center.x, Center.y);
    float Radius = MathF.Tan(Circle_Radius / 2f * Mathf.Deg2Rad) * Vector3.Distance(Center, Origin.position) * 2f;

    for (int i = Test_1_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_1_Not_Targets_Recorder[i].transform.position.x, Test_1_Not_Targets_Recorder[i].transform.position.y);
        float Distance_1 = Vector2.Distance(Cur, Center2D);

        if (Distance_1 <= Radius)
        {
            Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
            Test_1_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    for (int i = 0; i < Test_1_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_1_Targets_Recorder[i]);
    }
}

private void Test_1_Basic_Model_Hexagon(float Hex_Angle_Deg = 64f)
{
    Vector3 Pos = Origin.position;
    Vector3 Center = new Vector3(Pos.x, Pos.y, Pos.z + Test_1_Item_Distance);
    float scale = MathF.Tan(Test_1_Item_Size / 2f * Mathf.Deg2Rad) * Vector3.Distance(Center, Origin.position) * 2f;
    float Gap = scale;
    float Vertices = Gap + scale / 2;

    Vector2 Center2D = new Vector2(Center.x, Center.y);

    Vector2[] hex_Verts = new Vector2[6];
    float widthWorld = Mathf.Tan(Hex_Angle_Deg * 0.5f * Mathf.Deg2Rad) * Vector3.Distance(Center, Origin.position) * 2f;
    float R = widthWorld * 0.5f;

    for (int i = 0; i < 6; i++)
    {
        float angleDeg = i * 60f;
        float angleRad = angleDeg * Mathf.Deg2Rad;

        float x = Mathf.Cos(angleRad) * R;
        float y = Mathf.Sin(angleRad) * R;

        hex_Verts[i] = Center2D + new Vector2(x, y);
    }

    for (int i = Test_1_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_1_Not_Targets_Recorder[i].transform.position.x, Test_1_Not_Targets_Recorder[i].transform.position.y);

        if (IsPointInPolygon(Cur, hex_Verts))
        {
            Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
            Test_1_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    for (int i = 0; i < Test_1_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_1_Targets_Recorder[i]);
    }
}

private bool IsPointInPolygon(Vector2 p, Vector2[] poly)
{
    bool inside = false;
    int n = poly.Length;

    for (int i = 0, j = n - 1; i < n; j = i++)
    {
        Vector2 pi = poly[i];
        Vector2 pj = poly[j];

        // 判断 (pi, pj) 这条边与从 p 水平向右射线是否相交
        bool intersect =
            ((pi.y > p.y) != (pj.y > p.y)) &&
            (p.x < (pj.x - pi.x) * (p.y - pi.y) / (pj.y - pi.y + 1e-8f) + pi.x);

        if (intersect)
            inside = !inside;
    }

    return inside;
}

private void Test_1_Basic_Model_Hourglass(float Height_Angle_Deg = 65f, float Width_Angle_Deg = 45f, float neckRatio = 0.3f)
{
    Vector3 pos = Origin.position;
    Vector3 center3D = new Vector3(pos.x, pos.y, pos.z + Test_1_Item_Distance);
    float distance = Vector3.Distance(center3D, Origin.position);

    Vector2 center2D = new Vector2(center3D.x, center3D.y);

    // 高度、宽度对应的 world 尺寸（和你一贯做法一致）
    float heightWorld = Mathf.Tan(Height_Angle_Deg * 0.5f * Mathf.Deg2Rad) * distance * 2f;
    float widthWorld = Mathf.Tan(Width_Angle_Deg * 0.5f * Mathf.Deg2Rad) * distance * 2f;

    float halfH = heightWorld * 0.5f;
    float halfW = widthWorld * 0.5f;

    float neckHalfW = halfW * Mathf.Clamp01(neckRatio); // 腰部一半宽度

    // 构造凹多边形顶点（顺时针）
    // 顶部从左到右 → 右腰 → 底右 → 底左 → 左腰
    Vector2 TL = center2D + new Vector2(-halfW, halfH);
    Vector2 TR = center2D + new Vector2(halfW, halfH);
    Vector2 MR = center2D + new Vector2(neckHalfW, 0f);
    Vector2 BR = center2D + new Vector2(halfW, -halfH);
    Vector2 BL = center2D + new Vector2(-halfW, -halfH);
    Vector2 ML = center2D + new Vector2(-neckHalfW, 0f);

    Vector2[] hourglassVerts = new Vector2[]
    {
    TL, TR, MR, BR, BL, ML
    };

    // 挑选在沙漏内部的小球
    for (int i = Test_1_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector3 p3 = Test_1_Not_Targets_Recorder[i].transform.position;
        Vector2 p2 = new Vector2(p3.x, p3.y);

        if (IsPointInPolygon(p2, hourglassVerts))
        {
            Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
            Test_1_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    for (int i = 0; i < Test_1_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_1_Targets_Recorder[i]);
    }
}

private void Test_1_Basic_Model_V(float Outside_Vertice_Top = 4f, float Outside_Vertice_Buttom = -4f, float Outside_Length = 4f,
    float Inside_Vertice_Top = 4f, float Inside_Vertice_Button = 0f, float Inside_Length = 1f)
{
    Vector3 Pos = Origin.position;
    Vector3 Center = new Vector3(Pos.x, Pos.y, Pos.z + Test_1_Item_Distance);
    float scale = MathF.Tan(Test_1_Item_Size / 2f * Mathf.Deg2Rad) * Vector3.Distance(Center, Origin.position) * 2f;
    float Gap = scale;
    float Vertices = Gap + scale / 2;

    Vector2 First_Top_Vertices = new Vector2(Center.x, Center.y + Outside_Vertice_Buttom * Vertices);
    Vector2 First_Left_Vertices = new Vector2(Center.x - Outside_Length * Vertices, Center.y +  Outside_Vertice_Top * Vertices);
    Vector2 First_Right_Vertices = new Vector2(Center.x + Outside_Length * Vertices, Center.y + Outside_Vertice_Top * Vertices);

    for (int i = Test_1_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_1_Not_Targets_Recorder[i].transform.position.x, Test_1_Not_Targets_Recorder[i].transform.position.y);
        float c1 = Test_1_Cross(First_Top_Vertices - First_Left_Vertices, Cur - First_Left_Vertices);
        float c2 = Test_1_Cross(First_Right_Vertices - First_Top_Vertices, Cur - First_Top_Vertices);
        float c3 = Test_1_Cross(First_Left_Vertices - First_Right_Vertices, Cur - First_Right_Vertices);

        bool hasNeg = (c1 < 0) || (c2 < 0) || (c3 < 0);
        bool hasPos = (c1 > 0) || (c2 > 0) || (c3 > 0);

        if (hasNeg && hasPos)
        {
            float d1 = Test_1_Triangle_DistPointToSegment2D(Cur, First_Left_Vertices, First_Top_Vertices);
            float d2 = Test_1_Triangle_DistPointToSegment2D(Cur, First_Top_Vertices, First_Right_Vertices);
            float d3 = Test_1_Triangle_DistPointToSegment2D(Cur, First_Right_Vertices, First_Left_Vertices);

            float d = Mathf.Min(d1, Mathf.Min(d2, d3));

            if (d <= scale / 2 + 1e-5f)
            {
                Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
                Test_1_Not_Targets_Recorder.RemoveAt(i);
            }
            else
            {
                continue;
            }
        }
        else
        {
            Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
            Test_1_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    Vector2 Second_Top_Vertices = new Vector2(Center.x, Center.y + Inside_Vertice_Button * Vertices);
    Vector2 Second_Left_Vertices = new Vector2(Center.x - Inside_Length * Vertices, Center.y + Inside_Vertice_Top * Vertices);
    Vector2 Second_Right_Vertices = new Vector2(Center.x + Inside_Length * Vertices, Center.y + Inside_Vertice_Top * Vertices);

    for (int i = Test_1_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_1_Targets_Recorder[i].transform.position.x, Test_1_Targets_Recorder[i].transform.position.y);
        float c1 = Test_1_Cross(Second_Top_Vertices - Second_Left_Vertices, Cur - Second_Left_Vertices);
        float c2 = Test_1_Cross(Second_Right_Vertices - Second_Top_Vertices, Cur - Second_Top_Vertices);
        float c3 = Test_1_Cross(Second_Left_Vertices - Second_Right_Vertices, Cur - Second_Right_Vertices);

        bool hasNeg = (c1 < 0) || (c2 < 0) || (c3 < 0);
        bool hasPos = (c1 > 0) || (c2 > 0) || (c3 > 0);

        if (hasNeg && hasPos)
        {
            float d1 = Test_1_Triangle_DistPointToSegment2D(Cur, Second_Left_Vertices, Second_Top_Vertices);
            float d2 = Test_1_Triangle_DistPointToSegment2D(Cur, Second_Top_Vertices, Second_Right_Vertices);
            float d3 = Test_1_Triangle_DistPointToSegment2D(Cur, Second_Right_Vertices, Second_Left_Vertices);

            float d = Mathf.Min(d1, Mathf.Min(d2, d3));

            if (d <= scale / 2 + 1e-5f)
            {
                Test_1_Not_Targets_Recorder.Add(Test_1_Targets_Recorder[i]);
                Test_1_Targets_Recorder.RemoveAt(i);
            }
            else
            {
                continue;
            }
        }
        else
        {
            Test_1_Not_Targets_Recorder.Add(Test_1_Targets_Recorder[i]);
            Test_1_Targets_Recorder.RemoveAt(i);
        }
    }

    for (int i = 0; i < Test_1_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_1_Targets_Recorder[i]);
    }
}

private void Test_1_Basic_Model_Moon(float Outside_Circle_Radius = 35f, float Inside_Circle_Radius = 25f)
{
    Vector3 Pos = Origin.position;
    Vector3 Center = new Vector3(Pos.x, Pos.y, Pos.z + Test_1_Item_Distance);
    float scale = MathF.Tan(Test_1_Item_Size / 2f * Mathf.Deg2Rad) * Vector3.Distance(Center, Origin.position) * 2f;
    float Gap = scale;
    float Vertices = Gap + scale * 0.5f;

    Vector2 Center2D = new Vector2(Center.x, Center.y);
    float Radius = MathF.Tan(Outside_Circle_Radius / 2f * Mathf.Deg2Rad) * Vector3.Distance(Center, Origin.position) * 2f;

    Vector2 Inside_Center = new Vector2(Center2D.x + Vertices, Center2D.y);
    float Inside_Radius = MathF.Tan(Inside_Circle_Radius / 2f * Mathf.Deg2Rad) * Vector3.Distance(Center, Origin.position) * 2f;

    for (int i = Test_1_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_1_Not_Targets_Recorder[i].transform.position.x, Test_1_Not_Targets_Recorder[i].transform.position.y);
        float Distance_1 = Vector2.Distance(Cur, Center2D);
        float Distance_2 = Vector2.Distance(Cur, Inside_Center);

        if (Distance_1 <= Radius && Distance_2 > Inside_Radius)
        {
            Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
            Test_1_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    for (int i = 0; i < Test_1_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_1_Targets_Recorder[i]);
    }
}

private void Test_1_Basic_Model_Crosshair(float Circle_Radius = 40f, float Outside_Circle_Radius = 6.6f)
{
    Vector3 Pos = Origin.position;
    Vector3 Center = new Vector3(Pos.x, Pos.y, Pos.z + Test_1_Item_Distance);
    float scale = MathF.Tan(Test_1_Item_Size / 2f * Mathf.Deg2Rad) * Vector3.Distance(Center, Origin.position) * 2f;
    float Gap = scale;
    float Vertices = Gap + scale / 2;
    Vector2 center2D = new Vector2(Center.x, Center.y);
    float Radius = MathF.Tan(Circle_Radius / 2f * Mathf.Deg2Rad) * Vector3.Distance(Center, Origin.position) * 2f;

    Vector2 Left_Top_Center = new Vector2(center2D.x - 5 * Vertices, center2D.y + 5 * Vertices);
    Vector2 Left_Button_Center = new Vector2(center2D.x - 5 * Vertices, center2D.y - 5 * Vertices);
    Vector2 Right_Top_Center = new Vector2(center2D.x + 5 * Vertices, center2D.y + 5 * Vertices);
    Vector2 Right_Button_Center = new Vector2(center2D.x + 5 * Vertices, center2D.y - 5 * Vertices);
    float Outside_Radius = MathF.Tan(Outside_Circle_Radius / 2f * Mathf.Deg2Rad) * Vector3.Distance(Center, Origin.position) * 2f;

    for (int i = Test_1_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_1_Not_Targets_Recorder[i].transform.position.x, Test_1_Not_Targets_Recorder[i].transform.position.y);
        float Distance_1 = Vector2.Distance(Cur, center2D);
        float Distance_LT = Vector2.Distance(Cur, Left_Top_Center);
        float Distance_LB = Vector2.Distance(Cur, Left_Button_Center);
        float Distance_RT = Vector2.Distance(Cur, Right_Top_Center);
        float Distance_RB = Vector2.Distance(Cur, Right_Button_Center);

        if (Distance_1 <= Radius && Distance_LT > Outside_Circle_Radius && Distance_LB > Outside_Circle_Radius
            && Distance_RT > Outside_Circle_Radius && Distance_RB > Outside_Circle_Radius)
        {
            Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
            Test_1_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    for (int i = 0; i < Test_1_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_1_Targets_Recorder[i]);
    }
}

private void Test_1_Basic_Model_Star(float starLevel = 3f)
{
    Vector3 Pos = Origin.position;
    Vector3 Center = new Vector3(Pos.x, Pos.y, Pos.z + Test_1_Item_Distance);
    float scale = MathF.Tan(Test_1_Item_Size / 2f * Mathf.Deg2Rad) * Vector3.Distance(Center, Origin.position) * 2f;
    float Gap = scale;
    float Vertices = Gap + scale / 2;
    Vector2 center2D = new Vector2(Center.x, Center.y);

    float R = starLevel * Vertices;
    var star = MakeStar(center2D, R);

    for (int i = Test_1_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        var go = Test_1_Not_Targets_Recorder[i];
        Vector2 p = new Vector2(go.transform.position.x, go.transform.position.y);

        if (InsideOrTouchStar(p, star, scale * 0.5f))
        {
            Test_1_Targets_Recorder.Add(go);
            Test_1_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    for (int i = 0; i < Test_1_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_1_Targets_Recorder[i]);
    }
}

private List<Vector2> MakeStar(Vector2 Center, float Radius, float ratio = 0.3f, float thetaDeg = 90f)
{
    float r = Radius * ratio;
    float theta0 = thetaDeg * Mathf.Deg2Rad;
    var verts = new List<Vector2>(10);

    for (int k = 0; k < 10; k++)
    {
        float ang = theta0 + k * (Mathf.PI / 5f); // 每次 36° (π/5)
        float rad = (k % 2 == 0) ? Radius : r;         // 外外内外内...
        verts.Add(new Vector2(
            Center.x + rad * Mathf.Cos(ang),
            Center.y + rad * Mathf.Sin(ang)
        ));
    }
    return verts; // 10 点，凹多边形
}

private bool PointInStar(Vector2 p, IList<Vector2> poly)
{
    bool inside = false;
    int n = poly.Count;

    for (int i = 0, j = n - 1; i < n; j = i++)
    {
        Vector2 a = poly[i];
        Vector2 b = poly[j];

        bool intersect =
            ((a.y > p.y) != (b.y > p.y)) &&
            (p.x < (b.x - a.x) * (p.y - a.y) / (b.y - a.y + 1e-12f) + a.x);

        if (intersect) inside = !inside;
    }
    return inside;
}

private bool InsideOrTouchStar(Vector2 p, IList<Vector2> star, float touchRadius)
{
    if (PointInStar(p, star)) return true;

    float minD = float.MaxValue;
    int n = star.Count;
    for (int i = 0; i < n; i++)
    {
        Vector2 A = star[i];
        Vector2 B = star[(i + 1) % n];
        float d = DistPointToSegment2D(p, A, B);
        if (d < minD) minD = d;
        if (minD <= touchRadius + 1e-5f) return true;
    }
    return false;
}

private float DistPointToSegment2D(Vector2 P, Vector2 A, Vector2 B)
{
    Vector2 AB = B - A;
    float len2 = Vector2.Dot(AB, AB);
    if (len2 < 1e-12f) return Vector2.Distance(P, A);
    float t = Mathf.Clamp01(Vector2.Dot(P - A, AB) / len2);
    Vector2 Q = A + t * AB;
    return Vector2.Distance(P, Q);
}

private void Test_1_Basic_Model_Triangle(float Top_Vertices_Level = 2f, float Left_Right_Vertices_Level = 4f)
{
    Vector3 Pos = Origin.position;
    Vector3 Center = new Vector3(Pos.x, Pos.y, Pos.z + Test_1_Item_Distance);
    float scale = MathF.Tan(Test_1_Item_Size / 2f * Mathf.Deg2Rad) * Vector3.Distance(Center, Origin.position) * 2f;
    float Gap = scale;
    float Vertices = Gap + scale / 2;
    float T_Vertices = Top_Vertices_Level * Vertices;
    float LR_Vertices = Left_Right_Vertices_Level * Vertices;

    Vector2 Top_Vertices = new Vector2(Center.x, Center.y + T_Vertices);
    Vector2 Left_Vertices = new Vector2(Center.x - LR_Vertices, Center.y - 2f * Vertices);
    Vector2 Right_Vertices = new Vector2(Center.x + LR_Vertices, Center.y - 2f * Vertices);

    for(int i = Test_1_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_1_Not_Targets_Recorder[i].transform.position.x, Test_1_Not_Targets_Recorder[i].transform.position.y);
        float c1 = Test_1_Cross(Top_Vertices - Left_Vertices, Cur - Left_Vertices);
        float c2 = Test_1_Cross(Right_Vertices - Top_Vertices, Cur - Top_Vertices);
        float c3 = Test_1_Cross(Left_Vertices - Right_Vertices, Cur - Right_Vertices);

        bool hasNeg = (c1 < 0) || (c2 < 0) || (c3 < 0);
        bool hasPos = (c1 > 0) || (c2 > 0) || (c3 > 0);

        if(hasNeg && hasPos)
        {
            float d1 = Test_1_Triangle_DistPointToSegment2D(Cur, Left_Vertices, Top_Vertices);
            float d2 = Test_1_Triangle_DistPointToSegment2D(Cur, Top_Vertices, Right_Vertices);
            float d3 = Test_1_Triangle_DistPointToSegment2D(Cur, Right_Vertices, Left_Vertices); 

            float d = Mathf.Min(d1, Mathf.Min(d2, d3));

            if (d <= scale / 2 + 1e-5f)
            {
                Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
                Test_1_Not_Targets_Recorder.RemoveAt(i);
            }
            else
            {
                continue;
            }
        }
        else
        {
            Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
            Test_1_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    for(int i = 0; i < Test_1_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_1_Targets_Recorder[i]);
    }
}

static float Test_1_Cross(Vector2 u, Vector2 v) => u.x * v.y - u.y * v.x;

private float Test_1_Triangle_DistPointToSegment2D(Vector2 P, Vector2 A, Vector2 B)
{
    Vector2 p = new Vector2(P.x, P.y);
    Vector2 a = new Vector2(A.x, A.y);
    Vector2 b = new Vector2(B.x, B.y);

    Vector2 ab = b - a;
    float len2 = Vector2.Dot(ab, ab);

    if (len2 < 1e-12f) return Vector2.Distance(p, a);

    float t = Mathf.Clamp01(Vector2.Dot(p - a, ab) / len2);
    Vector2 proj = a + t * ab;
    return Vector2.Distance(p, proj);
}


private void Test_1_Basic_Model_Square(float Vertices_Level = 4f)
{
    Vector3 Pos = Origin.position;
    Vector3 Center = new Vector3(Pos.x, Pos.y, Pos.z + Test_1_Item_Distance);
    float scale = MathF.Tan(Test_1_Item_Size / 2f * Mathf.Deg2Rad) * Vector3.Distance(Center, Origin.position) * 2f;
    float Gap = scale;
    float Vertices = Vertices_Level * (Gap + scale / 2);
    Vector2 Center2D = new Vector2(Center.x, Center.y);

    for(int i = Test_1_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_1_Not_Targets_Recorder[i].transform.position.x, Test_1_Not_Targets_Recorder[i].transform.position.y);
        float dx = Mathf.Abs(Cur.x - Center2D.x);
        float dy = Mathf.Abs(Cur.y - Center2D.y);

        if((dx <= Vertices + 1e-5f) && (dy <= Vertices + 1e-5f))
        {
            Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
            Test_1_Not_Targets_Recorder.RemoveAt(i);
        } 
    }

    for (int i = 0; i < Test_1_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_1_Targets_Recorder[i]);
    }
}

private void Test_1_Basic_Model_Rhombus(float Vertices_Level = 3.5f)
{
    Vector3 Pos = Origin.position;
    Vector3 Center = new Vector3(Pos.x, Pos.y, Pos.z + Test_1_Item_Distance);
    float scale = MathF.Tan(Test_1_Item_Size / 2f * Mathf.Deg2Rad) * Vector3.Distance(Center, Origin.position) * 2f;
    float Gap = scale;
    float Vertices = Vertices_Level * (Gap + scale / 2);
    
    float thetaRad = 45f * Mathf.Deg2Rad;
    Vector2 Center2D = new Vector2(Center.x, Center.y);
    Vector2 u = new Vector2(Mathf.Cos(thetaRad), Mathf.Sin(thetaRad)) * Vertices;
    Vector2 v = new Vector2(-Mathf.Sin(thetaRad), Mathf.Cos(thetaRad)) * Vertices;

    for (int i = Test_1_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_1_Not_Targets_Recorder[i].transform.position.x, Test_1_Not_Targets_Recorder[i].transform.position.y);
        float c = Mathf.Cos(thetaRad);
        float s = Mathf.Sin(thetaRad);
        Vector2 d = Cur - Center2D;
        float du = c * d.x + s * d.y;
        float dv = -s * d.x + c * d.y;
        float limit = Vertices + 1e-5f;

        if(Mathf.Abs(du) <= limit && Mathf.Abs(dv) <= limit)
        {
            Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
            Test_1_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    for (int i = 0; i < Test_1_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_1_Targets_Recorder[i]);
    }
}

private void Test_1_Basic_Model_Pentagon(float Vertices_Level = 4.1f)
{
    Vector3 Pos = Origin.position;
    Vector3 Center = new Vector3(Pos.x, Pos.y, Pos.z + Test_1_Item_Distance);
    float scale = MathF.Tan(Test_1_Item_Size / 2f * Mathf.Deg2Rad) * Vector3.Distance(Center, Origin.position) * 2f;
    float Gap = scale;
    float Vertices = Vertices_Level * (Gap + scale / 2);
    float thetaRad = 90f * Mathf.Deg2Rad;

    Vector2[] poly = new Vector2[5];
    for (int k = 0; k < 5; k++)
    {
        float ang = thetaRad + k * (2f * Mathf.PI / 5f); // 72°
        poly[k] = new Vector2(Center.x + Vertices * Mathf.Cos(ang),
                              Center.y + Vertices * Mathf.Sin(ang));
    }

    const float eps = 1e-6f;
    for (int i = Test_1_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_1_Not_Targets_Recorder[i].transform.position.x, Test_1_Not_Targets_Recorder[i].transform.position.y);

        bool hasNeg = false, hasPos = false;

        // 半平面符号
        for (int j = 0; j < 5; j++)
        {
            Vector2 A = poly[j];
            Vector2 B = poly[(j + 1) % 5];
            Vector2 AB = new Vector2(B.x - A.x, B.y - A.y);
            Vector2 AP = new Vector2(Cur.x - A.x, Cur.y - A.y);
            float c = AB.x * AP.y - AB.y * AP.x; // Cross
            if (c < -eps) hasNeg = true;
            else if (c > eps) hasPos = true;
            if (hasNeg && hasPos) break; // 早停
        }

        if(!(hasNeg && hasPos))
        {
            Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
            Test_1_Not_Targets_Recorder.RemoveAt(i);
            continue;
        }

        float minD = float.MaxValue;
        for (int j = 0; j < 5; j++)
        {
            Vector2 A = poly[j];
            Vector2 B = poly[(j + 1) % 5];
            // 点到线段距离（2D）
            Vector2 AB = B - A;
            float len2 = Vector2.Dot(AB, AB);
            float d;
            if (len2 < 1e-12f) d = Vector2.Distance(Cur, A);
            else
            {
                float t = Mathf.Clamp01(Vector2.Dot(Cur - A, AB) / len2);
                Vector2 Q = A + t * AB;
                d = Vector2.Distance(Cur, Q);
            }
            if (d < minD) minD = d;
            if (minD <= scale / 2 + 1e-5f)
            {
                Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
                Test_1_Not_Targets_Recorder.RemoveAt(i);
                break;
            }
        }
    }

    for (int i = 0; i < Test_1_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_1_Targets_Recorder[i]);
    }

}

private void Test_1_Basic_Model_HollowArch(float Total_Area = 4f, float Top_Half_Circle_Radius = 13f, float Button_Vertices = 3f)
{
    Vector3 Pos = Origin.position;
    Vector3 Center = new Vector3(Pos.x, Pos.y, Pos.z + Test_1_Item_Distance);
    float scale = MathF.Tan(Test_1_Item_Size / 2f * Mathf.Deg2Rad) * Vector3.Distance(Center, Origin.position) * 2f;
    float Gap = scale;
    float Vertices = Gap + scale / 2;
    Vector2 Center2D = new Vector2(Center.x, Center.y);
    float Radius = MathF.Tan(Top_Half_Circle_Radius / 2f * Mathf.Deg2Rad) * Vector3.Distance(Center, Origin.position) * 2f;
    Vector2 Circle_Center2D = new Vector2(Center.x, Center.y + Vertices);
    Vector2 Base_Center2D = new Vector2(Center.x, Center.y - Vertices);

    for (int i = Test_1_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_1_Not_Targets_Recorder[i].transform.position.x, Test_1_Not_Targets_Recorder[i].transform.position.y);
        float dx = Mathf.Abs(Cur.x - Center2D.x);
        float dy = Mathf.Abs(Cur.y - Center2D.y);

        if ((dx <= Vertices * Total_Area + 1e-5f) && (dy <= Vertices * Total_Area + 1e-5f))
        {
            if(Cur.y >= Circle_Center2D.y)
            {
                float distance = Vector2.Distance(Cur, Center2D);
                if(distance >= Radius)
                {
                    Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
                    Test_1_Not_Targets_Recorder.RemoveAt(i);
                }
            }
            else
            {
                float dx_2 = Mathf.Abs(Cur.x - Base_Center2D.x);
                float dy_2 = Mathf.Abs(Cur.y - Base_Center2D.y);

                if ((dx_2 >= Vertices + 1e-5f) || (dy_2 >= Vertices * Button_Vertices + 1e-5f))
                {
                    Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
                    Test_1_Not_Targets_Recorder.RemoveAt(i);
                }
            }
        }
    }

    for (int i = 0; i < Test_1_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_1_Targets_Recorder[i]);
    }

}

private void Test_1_Normal_Christmas_tree(float First_Top_Vertices_Level = 1f, float First_Left_Right_Vertices_Level = 2f, int First_Center_Times = 3,
    float Second_Top_Vertices_Level = 2f, float Second_Left_Right_Vertices_Level = 3f, int Second_Center_Times = 0,
    float Third_Top_Vertices_Level = 3f, float Third_Left_Right_Vertices_Level = 4f, int Third_Center_Times = -1, 
    int Base_Top_Vertices_Level = -2, int Base_Buttom_Vertices_Level = -4)
{
    Vector3 Pos = Origin.position;
    Vector3 Center = new Vector3(Pos.x, Pos.y, Pos.z + Test_1_Item_Distance);
    float scale = MathF.Tan(Test_1_Item_Size / 2f * Mathf.Deg2Rad) * Vector3.Distance(Center, Origin.position) * 2f;
    float Gap = scale;
    float Vertices = Gap + scale / 2;

    float First_T_Vertices = First_Top_Vertices_Level * Vertices;
    float First_LR_Vertices = First_Left_Right_Vertices_Level * Vertices;
    Vector3 First_Center = new Vector3(Center.x, Center.y + First_Center_Times * Vertices, Center.z);
    Vector2 First_Top_Vertices = new Vector2(First_Center.x, First_Center.y + First_T_Vertices);
    Vector2 First_Left_Vertices = new Vector2(First_Center.x - First_LR_Vertices, First_Center.y - Vertices);
    Vector2 First_Right_Vertices = new Vector2(First_Center.x + First_LR_Vertices, First_Center.y - Vertices);

    for (int i = Test_1_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_1_Not_Targets_Recorder[i].transform.position.x, Test_1_Not_Targets_Recorder[i].transform.position.y);
        float c1 = Test_1_Cross(First_Top_Vertices - First_Left_Vertices, Cur - First_Left_Vertices);
        float c2 = Test_1_Cross(First_Right_Vertices - First_Top_Vertices, Cur - First_Top_Vertices);
        float c3 = Test_1_Cross(First_Left_Vertices - First_Right_Vertices, Cur - First_Right_Vertices);

        bool hasNeg = (c1 < 0) || (c2 < 0) || (c3 < 0);
        bool hasPos = (c1 > 0) || (c2 > 0) || (c3 > 0);

        if (hasNeg && hasPos)
        {
            float d1 = Test_1_Triangle_DistPointToSegment2D(Cur, First_Left_Vertices, First_Top_Vertices);
            float d2 = Test_1_Triangle_DistPointToSegment2D(Cur, First_Top_Vertices, First_Right_Vertices);
            float d3 = Test_1_Triangle_DistPointToSegment2D(Cur, First_Right_Vertices, First_Left_Vertices);

            float d = Mathf.Min(d1, Mathf.Min(d2, d3));

            if (d <= scale / 2 + 1e-5f)
            {
                Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
                Test_1_Not_Targets_Recorder.RemoveAt(i);
            }
            else
            {
                continue;
            }
        }
        else
        {
            Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
            Test_1_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    float Second_T_Vertices = Second_Top_Vertices_Level * Vertices;
    float Second_LR_Vertices = Second_Left_Right_Vertices_Level * Vertices;
    Vector3 Second_Center = new Vector3(Center.x, Center.y + Second_Center_Times * Vertices, Center.z);
    Vector2 Second_Top_Vertices = new Vector2(Second_Center.x, Second_Center.y + Second_T_Vertices);
    Vector2 Second_Left_Vertices = new Vector2(Second_Center.x - Second_LR_Vertices, Second_Center.y - Vertices);
    Vector2 Second_Right_Vertices = new Vector2(Second_Center.x + Second_LR_Vertices, Second_Center.y - Vertices);

    for (int i = Test_1_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_1_Not_Targets_Recorder[i].transform.position.x, Test_1_Not_Targets_Recorder[i].transform.position.y);
        float c1 = Test_1_Cross(Second_Top_Vertices - Second_Left_Vertices, Cur - Second_Left_Vertices);
        float c2 = Test_1_Cross(Second_Right_Vertices - Second_Top_Vertices, Cur - Second_Top_Vertices);
        float c3 = Test_1_Cross(Second_Left_Vertices - Second_Right_Vertices, Cur - Second_Right_Vertices);

        bool hasNeg = (c1 < 0) || (c2 < 0) || (c3 < 0);
        bool hasPos = (c1 > 0) || (c2 > 0) || (c3 > 0);

        if (hasNeg && hasPos)
        {
            float d1 = Test_1_Triangle_DistPointToSegment2D(Cur, Second_Left_Vertices, Second_Top_Vertices);
            float d2 = Test_1_Triangle_DistPointToSegment2D(Cur, Second_Top_Vertices, Second_Right_Vertices);
            float d3 = Test_1_Triangle_DistPointToSegment2D(Cur, Second_Right_Vertices, Second_Left_Vertices);

            float d = Mathf.Min(d1, Mathf.Min(d2, d3));

            if (d <= scale / 2 + 1e-5f)
            {
                Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
                Test_1_Not_Targets_Recorder.RemoveAt(i);
            }
            else
            {
                continue;
            }
        }
        else
        {
            Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
            Test_1_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    //float Third_T_Vertices = Third_Top_Vertices_Level * Vertices;
    //float Third_LR_Vertices = Third_Left_Right_Vertices_Level * Vertices;
    //Vector3 Third_Center = new Vector3(Center.x, Center.y + Third_Center_Times * Vertices, Center.z);
    //Vector2 Third_Top_Vertices = new Vector2(Third_Center.x, Third_Center.y + Third_T_Vertices);
    //Vector2 Third_Left_Vertices = new Vector2(Third_Center.x - Third_LR_Vertices, Third_Center.y - Vertices);
    //Vector2 Third_Right_Vertices = new Vector2(Third_Center.x + Third_LR_Vertices, Third_Center.y - Vertices);

    //for (int i = Test_1_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    //{
    //    Vector2 Cur = new Vector2(Test_1_Not_Targets_Recorder[i].transform.position.x, Test_1_Not_Targets_Recorder[i].transform.position.y);
    //    float c1 = Test_1_Cross(Third_Top_Vertices - Third_Left_Vertices, Cur - Third_Left_Vertices);
    //    float c2 = Test_1_Cross(Third_Right_Vertices - Third_Top_Vertices, Cur - Third_Top_Vertices);
    //    float c3 = Test_1_Cross(Third_Left_Vertices - Third_Right_Vertices, Cur - Third_Right_Vertices);

    //    bool hasNeg = (c1 < 0) || (c2 < 0) || (c3 < 0);
    //    bool hasPos = (c1 > 0) || (c2 > 0) || (c3 > 0);

    //    if (hasNeg && hasPos)
    //    {
    //        float d1 = Test_1_Triangle_DistPointToSegment2D(Cur, Third_Left_Vertices, Third_Top_Vertices);
    //        float d2 = Test_1_Triangle_DistPointToSegment2D(Cur, Third_Top_Vertices, Third_Right_Vertices);
    //        float d3 = Test_1_Triangle_DistPointToSegment2D(Cur, Third_Right_Vertices, Third_Left_Vertices);

    //        float d = Mathf.Min(d1, Mathf.Min(d2, d3));

    //        if (d <= scale / 2 + 1e-5f)
    //        {
    //            Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
    //            Test_1_Not_Targets_Recorder.RemoveAt(i);
    //        }
    //        else
    //        {
    //            continue;
    //        }
    //    }
    //    else
    //    {
    //        Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
    //        Test_1_Not_Targets_Recorder.RemoveAt(i);
    //    }
    //}

    Vector2 Base_Center = new Vector2(Center.x, Center.y + (Base_Buttom_Vertices_Level + Base_Top_Vertices_Level) / 2f * Vertices);

    for (int i = Test_1_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_1_Not_Targets_Recorder[i].transform.position.x, Test_1_Not_Targets_Recorder[i].transform.position.y);
        float dx = Mathf.Abs(Cur.x - Base_Center.x);
        float dy = Mathf.Abs(Cur.y - Base_Center.y);

        if ((dx <= Vertices + 1e-5f) && (dy <=  Vertices + 1e-5f))
        {
            Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
            Test_1_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    for (int i = 0; i < Test_1_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_1_Targets_Recorder[i]);
    }
}

private void Test_1_Normal_Arrow(float Top_Vertices_Level = 3f, float First_Left_Right_Vertices_Level = 4f, int First_Center_Times = 1,
    int Base_Top_Vertices_Level = -1, int Base_Buttom_Vertices_Level = -4)
{
    Vector3 Pos = Origin.position;
    Vector3 Center = new Vector3(Pos.x, Pos.y, Pos.z + Test_1_Item_Distance);
    float scale = MathF.Tan(Test_1_Item_Size / 2f * Mathf.Deg2Rad) * Vector3.Distance(Center, Origin.position) * 2f;
    float Gap = scale;
    float Vertices = Gap + scale / 2;

    float First_T_Vertices = Top_Vertices_Level * Vertices;
    float First_LR_Vertices = First_Left_Right_Vertices_Level * Vertices;
    Vector3 First_Center = new Vector3(Center.x, Center.y + First_Center_Times * Vertices, Center.z);
    Vector2 First_Top_Vertices = new Vector2(First_Center.x, First_Center.y + First_T_Vertices);
    Vector2 First_Left_Vertices = new Vector2(First_Center.x - First_LR_Vertices, First_Center.y - Vertices);
    Vector2 First_Right_Vertices = new Vector2(First_Center.x + First_LR_Vertices, First_Center.y - Vertices);

    for (int i = Test_1_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_1_Not_Targets_Recorder[i].transform.position.x, Test_1_Not_Targets_Recorder[i].transform.position.y);
        float c1 = Test_1_Cross(First_Top_Vertices - First_Left_Vertices, Cur - First_Left_Vertices);
        float c2 = Test_1_Cross(First_Right_Vertices - First_Top_Vertices, Cur - First_Top_Vertices);
        float c3 = Test_1_Cross(First_Left_Vertices - First_Right_Vertices, Cur - First_Right_Vertices);

        bool hasNeg = (c1 < 0) || (c2 < 0) || (c3 < 0);
        bool hasPos = (c1 > 0) || (c2 > 0) || (c3 > 0);

        if (hasNeg && hasPos)
        {
            float d1 = Test_1_Triangle_DistPointToSegment2D(Cur, First_Left_Vertices, First_Top_Vertices);
            float d2 = Test_1_Triangle_DistPointToSegment2D(Cur, First_Top_Vertices, First_Right_Vertices);
            float d3 = Test_1_Triangle_DistPointToSegment2D(Cur, First_Right_Vertices, First_Left_Vertices);

            float d = Mathf.Min(d1, Mathf.Min(d2, d3));

            if (d <= scale / 2 + 1e-5f)
            {
                Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
                Test_1_Not_Targets_Recorder.RemoveAt(i);
            }
            else
            {
                continue;
            }
        }
        else
        {
            Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
            Test_1_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    Vector2 Base_Center = new Vector2(Center.x, Center.y + (Base_Buttom_Vertices_Level + Base_Top_Vertices_Level) / 2f * Vertices);

    for (int i = Test_1_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_1_Not_Targets_Recorder[i].transform.position.x, Test_1_Not_Targets_Recorder[i].transform.position.y);
        float dx = Mathf.Abs(Cur.x - Base_Center.x);
        float dy = Mathf.Abs(Cur.y - Base_Center.y);

        if ((dx <= Vertices * 3 + 1e-5f) && (dy <= (Base_Top_Vertices_Level - Base_Buttom_Vertices_Level) / 2f * Vertices + 1e-5f))
        {
            Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
            Test_1_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    for (int i = 0; i < Test_1_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_1_Targets_Recorder[i]);
    }
}

private void Test_1_Normal_IceCream(float Top_Circle_Radius = 70f, int Top_Circle_Center = 2,
    float Base_Buttom_Vertices_Level = -4f, float Base_Buttom_Left_Right_Vertices_Level = 3f, int Buttom_Circle_Center = 0)
{
    Vector3 Pos = Origin.position;
    Vector3 Center = new Vector3(Pos.x, Pos.y, Pos.z + Test_1_Item_Distance);
    float scale = MathF.Tan(Test_1_Item_Size / 2f * Mathf.Deg2Rad) * Vector3.Distance(Center, Origin.position) * 2f;
    float Gap = scale;
    float Vertices = Gap + scale / 2;

    Vector2 Top_Center = new Vector3(Center.x, Center.y + Top_Circle_Center * Vertices);
    float Top_Radius = MathF.Tan(Top_Circle_Radius / 2f * Mathf.Deg2Rad) * Vector3.Distance(Top_Center, Origin.position) * 2f;
            
    for (int i = Test_1_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_1_Not_Targets_Recorder[i].transform.position.x, Test_1_Not_Targets_Recorder[i].transform.position.y);
        float Distance_1 = Vector2.Distance(Cur, Top_Center);

        if (Distance_1 <= Top_Radius)
        {
            Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
            Test_1_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    float Base_T_Vertices = Base_Buttom_Vertices_Level * Vertices;
    float Base_LR_Vertices = Base_Buttom_Left_Right_Vertices_Level * Vertices;
    Vector2 Base_Buttom_Vertices = new Vector2(Center.x, Center.y + Base_T_Vertices);
    Vector2 Base_Left_Vertices = new Vector2(Center.x - Base_LR_Vertices, Center.y + Vertices * Buttom_Circle_Center);
    Vector2 Base_Right_Vertices = new Vector2(Center.x + Base_LR_Vertices, Center.y + Vertices * Buttom_Circle_Center);

    for (int i = Test_1_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_1_Not_Targets_Recorder[i].transform.position.x, Test_1_Not_Targets_Recorder[i].transform.position.y);
        float c1 = Test_1_Cross(Base_Buttom_Vertices - Base_Left_Vertices, Cur - Base_Left_Vertices);
        float c2 = Test_1_Cross(Base_Right_Vertices - Base_Buttom_Vertices, Cur - Base_Buttom_Vertices);
        float c3 = Test_1_Cross(Base_Left_Vertices - Base_Right_Vertices, Cur - Base_Right_Vertices);

        bool hasNeg = (c1 < 0) || (c2 < 0) || (c3 < 0);
        bool hasPos = (c1 > 0) || (c2 > 0) || (c3 > 0);

        if (hasNeg && hasPos)
        {
            float d1 = Test_1_Triangle_DistPointToSegment2D(Cur, Base_Left_Vertices, Base_Buttom_Vertices);
            float d2 = Test_1_Triangle_DistPointToSegment2D(Cur, Base_Buttom_Vertices, Base_Right_Vertices);
            float d3 = Test_1_Triangle_DistPointToSegment2D(Cur, Base_Right_Vertices, Base_Left_Vertices);

            float d = Mathf.Min(d1, Mathf.Min(d2, d3));

            if (d <= scale / 2 + 1e-5f)
            {
                Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
                Test_1_Not_Targets_Recorder.RemoveAt(i);
            }
            else
            {
                continue;
            }
        }
        else
        {
            Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
            Test_1_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    Vector2 Button_Base_Center2d = new Vector2(Center.x, Center.y - 3f * Vertices);

    for (int i = Test_1_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_1_Not_Targets_Recorder[i].transform.position.x, Test_1_Not_Targets_Recorder[i].transform.position.y);
        float dx = Mathf.Abs(Cur.x - Button_Base_Center2d.x);
        float dy = Mathf.Abs(Cur.y - Button_Base_Center2d.y);

        if ((dx <= Vertices + 1e-5f) && (dy <= Vertices + 1e-5f))
        {
            Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
            Test_1_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    for (int i = 0; i < Test_1_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_1_Targets_Recorder[i]);
    }
}

private void Test_1_Normal_Avatar(float Top_Circle_Radius = 25f, int Top_Circle_Center = 3, float Mid = 3f,
    float Base_Buttom_Vertices_Level = 4f, float Base_Buttom_Left_Right_Vertices_Level = 4f, int Buttom_Circle_Center = -2,
    float Base_Center = -2f)
{
    Vector3 Pos = Origin.position;
    Vector3 Center = new Vector3(Pos.x, Pos.y, Pos.z + Test_1_Item_Distance);
    float scale = MathF.Tan(Test_1_Item_Size / 2f * Mathf.Deg2Rad) * Vector3.Distance(Center, Origin.position) * 2f;
    float Gap = scale;
    float Vertices = Gap + scale / 2;

    Vector2 Top_Center = new Vector3(Center.x, Center.y + Top_Circle_Center * Vertices);
    float Top_Radius = MathF.Tan(Top_Circle_Radius / 2f * Mathf.Deg2Rad) * Vector3.Distance(Top_Center, Origin.position) * 2f;

    for (int i = Test_1_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_1_Not_Targets_Recorder[i].transform.position.x, Test_1_Not_Targets_Recorder[i].transform.position.y);
        float Distance_1 = Vector2.Distance(Cur, Top_Center);

        if (Distance_1 <= Top_Radius)
        {
            Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
            Test_1_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    Vector2 Middle_Center = new Vector2(Center.x, Center.y + Vertices);

    for (int i = Test_1_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_1_Not_Targets_Recorder[i].transform.position.x, Test_1_Not_Targets_Recorder[i].transform.position.y);
        float dx = Mathf.Abs(Cur.x - Middle_Center.x);
        float dy = Mathf.Abs(Cur.y - Middle_Center.y);

        if ((dx <= Mid * Vertices + 1e-5f) && (dy <= 0f * Vertices + 1e-5f))
        {
            Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
            Test_1_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    Vector2 Base_Center2d = new Vector2(Center.x, Center.y + Base_Center * Vertices);

    for (int i = Test_1_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_1_Not_Targets_Recorder[i].transform.position.x, Test_1_Not_Targets_Recorder[i].transform.position.y);
        float dx = Mathf.Abs(Cur.x - Base_Center2d.x);
        float dy = Mathf.Abs(Cur.y - Base_Center2d.y);

        if ((dx <= 1f * Vertices + 1e-5f) && (dy <= 2f * Vertices + 1e-5f))
        {
            Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
            Test_1_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    //float Base_T_Vertices = Base_Buttom_Vertices_Level * Vertices;
    //float Base_LR_Vertices = Base_Buttom_Left_Right_Vertices_Level * Vertices;
    //Vector2 Base_Center2D = new Vector2(Center.x, Center.y + Buttom_Circle_Center * Vertices);
    //Vector2 Base_Buttom_Vertices = new Vector2(Base_Center2D.x, Base_Center2D.y + Base_T_Vertices);
    //Vector2 Base_Left_Vertices = new Vector2(Base_Center2D.x - Base_LR_Vertices, Base_Center2D.y - Vertices);
    //Vector2 Base_Right_Vertices = new Vector2(Base_Center2D.x + Base_LR_Vertices, Base_Center2D.y - Vertices);

    //for (int i = Test_1_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    //{
    //    Vector2 Cur = new Vector2(Test_1_Not_Targets_Recorder[i].transform.position.x, Test_1_Not_Targets_Recorder[i].transform.position.y);
    //    float c1 = Test_1_Cross(Base_Buttom_Vertices - Base_Left_Vertices, Cur - Base_Left_Vertices);
    //    float c2 = Test_1_Cross(Base_Right_Vertices - Base_Buttom_Vertices, Cur - Base_Buttom_Vertices);
    //    float c3 = Test_1_Cross(Base_Left_Vertices - Base_Right_Vertices, Cur - Base_Right_Vertices);

    //    bool hasNeg = (c1 < 0) || (c2 < 0) || (c3 < 0);
    //    bool hasPos = (c1 > 0) || (c2 > 0) || (c3 > 0);

    //    if (hasNeg && hasPos)
    //    {
    //        float d1 = Test_1_Triangle_DistPointToSegment2D(Cur, Base_Left_Vertices, Base_Buttom_Vertices);
    //        float d2 = Test_1_Triangle_DistPointToSegment2D(Cur, Base_Buttom_Vertices, Base_Right_Vertices);
    //        float d3 = Test_1_Triangle_DistPointToSegment2D(Cur, Base_Right_Vertices, Base_Left_Vertices);

    //        float d = Mathf.Min(d1, Mathf.Min(d2, d3));

    //        if (d <= scale / 2 + 1e-5f)
    //        {
    //            Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
    //            Test_1_Not_Targets_Recorder.RemoveAt(i);
    //        }
    //        else
    //        {
    //            continue;
    //        }
    //    }
    //    else
    //    {
    //        Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
    //        Test_1_Not_Targets_Recorder.RemoveAt(i);
    //    }
    //}

    for (int i = 0; i < Test_1_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_1_Targets_Recorder[i]);
    }
}

private void Test_1_Hard_Crown(float Top_Vertices_Level = 2f, float Top_Left_Right_Vertices_Level = 2f, float Top_Gap = 4f,
    float Base_Vertices_Level = -2f, float Base_Length = 4f, float Base_Height = 2f)
{
    Vector3 Pos = Origin.position;
    Vector3 Center = new Vector3(Pos.x, Pos.y, Pos.z + Test_1_Item_Distance);
    float scale = MathF.Tan(Test_1_Item_Size / 2f * Mathf.Deg2Rad) * Vector3.Distance(Center, Origin.position) * 2f;
    float Gap = scale;
    float Vertices = Gap + scale / 2;

    float T_Vertices = Top_Vertices_Level * Vertices;
    float LR_Vertices = Top_Left_Right_Vertices_Level * Vertices;

    Vector3 First_Center = new Vector3(Center.x - T_Vertices, Center.y + T_Vertices, Center.z);
    Vector2 First_Top_Vertices = new Vector2(First_Center.x, First_Center.y + Vertices);
    Vector2 First_Left_Vertices = new Vector2(First_Center.x - LR_Vertices, First_Center.y - Vertices);
    Vector2 First_Right_Vertices = new Vector2(First_Center.x + LR_Vertices, First_Center.y - Vertices);

    for (int i = Test_1_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_1_Not_Targets_Recorder[i].transform.position.x, Test_1_Not_Targets_Recorder[i].transform.position.y);
        float c1 = Test_1_Cross(First_Top_Vertices - First_Left_Vertices, Cur - First_Left_Vertices);
        float c2 = Test_1_Cross(First_Right_Vertices - First_Top_Vertices, Cur - First_Top_Vertices);
        float c3 = Test_1_Cross(First_Left_Vertices - First_Right_Vertices, Cur - First_Right_Vertices);

        bool hasNeg = (c1 < 0) || (c2 < 0) || (c3 < 0);
        bool hasPos = (c1 > 0) || (c2 > 0) || (c3 > 0);

        if (hasNeg && hasPos)
        {
            float d1 = Test_1_Triangle_DistPointToSegment2D(Cur, First_Left_Vertices, First_Top_Vertices);
            float d2 = Test_1_Triangle_DistPointToSegment2D(Cur, First_Top_Vertices, First_Right_Vertices);
            float d3 = Test_1_Triangle_DistPointToSegment2D(Cur, First_Right_Vertices, First_Left_Vertices);

            float d = Mathf.Min(d1, Mathf.Min(d2, d3));

            if (d <= scale / 2 + 1e-5f)
            {
                Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
                Test_1_Not_Targets_Recorder.RemoveAt(i);
            }
            else
            {
                continue;
            }
        }
        else
        {
            Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
            Test_1_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    Vector3 Second_Center = new Vector3(Center.x + T_Vertices, Center.y + T_Vertices, Center.z);
    Vector2 Second_Top_Vertices = new Vector2(Second_Center.x, Second_Center.y + Vertices);
    Vector2 Second_Left_Vertices = new Vector2(Second_Center.x - LR_Vertices, Second_Center.y - Vertices);
    Vector2 Second_Right_Vertices = new Vector2(Second_Center.x + LR_Vertices, Second_Center.y - Vertices);

    for (int i = Test_1_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_1_Not_Targets_Recorder[i].transform.position.x, Test_1_Not_Targets_Recorder[i].transform.position.y);
        float c1 = Test_1_Cross(Second_Top_Vertices - Second_Left_Vertices, Cur - Second_Left_Vertices);
        float c2 = Test_1_Cross(Second_Right_Vertices - Second_Top_Vertices, Cur - Second_Top_Vertices);
        float c3 = Test_1_Cross(Second_Left_Vertices - Second_Right_Vertices, Cur - Second_Right_Vertices);

        bool hasNeg = (c1 < 0) || (c2 < 0) || (c3 < 0);
        bool hasPos = (c1 > 0) || (c2 > 0) || (c3 > 0);

        if (hasNeg && hasPos)
        {
            float d1 = Test_1_Triangle_DistPointToSegment2D(Cur, Second_Left_Vertices, Second_Top_Vertices);
            float d2 = Test_1_Triangle_DistPointToSegment2D(Cur, Second_Top_Vertices, Second_Right_Vertices);
            float d3 = Test_1_Triangle_DistPointToSegment2D(Cur, Second_Right_Vertices, Second_Left_Vertices);

            float d = Mathf.Min(d1, Mathf.Min(d2, d3));

            if (d <= scale / 2 + 1e-5f)
            {
                Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
                Test_1_Not_Targets_Recorder.RemoveAt(i);
            }
            else
            {
                continue;
            }
        }
        else
        {
            Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
            Test_1_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    Vector2 Base_Center = new Vector2(Center.x, Center.y + Base_Vertices_Level * Vertices);

    for (int i = Test_1_Not_Targets_Recorder.Count - 1; i >= 0; i--)
    {
        Vector2 Cur = new Vector2(Test_1_Not_Targets_Recorder[i].transform.position.x, Test_1_Not_Targets_Recorder[i].transform.position.y);
        float dx = Mathf.Abs(Cur.x - Base_Center.x);
        float dy = Mathf.Abs(Cur.y - Base_Center.y);

        if ((dx <= Base_Length *  Vertices + 1e-5f) && (dy <= 2f * Vertices + 1e-5f))
        {
            Test_1_Targets_Recorder.Add(Test_1_Not_Targets_Recorder[i]);
            Test_1_Not_Targets_Recorder.RemoveAt(i);
        }
    }

    for (int i = 0; i < Test_1_Targets_Recorder.Count; i++)
    {
        Change_Sphere_Color(Test_1_Targets_Recorder[i]);
    }
}

private void Test_1_Build_Sphere(Vector3 Pos, float scale, string Name, int Num)
{
    GameObject Sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    Sphere.transform.position = Pos;
    //float scale = MathF.Tan(Test_1_Item_Size / 2f * Mathf.Deg2Rad) * Vector3.Distance(Pos, Origin.position) * 2f;
    Sphere.transform.localScale = new Vector3(scale, scale, scale);
    Sphere.name = "Sphere" + Name + Num.ToString();
    Sphere.tag = "Dwell";
    Sphere.AddComponent<EyeTrackingArea>();
    Sphere.AddComponent<Angle_Distance>();
    Test_1_Not_Targets_Recorder.Add(Sphere);
}
