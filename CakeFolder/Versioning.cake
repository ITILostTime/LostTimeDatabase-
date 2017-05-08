using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Versioning
{
    public static string ProjectVersion { get; set; }

    public static string Major { get; set; }
    public static string Minor { get; set; }
    public static string Patch { get; set; }

    public static void temporaireSemver()
    {
        resetMajorMinorPatch();

        DefineSemverParts();

        Console.WriteLine("Are you doing a :(insert the number relative to your work) 1 : Major, 2 : Minor, 3 : Patch");
        
        string nb = Console.ReadLine();

        switch (nb)
        {
            case "1":
                SemverMajor();
                break;
            case "2":
                SemverMinor();
                break;
            case "3":
                SemverPatch();
                break;
            default:
                temporaireSemver();
                break;
        }
    }

    public static void SemverPatch()
    {
        int tmpPatch = Int32.Parse(Patch);
        tmpPatch += 1;
        Patch = tmpPatch.ToString();
        
        ProjectVersion = Major + "." + Minor + "." + Patch;
    }

    public static void SemverMinor()
    {
        int tmpMinor = Int32.Parse(Minor);
        tmpMinor += 1;
        Minor = tmpMinor.ToString();

        Patch = "0";

        ProjectVersion = Major + "." + Minor + "." + Patch;
    }

    public static void SemverMajor()
    {
        int tmpMajor = Int32.Parse(Major);
        tmpMajor += 1;
        Major = tmpMajor.ToString();

        Patch = "0";
        Minor = "0";

        ProjectVersion = Major + "." + Minor + "." + Patch;
    }

    public static void DefineSemverParts()
    {
        int count = 0;

        for(int i = 0; i < ProjectVersion.Count(); i++)
        {
            if(ProjectVersion[i] != '.')
            {
                if(count == 0)
                {
                    Major += ProjectVersion[i];
                }
                else if(count == 1)
                {
                    Minor += ProjectVersion[i];
                }
                else if(count == 2)
                {
                    Patch += ProjectVersion[i];
                }
            }else if(ProjectVersion[i] == '.')
            {
                count++;
            }
        }

        // Vérifie que Major, minor, patch sont corrects par rapport la version contenu dans ReleaseNotes.md
        Console.WriteLine(Major);
        Console.WriteLine(Minor);
        Console.WriteLine(Patch);
    }

    private static void resetMajorMinorPatch()
    {
        Major = "";
        Minor = "";
        Patch = "";
    }
}