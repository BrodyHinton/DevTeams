 public class DevTeam
{
    public DevTeam()
    {

    }


//constructor
    public DevTeam(string teamName, List<Developer> developersOnTeam)
    {
        TeamName=teamName;
        DevelopersOnTeam=developersOnTeam;
    }
    

//properties
    public List<Developer> DevelopersOnTeam { get; set; } = new List<Developer>();
    public string TeamName { get; set;}
    public int teamID { get; set;}

 public override string ToString()
    {
        var str = $"TeamId: {teamID}\n" +
                  $"TeamName: {TeamName}\n" +
                  $"--------  Team Members -------------\n";
        foreach (Developer dev in DevelopersOnTeam)
        {
            str += dev + "\n";
        }

        return str;
    }
}