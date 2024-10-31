using Build1.PostMVC.Core.MVCS.Commands;
using Build1.PostMVC.Unity.Firebase.Impl;

namespace Build1.PostMVC.Unity.Firebase.Commands
{
    public sealed class FirebaseInitializeAsyncCommand : Command
    {
        public override void Execute()
        {
            if (!FirebaseAdapter.Initialized && !FirebaseAdapter.Initializing)
                FirebaseAdapter.Initialize();
        }
    }
}