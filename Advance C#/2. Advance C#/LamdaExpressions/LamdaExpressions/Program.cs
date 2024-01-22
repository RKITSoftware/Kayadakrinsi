public class LMA01
{
    #region Public Members

    /// <summary>
    /// Id of lamda expression
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of lamda expression
    /// </summary>
    public string Name { get; set; }

    #endregion

    #region Public Methods

    public static void Main(String[] args)
    {
        List<LMA01> list = new List<LMA01> { 
            new LMA01 { Id = 1,Name = "exp1"},
            new LMA01 { Id = 2,Name = "exp2"},
            new LMA01 { Id = 3,Name = "exp3"},
            new LMA01 { Id = 4,Name = "exp4"},
            new LMA01 { Id = 4,Name = "exp5"}
        };
        
        // Simple lamda expression
        var objPrint = list.FindAll(o => o.Id == 4);
        foreach(var obj in objPrint)
        {
            Console.WriteLine(obj.Name);
        }

        // Lamda expression with two parameters returns sum of arguments
        Func<int,int,int> sum = (x,y) => x + y;
        Console.WriteLine(sum(20,30));

        // Lamda expression with methods
        Action<string> greet = name =>
        {
            string greeting = $"Hello {name}!";
            Console.WriteLine(greeting);
        };
        greet("Everyone");

        // Lamda expression with two parameters returns equality of arguments
        Func<int, int, bool> test = (x, y) => x == y;
        Console.WriteLine(test(1,3));

        // Lamda expression with constant return value on any arguments
        Func<int, int, string> constant = (_, _) => "How Are You?";
        Console.WriteLine(constant(2,3));

        // Lamda expression with constant return value on any arguments
        Func<int, int, LMA01> constantObj = (_, _) => list.FirstOrDefault(o=> o.Id ==1 );
        Console.WriteLine(constantObj(2, 3).Name);

    }

    #endregion
}