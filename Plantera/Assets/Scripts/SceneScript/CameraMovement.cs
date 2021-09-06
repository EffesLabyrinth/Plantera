using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    LevelInfo levelInfo;
    Transform target;
    float smoothValue;
    Camera cameraCurrent;
    float zoomSpeed;
    float bossFightViewSize;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        levelInfo = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelInfo>();
        cameraCurrent = Camera.main;
        zoomSpeed = 1;
        bossFightViewSize = 6;
        smoothValue = 5;
        transform.position = target.position;
    }
    private void FixedUpdate()
    {
        if (target)
        {
            Vector2 newPos = SmoothFollow();

            if (newPos.x < levelInfo.borderL) newPos.Set(levelInfo.borderL, newPos.y);
            else if (newPos.x > levelInfo.borderR) newPos.Set(levelInfo.borderR, newPos.y);

            if (newPos.y > levelInfo.borderT) newPos.Set(newPos.x, levelInfo.borderT);
            else if (newPos.y < levelInfo.borderB) newPos.Set(newPos.x, levelInfo.borderB);
            
            transform.position = new Vector3(newPos.x,newPos.y,-10);
        }
    }
    public Vector2 SmoothFollow()
    {

        return Vector2.Lerp(transform.position, target.position, smoothValue * Time.deltaTime);
    }
    public void StartBossFightView()
    {
        StopAllCoroutines();
        StartCoroutine(BossFightView());
    }
    public void StartNormalView()
    {
        StopAllCoroutines();
        StartCoroutine(NormalView());
    }
    IEnumerator BossFightView()
    {
        while (cameraCurrent.orthographicSize < bossFightViewSize)
        {
            cameraCurrent.orthographicSize += Time.deltaTime * zoomSpeed;
            yield return null;
        }
        cameraCurrent.orthographicSize = bossFightViewSize;
    }
    IEnumerator NormalView()
    {
        while (cameraCurrent.orthographicSize > 5)
        {
            cameraCurrent.orthographicSize -= Time.deltaTime * (zoomSpeed/2f);
            yield return null;
        }
        cameraCurrent.orthographicSize = 5;
    }
}
