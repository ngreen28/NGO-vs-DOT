using UnityEditor;
using UnityEngine;
using System.IO;
using System.Reflection;
using UnityEditorInternal;

public class EnhancedProfilerDataExporter
{
    [MenuItem("Tools/Export Enhanced Profiler Data")]
    public static void ExportEnhancedData()
    {
        if (!ProfilerDriver.enabled)
        {
            Debug.LogError("Profiler is not enabled. Please start profiling first.");
            return;
        }

        string outputPath = EditorUtility.SaveFilePanel("Save Enhanced Profiler Data", "", "ProfilerEnhancedExport.csv", "csv");
        if (string.IsNullOrEmpty(outputPath)) return;

        // Preparar métodos via reflection
        var getNameMethod = GetHiddenMethod<ProfilerFrameDataIterator>("GetName");
        var getCategoryMethod = GetHiddenMethod<ProfilerFrameDataIterator>("GetCategory");

        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            writer.WriteLine("Frame,Depth,DurationMS,Name,Category");

            int firstFrame = ProfilerDriver.firstFrameIndex;
            int lastFrame = ProfilerDriver.lastFrameIndex;

            for (int frame = firstFrame; frame <= lastFrame; frame++)
            {
                var iterator = new ProfilerFrameDataIterator();
                iterator.SetRoot(frame, 0);

                while (iterator.Next(true))
                {
                    try
                    {
                        float duration = iterator.durationMS;
                        int depth = iterator.depth;
                        string name = getNameMethod?.Invoke(iterator, null) as string ?? "Unknown";
                        string category = getCategoryMethod?.Invoke(iterator, null) as string ?? "Unknown";

                        writer.WriteLine($"{frame},{depth},{duration:F4},\"{name}\",\"{category}\"");
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogWarning($"Error processing frame {frame}: {e.Message}");
                        continue;
                    }
                }
            }
        }

        Debug.Log($"✅ Enhanced profiler data exported to {outputPath}");
    }

    private static MethodInfo GetHiddenMethod<T>(string methodName)
    {
        try
        {
            return typeof(T).GetMethod(methodName, 
                BindingFlags.Instance | BindingFlags.NonPublic);
        }
        catch
        {
            Debug.LogWarning($"Could not access {methodName} method via reflection");
            return null;
        }
    }
}