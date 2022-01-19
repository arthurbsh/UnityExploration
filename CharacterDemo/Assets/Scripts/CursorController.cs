using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{

    const int MIN_X = 0 , MAX_X = 1920, MIN_Y = 0, MAX_Y = 1080;

    [SerializeField] float cursorSpeed = 1;
    [SerializeField] float xDelta = 0;
    [SerializeField] float yDelta = 0;

    [SerializeField] Sprite defaultCursor;

    [SerializeField] Sprite attackCursor;

    [SerializeField] Sprite lockCursor;

    Image image;


    Vector3 position;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        position = new Vector3(MAX_X/2, MAX_Y/2, 0);
        image = GetComponentInChildren<Image>();
        image.sprite = defaultCursor;
    }

    // Update is called once per frame
    void Update()
    {
        CursorMovement();
        CursorType();
    }

    void CursorMovement() {
        if (Input.GetAxis("MB0") > 0 || Input.GetAxis("MB1") > 0) {
            return;
        }

        // move according to mouse movement
        float xAxis = Input.GetAxis("Mouse X") * cursorSpeed;
        float yAxis = Input.GetAxis("Mouse Y") * cursorSpeed;
        position.x += xAxis;
        position.y += yAxis;

        //clamp x position
        if (position.x < MIN_X) {
            position.x = MIN_X;
        } else if (position.x > MAX_X) {
            position.x = MAX_X;
        }
        
        //clamp y position
        if (position.y < MIN_Y) {
            position.y = MIN_Y;
        } else if (position.y > MAX_Y) {
            position.y = MAX_Y;
        }

        transform.position = new Vector3(position.x + xDelta, position.y + yDelta, 0);
    }

    void CursorType() {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(position);
        string hitTag = "TERRAIN";
         if(Physics.Raycast(ray, out hit))
         {
             
                 hitTag = hit.collider.name;
         }

        //print(hitTag);

        Sprite newImage = defaultCursor;

        switch (hitTag) {
            case "Terrain":
                newImage = defaultCursor;
                break;
            case "Enemy":
                newImage = attackCursor;
                break;
            default:
                break; 
        }
        
        if (newImage != image.sprite) {
            image.sprite = newImage;
        }
    }

}
