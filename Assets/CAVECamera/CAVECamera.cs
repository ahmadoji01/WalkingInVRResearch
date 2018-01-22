using UnityEngine;
using System;
using System.Collections.Generic;
using System.Xml;

public class CAVECamera : MonoBehaviour {

    public float _near = 0.3f;
    public float _far = 10.0f;

    private List<Transform> _cameraList;
    private Dictionary<String, Transform> _cameraDict;
    private Transform _lEye;
    private Transform _rEye;

    void OnDrawGizmos()
    {
        Color c = Gizmos.color;

        Vector3 v0 = new Vector3(-1.25f, 0.0f, -1.25f);
        Vector3 v1 = new Vector3( 1.25f, 0.0f, -1.25f);
        Vector3 v2 = new Vector3(-1.25f, 2.5f, -1.25f);
        Vector3 v3 = new Vector3( 1.25f, 2.5f, -1.25f);
        Vector3 v4 = new Vector3(-1.25f, 0.0f,  1.25f);
        Vector3 v5 = new Vector3( 1.25f, 0.0f,  1.25f);
        Vector3 v6 = new Vector3(-1.25f, 2.5f,  1.25f);
        Vector3 v7 = new Vector3( 1.25f, 2.5f,  1.25f);
        Vector3 dx = new Vector3(0.25f, 0.0f, 0.0f);
        Vector3 dy = new Vector3(0.0f, 0.25f, 0.0f);
        Vector3 dz = new Vector3(0.0f, 0.0f, 0.25f);

        v0 = transform.TransformPoint(v0);
        v1 = transform.TransformPoint(v1);
        v2 = transform.TransformPoint(v2);
        v3 = transform.TransformPoint(v3);
        v4 = transform.TransformPoint(v4);
        v5 = transform.TransformPoint(v5);
        v6 = transform.TransformPoint(v6);
        v7 = transform.TransformPoint(v7);
        dx = transform.TransformDirection(dx);
        dy = transform.TransformDirection(dy);
        dz = transform.TransformDirection(dz);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(v0, v1);
        //Gizmos.DrawLine(v2, v3);
        Gizmos.DrawLine(v4, v5);
        Gizmos.DrawLine(v6, v7);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(v0, v2);
        Gizmos.DrawLine(v1, v3);
        Gizmos.DrawLine(v4, v6);
        Gizmos.DrawLine(v5, v7);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(v0, v4);
        Gizmos.DrawLine(v1, v5);
        Gizmos.DrawLine(v2, v6);
        Gizmos.DrawLine(v3, v7);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(v1 + dy + dz, v3 - dy + dz);
        Gizmos.DrawLine(v3 - dy + dz, v7 - dy - dz);
        Gizmos.DrawLine(v7 - dy - dz, v5 + dy - dz);
        Gizmos.DrawLine(v5 + dy - dz, v1 + dy + dz);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(v0 + dx + dz, v1 - dx + dz);
        Gizmos.DrawLine(v1 - dx + dz, v5 - dx - dz);
        Gizmos.DrawLine(v5 - dx - dz, v4 + dx - dz);
        Gizmos.DrawLine(v4 + dx - dz, v0 + dx + dz);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(v4 + dx + dy, v5 - dx + dy);
        Gizmos.DrawLine(v5 - dx + dy, v7 - dx - dy);
        Gizmos.DrawLine(v7 - dx - dy, v6 + dx - dy);
        Gizmos.DrawLine(v6 + dx - dy, v4 + dx + dy);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(v4 + dy - dz, v6 - dy - dz);
        Gizmos.DrawLine(v6 - dy - dz, v2 - dy + dz);
        Gizmos.DrawLine(v2 - dy + dz, v0 + dy + dz);
        Gizmos.DrawLine(v0 + dy + dz, v4 + dy - dz);

        v0 = new Vector3(-_far, -_far + 1.25f, -_far);
        v1 = new Vector3(_far, -_far + 1.25f, -_far);
        v2 = new Vector3(-_far, _far + 1.25f, -_far);
        v3 = new Vector3(_far, _far + 1.25f, -_far);
        v4 = new Vector3(-_far, -_far + 1.25f, _far);
        v5 = new Vector3(_far, -_far + 1.25f, _far);
        v6 = new Vector3(-_far, _far + 1.25f, _far);
        v7 = new Vector3(_far, _far + 1.25f, _far);
        dx = new Vector3(_far * 0.1f, 0.0f, 0.0f);
        dy = new Vector3(0.0f, _far * 0.1f, 0.0f);
        dz = new Vector3(0.0f, 0.0f, _far * 0.1f);

        v0 = transform.TransformPoint(v0);
        v1 = transform.TransformPoint(v1);
        v2 = transform.TransformPoint(v2);
        v3 = transform.TransformPoint(v3);
        v4 = transform.TransformPoint(v4);
        v5 = transform.TransformPoint(v5);
        v6 = transform.TransformPoint(v6);
        v7 = transform.TransformPoint(v7);
        dx = transform.TransformDirection(dx);
        dy = transform.TransformDirection(dy);
        dz = transform.TransformDirection(dz);

        Gizmos.color = Color.gray;
        Gizmos.DrawLine(v4, v5);
        Gizmos.DrawLine(v5, v7);
        Gizmos.DrawLine(v7, v6);
        Gizmos.DrawLine(v6, v4);

        //Gizmos.color = Color.blue;
        //Gizmos.DrawLine(v4 + dx + dy, v5 - dx + dy);
        //Gizmos.DrawLine(v5 - dx + dy, v7 - dx - dy);
        //Gizmos.DrawLine(v7 - dx - dy, v6 + dx - dy);
        //Gizmos.DrawLine(v6 + dx - dy, v4 + dx + dy);

        Gizmos.color = Color.gray;
        Gizmos.DrawLine(v0, v4);
        Gizmos.DrawLine(v4, v6);
        Gizmos.DrawLine(v6, v2);
        Gizmos.DrawLine(v2, v0);

        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(v0 + dz + dy, v4 - dz + dy);
        //Gizmos.DrawLine(v4 - dz + dy, v6 - dz - dy);
        //Gizmos.DrawLine(v6 - dz - dy, v2 + dz - dy);
        //Gizmos.DrawLine(v2 + dz - dy, v0 + dz + dy);

        Gizmos.color = Color.gray;
        Gizmos.DrawLine(v1, v5);
        Gizmos.DrawLine(v5, v7);
        Gizmos.DrawLine(v7, v3);
        Gizmos.DrawLine(v3, v1);
        
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(v1 + dz + dy, v5 - dz + dy);
        //Gizmos.DrawLine(v5 - dz + dy, v7 - dz - dy);
        //Gizmos.DrawLine(v7 - dz - dy, v3 + dz - dy);
        //Gizmos.DrawLine(v3 + dz - dy, v1 + dz + dy);

        Gizmos.color = Color.gray;
        Gizmos.DrawLine(v0, v1);
        Gizmos.DrawLine(v1, v5);
        Gizmos.DrawLine(v5, v4);
        Gizmos.DrawLine(v4, v0);

        //Gizmos.color = Color.green;
        //Gizmos.DrawLine(v0 + dx + dz, v1 - dx + dz);
        //Gizmos.DrawLine(v1 - dx + dz, v5 - dx - dz);
        //Gizmos.DrawLine(v5 - dx - dz, v4 + dx - dz);
        //Gizmos.DrawLine(v4 + dx - dz, v0 + dx + dz);

        Gizmos.color = c;
    }

	// Use this for initialization
	void Start ()
    {
	    for(int i = 0; i < transform.childCount; ++i)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(true);
        }

        try
        {

            transform.FindChild("Eyes").gameObject.SetActive(false);
        }
        catch (System.Exception)
        { }

        _lEye = transform.FindChild("Eyes/LeftEye");
        _rEye = transform.FindChild("Eyes/RightEye");


        transform.FindChild("Wall").gameObject.SetActive(true);

        _cameraDict = new Dictionary<string, Transform>();
        _cameraDict["LL"] = transform.Find("Wall/CameraLL");
        _cameraDict["LR"] = transform.Find("Wall/CameraLR");
        _cameraDict["CL"] = transform.Find("Wall/CameraCL");
        _cameraDict["CR"] = transform.Find("Wall/CameraCR");
        _cameraDict["RL"] = transform.Find("Wall/CameraRL");
        _cameraDict["RR"] = transform.Find("Wall/CameraRR");
        _cameraDict["BL"] = transform.Find("Wall/CameraBL");
        _cameraDict["BR"] = transform.Find("Wall/CameraBR");

        _cameraList = new List<Transform>();
        _cameraList.Add(_cameraDict["LL"]);
        _cameraList.Add(_cameraDict["LR"]);
        _cameraList.Add(_cameraDict["CL"]);
        _cameraList.Add(_cameraDict["CR"]);
        _cameraList.Add(_cameraDict["RL"]);
        _cameraList.Add(_cameraDict["RR"]);
        _cameraList.Add(_cameraDict["BL"]);
        _cameraList.Add(_cameraDict["BR"]);


        List<Transform> pjList = new List<Transform>();
        pjList.Add(transform.Find("Wall/PJLL/PJC"));
        pjList.Add(transform.Find("Wall/PJLR/PJC"));
        pjList.Add(transform.Find("Wall/PJCL/PJC"));
        pjList.Add(transform.Find("Wall/PJCR/PJC"));
        pjList.Add(transform.Find("Wall/PJRL/PJC"));
        pjList.Add(transform.Find("Wall/PJRR/PJC"));
        pjList.Add(transform.Find("Wall/PJBL/PJC"));
        pjList.Add(transform.Find("Wall/PJBR/PJC"));

        foreach(Transform c in pjList)
        {
            c.GetComponent<Camera>().cullingMask = 1 << 31;
        }

        List<Transform> gridList = new List<Transform>();
        gridList.Add(transform.Find("Wall/PJLL/distortion_grid"));
        gridList.Add(transform.Find("Wall/PJLR/distortion_grid"));
        gridList.Add(transform.Find("Wall/PJCL/distortion_grid"));
        gridList.Add(transform.Find("Wall/PJCR/distortion_grid"));
        gridList.Add(transform.Find("Wall/PJRL/distortion_grid"));
        gridList.Add(transform.Find("Wall/PJRR/distortion_grid"));
        gridList.Add(transform.Find("Wall/PJBL/distortion_grid"));
        gridList.Add(transform.Find("Wall/PJBR/distortion_grid"));

        foreach(Transform c in gridList)
        {
            c.gameObject.layer = 31;
        }



        //中央・歪みなしのグリッドを仮生成
        foreach (Transform c in gridList)
        {
            ResetGrid(c.gameObject);
        }

        //歪み補正データでのグリッド生成
        try
        {
            XmlDocument gridDoc = new XmlDocument();
            gridDoc.Load(@"C:\KAIT_CAVE\DistortionGrid.xml");

            LoadGrid(gridDoc.DocumentElement, "LL", gridList[0].gameObject);
            LoadGrid(gridDoc.DocumentElement, "LR", gridList[1].gameObject);
            LoadGrid(gridDoc.DocumentElement, "CL", gridList[2].gameObject);
            LoadGrid(gridDoc.DocumentElement, "CR", gridList[3].gameObject);
            LoadGrid(gridDoc.DocumentElement, "RL", gridList[4].gameObject);
            LoadGrid(gridDoc.DocumentElement, "RR", gridList[5].gameObject);
            LoadGrid(gridDoc.DocumentElement, "BL", gridList[6].gameObject);
            LoadGrid(gridDoc.DocumentElement, "BR", gridList[7].gameObject);

        }
        catch (System.Exception ex)
        {
            Debug.Log("failed to generate distortion grid");
            Debug.LogException(ex);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        //foreach (Transform c in _cameraList)
        //{
        //    c.camera.nearClipPlane = _near;
        //    c.camera.farClipPlane = _far;
        //}

        Vector3 lEye = transform.worldToLocalMatrix.MultiplyPoint(_lEye.position);
        Vector3 rEye = transform.worldToLocalMatrix.MultiplyPoint(_rEye.position);

        _cameraDict["LL"].localPosition = lEye;
        _cameraDict["CL"].localPosition = lEye;
        _cameraDict["RL"].localPosition = lEye;
        _cameraDict["BL"].localPosition = lEye;
        _cameraDict["LR"].localPosition = rEye;
        _cameraDict["CR"].localPosition = rEye;
        _cameraDict["RR"].localPosition = rEye;
        _cameraDict["BR"].localPosition = rEye;

        const float x0 = -1.25f;
        const float y0 = 0.0f;
        const float z0 = -1.25f;
        const float x1 = 1.25f;
        const float y1 = 2.5f;
        const float z1 = 1.25f;

        float a;

        //LL
        a = Mathf.Abs(1.0f/(x0 - lEye.x)*_near);
        _cameraDict["LL"].GetComponent<Camera>().projectionMatrix = PerspectiveOffCenter(
            (z0 - lEye.z) * a, (z1 - lEye.z) * a,
            (y0 - lEye.y) * a, (y1 - lEye.y) * a,
            _near, _far);

        //LR
        a = Mathf.Abs(1.0f / (x0 - rEye.x) * _near);
        _cameraDict["LR"].GetComponent<Camera>().projectionMatrix = PerspectiveOffCenter(
            (z0 - rEye.z) * a, (z1 - rEye.z) * a,
            (y0 - rEye.y) * a, (y1 - rEye.y) * a,
            _near, _far);

        //CL
        a = Mathf.Abs(1.0f / (z1 - lEye.z) * _near);
        _cameraDict["CL"].GetComponent<Camera>().projectionMatrix = PerspectiveOffCenter(
            (x0 - lEye.x) * a, (x1 - lEye.x) * a,
            (y0 - lEye.y) * a, (y1 - lEye.y) * a,
            _near, _far);

        //CR
        a = Mathf.Abs(1.0f / (z1 - rEye.z) * _near);
        _cameraDict["CR"].GetComponent<Camera>().projectionMatrix = PerspectiveOffCenter(
            (x0 - rEye.x) * a, (x1 - rEye.x) * a,
            (y0 - rEye.y) * a, (y1 - rEye.y) * a,
            _near, _far);

        //RL
        a = Mathf.Abs(1.0f / (x1 - lEye.x) * _near);
        _cameraDict["RL"].GetComponent<Camera>().projectionMatrix = PerspectiveOffCenter(
            -(z1 - lEye.z) * a, -(z0 - lEye.z) * a,
            (y0 - lEye.y) * a, (y1 - lEye.y) * a,
            _near, _far);

        //RR
        a = Mathf.Abs(1.0f / (x1 - rEye.x) * _near);
        _cameraDict["RR"].GetComponent<Camera>().projectionMatrix = PerspectiveOffCenter(
            -(z1 - rEye.z) * a, -(z0 - rEye.z) * a,
            (y0 - rEye.y) * a, (y1 - rEye.y) * a,
            _near, _far);

        //BL
        a = Mathf.Abs(1.0f / (y0 - lEye.y) * _near);
        _cameraDict["BL"].GetComponent<Camera>().projectionMatrix = PerspectiveOffCenter(
            (x0 - lEye.x) * a, (x1 - lEye.x) * a,
            (z0 - lEye.z) * a, (z1 - lEye.z) * a,
            _near, _far);

        //BR
        a = Mathf.Abs(1.0f / (y0 - rEye.y) * _near);
        _cameraDict["BR"].GetComponent<Camera>().projectionMatrix = PerspectiveOffCenter(
            (x0 - rEye.x) * a, (x1 - rEye.x) * a,
            (z0 - rEye.z) * a, (z1 - rEye.z) * a,
            _near, _far);

    }

    private void ResetGrid(GameObject go)
    {
        float a = 1.0f;

        Vector3[] pos = new Vector3[4];
        pos[0] = new Vector3(-a, -a, 0.0f);
        pos[1] = new Vector3(-a, a, 0.0f);
        pos[2] = new Vector3(a, -a, 0.0f);
        pos[3] = new Vector3(a, a, 0.0f);

        Vector2[] uv = new Vector2[4];
        uv[0] = new Vector2(0.0f, 0.0f);
        uv[1] = new Vector2(0.0f, 1.0f);
        uv[2] = new Vector2(1.0f, 0.0f);
        uv[3] = new Vector2(1.0f, 1.0f);

        int[] ia = new int[6];
        ia[0] = 0;
        ia[1] = 1;
        ia[2] = 2;
        ia[3] = 2;
        ia[4] = 1;
        ia[5] = 3;

        Mesh mesh = new Mesh();
        mesh.vertices = pos;
        mesh.uv = uv;
        mesh.triangles = ia;

        go.GetComponent<MeshFilter>().mesh = mesh;
    }

    private void LoadGrid(XmlNode docRoot, string name, GameObject go)
    {
        XmlNode node = docRoot.SelectSingleNode("//Grid[@Name=\"" + name + "\"]");
        XmlNode vtxNode = node.SelectSingleNode("Vertices");

        string[] vtxToken = vtxNode.InnerText.Split(new char[] { ',', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

        int I = 10;
        int J = 10;
        int vc = (I + 1) * (J + 1);

        Vector3[] pos = new Vector3[vc];
        for (int i = 0; i < vc; ++i)
        {
            pos[i] = new Vector3(
                float.Parse(vtxToken[i * 2]),
                float.Parse(vtxToken[i * 2 + 1]),
                0.0f);
        }

        float du = 1.0f / I;
        float dv = 1.0f / J;
        Vector2[] uv = new Vector2[vc];
        for (int j = 0; j < J + 1; ++j)
        {
            for (int i = 0; i < I + 1; ++i)
            {
                uv[i + j * (I + 1)] = new Vector2(du * i, 1.0f - dv * j);
            }
        }

        int[] ia = new int[I * J * 6];
        int idx = 0;
        for (int j = 0; j < J; ++j)
        {
            for (int i = 0; i < I; ++i)
            {
                int i0 = i + j*(I+1);
                ia[idx++] = i0;
                ia[idx++] = i0 + 1;
                ia[idx++] = i0 + I + 1;
                ia[idx++] = i0 + I + 1;
                ia[idx++] = i0 + 1;
                ia[idx++] = i0 + I + 1 + 1;
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = pos;
        mesh.uv = uv;
        mesh.triangles = ia;

        go.GetComponent<MeshFilter>().mesh = mesh;
    }

    private static Matrix4x4 PerspectiveOffCenter(float left, float right, float bottom, float top, float near, float far)
    {
        float x = 2.0F * near / (right - left);
        float y = 2.0F * near / (top - bottom);
        float a = (right + left) / (right - left);
        float b = (top + bottom) / (top - bottom);
        float c = -(far + near) / (far - near);
        float d = -(2.0F * far * near) / (far - near);
        float e = -1.0F;
        Matrix4x4 m = new Matrix4x4();
        m[0, 0] = x;
        m[0, 1] = 0;
        m[0, 2] = a;
        m[0, 3] = 0;
        m[1, 0] = 0;
        m[1, 1] = y;
        m[1, 2] = b;
        m[1, 3] = 0;
        m[2, 0] = 0;
        m[2, 1] = 0;
        m[2, 2] = c;
        m[2, 3] = d;
        m[3, 0] = 0;
        m[3, 1] = 0;
        m[3, 2] = e;
        m[3, 3] = 0;
        return m;
    }
}
