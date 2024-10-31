using Build1.PostMVC.Core.MVCS.Commands;
using Build1.PostMVC.Unity.Firebase.Impl;

namespace Build1.PostMVC.Unity.Firebase.Commands
{
    public sealed class FirebaseWaitCommand : Command
    {
        public override void Execute()
        {
            if (FirebaseAdapter.Initialized)
                return;

            Retain();
            
            FirebaseAdapter.OnInitialized += FirebaseAdapterOnOnInitialized;
        }
        
        private void FirebaseAdapterOnOnInitialized()
        {
            FirebaseAdapter.OnInitialized -= FirebaseAdapterOnOnInitialized;

            Release();
        }
    }
}