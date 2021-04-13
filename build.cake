///////////////////////////////////////////////////////////////////////////////
// TOOLS / ADDINS
///////////////////////////////////////////////////////////////////////////////

#tool nuget:?package=vswhere&version=2.8.4

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var msBuildPath = GetFiles(VSWhereLatest() + "/**/MSBuild.exe").FirstOrDefault();

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var verbosity = Argument("verbosity", Verbosity.Minimal);
var version = Argument("version", "1.1.1");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

var solution = "./HN.Social.Weibo.sln";

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .ContinueOnError()
    .Does(() =>
{
    CleanDirectories("./src/*/bin");
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore(solution, new NuGetRestoreSettings { NoCache = true });
});

Task("Version")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    var file = "./src/SolutionInfo.cs";
    CreateAssemblyInfo(file, new AssemblyInfoSettings
    {
        Version = version,
        FileVersion = version,
        InformationalVersion = version,
        Copyright = string.Format("Copyright © h82258652 2018 - {0}", DateTime.Now.Year)
    });
});

Task("Build")
    .IsDependentOn("Version")
    .Does(() =>
{
    if(IsRunningOnWindows())
    {
        // Use MSBuild
        var settings = new MSBuildSettings
        {
            ToolPath = msBuildPath
        }
        .SetConfiguration(configuration)
        .SetVerbosity(verbosity);
        MSBuild(solution, settings);
    }
    else
    {
        // Use XBuild
        XBuild(solution, configurator =>
            configurator.SetConfiguration(configuration)
                .SetVerbosity(verbosity));
    }
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    var settings = new DotNetCoreTestSettings
    {
        Configuration = "Release"
    };

    var testProjects = GetFiles("./test/**/*.csproj");
    foreach(var testProject in testProjects)
    {
        DotNetCoreTest(testProject.FullPath, settings);
    }
});

Task("Package")
    .IsDependentOn("Test")
    .Does(() => 
{
    var nugetPackSettings = new NuGetPackSettings
    {
        Version = version,
        OutputDirectory = "./artifacts",
        Repository = new NuGetRepository
        {
            Type = "git",
            Url = "https://github.com/h82258652/HN.Social.Weibo"
        }
    };

    var nuspecFiles = GetFiles("./src/*/*.nuspec");
    NuGetPack(nuspecFiles, nugetPackSettings);
});

Task("Publish")
    .WithCriteria(BuildSystem.IsRunningOnGitHubActions)
    .IsDependentOn("Package")
    .Does(() =>
{
    var packages = GetFiles("./artifacts/*.nupkg");
    // 暂时不发布 efcore 的包
    for (var i = packages.Count - 1; i >= 0; i--)
    {
        var package = packages.ElementAt(i);
        if(package.FullPath.Contains("EfCore"))
        {
            packages.Remove(package);
        }
    }
    var nugetApiKey = EnvironmentVariable("NUGET_APIKEY");
    NuGetPush(packages, new NuGetPushSettings
    {
        Source = "https://www.nuget.org",
        ApiKey = nugetApiKey,
        SkipDuplicate = true
    });
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Publish");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);