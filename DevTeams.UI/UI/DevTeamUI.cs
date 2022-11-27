using static System.Console;

public class DevTeamUI
{
    protected readonly List<DevTeam> _teamDb = new List<DevTeam>();
    private readonly DevTeamRepo _dTeamRepo;
    private readonly DeveloperRepo _devRepo;

    private bool isRunningDevTeamUI;


    public DevTeamUI(DeveloperRepo devRepo)
    {
        _devRepo = devRepo;
        _dTeamRepo = new DevTeamRepo(_devRepo);
    }

    public void Run()
    {
        RunApplication();
    }

    private void RunApplication()
    {
        isRunningDevTeamUI = true;
        while (isRunningDevTeamUI)
        {
            WriteLine("== DeveloperTeams UI ==\n" +
                  "Please Make a Selection:\n" +
                  "1. Add A Developer Team\n" +
                  "2. View All Developer Teams\n" +
                  "3. View Developer Team By Name\n" +
                  "4. Update Existing Developer Team\n" +
                  "5. Delete Existing Developer Team\n" +
                  "6. Add Multi. Devs To A Team.\n" +
                  "-------------------------------\n" +
                  "7. Open Main Menu\n" +
                  "-------------------------------\n" +
                  "0. Close Application\n");

            string userInputMenuSelection = ReadLine();

            switch (userInputMenuSelection)
            {
                case "1":
                    AddADeveloperTeam();
                    break;
                case "2":
                    ViewAllDeveloperTeams();
                    break;
                case "3":
                    ViewDeveloperTeamByName();
                    break;
                case "4":
                    UpdateAnExistingDeveloperTeam();
                    break;
                case "5":
                    DeleteAnExistingDeveloperTeam();
                    break;
                case "6":
                    AddMultiDevsToATeam();
                    break;
                case "7":
                    BackToMainMenu();
                    break;
                case "0":
                    CloseApplication();
                    break;
                default:
                    WriteLine("Invalid Selection");
                    DTUtils.PressAnyKey();
                    break;
            }
        }

    }

    private void CloseApplication()
    {
        isRunningDevTeamUI = false;
        DTUtils.isRunning = false;
        WriteLine("Closing Application");
        DTUtils.PressAnyKey();
    }

    private void BackToMainMenu()
    {
        Clear();
        isRunningDevTeamUI = false;
    }

    private void AddMultiDevsToATeam()
    {
        Clear();
        WriteLine("== Developer Team Listing");
        GetDevTeamData();
        List<DevTeam> dTeam = _dTeamRepo.GetDevTeams();
        if (dTeam.Count() > 0)
        {
            WriteLine("Please Select a DevTeam by Name for Multi Dev Addition.");
            string userInputDevTeamName = ReadLine();
            DevTeam team = _dTeamRepo.GetTeamByName(userInputDevTeamName);

            List<Developer> auxDevInDb = _devRepo.GetAllDevelopers();

            List<Developer> devsToAdd = new List<Developer>();

            if (team != null)
            {
                bool hasFilledPositions = false;
                while (!hasFilledPositions)
                {
                    if (auxDevInDb.Count() > 0)
                    {
                        DisplayDevelopersInDB(auxDevInDb);
                        WriteLine("Do you want to add a Developer y/n?");
                        var userInputAnyDevs = ReadLine();
                        if (userInputAnyDevs == "Y".ToLower())
                        {
                            WriteLine("Please Choose Dev by Name:");
                            string userInputDevName = ReadLine();
                            Developer developer = _devRepo.GetDeveloperByName(userInputDevName);
                            if (developer != null)
                            {
                                devsToAdd.Add(developer);
                                auxDevInDb.Remove(developer);
                            }
                            else
                            {
                                WriteLine($"Sorry, the Dev with the Name: {userInputDevName} doesn't Exist.");
                                WriteLine("Press Any Key to continue.");
                                ReadKey();
                            }
                        }
                        else
                        {
                            hasFilledPositions = true;
                        }
                    }
                    else
                    {
                        WriteLine("There are no Develpers in the Database.");
                        ReadKey();
                        break;
                    }
                }

                if (_dTeamRepo.AddMultiDevsToTeam(team.TeamName, devsToAdd))
                {
                    WriteLine("Success");
                }
                else
                {
                    WriteLine("Failure");
                }
            }


        }
        else
        {
            WriteLine("There aren't any available Developer Teams to Delete.");
        }
        ReadKey();
    }

    private DevTeam InitializeDTeamCreation()
    {
        try
        {
            DevTeam team = new DevTeam();
            WriteLine("Please enter the DevTeam Name:");
            team.TeamName = ReadLine();

            bool hasFilledPositions = false;

            //I want to have all of the devs in db w/n a variable,
            //but this will be used to update the UI.
            List<Developer> auxDevelopers = _devRepo.GetAllDevelopers();

            while (!hasFilledPositions)
            {
                WriteLine("Does this team have any Developers? y/n");
                string userInputAnyDevs = ReadLine();
                if (userInputAnyDevs == "Y".ToLower())
                {
                    if (auxDevelopers.Count() > 0)
                    {
                        DisplayDevelopersInDB(auxDevelopers);
                        WriteLine("Select the Dev you want on this team by DevId.");
                        var userInputDevName = ReadLine();
                        Developer selectedDev = _devRepo.GetDeveloperByName(userInputDevName);
                        if (selectedDev != null)
                        {
                            team.DevelopersOnTeam.Add(selectedDev);
                            auxDevelopers.Remove(selectedDev);
                        }
                        else
                        {
                            WriteLine($"Sorry the Dev with the Name: {userInputDevName} Doesn't Exist.");
                        }
                    }
                    else
                    {
                        WriteLine("There are no Develpers in the Database.");
                        ReadKey();
                        break;
                    }
                }
                else
                {
                    hasFilledPositions = true;
                }
            }
            return team;
        }
        catch
        {
            SomeThingWentWrong();
        }
        return null;
    }

    private void DisplayDevelopersInDB(List<Developer> auxDevelopers)
    {
        foreach (var dev in auxDevelopers)
        {
            WriteLine(dev);
        }
    }

    private void SomeThingWentWrong()
    {
        WriteLine("Something went wrong.\n" +
                       "Please try again\n" +
                       "Returning to Developer Menu.");
    }

    private void DeleteAnExistingDeveloperTeam()
    {
        Clear();
        WriteLine("== Developer Team Listing");
        GetDevTeamData();
        try
        {
            List<DevTeam> dTeam = _dTeamRepo.GetDevTeams();
            if (dTeam.Count() > 0)
            {
                WriteLine("Please select DevTeam by Name for Deletion.");
                string userInputDevTeamName = ReadLine();
                var team = _dTeamRepo.GetTeamByName(userInputDevTeamName);
                if (team != null)
                {
                    if (_dTeamRepo.DeleteExistingDevTeams(team.TeamName))
                    {
                        WriteLine("Success");
                    }
                    else
                    {
                        WriteLine("Fail");
                    }
                }
                else
                {
                    WriteLine("There aren't any available Developer Teams to Delete.");
                }
            }
        }
        catch
        {
            SomeThingWentWrong();
        }
        ReadKey();
    }

    private void UpdateAnExistingDeveloperTeam()
    {
        Clear();
        WriteLine("== Developer Team Listing ==");
        GetDevTeamData();
        List<DevTeam> dTeam = _dTeamRepo.GetDevTeams();
        if (dTeam.Count() > 0)
        {
            WriteLine("Please select a DevTeam by Name:");
            string userInputDevTeamName = ReadLine();

            DevTeam teamInRepo = _dTeamRepo.GetTeamByName(userInputDevTeamName);

            if (teamInRepo != null)
            {
                DevTeam updateTeamData = InitializeDTeamCreation();

                if (_dTeamRepo.UpdateExistingTeams(teamInRepo.TeamName, updateTeamData))
                {
                    WriteLine("Successful");
                }
                else
                {
                    WriteLine("Update Failed.");
                }
            }
            else
            {
                WriteLine($"Sorry, the DevTeam with the Name: {userInputDevTeamName} doesn't Exist.");
            }
        }

        ReadKey();
    }

    private void ViewDeveloperTeamByName()
    {
        Clear();
        WriteLine("== Developer Team Listing ==");
        GetDevTeamData();
        List<DevTeam> devTeam = _dTeamRepo.GetDevTeams();
        if (devTeam.Count() > 0)
        {
            WriteLine("Please select DevTeam by Name:");
            string userInputDevTeamName = ReadLine();
            ValidateDevTeamData(userInputDevTeamName);
        }

        ReadKey();
    }

    private void ValidateDevTeamData(string userInputDevTeamName)
    {
        var team = _dTeamRepo.GetTeamByName(userInputDevTeamName);
        if (team != null)
        {
            DisplayDeveloperTeamData(team);
        }
        else
        {
            WriteLine($"Sorry the DevTeam with the Name: {userInputDevTeamName} doesn't Exist.");
        }
    }

    private void ViewAllDeveloperTeams()
    {
        Clear();
        WriteLine("== Dev Team Listing ==");
        GetDevTeamData();
        ReadKey();
    }

    private void GetDevTeamData()
    {
        foreach (DevTeam team in _dTeamRepo.GetDevTeams())
        {
            DisplayDeveloperTeamData(team);
        }

    }

    private void DisplayDeveloperTeamData(DevTeam team)
    {
        WriteLine(team);
    }

    private void AddADeveloperTeam()
    {
        Clear();
        DevTeam dTeam = InitializeDTeamCreation();
        if (_dTeamRepo.AddDevTeamToDb(dTeam))
        {
            WriteLine($"Team: {dTeam.TeamName} was Added.");
        }
        else
        {
            WriteLine("Team: was Not addAdded.");
        }
        ReadKey();
    }
}