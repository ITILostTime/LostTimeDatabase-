site : http://cakebuild.net/

### Setting Up A new Project 

Open new PowerShell windows, go in your project folder and run the following command (for windows) : 
Invoke-WebRequest http://cakebuild.net/download/bootstrapper/windows -OutFile build.ps1

In your folder you've got a new file named : build.ps1

### Create a Cake Script

Create a new script with Notepad++ or anything else and call it [name].cake (for example in my project I call it Build.cake) and write in it : 

----------------------------------------------------------------

var target = Argument("target", "Default");

Task("Default")
  .Does(() =>
{
  Information("Hello World!");
});

RunTarget(target);

----------------------------------------------------------------

### Run the Cale Script

In your PowerSjell windows use the following command : ./build.ps1

### Task and Dependecies

Task is use to perform specif work in a specific order.

----------------------------------------------------------------

Task("A")
    .Does(() =>
    {

    });

----------------------------------------------------------------

To add a dependency on another task, use the IsDependentOn method.

----------------------------------------------------------------

Task("A")
    .Does(() => 
    {

    });

Task("B")
    .IsDependentOn("A")
    .Does(() => 
    {

    });

RunTarget("B");

----------------------------------------------------------------

This will first execute target A and the B as expected;

### Tools

Add a tool in your project : 

NUNit : #tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0 (here i use the version 3.4.0)
Git : #tool nuget:?package=GitVersion.CommandLine

### Build your project

If you want to build your project you can use MSBuild (MSBuild is already present in Cake)

----------------------------------------------------------------

var configuration = Argument("configuration", "Release");

Task("Build")
    .IsDependentOn("RestoreNugetPackage")
    .Does(() => 
    {
        MSBuild([ProjectSolution], settings =>
            settings.SetConfiguration(configuration));
    });

----------------------------------------------------------------

ProjectSolution is the path to your project (for example : [ProjectName].sln)

You need to create a variable whose have for argument : configuration and Release

### NUnit test

----------------------------------------------------------------

Task("RestoreNugetPackage")
    .Does(() => 
    {
        NuGetRestore([ProjectSolution]);
    });

----------------------------------------------------------------

----------------------------------------------------------------

Task("RunNUnitTest")
    .Does(() =>
    {
        var testsFile = CakeParameters.ProjectApplicationTestDLL + configuration + "/*Test.dll";

        NUnit3(testsFile, new NUnit3Settings {
        Results = CakeParameters.ProjectApplicationResultBuild,
        NoResults = false,
        });
    });

----------------------------------------------------------------

testFile is the path to your projectTest.dll

Results need a path with a name (like ./[ProjectSolution]/TestResults.xml) and it will write in a xml file the results of the test 

### Git 

You want to use Git with Cake ?

Use this : Process.Start();

----------------------------------------------------------------

Task("GitHubCommit")
    .Does(() =>
    {
        Console.WriteLine("Write a note for your commit without space");
        string CommitMessage = Console.ReadLine();
        CommitMessage += "Version" + Versioning.ProjectVersion;

        string GitCommand = "git";
        string GitAddAll = @"add --all";
        string GitCommit = @"commit -a -m " + CommitMessage;

        Process.Start(GitCommand, GitAddAll);
        Process.Start(GitCommand, GitCommit);
    });

----------------------------------------------------------------

### Create .nupkg 

----------------------------------------------------------------

Task("CreateNugetPackage")
    .Does(() => 
    {
        var nuGetPackSettings = new NuGetPackSettings
        {
            Id = "[PutSomethingHere]",
            Version = [PutSomethingHere],
            Title = "[PutSomethingHere]",
            Authors = new[] 
            {
                "[PutSomethingHere]"
            },
            Owners = new[] 
            {
                "[PutSomethingHere]"
            },
            Description = "[PutSomethingHere]",
            Summary = "[PutSomethingHere]",
            ProjectUrl = [PutSomethingHere].ProjectUrl,
            LicenseUrl = [PutSomethingHere].LicenseUrl,
            Copyright = "[PutSomethingHere]",
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
                    Source = "[PutSomethingHere]", Target = "bin"
                },
            },
            BasePath = [PutSomethingHere],
            OutputDirectory = [PutSomethingHere],
        };

        NuGetPack(nuGetPackSettings);
    });

----------------------------------------------------------------

