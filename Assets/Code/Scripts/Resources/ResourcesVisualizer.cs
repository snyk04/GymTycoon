using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Scripts.Resources
{
    public sealed class ResourcesVisualizer : MonoBehaviour
    {
        [SerializeField] private TMP_Text moneyText;
        [SerializeField] private TMP_Text diamondsText;

        private ResourcesHolder resourcesHolder;

        [Inject]
        private void Construct(ResourcesHolder resourcesHolder)
        {
            this.resourcesHolder = resourcesHolder;
        }
        
        private void Update()
        {
            moneyText.text = resourcesHolder.GetResource(ResourceType.Money).ToString();
            diamondsText.text = resourcesHolder.GetResource(ResourceType.Diamonds).ToString();
        }
    }
}