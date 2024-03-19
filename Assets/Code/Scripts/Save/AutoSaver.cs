using System;
using System.Threading;
using System.Threading.Tasks;
using Code.Scripts.Save.Interfaces;
using Zenject;

namespace Code.Scripts.Save
{
    public class AutoSaver : IInitializable, IDisposable
    {
        private readonly TimeSpan autoSaveDelay = TimeSpan.FromSeconds(1);
        
        private readonly IGameSaveManager gameSaveManager;

        private readonly CancellationTokenSource cancellationTokenSource = new();
        
        public AutoSaver(IGameSaveManager gameSaveManager)
        {
            this.gameSaveManager = gameSaveManager;
        }

        public void Initialize()
        {
            Task.Run(async () =>
            {
                while (!cancellationTokenSource.IsCancellationRequested)
                {
                    await Task.Delay(autoSaveDelay);
                    await gameSaveManager.SaveAsync();
                }
            });
        }

        public void Dispose()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}