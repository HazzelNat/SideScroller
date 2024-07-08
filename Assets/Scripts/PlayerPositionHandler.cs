using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionHandler : MonoBehaviour
{
    [SerializeField] Vector2 playerCurrentPosition;
    [SerializeField] Vector2 currentCheckpointPosition;
    public TransformData playerPositionData;
    private TriggerEvent playerTriggerEvent;

    void Start()
    {
        playerTriggerEvent = GetComponent<TriggerEvent>();
    }
		
		//berguna untuk ketika Player Menabrak Checkpoint
    public void OnCheckpoint(GameObject col) 
    {
        Vector2 newCheckpointPosition = col.transform.position;
        currentCheckpointPosition = newCheckpointPosition;
        SavePosition(currentCheckpointPosition);

    }
		
		//2. berguna untuk ketika Player Menabrak Checkpoint
    public void OnTrap()
    {
        ChangePlayerPosition(currentCheckpointPosition);
    }

    //berguna ketika Player menabrak garis Finish
    public void OnFinish()
    {
        playerPositionData.ResetData();

    }
		//1. berguna untuk mengubah posisi player
    private void ChangePlayerPosition(Vector2 newPosition)
    {
        transform.position = newPosition;
    }
		//berguna untuk Load Position dari Scriptable object
    private void LoadPosition()
    {
        playerCurrentPosition = playerPositionData.position;
    }
		//berguna untuk Save Position ke Scriptable Object
    private void SavePosition(Vector2 newPosition)
    {
        playerPositionData.position = newPosition;
    }
}