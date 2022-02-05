using TMPro;
using UnityEngine;

namespace UI
{
    public class TodoListElement : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        public TextMeshProUGUI Text => text;
    }
}