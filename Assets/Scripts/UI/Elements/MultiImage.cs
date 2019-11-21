using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Elements
{
    public class MultiImage : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private List<Color> colors;

        public void ChangeColor(int index)
        {
            if (colors == null || index >= colors.Count)
            {
                Debug.LogError("Wrong Index for multiImage method.");
                return;
            }
            image.color = colors[index];
        }
    }
}
