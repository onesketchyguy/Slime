using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slime
{
    public class ScaleAroundParent : MonoBehaviour
    {
        private Vector3 scale = Vector3.one;

        private void Update()
        {
            transform.localPosition = Vector3.zero;

            if (transform.parent.localScale.z < 1f)
            {
                transform.localScale = Vector3.one * 1 + (scale - transform.parent.localScale);
            }
            else
            {
                transform.localScale = Vector3.one;
            }
        }
    }
}