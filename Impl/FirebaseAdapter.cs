using System;

#if UNITY_ANDROID && !UNITY_EDITOR
using Firebase;
using Firebase.Extensions;
#endif

namespace Build1.PostMVC.Unity.Firebase.Impl
{
    internal class FirebaseAdapter
    {
        public static bool Initialized  { get; private set; }
        public static bool Initializing { get; private set; }

        public static event Action OnInitialized;

        public static void Initialize()
        {
            if (Initialized)
                return;

            Initializing = true;
            
            #if UNITY_ANDROID && !UNITY_EDITOR

            FirebaseApp.CheckAndFixDependenciesAsync()
                       .ContinueWithOnMainThread(task =>
                        {
                            Complete();
                        });

            #else

            Complete();

            #endif
        }

        private static void Complete()
        {
            Initialized = true;
            Initializing = false;
            OnInitialized?.Invoke();
        }
    }
}