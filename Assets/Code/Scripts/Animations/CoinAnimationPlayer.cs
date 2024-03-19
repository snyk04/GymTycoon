using System.Threading;
using System.Threading.Tasks;
using Code.Scripts.Utils;
using Code.Scripts.Zones.Events;
using UnityEngine;
using Zenject;

namespace Code.Scripts.Animations
{
    public sealed class CoinAnimationPlayer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer coinPrefab;
        [SerializeField] private float coinLifetimeInSeconds;
        [SerializeField] private float coinGoalOffset;

        private EventBus eventBus;

        private readonly CancellationTokenSource cts = new();

        [Inject]
        private void Construct(EventBus eventBus)
        {
            this.eventBus = eventBus;
            
            eventBus.Subscribe<ZoneProducedResourceEvent>(HandleZoneProducedResourceEvent);
        }

        private void OnDestroy()
        {
            eventBus.Unsubscribe<ZoneProducedResourceEvent>(HandleZoneProducedResourceEvent);
            cts.Cancel();
        }

        private void HandleZoneProducedResourceEvent(ZoneProducedResourceEvent @event)
        {
            var coin = Instantiate(coinPrefab, @event.ZonePosition + Vector3.up, Quaternion.identity);
            HideAndMoveCoin(coin).CatchAndLog();
        }

        private async Task HideAndMoveCoin(SpriteRenderer coin)
        {
            var goalColor = coin.color;
            goalColor.a = 0;
            var changeColorTask = Lerper.To(
                coin.color,
                value => coin.color = value, 
                goalColor, 
                coinLifetimeInSeconds,
                cts.Token
                );
            var changePositionTask = Lerper.To(
                coin.transform.position, 
                value => coin.transform.position = value, 
                coin.transform.position + Vector3.up * coinGoalOffset,
                coinLifetimeInSeconds,
                cts.Token
                );

            await Task.WhenAll(changeColorTask, changePositionTask);

            if (!cts.IsCancellationRequested)
            {
                Destroy(coin.gameObject);
            }
        }
    }
}