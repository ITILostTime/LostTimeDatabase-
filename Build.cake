#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0
#tool nuget:?package=GitVersion.CommandLine

using System;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;

//////////////////////////////////////////////
// Load / Add Script
//////////////////////////////////////////////

#load "./CakeFolder/CakeParameters.cake"
#load "./CakeFolder/LostTimeInformation.cake"

//////////////////////////////////////////////
// Argument
//////////////////////////////////////////////

// Target to launch the build
var target = Argument("target", "Default");

// Folder To Clean
public var CleanFolder = Argument("bin", "obj");

//  Argument pour parcourir les dossiers du projet
var configuration = Argument("configuration", "Release");

//  Fichier ReleaseNotes
public var releaseNotes = ParseReleaseNotes("./ReleaseNotes.md");

//  Dernière version du projet
public string version = releaseNotes.Version.ToString();

//////////////////////////////////////////////
// Task
//////////////////////////////////////////////

// Execute before first Task
Setup(ctx => 
{
    Information("Started Build Project");
    Information(version);

    Versioning.ProjectVersion = version;
});

// Execute after last Task
Teardown(ctx => 
{
    Information("Finished Build Project");
});

Task("Clean")
    .Does(() => 
    {
        CleanDirectories(new DirectoryPath[]
        {
            CakeParameters.BuildResultDirectory,
            Directory(CakeParameters.LostTimeDB + CleanFolder),
            Directory(CakeParameters.LostTimeDBTest + CleanFolder),
        });
    });

Task("RestoreNugetPackage")
    .IsDependentOn("Clean")
    .Does(() => 
    {
        NuGetRestore(CakeParameters.ProjectSolution);
    });

Task("Build")
    .IsDependentOn("RestoreNugetPackage")
    .Does(() => 
    {
        if(IsRunningOnWindows())
        {
        MSBuild(CakeParameters.ProjectSolution, settings =>
            settings.SetConfiguration(configuration));
        }
        else
        {
        XBuild(CakeParameters.ProjectSolution, settings =>
            settings.SetConfiguration(configuration));
        }
    });

Task("RunNUnitTest")
    .IsDependentOn("Build")
    .Does(() =>
    {
        var testsFile = CakeParameters.ProjectApplicationTestDLL + configuration + "/*Test.dll";

        NUnit3(testsFile, new NUnit3Settings {
        Results = CakeParameters.ProjectApplicationResultBuild,
        NoResults = false,
        });
    });

Task("Version")
    .IsDependentOn("RunNUnitTest")
    .Does(() => 
    {
        // à implémenté
    });

Task("CopyFiles")
    .IsDependentOn("Version")
    .Does(() => 
    {
        EnsureDirectoryExists(CakeParameters.BuildResultDirectory + "bin");

        CopyFileToDirectory(CakeParameters.LostTimeDB + "bin/" + configuration + "/LostTimeDB.dll", CakeParameters.BuildResultDirectory + "bin");
        CopyFileToDirectory(CakeParameters.LostTimeDB + "bin/" + configuration + "/LostTimeDB.pdb", CakeParameters.BuildResultDirectory + "bin");
        CopyFileToDirectory(CakeParameters.LostTimeDBTest + "bin/" + configuration + "/LostTimeDBTest.dll", CakeParameters.BuildResultDirectory + "bin");
        CopyFileToDirectory(CakeParameters.LostTimeDBTest + "bin/" + configuration + "/LostTimeDBTest.pdb", CakeParameters.BuildResultDirectory + "bin");

        CopyFiles(new FilePath[] {"License", "README.md", "ReleaseNotes.md"}, CakeParameters.BuildResultDirectory + "bin");
    });

Task("CreateNugetPackage")
    .IsDependentOn("CopyFiles")
    .Does(() => 
    {
        var nuGetPackSettings = new NuGetPackSettings
        {
            Id = "LostTimeDB",
            Version = Versioning.ProjectVersion,
            Title = "LostTimeDB",
            Authors = new[] 
            {
                "Guillaume Elisabeth"
            },
            Owners = new[] 
            {
                "LostTime"
            },
            Description = "Lost Time Database",
            Summary = "All script to use LostTime database",
            ProjectUrl = LostTimeInformation.ProjectUrl,
            LicenseUrl = LostTimeInformation.LicenseUrl,
            Copyright = "Lost Time",
            ReleaseNotes = releaseNotes.Notes.ToArray(),
            Tags = new[] 
            {
                "Cake", "Script", "Build"
            },
            RequireLicenseAcceptance = false,
            Symbols = false,
            NoPackageAnalysis = true,
            Files = new[] 
            {
                new NuSpecContent 
                {
                    Source = "LostTimeDB.dll", Target = "bin"
                },
            },
            BasePath = CakeParameters.BuildResultDirectory + "bin",
            OutputDirectory = CakeParameters.BuildResultDirectory,
        };

        NuGetPack(nuGetPackSettings);
    });

Task("OctoPush")
    .IsDependentOn("CreateNugetPackage")
    .Does( () =>
    {
        // à implémenté
    });

Task("OctoRelease")
    .IsDependentOn("OctoPush")
    .Does(() => 
    {
        // à implémenté
    });

Task("ReleaseNotesReadText")
    .IsDependentOn("OctoRelease")
    .Does(() => 
    {
        string[] lines = System.IO.File.ReadAllLines("./ReleaseNotes.md");

        System.Console.WriteLine("Contents of WriteLines2.txt = ");

        foreach (string line in lines)
        {
            // Use a tab to indent each line of the file.
            Console.WriteLine("\t" + line);
        }
    });

Task("ReleaseNotesWriteText")
    .IsDependentOn("ReleaseNotesReadText")
    .Does(() => 
    {
        Console.WriteLine("Give a comment to your tasks for this build");
        string comment = Console.ReadLine();

        string[] lines = System.IO.File.ReadAllLines("./ReleaseNotes.md");
        string[] text = {"### New in " + Versioning.ProjectVersion + " (Released : " + System.DateTime.Now.ToShortDateString() + ")", " ", comment , " "};
        string[] newFiles = text.Concat(lines).ToArray();

        System.IO.File.WriteAllLines("./ReleaseNotes.md", newFiles);
    });

Task("GitHubCommit")
    .IsDependentOn("ReleaseNotesWriteText")
    .Does(() =>
    {
        // à implémenté
    });

Task("GitHubPush")
    .IsDependentOn("GitHubCommit")
    .Does(() => 
    {
        // à implémenté
    });

Task("GitHubTag")
    .IsDependentOn("GitHubPush")
    .Does(() =>
    {
        // à implémenté
    });


//////////////////////////////////////////////
// Task Target
//////////////////////////////////////////////

Task("Default")
    .IsDependentOn("ReleaseNotesWriteText");

//////////////////////////////////////////////
// Execution
//////////////////////////////////////////////

RunTarget(target);