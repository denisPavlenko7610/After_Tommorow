using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] float xRotation;
    [SerializeField] float xSensitivity = 30f;
    [SerializeField] float ySensitivity = 30f;
    [SerializeField] float clampXRotation = 80f;
    
    public void MouseLook(Vector2 input)
    {
        var mouseX = input.x;
        var mouseY = input.y;
        xRotation -= mouseY * Time.deltaTime * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -clampXRotation, clampXRotation);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation,0,0);
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime * xSensitivity));
    }
}
