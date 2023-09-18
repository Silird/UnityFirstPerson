using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        XandY,
        X,
        Y
    }
    
    public RotationAxes axes = RotationAxes.XandY;
    public float rotationSpeedHor = 5.0f;
    public float rotationSpeedVer = 5.0f;

    public float maxVert = 60.0f;
    public float minVert = -35.0f;

    private float _rotationX;

    private void Update()
    {
        var objectTransform = transform;
        // Проверим ось движения
        if (axes == RotationAxes.XandY)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * rotationSpeedVer;
            _rotationX = Mathf.Clamp(_rotationX, minVert, maxVert);

            var delta = Input.GetAxis("Mouse X") * rotationSpeedHor;
            
            var rotationY = objectTransform.localEulerAngles.y + delta;

            objectTransform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
        else if (axes == RotationAxes.X)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * rotationSpeedHor, 0);
        }
        else if (axes == RotationAxes.Y)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * rotationSpeedVer;
            _rotationX = Mathf.Clamp(_rotationX, minVert, maxVert);

            var rotationY = transform.localEulerAngles.y;

            objectTransform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
    }
}
