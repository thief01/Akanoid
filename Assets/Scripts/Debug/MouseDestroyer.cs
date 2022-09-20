using System;
using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEditor;
using UnityEngine;

public class MouseDestroyer : MonoBehaviour
{
    [SerializeField] private float size;
    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, size);
            for (int i = 0; i < colliders.Length; i++)
            {
                Brick brick = colliders[i].gameObject.GetComponent<Brick>();
                if (brick != null)
                {
                    brick.DestroyBlock();
                }
            }
        }
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        Handles.DrawWireDisc(pos, Vector3.forward, size );
    }
    #endif
}
