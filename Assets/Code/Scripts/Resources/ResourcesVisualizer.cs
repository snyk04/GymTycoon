using System.Globalization;
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
            moneyText.text = FormatNumber(resourcesHolder.GetResource(ResourceType.Money));
            diamondsText.text = FormatNumber(resourcesHolder.GetResource(ResourceType.Diamonds));
        }
        
        private string FormatNumber(float number)
        {
            if (number < 1000)
            {
                return number.ToString(CultureInfo.InvariantCulture);
            }
            
            string[] suffixes = { "", "K", "M", "B", "T" };

            var suffixIndex = 0;
            var formattedNumber = number;

            while (formattedNumber >= 1000f && suffixIndex < suffixes.Length - 1)
            {
                formattedNumber /= 1000f;
                suffixIndex++;
            }

            return formattedNumber.ToString(CultureInfo.InvariantCulture) + suffixes[suffixIndex];
        }
    }
}