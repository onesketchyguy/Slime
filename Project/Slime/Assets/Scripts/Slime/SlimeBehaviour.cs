using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slime
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SlimeBehaviour : MonoBehaviour
    {
        private SwipeMovement movement;
        private SlimeManager manager;

        bool grounded;

        public GameObject slime;

        public bool isActiveSlime = true;

        private void Start()
        {
            movement = GetComponent<SwipeMovement>();
            movement.OnSwipe += OnSwipe;

            manager = FindObjectOfType<SlimeManager>();
            manager.Add(this);

            manager.SetSelected(this);
        }

        private void OnMouseUp()
        {
            manager.SetSelected(this);
        }

        void OnSwipe(Vector3 start, Vector3 end)
        {
            if (isActiveSlime == false || grounded == false) return;
            isActiveSlime = false;

            // Toss the slime
            var force = end - start;

            // Calculate the new scale of this slime.
            var scale = transform.localScale.magnitude / 3;

            var difference = GetDifference(start, end);
            difference = Mathf.Clamp(difference, manager.minScale, scale);

            transform.localScale = Vector3.one * (scale - difference);

            // Generate the spawn position
            var spawnPos = transform.position + force;
            spawnPos.x = Mathf.Clamp(spawnPos.x, transform.position.x - scale, transform.position.x + scale);
            spawnPos.y = Mathf.Clamp(spawnPos.y, transform.position.y - scale, transform.position.y + scale);

            var go = Instantiate(slime, spawnPos, Quaternion.identity);
            go.transform.localScale = Vector3.one * difference;
            go.GetComponent<Rigidbody2D>().AddForce(force);
        }

        float GetDifference(Vector3 start, Vector3 end)
        {
            var screenScale = Utility.Utilities.ScreenMax / 2;
            var x = 0f;
            var y = 0f;

            // Set the x and y values
            if (start.x > end.x)
                x = start.x - end.x;
            else
                x = end.x - start.x;

            if (start.y > end.y)
                y = start.y - end.y;
            else
                y = end.y - start.y;

            x = (screenScale.x - x) / screenScale.x;
            y = (screenScale.y - y) / screenScale.y;

            return (1 - x) + (1 - y);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Ground")
                grounded = true;

            var other = collision.gameObject.GetComponent<SlimeBehaviour>();

            if (other != null)
            {
                if (isActiveSlime)
                {
                    var theirScale = other.transform.localScale.magnitude / 3;
                    var myScale = transform.localScale.magnitude / 3;

                    transform.localScale = Vector3.one * (theirScale + myScale);

                    manager.Remove(other);
                }
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Ground")
                grounded = false;
        }
    }
}