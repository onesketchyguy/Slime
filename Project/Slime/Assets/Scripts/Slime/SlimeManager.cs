using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slime
{
    public class SlimeManager : MonoBehaviour
    {
        private List<SlimeBehaviour> Slimes = new List<SlimeBehaviour>();

        float maxSize = 1;
        internal float minScale = 0.1f;

        private void Awake()
        {
            var slimes = FindObjectsOfType<SlimeBehaviour>();

            maxSize = 0;
            foreach (var item in slimes)
            {
                Slimes.Add(item);

                maxSize += item.transform.localScale.magnitude;
            }
        }

        public SlimeBehaviour GetActiveSlime()
        {
            foreach (var item in Slimes)
            {
                if (item.isActiveSlime)
                    return item;
            }

            return null;
        }

        public void SetSelected(SlimeBehaviour slime)
        {
            foreach (var item in Slimes)
                item.isActiveSlime = item == slime;
        }

        public void Add(SlimeBehaviour slime)
        {
            if (!Slimes.Contains(slime))
            {
                Slimes.Add(slime);
            }
        }

        public void Remove(SlimeBehaviour slime)
        {
            if (Slimes.Contains(slime))
            {
                Destroy(slime.gameObject);
                Slimes.Remove(slime);
            }

            CorrectScale();
        }

        private float GetTotalSlimesScale()
        {
            var value = 0f;

            foreach (var item in Slimes)
            {
                value += item.transform.localScale.magnitude;
            }

            return value;
        }

        void CorrectScale()
        {
            var missingScale = maxSize - GetTotalSlimesScale();

            GetActiveSlime().transform.localScale = Vector3.one * missingScale;
        }
    }
}