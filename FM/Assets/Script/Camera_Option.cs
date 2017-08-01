using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Option : MonoBehaviour {

    //카메라 옵션 0 1 2 
    // 해당 옵션일 때, 필요한 인자, 
    //메시지를 받으면 적용됨
    //두개의 오브젝트 사이를 비추는 옵션
    //왼쪽 오른쪽 방향을 주어 d값만큼 그쪽을 보는 옵션

    Vector3 firstCamera;

    Vector3 playerPosition;
    Vector3 otherPosition;
    Vector3 cDir;
    float moveSize;
    enum CameraOption
    {
        Nomal = 0,
        Center,
        Forward
    }

    int cameraOption;
    // Use this for initialization
    void Start()
    {
        cameraOption = (int)CameraOption.Nomal;
        playerPosition = gameObject.transform.position;
        firstCamera = gameObject.transform.position;
    }

    public void ChangeCamera_Nomal(Vector3 playerPosition)
    {
        cameraOption = (int)CameraOption.Nomal;
        gameObject.transform.position = firstCamera;
    }

    public void ChangeCamera_Center(Vector3 playerPosition , Vector3 otherPosition)
    {
        cameraOption = (int)CameraOption.Center;
        Vector3 tmp; tmp.x = (otherPosition.x + playerPosition.x);
        tmp.x = tmp.x / 2;
        tmp.y = playerPosition.y + 1.7f;
        tmp.z = -10;
        gameObject.transform.position = tmp;
    }

    public void ChangeCamera_Forward(Vector3 playerPosition, Vector3 dir, float size)
    {
        cameraOption = (int)CameraOption.Forward;
        Vector3 tmp = playerPosition;
        tmp += cDir * moveSize;
        tmp.y += 1.7f;
        tmp.z = -10;
        gameObject.transform.position = tmp;
    }

    /*IEnumerator Nomal()
    {
        Vector3 tmp = playerPosition;
        tmp.z = -10;
        while ( gameObject.transform.position != playerPosition)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, tmp, Time.deltaTime * 1);
            yield return null;
        }
        yield break; 
    }

    IEnumerator Center()
    {
        Vector3 tmp = ( otherPosition + playerPosition );
        tmp = tmp / 2;
        tmp.z = -10;
        while (gameObject.transform.position != tmp)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, tmp, Time.deltaTime * 1);
            yield return null;
        }
        yield break;
    }
    IEnumerator Forward()
    {
        Vector3 tmp = playerPosition;
        tmp += cDir * moveSize;
        tmp.z = -10;
        while (gameObject.transform.position != tmp)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position,tmp, Time.deltaTime * 1);
            yield return null;
        }
        yield break;
    }*/

    private void Update()
    {
    }
}
