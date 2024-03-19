using System;
using System.Threading;
using System.Threading.Tasks;
using Code.Scripts.Save.Interfaces;
using UnityEngine;
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
                try
                {
                    while (!cancellationTokenSource.IsCancellationRequested)
                    {
                        await gameSaveManager.SaveAsync();
                        await Task.Delay(autoSaveDelay, cancellationTokenSource.Token);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
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