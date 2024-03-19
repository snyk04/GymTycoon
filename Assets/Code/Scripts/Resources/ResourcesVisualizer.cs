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

        private string FormatNumber(long num)
        {
            var strNum = num.ToString().Replace("[^0-9.]", "");

            if (num < 1000)
            {
                return strNum;
            }

            var si = new[]
            {
                new { v = 1E3, s = "K" },
                new { v = 1E6, s = "M" },
                new { v = 1E9, s = "B" },
                new { v = 1E12, s = "T" },
                new { v = 1E15, s = "P" },
                new { v = 1E18, s = "E" }
            };

            int index;
            for (index = si.Length - 1; index > 0; index--)
            {
                if (num >= si[index].v)
                {
                    break;
                }
            }

            var result = num / si[index].v;

            return result.ToString("F2").TrimEnd('.') + si[index].s;
        }
    }
}