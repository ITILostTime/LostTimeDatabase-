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

    Versioning.temporaireSemver();
});

// Execute after last Task
Teardown(ctx => 
{
    Information("Finished Build Project");
    Information("Last Project Version = " + Versioning.ProjectVersion);
});

// Clean Folder all folder bin and object of the project
Task("Clean")
    .Does(() => 
    {
        CleanDirectories(new DirectoryPath[]
        {
            CakeParameters.BuildResultDirectory,
            Directory(CakeParameters.LostTimeDB + CleanFolder),
            Directory(CakeParameters.LostTimeDBTest + CleanFolder),
            Directory(CakeParameters.LostTimeDbUp + CleanFolder)
        });
    });

// Restore NuGet Package
Task("RestoreNugetPackage")
    .IsDependentOn("Clean")
    .Does(() => 
    {
        NuGetRestore(CakeParameters.ProjectSolution);
    });

// Build Project .sln
Task("Build")
    .IsDependentOn("RestoreNugetPackage")
    .Does(() => 
    {
        DotNetBuild(CakeParameters.ProjectSolution, settings =>
            settings.SetConfiguration(configuration));
    });

// Run NUnit Test
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

// CopyFiles in the Versioning Folder
Task("CopyFiles")
    .IsDependentOn("RunNUnitTest")
    .Does(() => 
    {
        EnsureDirectoryExists(CakeParameters.BuildResultDirectory + "bin");
        EnsureDirectoryExists(CakeParameters.BuildResultDirectory + "bin/DbUp");

        CopyFileToDirectory(CakeParameters.LostTimeDB + "bin/" + configuration + "net452/LostTimeDB.dll", CakeParameters.BuildResultDirectory + "bin/net452");
        CopyFileToDirectory(CakeParameters.LostTimeDB + "bin/" + configuration + "netstandard1.4/LostTimeDB.dll", CakeParameters.BuildResultDirectory + "bin/netstandard1.4");
        CopyFileToDirectory(CakeParameters.LostTimeDB + "bin/" + configuration + "/LostTimeDB.pdb", CakeParameters.BuildResultDirectory + "bin");
        CopyFileToDirectory(CakeParameters.LostTimeDBTest + "bin/" + configuration + "/LostTimeDBTest.dll", CakeParameters.BuildResultDirectory + "bin");
        CopyFileToDirectory(CakeParameters.LostTimeDBTest + "bin/" + configuration + "/LostTimeDBTest.pdb", CakeParameters.BuildResultDirectory + "bin");
        CopyFileToDirectory(CakeParameters.LostTimeDbUp + "bin/" + configuration + "/LostTimeDbUp.exe", CakeParameters.BuildResultDirectory + "bin/DbUp");
        CopyFileToDirectory(CakeParameters.LostTimeDbUp + "bin/" + configuration + "/LostTimeDbUp.exe.config", CakeParameters.BuildResultDirectory + "bin/DbUp");


        CopyFiles(new FilePath[] {"License", "README.md", "ReleaseNotes.md"}, CakeParameters.BuildResultDirectory + "bin");
    });

// Create NuGet Package SQL.DLL
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
                    Source = "net452\\LostTimeDB.dll", Target = "lib\\net452"
                },
                new NuSpecContent 
                {
                    Source = "netstandard1.4\\LostTimeDB.dll", Target = "lib\\netstandard1.4"
                }
            },
            BasePath = CakeParameters.BuildResultDirectory + "bin",
            OutputDirectory = CakeParameters.BuildResultDirectory,
        };

        NuGetPack(nuGetPackSettings);
    });

// Create NuGet Package DbUp
Task("CreateNugetDbUpPackage")
    .IsDependentOn("CreateNugetPackage")
    .Does(() => 
    {
        var nuGetPackSettings = new NuGetPackSettings
        {
            Id = "LostTimeDbUp",
            Version = Versioning.ProjectVersion,
            Title = "LostTimeDbUp",
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
                    Source = "*.exe", Target = "Tools"
                },
                
            },
            BasePath = CakeParameters.BuildResultDirectory + "bin/DbUp",
            OutputDirectory = CakeParameters.BuildResultDirectory,
        };

        NuGetPack(nuGetPackSettings);
    });

// Read ReleaseNotes.md
Task("ReleaseNotesReadText")
    .IsDependentOn("CreateNugetDbUpPackage")
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

// Write in ReleaseNotes.md
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

//  Publish LostTimeDB.nupgk on MyGet
Task("PublishMyGet")
    .IsDependentOn("ReleaseNotesWriteText")
    .Does( () =>
    {
        //      Resolve the API Key
        var apiKey = LostTimeInformation.MyGet_API_Key;
        if(string.IsNullOrEmpty(apiKey))
        {
            throw new InvalidOperationException("Could not resolve MyGet API Key");
        }

        //      Resolve the API Url
        var apiUrl = LostTimeInformation.MyGet_API_URL;
        if(string.IsNullOrEmpty(apiUrl))
        {
            throw new InvalidOperationException("Could not reseolve MyGet API Url");
        }

        //      Push the package
        NuGetPush(CakeParameters.NugetPath, new NuGetPushSettings {
            Source = apiUrl,
            ApiKey = apiKey
        });

        NuGetPush(CakeParameters.NugetPathDbUp, new NuGetPushSettings {
            Source = apiUrl,
            ApiKey = apiKey
        });
        
    });

// Commit in GitHub
Task("GitHubCommit")
    .IsDependentOn("ReleaseNotesWriteText")
    .Does(() =>
    {
        Console.WriteLine("Write a note for your commit without space");
        string CommitMessage = Console.ReadLine();
        CommitMessage += "Version" + Versioning.ProjectVersion;

        string GitCommand = "git";
        string GitAddAll = @"add --all";
        string GitCommit = @"commit -a -m" + " " + CommitMessage;

        Process.Start(GitCommand, GitAddAll);
        Process.Start(GitCommand, GitCommit);
    });

// Push in GitHub
Task("GitHubPush")
    .IsDependentOn("GitHubCommit")
    .Does(() => 
    {
        string GitCommand = "git";
        string GitPush = @"push --all";

        Process.Start(GitCommand, GitPush);
    });

// Tag in GitHub
Task("GitHubTag")
    .IsDependentOn("GitHubPush")
    .Does(() =>
    {
        string GitCommand = "git";
        string GitTag = @"tag -a" + " " + Versioning.ProjectVersion + " " + "-m" + " " + Versioning.ProjectVersion;
        string GitPushTags = @"push --tags";
        string GitPush = @"push --all";

        Process.Start(GitCommand, GitTag);
        Process.Start(GitCommand, GitPushTags);
        Process.Start(GitCommand, GitPush);
    });


//////////////////////////////////////////////
// Task Target
//////////////////////////////////////////////

Task("Default")
    .IsDependentOn("RunNUnitTest");

//////////////////////////////////////////////
// Execution
//////////////////////////////////////////////

RunTarget(target);