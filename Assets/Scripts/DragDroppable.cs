using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDroppable : MonoBehaviour
{
    Vector3 mousePosOffset;

    string originalTag;

    private Vector3 GetMouseWorldPos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    private void OnMouseDown()
    {
        mousePosOffset = gameObject.transform.position - GetMouseWorldPos();
        this.originalTag = gameObject.tag;
        gameObject.tag = "PickedUp";
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mousePosOffset;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    private void OnMouseUp()
    {
        gameObject.tag = this.originalTag;
    }
}
