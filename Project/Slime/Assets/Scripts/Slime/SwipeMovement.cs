using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slime
{
    public class SwipeMovement : MonoBehaviour
    {
        /// <summary>
        /// The start position for a swipe.
        /// </summary>
        Vector2 startPos;
        /// <summary>
        /// The end position of a swipe.
        /// </summary>
        Vector2 endPos;

        public delegate void Swiped(Vector3 start, Vector3 end);

        public Swiped OnSwipe;

        private void Update()
        {
            // Set the Positions
            if (Input.GetButtonDown("Fire1"))
            {
                startPos = Input.mousePosition;

                if (Input.touches.Length > 0)
                {
                    startPos = Input.GetTouch(0).position;
                }
            }
            if (Input.GetButtonUp("Fire1"))
            {
                endPos = Input.mousePosition;

                if (Input.touches.Length > 0)
                {
                    endPos = Input.GetTouch(0).position;
                }

                // If distance is less than two than this is just a tap.
                if (Vector2.Distance(endPos, startPos) > 2)
                {
                    if (OnSwipe != null)
                    {
                        OnSwipe.Invoke(startPos, endPos);
                    }
                }
            }
        }
    }
}