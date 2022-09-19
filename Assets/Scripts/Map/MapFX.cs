using System;
using System.Collections;
using System.Collections.Generic;
using Patterns;
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
            StartCoroutine(DelayRemoveAnimation());
        }

        private IEnumerator DelayRemoveAnimation()
        {
            yield return new WaitForSeconds(0.1f);
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Null"))
            {
                Pool<MapFX>.Instance.BackToPool(this);
            }
        }
    }
}