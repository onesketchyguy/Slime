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

        private bool grounded;

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

        private void OnSwipe(Vector3 start, Vector3 end)
        {
            if (isActiveSlime == false || grounded == false) return;
            isActiveSlime = false;

            // Toss the slime
            var force = end - start;

            // Calculate the new scale of this slime.
            var scale = transform.localScale.z;

            var tossed = 1f;

            if (Vector3.Distance(transform.position, start) < 2f)
            {
                var pos = Camera.main.WorldToScreenPoint(transform.position);

                tossed = Mathf.Clamp(GetDifference(start, end, pos.x, pos.y), manager.minScale, scale + 0.03f);
            }
            else
            {
                tossed = Mathf.Clamp(GetDifference(start, end), manager.minScale, scale + 0.03f);
            }

            transform.localScale = Vector3.one * (scale - tossed);

            // Generate the spawn position
            var spawnPos = transform.position + (force * tossed);
            spawnPos.x = Mathf.Clamp(spawnPos.x, transform.position.x - scale / 2, transform.position.x + scale / 2);
            spawnPos.y = Mathf.Clamp(spawnPos.y, transform.position.y - scale / 2, transform.position.y + scale / 2);

            var go = Instantiate(slime, spawnPos, Quaternion.identity);
            go.transform.localScale = Vector3.one * tossed;
            go.GetComponent<Rigidbody2D>().AddForce(force);

            go.name = slime.name;

            manager.CleanUp();
        }

        private float GetDifference(Vector3 start, Vector3 end, float zeroX = 0, float zeroY = 0)
        {
            var screenScale = new Vector2(Screen.width, Screen.height);

            screenScale.x -= zeroX;
            screenScale.y -= zeroY;

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

            x = 1 - ((screenScale.x - x) / screenScale.x);
            y = 1 - ((screenScale.y - y) / screenScale.y);

            var val = ((x + y) / 2);

            return val;
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
                    var theirScale = other.transform.localScale.z;
                    var myScale = transform.localScale.z;

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