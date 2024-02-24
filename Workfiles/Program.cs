using Workfiles;

const string DateFormat = "dd.MM.yyyy";
const string MyDbDirectory = @"D:\MyDB";
Console.WriteLine("What do you want to do?\n 1-Create student\n  2-See all registered students");
int option = int.Parse(Console.ReadLine());
Console.WriteLine();
switch (option)
{
    case 1:
        Student st = CreateStudent();
        RegisterStudentinDb(st);
        break;
    case 2:
        Student[] students = GetStudentsfromDb();
        PrintStudents(students);
        break;
    default:
        Console.WriteLine("Please the correct number :(");
        break;
}
//Student-i yaratmaq. 
static Student CreateStudent()
{
    Student st = new();
    Console.WriteLine("Registering a student....");
    Console.WriteLine();
    Console.Write("Name:");
    st.Name= Console.ReadLine();
    Console.Write("Surname:");
    st.Surname= Console.ReadLine();
    Console.Write($"DateofBirth {DateFormat}:");
    st.DateofBirth=DateTime.Parse(Console.ReadLine());

    st.Id=Guid.NewGuid();

    return st;
}
//Fayl-a yazmaq.
static void RegisterStudentinDb(Student st)
{
    var files = Path.Combine(MyDbDirectory,st.Id+"txt");
    if (File.Exists(files))
    {
        Console.WriteLine("This student has already been registered!");
    }
    else
    {
        var filetext = $"Name:{st.Name}\n" +
            $"Surname:{st.Surname}\n" +
            $"DateofBirth:{st.DateofBirth.ToString(DateFormat)}";
        File.WriteAllText(files,filetext);
        Console.WriteLine("Student is successfully registered.");
    }
}
//Studentleri fayldan almaq.
static Student[] GetStudentsfromDb()
{
  DirectoryInfo directory=new DirectoryInfo(MyDbDirectory);
    var files= directory.GetFiles();
    
    Student[] students = new Student[10];
    var i = 0;
    foreach (var file in files)
    {
        Student st = new();    
        var filetext = File.ReadAllLines(file.FullName);
        st.Name = filetext[0].Split(':')[1].Trim();
        st.Surname = filetext[1].Split(':')[1].Trim();
        st.DateofBirth = DateTime.Parse(filetext[2].Split(':')[1].Trim());
        students[i] = st;
        i++;
        
    }
    return students;
}
//Studentleri cap etmek.
static void PrintStudents(Student[]students)
{
    foreach (Student st in students)
    {
        if (st is null)
        {
            break;
        }
        PrintStudent(st);
        Console.WriteLine();
        Console.WriteLine("--------------------");
        Console.WriteLine();
    }
    static void PrintStudent(Student st)
    {

        Console.WriteLine($"{nameof(st.Name)} : {st.Name}");
        Console.WriteLine($"{nameof(st.Surname)} : {st.Surname}");
        Console.WriteLine($"{nameof(st.DateofBirth)} : {st.DateofBirth.ToString(DateFormat)}");
    }
}


