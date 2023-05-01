// My data
ISearchable[][] searchables =
{
    new Course[]
    {
        new Course("Studieteknik"),
        new Course("Grundlæggende programmering"),
        new Course("Database"),
        new Course("Computerteknologi"),
        new Course("Oop"),
        new Course("Clientside programmering"),
        new Course("Netwærk")
    },
    new Teacher[]
    {
        new Teacher("Niels", "Oleson"),
        new Teacher("Flemming", "Sørensen"),
        new Teacher("Peter Suni", "Lindenskov"),
        new Teacher("Henrik Vincents", "Poulsen")
    },
    new Student[]
    {
        new Student("Alexander Mathias", "Thamdrup"),
        new Student("Allan", "Gawron"),
        new Student("Andreas Carl", "Balle"),
        new Student("Darab", "Haqnazar"),
        new Student("Felix Enok", "Berger"),
        new Student("Jamie Jamahl de la Sencerie", "El-Dessouky"),
        new Student("Jeppe Carlseng", "Pedersen"),
        new Student("Joseph", "Holland-Hale"),
        new Student("Kamil Marcin", "Kulpa"),
        new Student("Loke Emil", "Bendtsen"),
        new Student("Mark Hyrsting", "Larsen"),
        new Student("Niklas Kim", "Jensen"),
        new Student("Hjorth", "Hjorth"),
        new Student("Sammy", "Damiri"),
        new Student("Thomas Mose", "Holmberg"),
        new Student("Tobias Casanas", "Besser"),
        new Student("Tobias Kofoed", "Larsen")
    }
};
// syncronisor loop
// the teacher part dependent on specifics and is only useable in this program
foreach (Course course in searchables[0])
{
    int i = 0;
    if (course.Name == "Oop" || i > 0)
        i++;
    course.Teacher = (Teacher)searchables[1][i];
    if (course.Teacher != null)
    {       
        List<Course> courseList = new List<Course>();
        if (course.Teacher.Courses != null)
            courseList = course.Teacher.Courses.ToList();
        courseList.Add(course);
        course.Teacher.Courses = courseList.ToArray();        
    }
    course.students = (Student[]?)searchables[2];
    if (course.students != null)
    {
        foreach (Student student in course.students)
        {
            List<Course> courseList = new List<Course>();
            if (student.Courses != null)
                courseList = student.Courses.ToList();
            courseList.Add(course);
            student.Courses = courseList.ToArray();
        }
    }
}

// Getting display attribute from SearchCategory enum 
DisplayAttribute?[] displayAttributes =
{
    SearchCategory.course.GetType().GetMember(SearchCategory.course.ToString()).First().GetCustomAttribute<DisplayAttribute>(),
    SearchCategory.teacher.GetType().GetMember(SearchCategory.teacher.ToString()).First().GetCustomAttribute<DisplayAttribute>(),
    SearchCategory.student.GetType().GetMember(SearchCategory.student.ToString()).First().GetCustomAttribute<DisplayAttribute>()
};

// variables
SearchCategory searchCategory = SearchCategory.course;
ConsoleKey keyInput;
bool rightInput = false;
int index = 0,
    searchIndex = 0;
string? searchInput;

// program start:
while (true)
{
    // Select category
    do
    {
        Write($"1: {displayAttributes[0]?.Name}\n2: {displayAttributes[1]?.Name}\n3: {displayAttributes[2]?.Name}\n4: Afslut\nTryk 1, 2, 3 eller 4 ");

        keyInput = ReadKey(true).Key;

        switch (keyInput)
        {
            case ConsoleKey.D1:
            case ConsoleKey.NumPad1:
                searchCategory = SearchCategory.course;
                rightInput = true;
                break;

            case ConsoleKey.D2:
            case ConsoleKey.NumPad2:
                searchCategory = SearchCategory.teacher;
                rightInput = true;
                break;

            case ConsoleKey.D3:
            case ConsoleKey.NumPad3:
                searchCategory = SearchCategory.student;
                rightInput = true;
                break;

            case ConsoleKey.D4:
            case ConsoleKey.NumPad4:
                Environment.Exit(0);
                break;

            default:
                rightInput = false;
                break;
        }
        Clear();
    }
    while (!rightInput);

    searchIndex = searchCategory.GetHashCode();
    // Search page
    do
    {
        if (!rightInput) 
            WriteLine("Det var ikke fundet prøv igen");
        rightInput = false;
        Write($"Søg {displayAttributes[searchIndex]?.Name} her: ");
        int left = CursorLeft,
            top = CursorTop;
        SetCursorPosition(0, top + 1);
        WriteLine($"\nValgmuligheder:");
        for (int i = 0; i < searchables[searchIndex].Length; i++)
        {
            WriteLine(searchables[searchIndex][i].Name);
        }
        SetCursorPosition(left, top);
        searchInput = ReadLine()?.ToLower().Trim();

        for (int i = 0; i < searchables[searchIndex].Length; i++)
        {
            rightInput = searchables[searchIndex][i].Name.ToLower() == searchInput;
            if (rightInput)
            {
                index = i;
                break;
            }
        }                
        Clear();
    }
    while (!rightInput);

    Type type = searchables[searchIndex][index].GetType();

    // Output page
    if (type == typeof(Course))
    {
        Course course = (Course)searchables[searchIndex][index];
        WriteLine($"{displayAttributes[0]?.Name}: {course.Name}");
        WriteLine($"{displayAttributes[1]?.Name}: {course.Teacher?.Name}");
        PrintCourseStudents(displayAttributes, course);
    }
    else if (type == typeof(Teacher)) 
    {
        Teacher teacher = (Teacher)searchables[searchIndex][index];
        WriteLine($"{displayAttributes[1]?.Name}: {teacher.Name}");
        if (teacher.Courses !=  null) 
        { 
            foreach (Course course in teacher.Courses)
            {
                WriteLine($"{displayAttributes[0]?.Name}: {course.Name}");
                PrintCourseStudents(displayAttributes,course);
                WriteLine();
            }
        }
    }
    else if (type == typeof(Student))
    {
        Student student = (Student)searchables[searchIndex][index];
        if (student.Courses != null)
        {
            foreach (Course course in student.Courses)
            {
                WriteLine($"{displayAttributes[0]?.Name}: {course.Name}");
                WriteLine($"{displayAttributes[1]?.Name}: {course.Teacher?.Name}\n");
            }
        }
    }
    WriteLine("Tryk på en tast for at vende tilbage");
    ReadKey(true);
    Clear();
}

// Local functions
static void PrintCourseStudents(DisplayAttribute?[] displayAttributes ,Course course)
{
    WriteLine($"{displayAttributes[2]?.Name}er:");
    if (course.students != null)
    {
        foreach (Student student in course.students)
        {
            WriteLine($"    {student.Name}");
        }
    }
}