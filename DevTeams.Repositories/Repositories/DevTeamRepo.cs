//Make this second

public class DevTeamRepo
{
    protected readonly List<DevTeam> _teamDb = new List<DevTeam>();

    private DeveloperRepo _devRepo;
    private int _count;
    
     public DevTeamRepo(DeveloperRepo devRepo)
    {
        _devRepo = devRepo;
        SeedData();
    }

    //Create
    public bool AddDevTeamToDb(DevTeam team)
    {
    
        if (team is null)
        {
            return false;
        }
        else
        {
            _count++;
            team.teamID = _count;
            _teamDb.Add(team);
            return true;
        }
    }

     public List<DevTeam> GetDevTeams()
    {
        return _teamDb;
    }

    //Read
   
    public DevTeam GetTeamByName(string TeamName)
    {
        foreach (var team in _teamDb)
        {
            if (team.TeamName == TeamName)
            {
                return team;
            }
        }
        return null;
    }

    //Update
    public bool UpdateExistingTeams(string TeamName, DevTeam updatedData)
    {
        DevTeam oldTeam = GetTeamByName(TeamName);

        if (oldTeam != null)
        {
            oldTeam.TeamName = updatedData.TeamName;
            oldTeam.DevelopersOnTeam = updatedData.DevelopersOnTeam;
            return true;
        }
        else
        {
            return false;
        }
    }

    //Delete
    public bool DeleteExistingDevTeams(string TeamName)
    {
        DevTeam oldTeam = GetTeamByName(TeamName);
        return _teamDb.Remove(oldTeam);
    }

//Challenge
public bool AddMultiDevsToTeam(string TeamName, List<Developer> devs)
    {
        var oldTeam = GetTeamByName(TeamName);
        if (oldTeam != null && devs != null)
        {
            oldTeam.DevelopersOnTeam.AddRange(devs);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SeedData()
    {

        Developer akuma = _devRepo.GetDeveloperByName("Akuma n/a"); 
        Developer ryu = _devRepo.GetDeveloperByName("Ryu n/a"); 

        DevTeam teamA = new DevTeam("Front-End", new List<Developer>
        {
          akuma,
          ryu
        });

        Developer bison = _devRepo.GetDeveloperByName("M. Bison"); 
        Developer chun = _devRepo.GetDeveloperByName("Chun Li"); 
        
        DevTeam teamB = new DevTeam("Back-End", new List<Developer>
        {
            bison,
            chun
        });



        AddDevTeamToDb(teamA);
        AddDevTeamToDb(teamB);
    }
    }
