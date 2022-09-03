using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PerspectivePan : MonoBehaviour
{
    private Vector3 touchStart;
    public Camera cam;
    public float groundZ = 0;
   

    // Update is called once per frame
    private void Update()
    {
        if (Globals.alert == false && Globals.buildRoom == false)
        {
            if(EventSystem.current.IsPointerOverGameObject() || EventSystem.current.currentSelectedGameObject != null)
            {
                return;
            }
            if (Input.GetMouseButtonDown(0))
                {
                    touchStart = GetWorldPosition(groundZ);
                }

            if (Input.GetMouseButton(0))
                {

                Vector3 direction = touchStart - GetWorldPosition(groundZ);
                Camera.main.transform.position += direction;
                }
            }
    }

    private Vector3 GetWorldPosition(float z)
    {
        Ray mousePos = cam.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, new Vector3(0, 0, z));
        float distance;
        ground.Raycast(mousePos, out distance);

        return mousePos.GetPoint(distance);
    }

    public void setCamera()
    {
        Vector3 doorCamPosition = new Vector3();
        doorCamPosition.x = cam.transform.position.x;
        doorCamPosition.z = cam.transform.position.z;
        doorCamPosition.y = cam.transform.position.y - 40f;

        cam.transform.position = doorCamPosition;
    }
}
