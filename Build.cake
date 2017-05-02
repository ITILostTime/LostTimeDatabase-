#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0
#tool nuget:?package=GitVersion.CommandLine



//////////////////////////////////////////////
// Load / Add Script
//////////////////////////////////////////////

#load "./CakeFolder/CakeParameters.cake"

//////////////////////////////////////////////
// Argument
//////////////////////////////////////////////

// Target to launch the build
var target = Argument("Target", "Default");

// Folder To Clean
public var CleanFolder = Argument("bin", "obj");

//  Argument pour parcourir les dossiers du projet
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////
// Task
//////////////////////////////////////////////

// Execute before first Task
Setup(ctx => 
{
    Information("Started Build Project");
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
        // à implémenté
    });

Task("CreateNugetPackage")
    .IsDependentOn("CopyFiles")
    .Does(() => 
    {
        // à implémenté
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
        // à implémenté
    });

Task("ReleaseNotesWriteText")
    .IsDependentOn("ReleaseNotesReadText")
    .Does(() => 
    {
        // à implémenté
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
    .IsDependentOn("Build");

//////////////////////////////////////////////
// Execution
//////////////////////////////////////////////

RunTarget(target);