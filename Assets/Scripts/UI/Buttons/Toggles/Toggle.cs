namespace UnityEngine.UI.Extensions
{
    [RequireComponent(typeof(UI.Toggle))]
    public abstract class Toggle : MonoBehaviour
    {
        [SerializeField] protected Image EnabledSprite;
        [SerializeField] protected Image DisabledSprite;

        protected UI.Toggle ToggleComponent;

        private void Awake()
        {
            ToggleComponent = GetComponent<UI.Toggle>();
        }

        protected virtual void OnEnable()
        {
            ToggleComponent.onValueChanged.AddListener(OnToggleClick);
        }

        protected virtual void OnDisable()
        {
            ToggleComponent.onValueChanged.RemoveListener(OnToggleClick);
        }

        protected virtual void Enable()
        {
            DisabledSprite.gameObject.SetActive(false);
            EnabledSprite.gameObject.SetActive(true);
        }

        protected virtual void Disable()
        {
            EnabledSprite.gameObject.SetActive(false);
            DisabledSprite.gameObject.SetActive(true);
        }

        protected abstract void OnToggleClick(bool toggleState);
    }
}