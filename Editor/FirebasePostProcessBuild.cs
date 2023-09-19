#if UNITY_IOS

using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

namespace Build1.PostMVC.Unity.Firebase.Editor
{
    internal static class FirebasePostProcessBuild
    {
        [PostProcessBuild(999)]
        private static void OnPostProcessBuild(BuildTarget buildTarget, string buildPath)
        {
            var plist = new PlistDocument();
            var filePath = Path.Combine(buildPath, "Info.plist");
            plist.ReadFromFile(filePath);

            var rootDict = plist.root;
            rootDict.SetBoolean("FirebaseAutomaticScreenReportingEnabled", false);
            plist.WriteToFile(filePath);
        }
    }
}

#endif