using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Reporting;

namespace ProjectBuild
{
    public class BuildScript
    {
        [MenuItem("Build/WEB GL Build")]
        public static void MyBuild()
        {
            Debug.Log( "Starting Build . . ." );

            // var editorBuildSettings = PlayerSettings.settings
            // var buildPlayerOptions = new BuildPlayerOptions();
            // BuildPlayerWindow.DefaultBuildMethods.GetBuildPlayerOptions(buildPlayerOptions);

            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            var scenes = new List<string>();
            foreach ( var scene in EditorBuildSettings.scenes )
            {
                Debug.Log(scene.path);
                scenes.Add( $"{ scene.path }" );
            }
            buildPlayerOptions.scenes = scenes.ToArray();
            buildPlayerOptions.locationPathName = $"{Application.dataPath.Replace("Assets", "")}builds/WebGl";
            buildPlayerOptions.target = BuildTarget.WebGL;
            buildPlayerOptions.options = BuildOptions.None;

            Debug.Log( "Build Options creating . . . " );
            Debug.Log( $"Saving buidl to {buildPlayerOptions.locationPathName}" );

            BuildReport report = BuildPipeline.BuildPlayer( buildPlayerOptions );
            BuildSummary summary = report.summary;

            if (summary.result == BuildResult.Succeeded)
            {
                Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
            }

            if (summary.result == BuildResult.Failed)
            {
                Debug.Log("Build failed");
            }
        }
    }
}
