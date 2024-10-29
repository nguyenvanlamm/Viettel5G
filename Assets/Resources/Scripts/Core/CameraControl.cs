using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject parentModel;
    private float rotationSpeed = 500.0f;
    private Vector3 mouseWorldPosStart;
    private float zoomScale = 10.0f;
    private float maxFiledOfView = 160.0f;
    private float minFiledOfView = 0.0f;
    private float defaultFieldOfView = 60.0f;

    private void Update()
    {
        if ( Input.GetKey(KeyCode.Mouse0)) 
        {
            CamOrbit();
        }

        if (Input.GetKey(KeyCode.F))
        {
            FitToScreen();
        }

        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }
    private void CamOrbit()
    {
        if (Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0)
        {
            float verticalInput = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            float horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.right, -verticalInput);
            transform.Rotate(Vector3.up, horizontalInput, Space.World);
        }
    }

    private Bounds GetBound(GameObject praentGameObj)
    {
        Bounds bound = new Bounds(praentGameObj.transform.position, Vector3.zero);
        var rList = praentGameObj.GetComponentsInChildren(typeof(Renderer));
        foreach (Renderer r in rList)
        {
            bound.Encapsulate(r.bounds);
        }
        return bound;
    }
    private void FitToScreen()
    {
        Camera.main.fieldOfView = defaultFieldOfView;
        Bounds bound = GetBound(parentModel);
        Vector3 boundSize = bound.size;
        float boundDiagonal = Mathf.Sqrt((boundSize.x * boundSize.x) + (boundSize.y * boundSize.y) + (boundSize.z * boundSize.z));
        float camDistanceToBoundCentre = boundDiagonal / 2.0f / (Mathf.Tan(Camera.main.fieldOfView / 2.0f * Mathf.Deg2Rad));
        float camDistanceToBoundWithOffset = camDistanceToBoundCentre + boundDiagonal / 2.0f - (Camera.main.transform.position - transform.position).magnitude;
        transform.position = bound.center = (-transform.forward * camDistanceToBoundWithOffset);
    }
    private void Zoom(float zoomDiff)
    {
        if (zoomDiff != 0)
        {
            mouseWorldPosStart = GetPerspectivePos();
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - zoomDiff * zoomScale, minFiledOfView, maxFiledOfView);
            Vector3 mouseWorldPosDiff = mouseWorldPosStart - GetPerspectivePos();
            transform.position += mouseWorldPosDiff;
        }
    }
    public Vector3 GetPerspectivePos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(transform.forward, 0.0f);
        float dist;
        plane.Raycast(ray, out dist);
        return ray.GetPoint(dist);
    }
}
