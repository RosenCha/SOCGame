using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform cameraTransform;
    GameManager gameManager;
    static Vector3 zero = Vector3.zero;
    //镜头移动速度
    public float speed;
    //当前镜头距离初始位置的距离
    public float distance;
    //镜头最大移动距离
    public float maxDistance;
    public Transform CameraFollow;
    Vector3 CameraPos;
    void Start()
    {
        cameraTransform = gameObject.GetComponent<Transform>();
        speed = 20f;
        //zero = new Vector3(0, 0, 0);
        maxDistance = 20;
        gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        //mouse = GameObject.FindGameObjectWithTag("mouse");
    }

    // Update is called once per frame
    void Update()
    {
        CameraMoveNew();
    }

    void CameraFollowPlayer()
    {

    }
    void CameraMoveNew()
    {
        if (CameraFollow != null)
        {
            CameraPos = CameraFollow.position;
        }

        if (cameraTransform.position != CameraPos)
        {
            cameraTransform.position = new Vector3(CameraFollow.position.x+ speed * Time.deltaTime * Input.GetAxis("Mouse X"), CameraFollow.position.y+ speed * Time.deltaTime * Input.GetAxis("Mouse Y"), -10);
        }

    }
    void CameraMove()
    {
        Vector3 basicPos = new Vector3(CameraFollow.position.x, CameraFollow.position.y, 0);
        if (Input.GetAxis("Mouse X") > 0)
        {
            Vector3 temp = new Vector3(cameraTransform.localPosition.x + speed * Time.deltaTime * Input.GetAxis("Mouse X"), cameraTransform.localPosition.y, cameraTransform.position.z);
            if ((temp - zero).magnitude < maxDistance)
            {
                cameraTransform.localPosition = temp + basicPos;
            }
        }
        if (Input.GetAxis("Mouse X") < 0)
        {
            Vector3 temp = new Vector3(cameraTransform.localPosition.x + speed * Time.deltaTime * Input.GetAxis("Mouse X"), cameraTransform.localPosition.y, cameraTransform.position.z);
            if ((temp - zero).magnitude < maxDistance)
            {
                cameraTransform.localPosition = temp + basicPos;
            }
        }
        if (Input.GetAxis("Mouse Y") > 0)
        {
            Vector3 temp = new Vector3(cameraTransform.localPosition.x, cameraTransform.localPosition.y + speed * Time.deltaTime * Input.GetAxis("Mouse Y"), cameraTransform.position.z);
            if ((temp - zero).magnitude < maxDistance)
            {
                cameraTransform.localPosition = temp + basicPos;
            }
        }
        if (Input.GetAxis("Mouse Y") < 0)
        {
            Vector3 temp = new Vector3(cameraTransform.localPosition.x, cameraTransform.localPosition.y + speed * Time.deltaTime * Input.GetAxis("Mouse Y"), cameraTransform.position.z);
            if ((temp - zero).magnitude < maxDistance)
            {
                cameraTransform.localPosition = temp + basicPos;
            }
        }
    }

}
