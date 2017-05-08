#load "./Versioning.cake"

public class CakeParameters
{

    //  Fichier .sln du projet
    public static string ProjectSolution = "./LostTimeDB/LostTimeDB.sln";

    //  Dossier des sous-projets contenus dans le Projet
    public static string LostTimeDB = "./LostTimeDB/LostTimeDB/";
    public static string LostTimeDBTest = "./LostTimeDB/LostTimeDBTest/";

    //  Dossier contenant les Tests.dll
    public static string ProjectApplicationTestDLL = "./LostTimeDB/**/bin/";

    //  Dossier contenant les resultas du build
    public static string BuildResultDirectory = "./BuildResult/Package_v_" +  Versioning.ProjectVersion +  "/";

    //  Dossier contenant les résultats du build
    public static string ProjectApplicationResultBuild = BuildResultDirectory + "/TestsResults.xml";
}