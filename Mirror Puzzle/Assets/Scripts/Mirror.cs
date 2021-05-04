using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mirror : MonoBehaviour
{
    private Node currentNode;
    private Node targetNode;

    private bool attached = false;

    private GameObject attachedMirror = null;
    enum MovingState{
        moving,
        arrived
    }

    MovingState movingState = MovingState.arrived;
    void Start()
    {
        Node node = GetNodeAtPosition(transform.localPosition);

        if(node != null){
            currentNode = node;
        }

    }

    Node GetNodeAtPosition(Vector2 pos){
        GameObject tile = GameObject.Find("Game").GetComponent<GameBoard>().board[(int)pos.x, (int)pos.y];

        if(tile!= null){
            return tile.GetComponent<Node>();
        }

        return null;
    }

    void Update()
    {
        if(movingState == MovingState.arrived){
            CheckInput();
        }

        if(movingState == MovingState.moving){
            if(targetNode == GetNodeAtPosition(transform.localPosition)){
                currentNode = targetNode;
                movingState = MovingState.arrived;
            }
        }

        if(attachedMirror != null){
            attachedMirror.transform.position = transform.position;
        }
    }

    void CheckInput(){
        Vector2 currentPosition = transform.position;
        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)){
            //ChangePosition(Vector2.up);
            currentPosition.y += 0.1f;
            transform.position = currentPosition;
        }
        if(Input.GetKey(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)){
            //ChangePosition(Vector2.down);
            currentPosition.y -= 0.1f;
            transform.position = currentPosition;
        }
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)){
            //ChangePosition(Vector2.left);
            currentPosition.x -= 0.1f;
            transform.position = currentPosition;
        }
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)){
            //ChangePosition(Vector2.right);
            currentPosition.x += 0.1f;
            transform.position = currentPosition;
        }
    }

    void ChangePosition(Vector2 d){
        Node temporaryNode = CanMove(d);
        if(temporaryNode != null){
            GameObject.Find("SoundManager").GetComponent<SoundManager>().PlayMoveSound();

            targetNode = temporaryNode;
            movingState = MovingState.moving;
            transform.DOMove(targetNode.transform.position, 0.5f, false);
        }
    }

    Node CanMove(Vector2 d){
        Node moveToNode = null;

        for (int i = 0; i < currentNode.neighbours.Length; i++)
        {
            if(currentNode.validDirections[i] == d){
                moveToNode = currentNode.neighbours[i];
                break;
            }
            
        }
        return moveToNode;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        attachedMirror = other.gameObject;
        attached = true;
        Debug.Log("Enter");
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        // attachedMirror = null;
        attached = false;
        Debug.Log("Exit");
    }


}
