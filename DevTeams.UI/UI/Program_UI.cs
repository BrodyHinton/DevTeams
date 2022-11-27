using static System.Console;

public class Program_UI
{
    private DeveloperUI _devUI;
    private DevTeamUI _devTeamUI;
    private DeveloperRepo _devRepo;
    public Program_UI()
    {
        _devRepo = new DeveloperRepo();
        _devUI = new DeveloperUI();
        _devTeamUI = new DevTeamUI(_devRepo);
    }

    public void Run()
    {
        RunApplication();
    }

    private void RunApplication()
    {

        while (DTUtils.isRunning)
        {
            WriteLine("== Welcome to Komodo DevTeams ==\n" +
                  "Please Make a Selection:\n" +
                  "1. Developer Menu\n" +
                  "2. Developer Teams Menu\n" +
                  "-------------------------------\n" +
                  "0. Close Application\n");

            string userInputMenuSelection = ReadLine();
            switch (userInputMenuSelection)
            {
                case "1":
                    DeveloperMenu();
                    break;
                case "2":
                    DeveloperTeamsMenu();
                    break;
                case "0":
                    DTUtils.isRunning = CloseApplication();
                    break;
                default:
                    WriteLine("Invalid Selection");
                    DTUtils.PressAnyKey();
                    break;
            }
        }
    }

    private bool CloseApplication()
    {
        WriteLine("Thanks, for using Komodo Dev Teams.");
        DTUtils.PressAnyKey();
        return false;
    }

    private void DeveloperTeamsMenu()
    {
        Clear();
        _devTeamUI.Run();
    }

    private void DeveloperMenu()
    {
        Clear();
        _devUI.Run();
    }


}