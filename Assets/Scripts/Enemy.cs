using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    private Vector3 endPosition;
    [SerializeField] private float speed = 2; 
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject losingUIParent;
    [SerializeField] private GameObject inGameParent;
    [SerializeField] private LevelManager levelManager;
    private List<Vector3> _positions;
    private int i = 0;
    public bool isChasingPlayer = false;
    private Vector3 targetDirection = new Vector3();

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position,endPosition,speed*Time.fixedDeltaTime);
        transform.LookAt(endPosition);
        if (Vector3.Distance(transform.position,endPosition) <= 0.01f){
            Debug.Log(i);
            i = (i+1)%_positions.Count;
            endPosition = _positions[i];
        }
        targetDirection = playerTransform.position - transform.position;
        if (isChasingPlayer && Vector3.Angle(targetDirection,transform.forward) <= 30f){
            CheckPlayerDetection();
        }
    }

    public void SetPositions(List<Vector3> positions){
        i = 0;
        _positions = positions;
    }

    public void SetEndPosition(Vector3 endPos){
        endPosition = endPos;
    }

    private void CheckPlayerDetection(){
        RaycastHit hit;
        Vector3 transformPosition = new Vector3(transform.position.x,.5f,transform.position.z);
        Vector3 newTargetDirection = new Vector3(targetDirection.x,.5f,targetDirection.z);
        if (Physics.Raycast(transform.position,newTargetDirection, out hit, 10) && hit.transform.tag == "Player"){
            losingUIParent.SetActive(true);
            inGameParent.SetActive(false);
            levelManager.DestroyLevels();
        }
    }
}
