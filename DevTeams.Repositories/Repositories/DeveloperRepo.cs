//Make this first

public class DeveloperRepo 
{
    private readonly List<Developer> _developerDb = new List<Developer>();
    private int _count;

    public DeveloperRepo()
    {
        SeedData();
    }

    //Create
    public bool AddDeveloperToDb(Developer developer)
    {
        AssignID(developer);
        _developerDb.Add(developer);
        return true;
    }

     private void AssignID(Developer dev)
    {
        _count++;
        dev.Id = _count;
    }

    //Read
    public List<Developer> GetAllDevelopers()
    {
        return _developerDb;
    }
     public Developer GetDeveloperByName(string FullName) {
          foreach (var developer in _developerDb)
        {
            if (developer.FullName == FullName)
            {
                return developer;
            }
        }
        return null;
    }

    //Update
     public bool UpdateDevelopers(string originalName, Developer updatedData)
    {
        Developer developerinDb = GetDeveloperByName(originalName);

        if (developerinDb != null)
        {
            developerinDb.FirstName = updatedData.FirstName;
            developerinDb.LastName = updatedData.LastName;
            developerinDb.HasPluralSight = updatedData.HasPluralSight;
            return true;
        }
        else
        {
            return false;
        }
    }

    //Delete
       public bool DeleteExistingDevelopers(string FullName)
    {
        Developer developerinDb = GetDeveloperByName(FullName);
        return _developerDb.Remove(developerinDb);
    }


//Challenge


public List<Developer> DevsWithOutPluralSight()
    {
        return _developerDb.Where(developer => developer.HasPluralSight == false).ToList();
    }

    public List<Developer> DevsWithOutPluralsight()
    {
        List<Developer> devsWithoutPs = new List<Developer>();
        foreach (Developer developer in _developerDb)
        {
            if (developer.HasPluralSight == false)
            {
                devsWithoutPs.Add(developer);
            }
        }
        return devsWithoutPs;
    }

    private void SeedData()
    {
        var devA = new Developer("Akuma", "n/a", false);
        var devB = new Developer("Ryu", "n/a", true);
        var devC = new Developer("M.", "Bison", true);
        var devD = new Developer("Chun", "Li", false);
        var devE = new Developer("Cammy", "n/a", false);
        var devF = new Developer("Blanka", "n/a", false);
        var devG = new Developer("Guile", "n/a", false);
        var devH = new Developer("Daslim", "n/a", false);
        var devI = new Developer("Vega", "n/a", true);

        AddDeveloperToDb(devA);
        AddDeveloperToDb(devB);
        AddDeveloperToDb(devC);
        AddDeveloperToDb(devD);
        AddDeveloperToDb(devE);
        AddDeveloperToDb(devF);
        AddDeveloperToDb(devG);
        AddDeveloperToDb(devH);
        AddDeveloperToDb(devI);
    }
        }
