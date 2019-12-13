using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slime
{
    public class SetActiveOnClick : MonoBehaviour
    {
        public SlimeBehaviour slime;

        private void Start()
        {
            if (slime == null)
                slime = GetComponent<SlimeBehaviour>();
        }

        private void OnMouseUp()
        {
            FindObjectOfType<SlimeManager>().SetSelected(slime);
        }
    }
}