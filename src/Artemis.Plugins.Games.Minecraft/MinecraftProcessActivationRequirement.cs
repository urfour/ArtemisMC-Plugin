using Artemis.Core.Modules;
using System;
using System.Diagnostics;
using System.Linq;
using System.Management;

public class MinecraftProcessActivationRequirement : IModuleActivationRequirement
{
    public MinecraftProcessActivationRequirement() { }

    public bool Evaluate()
    {
        var processes1 = Process.GetProcessesByName("javaw");
        var processes2 = Process.GetProcessesByName("java");
        var processes = processes1.Concat(processes2);

        return processes.Any(proc => GetCommandLine(proc).ToLower().Contains("minecraft"));
    }

    public string GetUserFriendlyDescription()
    {
        string text = "Requirement met when Minecraft is running";
        return text;
    }

    static string GetCommandLine(Process process)
    {
        string commandLine = "";
        if (OperatingSystem.IsWindows())
        {
            using (var searcher = new ManagementObjectSearcher(
                "SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + process.Id))
            {
                foreach (var @object in searcher.Get())
                {
                    commandLine += @object["CommandLine"] + " ";
                }
            }
        }
        return commandLine.Trim();
    }
}