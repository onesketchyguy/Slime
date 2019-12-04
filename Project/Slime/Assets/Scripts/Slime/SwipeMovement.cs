﻿using System.Collections;
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

        public Rigidbody2D rigidbody;

        private void OnGUI()
        {
            GUILayout.Label(startPos.ToString());
            GUILayout.Label(endPos.ToString());
        }

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

                // Toss the slime
                var force = endPos - startPos;

                rigidbody.AddForce(force);
            }
        }
    }
}