using System;
using Build1.PostMVC.Unity.App.Modules.Async;
using Build1.PostMVC.Unity.App.Modules.Logging;
using Firebase;
using Firebase.Auth;
using LogLevel = Build1.PostMVC.Unity.App.Modules.Logging.LogLevel;

namespace Build1.PostMVC.Unity.Firebase.Impl
{
    internal class FirebaseAdapter
    {
        private static readonly ILog Log = LogProvider.GetLog<FirebaseAdapter>(LogLevel.Warning);

        public static bool Initialized { get; private set; }

        public static event Action OnInitialized;

        public static void Initialize()
        {
            if (Initialized)
            {
                Log.Debug("Already initialized.");
                return;
            }

            Log.Debug("Initializing...");

            #if UNITY_ANDROID && !UNITY_EDITOR
            
            Log.Debug("Checking and fixing Android dependencies...");

            FirebaseApp.CheckAndFixDependenciesAsync().Resolve(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                    Log.Debug("Resolved.");
                else
                    Log.Error(ds => $"Could not resolve Firebase dependencies: {ds}", dependencyStatus);

                Complete();
            });

            #else

            Complete();

            #endif
        }

        private static void Complete()
        {
            try
            {
                //DON'T REMOVE. Firebase requires FirebaseAuth.DefaultInstance to be called before any get credential method invocation.
                var auth = FirebaseAuth.DefaultInstance;
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                throw;
            }

            Log.Debug("Initialized");

            Initialized = true;
            OnInitialized?.Invoke();
        }
    }
}