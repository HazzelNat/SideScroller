using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    
    public playerController controller;
    public Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    

    private void FixedUpdate() 
    {
        
    }
}
