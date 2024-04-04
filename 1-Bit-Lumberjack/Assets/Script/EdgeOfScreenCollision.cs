using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EdgeOfScreenCollision : MonoBehaviour
{
    [SerializeField] float colDepth = 1.0f;
    Vector2 screenSize;
    Transform topCollider;
    Transform bottomCollider;
    Transform leftCollider;
    Transform rightCollider;

    Vector3 camPos;

    private void Start()
    {
        GenerateCollider();
    }

    void GenerateCollider()
    {
        //Generat Empty Object
        topCollider = new GameObject("top").transform;
        bottomCollider = new GameObject("bottom").transform;
        leftCollider = new GameObject("left").transform;
        rightCollider = new GameObject("right").transform;

        //Set Parent
        topCollider.SetParent(transform);
        bottomCollider.SetParent(transform);
        leftCollider.SetParent(transform);
        rightCollider.SetParent(transform);

        //Add Collider
        topCollider.AddComponent<BoxCollider2D>();
        bottomCollider.AddComponent<BoxCollider2D>();
        leftCollider.AddComponent<BoxCollider2D>();
        rightCollider.AddComponent<BoxCollider2D>();

        //Generat World Space Point Information
        camPos = Camera.main.transform.position;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0f))) * 0.5f;
        screenSize.y = Vector3.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0f, Screen.height))) * 0.5f;

        //Chae Scale And Positon
        rightCollider.localScale = new Vector3(colDepth, screenSize.y * 2, colDepth);
        rightCollider.position = new Vector3(camPos.x + screenSize.x + (rightCollider.localScale.x * 0.5f), camPos.y, 0f);

        leftCollider.localScale = new Vector3(colDepth, screenSize.y * 2, colDepth);
        leftCollider.position = new Vector3(camPos.x - screenSize.x - (leftCollider.localScale.x * 0.5f), camPos.y, 0f);

        topCollider.localScale = new Vector3(screenSize.x * 2, colDepth, colDepth);
        topCollider.position = new Vector3(camPos.x, camPos.y + screenSize.y + (topCollider.localScale.y * 0.5f), 0f);

        bottomCollider.localScale = new Vector3(screenSize.x * 2, colDepth, colDepth);
        bottomCollider.position = new Vector3(camPos.x, camPos.y - screenSize.y - (bottomCollider.localScale.y * 0.5f), 0f);
    }

}
