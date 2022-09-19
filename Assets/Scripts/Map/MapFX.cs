using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{

    public enum AnimationType
    {
        explodeBrick,
        freeBall
    }

    public class MapFX : MonoBehaviour
    {
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void PlayAnimation(AnimationType animationType)
        {
            animator.SetInteger("AnimationType", (int)animationType);
            animator.SetTrigger("Play");
        }
    }
}