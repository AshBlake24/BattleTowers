namespace UnityEngine.UI.Extensions
{
    [RequireComponent(typeof(UI.Button))]
    public abstract class Button : MonoBehaviour
    {
        private UI.Button _button;

        private void Awake()
        {
            _button = GetComponent<UI.Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        protected abstract void OnButtonClick();
    }
}