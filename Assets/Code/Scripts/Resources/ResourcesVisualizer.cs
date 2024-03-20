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

        private string FormatNumber(long number)
        {
            var stringNumber = number.ToString().Replace("[^0-9.]", "");

            if (number < 1000)
            {
                return stringNumber;
            }

            var suffixesAndNumbers = new[]
            {
                new { v = 1E3, s = "K" },
                new { v = 1E6, s = "M" },
                new { v = 1E9, s = "B" },
                new { v = 1E12, s = "T" },
                new { v = 1E15, s = "P" },
                new { v = 1E18, s = "E" }
            };

            int index;
            for (index = suffixesAndNumbers.Length - 1; index > 0; index--)
            {
                if (number >= suffixesAndNumbers[index].v)
                {
                    break;
                }
            }

            var result = number / suffixesAndNumbers[index].v;

            return result.ToString("F2").TrimEnd('.') + suffixesAndNumbers[index].s;
        }
    }
}