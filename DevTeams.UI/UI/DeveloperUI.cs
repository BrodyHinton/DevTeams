using static System.Console;

public class DeveloperUI
{
    private DeveloperRepo _devRepo;
    private bool isRunningDevUI;
    private DevTeamUI _dtUI;

    public DeveloperUI()
    {
        _devRepo = new DeveloperRepo();
    }

    public void Run()
    {
        RunApplication();
    }

    private void RunApplication()
    {
        isRunningDevUI = true;
        while (isRunningDevUI)
        {
            Clear();
            WriteLine("== Komodo DevTeams Developer UI ==\n" +
                  "Please Make a Selection:\n" +
                  "1. Add A Developer\n" +
                  "2. View All Developers\n" +
                  "3. View Developer By Name\n" +
                  "4. Update Existing Developer\n" +
                  "5. Delete Existing Developer\n" +
                  "6. View All Developers with a Pluralsight Acct.\n" +
                  "-------------------------------\n" +
                  "7. Back To Main Menu\n" +
                  "-------------------------------\n" +
                  "0. Close Application\n");

            string userInputMenuSelection = ReadLine();
            switch (userInputMenuSelection)
            {
                case "1":
                    AddADeveloper();
                    break;
                case "2":
                    ViewAllDevelopers();
                    break;
                case "3":
                    ViewDeveloperByName();
                    break;
                case "4":
                    UpdateAnExistingDeveloper();
                    break;
                case "5":
                    DeleteAnExistingDeveloper();
                    break;
                case "6":
                    ViewDevsWithPluralsight();
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
        isRunningDevUI = false;
        DTUtils.isRunning = false;
        WriteLine("Closing Application");
        DTUtils.PressAnyKey();
    }

    private void BackToMainMenu()
    {
        Clear();
        isRunningDevUI = false;
    }

    private void ViewDevsWithPluralsight()
    {
        Clear();
        WriteLine("== Devs Without Pluralsight ==\n");
        ShowDevsWithOutPs();
        ReadKey();
    }

    private void ShowDevsWithOutPs()
    {
        List<Developer> devsWoPS = _devRepo.DevsWithOutPluralsight();
        if (devsWoPS.Count > 0)
        {
            foreach (var Developer in devsWoPS)
            {
                DisplayDevData(Developer);
            }
        }
        else
        {
            WriteLine("There Are No Developers at this time with out a Pluralsight Membership.");
        }
    }

    private void DeleteAnExistingDeveloper()
    {
        Clear();
        ShowEnlistedDevs();
        WriteLine("----------\n");
        try
        {
            WriteLine("Select developer by Name.");
            string userInputDevName = ReadLine();
            ValidateDeveloperInDatabase(userInputDevName);
            WriteLine("Do you want to Delete this Developer? y/n?");
            string userInputDeleteDev = ReadLine();
            if (userInputDeleteDev == "Y".ToLower())
            {
                if (_devRepo.DeleteExistingDevelopers(userInputDevName))
                {
                    WriteLine($" The Developer with the Name: {userInputDevName}, was Successfully Deleted.");
                }
                else
                {
                    WriteLine($"The Developer with the Name: {userInputDevName}, was NOT Deleted.");
                }
            }
        }
        catch
        {
            SomethingWentWrong();
        }

        ReadKey();
    }

    private void UpdateAnExistingDeveloper()
    {
        Clear();
        ShowEnlistedDevs();
        WriteLine("----------\n");
        try
        {
            WriteLine("Select developer by Name.");
            string userInputDevName = ReadLine();
            Developer devInDb = GetDeveloperDataFromDb(userInputDevName);
            bool isValidated = ValidateDeveloperInDatabase(devInDb.FullName);

            if (isValidated)
            {
                WriteLine("Do you want to Update this Developer? y/n?");
                string userInputDeleteDev = ReadLine();
                if (userInputDeleteDev == "Y".ToLower())
                {
                    Developer updatedDevData = InitialDevCreationSetup();

                    if (_devRepo.UpdateDevelopers(devInDb.FullName, updatedDevData))
                    {
                        WriteLine($" The Developer {updatedDevData.Id}, was Successfully Updated.");
                    }
                    else
                    {
                        WriteLine($"The Developer {updatedDevData.Id}, was NOT Updated.");
                    }
                }
                else
                {
                    WriteLine("Returning to Developer Menu.");
                }
            }
        }
        catch
        {
            SomethingWentWrong();
        }
        ReadKey();
    }

    private void ViewDeveloperByName()
    {
        Clear();
        ShowEnlistedDevs();
        WriteLine("----------\n");
        try
        {
            WriteLine("Select developer by Name.");
            string userInputDevName = ReadLine();
            ValidateDeveloperInDatabase(userInputDevName);
        }
        catch
        {
            SomethingWentWrong();
        }
        ReadKey();
    }

    private void SomethingWentWrong()
    {
        WriteLine("Something went wrong.\n" +
                       "Please try again\n" +
                       "Returning to Developer Menu.");
    }

    private bool ValidateDeveloperInDatabase(string userInputDevName)
    {
        Developer dev = GetDeveloperDataFromDb(userInputDevName);
        if (dev != null)
        {
            Clear();
            DisplayDevData(dev);
            return true;
        }
        else
        {
            WriteLine($"The Developer with the Name: {userInputDevName} doesn't Exist!");
            return false;
        }
    }

    private Developer GetDeveloperDataFromDb(string userInputDevName)
    {
        return _devRepo.GetDeveloperByName(userInputDevName);
    }

    private void ShowEnlistedDevs()
    {
        Clear();
        WriteLine("== Developer Listing ==");
        List<Developer> devsInDb = _devRepo.GetAllDevelopers();
        ValidateDeveloperDatabaseData(devsInDb);
    }

    private void ViewAllDevelopers()
    {
        Clear();
        ShowEnlistedDevs();
        ReadKey();
    }

    private void ValidateDeveloperDatabaseData(List<Developer> devsInDb)
    {
        if (devsInDb.Count > 0)
        {
            Clear();
            foreach (var dev in devsInDb)
            {
                DisplayDevData(dev);
            }
        }
        else
        {
            WriteLine("There are no Developers in the Database.");
        }
    }

    private void DisplayDevData(Developer dev)
    {
        WriteLine(dev);
    }

    private void AddADeveloper()
    {
        Clear();
        try
        {
            Developer dev = InitialDevCreationSetup();
            if (_devRepo.AddDeveloperToDb(dev))
            {
                WriteLine($"Successfully Added {dev.FullName} to the Database!");
            }
            else
            {
                SomethingWentWrong();
            }
        }
        catch
        {

            SomethingWentWrong();
        }
        ReadKey();
    }

    private Developer InitialDevCreationSetup()
    {
        Developer dev = new Developer();

        WriteLine("== Add Develper Menu ==");

        WriteLine("What is the Developers first name?");
        dev.FirstName = ReadLine();

        WriteLine("What is the Developers Last name?");
        dev.LastName = ReadLine();

        bool hasMadeSelection = false;

        while (!hasMadeSelection)
        {
            WriteLine("Does this Developer have a Pluralsight Account?\n" +
                "1. yes\n" +
                "2. no\n");

            string userInputPsAcct = ReadLine();

            switch (userInputPsAcct)
            {
                case "1":
                    dev.HasPluralSight = true;
                    hasMadeSelection = true;
                    break;

                case "2":
                    dev.HasPluralSight = false;
                    hasMadeSelection = true;
                    break;

                default:
                    WriteLine("Invalid Selection");
                    DTUtils.PressAnyKey();
                    break;
            }
        }

        return dev;
    }


}