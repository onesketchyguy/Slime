using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Slime
{
    public class SlimeManager : MonoBehaviour
    {
        private List<SlimeBehaviour> TotalSlimes = new List<SlimeBehaviour>();

        private float maxSize = 1;
        internal float minScale = 0.1f;

        private void Awake()
        {
            var slimes = FindObjectsOfType<SlimeBehaviour>();

            maxSize = 0;
            foreach (var item in slimes)
            {
                TotalSlimes.Add(item);

                maxSize += item.transform.localScale.z;
            }
        }

        public SlimeBehaviour GetActiveSlime()
        {
            foreach (var item in TotalSlimes)
            {
                if (item.isActiveSlime)
                    return item;
            }

            return TotalSlimes.FirstOrDefault();
        }

        public void SetSelected(SlimeBehaviour slime)
        {
            foreach (var item in TotalSlimes)
                item.isActiveSlime = item == slime;
        }

        public SlimeBehaviour Create()
        {
            return null;
        }

        public void Add(SlimeBehaviour slime)
        {
            if (!TotalSlimes.Contains(slime))
            {
                TotalSlimes.Add(slime);
            }
        }

        public void Remove(SlimeBehaviour slime)
        {
            if (TotalSlimes.Contains(slime))
            {
                Destroy(slime.gameObject);
                TotalSlimes.Remove(slime);
            }
        }

        private float GetTotalSlimesScale()
        {
            var value = 0f;

            foreach (var item in TotalSlimes)
            {
                if (item.isActiveSlime) continue;
                value += item.transform.localScale.z;
            }

            return value;
        }

        public void CleanUp()
        {
            var ToRemove = new List<SlimeBehaviour>();

            var scale = 0f;

            foreach (var item in TotalSlimes)
            {
                if (item.transform.localScale.z < minScale || item.transform.localScale.z > maxSize)
                {
                    scale += item.transform.localScale.z;
                    ToRemove.Add(item);
                }
            }

            // Correct the scale of the items.
            if (scale > 0)
            {
                if (TotalSlimes.Count > 1)
                    scale /= TotalSlimes.Count - 1;

                foreach (var item in TotalSlimes)
                    item.transform.localScale = item.transform.localScale + (Vector3.one * scale);
            }

            foreach (var item in ToRemove)
            {
                TotalSlimes.Remove(item);
                Destroy(item.gameObject);
            }
        }
    }
}