using System;
using UnityEngine;

namespace Patterns
{
    public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance
        {
            get
            {
                if (instace == null)
                {
                    instace = FindObjectOfType<T>();
                    if (instace == null)
                    {
                        GameObject g = new GameObject();
                        instace=g.AddComponent<T>();
                    }
                }

                return instace;
            }
        }

        private static T instace;

        private void OnDestroy()
        {
            instace = null;
        }
    }
}
