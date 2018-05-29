using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {
    

    public Texture2D textureCursor;

    private CursorMode mode = CursorMode.ForceSoftware;
    private readonly Vector2 hotSpot = Vector2.zero;

    public GameObject cursorPoint;
    private GameObject instantiatedMouse;
    private Vector3 cursorPosition;
    private Transform player;


    void Awake () {

        player = GameObject.FindGameObjectWithTag("Player").transform;

	}

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Cursor.SetCursor(textureCursor, hotSpot, mode);

        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider is TerrainCollider)
                {
                    cursorPosition = hit.point;
                    cursorPosition.y = 0.25f;

                    if (instantiatedMouse == null)
                    {
                        instantiatedMouse = Instantiate(cursorPoint) as GameObject;
                        instantiatedMouse.transform.position = cursorPosition;

                    }
                    else
                    {
                        Destroy(instantiatedMouse);
                        instantiatedMouse = Instantiate(cursorPoint) as GameObject;
                        instantiatedMouse.transform.position = cursorPosition;
                    }


                }
            }

        }

        if (Vector3.Distance(player.position, cursorPosition) <= 1.0f){
            Destroy(instantiatedMouse);
        }

	}
}
