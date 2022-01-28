using TMPro;
using UnityEngine;

namespace UI
{
    public class DaytimeView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI field;

        private void Awake()
        {
            field.text = "Midday";
        }
    }
}