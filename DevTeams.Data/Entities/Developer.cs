public class Developer
{
    public Developer()
    {

    }
    public Developer(string firstName, string lastName, bool hasPluralSight)
    {
        FirstName = firstName;
        LastName = lastName;
        HasPluralSight = hasPluralSight;
    }
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }
    public bool HasPluralSight { get; set; }

    public override string ToString()
    {
        string str = $"ID: {Id.ToString()}\n" +
                     $"Name: {FullName}\n" +
                     $"Has Pluralsight: {HasPluralSight}\n" +
                     "====================================\n";
        return str;
    }
}
