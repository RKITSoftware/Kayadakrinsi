public class ListString
{
    #region Public Methods

    // Displays list of week days
    public static void DisplayWeekDays(List<string> list)
    {
        foreach (var item in list)
        {
            Console.WriteLine(item + " ");
        }
        Console.WriteLine();
    }

    // Displays list of continents along with it's name and population in percentage
    public static void DisplayContinent(List<Continent> continentList)
    {
        foreach (Continent continent in continentList)
        {
            Console.WriteLine("Name : " + continent.Name + " | Population : " + continent.Population + "%");
        }
        Console.WriteLine();
    }

    // Displays dictonary of key and value of type Element pair after creating it by BuildDictionary() method
    public static void DisplayDictionary()
    {
        Dictionary<string, Element> elements = BuildDictionary();

        foreach (KeyValuePair<string, Element> kvp in elements)
        {
            Element theElement = kvp.Value;

            Console.WriteLine("Key : " + kvp.Key + " | Values : " + theElement.Symbol + " " + theElement.Name + " " + theElement.AtomicNumber);
        }
        Console.WriteLine();
    }

    // Returns a new dictonary of key and value of type Element pair
    public static Dictionary<string, Element> BuildDictionary()
    {
        return new Dictionary<string, Element> {
            {"K",
                new Element(){ Symbol="K", Name="Potassium", AtomicNumber=19}
            },
            {"Ca",
                new() { Symbol = "Ca", Name = "Calcium", AtomicNumber = 20 }
            },
            {"Sc",
                new() { Symbol = "Sc", Name = "Scandium", AtomicNumber = 21 }
            },
            {"Ti",
                new() { Symbol = "Ti", Name = "Titanium", AtomicNumber = 22 }
            }
        };
    }

    // Displays selected List's elements of type Elements after creating it by BuildListMethod() and selects elements using LINQ query
    public static void DisplayList()
    {
        List<Element> elements = BuildList();

        var subset = from theElement in elements
                     where theElement.AtomicNumber < 22
                     orderby theElement.Name
                     select theElement;

        foreach (Element theElement in subset)
        {
            Console.WriteLine(theElement.Name + " " + theElement.AtomicNumber);
        }

        Console.WriteLine(subset.GetType());
    }

    // Returns a new List of type Element
    public static List<Element> BuildList()
    {
        return new List<Element>
        {
            { new(){ Symbol="K", Name="Potassium", AtomicNumber=19}},
            { new(){ Symbol="Ca", Name="Calcium", AtomicNumber=20}},
            { new(){ Symbol="Sc", Name="Scandium", AtomicNumber=21}},
            { new(){ Symbol="Ti", Name="Titanium", AtomicNumber=22}}
        };
    }
    public static void Main(string[] args)
    {
        // Creates List of type string
        var weekDays = new List<string> { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
        DisplayWeekDays(weekDays);

        // Adds an element at end of the List
        weekDays.Add("Saturday");
        DisplayWeekDays(weekDays);

        // Removes an element given in argument
        weekDays.Remove("Sunday");
        DisplayWeekDays(weekDays);

        // Removes an element from given index of the List
        weekDays.RemoveAt(0);
        DisplayWeekDays(weekDays);

        // Sorts element of List
        weekDays.Sort();
        DisplayWeekDays(weekDays);

        // Gives maximum, minimum and count value of List
        Console.WriteLine("Max : " + weekDays.Max() + " | Min : " + weekDays.Min() + " | Count : " + weekDays.Count());
        Console.WriteLine();


        // Creates List of type Cpntinent class's object
        var continents = new List<Continent>
        {
            new (){Name="Asia",Population=59.4},
            new (){Name="Europe",Population=9.4},
            new (){Name="Africa",Population=17.6},
            new (){Name="Antarctica",Population=0},
        };
        DisplayContinent(continents);

        // Adds new element at end of the List
        continents.Add(new() { Name = "North America", Population = 1.5 });
        DisplayContinent(continents);

        // Removes an element from given index of the List
        continents.RemoveAt(1);
        DisplayContinent(continents);

        // Displays dictionary having key of type string and values of type Element's object
        DisplayDictionary();

        // Displays List of type Element's object
        DisplayList();

    }

    #endregion

}