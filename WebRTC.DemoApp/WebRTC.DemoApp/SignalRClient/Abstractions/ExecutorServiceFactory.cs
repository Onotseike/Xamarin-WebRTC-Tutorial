using System;
using System.Collections.Concurrent;
using System.Threading;

namespace WebRTC.DemoApp.SignalRClient.Abstractions
{
    #region IExecutor Interface

    public interface IExecutor
    {
        #region Method(s)

        bool IsCurrentExecutor { get; }
        void Execute(Action _action);

        #endregion
    }
    #endregion

    #region IExecutorService Interface 

    public interface IExecutorService : IExecutor
    {
        #region Method(s)

        void Release();

        #endregion
    }
    #endregion

    public static class ExecutorServiceFactory
    {
        public static IExecutor MainExecutor { get; set; }
        public static IExecutorService CreateExecutorService(string tag) => new ExecuteService(tag);


        private class ExecuteService : IExecutorService
        {
            private readonly BlockingCollection<Action> _jobs = new BlockingCollection<Action>();
            private readonly CancellationTokenSource _cts = new CancellationTokenSource();
            private readonly Thread _thread;
            private readonly string _tag;

            public ExecuteService(string tag)
            {
                _tag = tag;
                _thread = new Thread(OnStart)
                {
                    IsBackground = true
                };
                _thread.Start();
            }

            public bool IsCurrentExecutor => _thread == Thread.CurrentThread;
            public void Execute(Action action)
            {
                _jobs.Add(action);
            }

            public void Release()
            {
                _cts.Cancel();
            }

            private void OnStart()
            {
                try
                {
                    using (_jobs)
                    {
                        foreach (var job in _jobs.GetConsumingEnumerable(_cts.Token))
                        {
                            Console.WriteLine("Executing job - {0}", _tag);
                            job();
                            Console.WriteLine($"Job {_tag} has been executed");
                        }
                    }
                }
                catch (OperationCanceledException)
                {

                }
            }
        }
    }
}
