using Newtonsoft.Json;

namespace TaskManagement
{
    class Task
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; } = false;

    }

    class TaskManager
    {
        private List<Task> tasks = new List<Task>(); //For Containing tasks
        string path = "C:\\Users\\dk530\\OneDrive\\Desktop\\Csharp\\MY PROJECT\\TaskManagement\\Tasks.json"; //Path for task store and retrieve
                                                                                                             //NOTE: CHANG PATH ACCORDING TO YOUR'S WHERE YOU WANT TO STORE JSON FILE.
        public TaskManager()
        {
            if (!File.Exists(path)) //If file not exist then it create
            {
                File.CreateText(path).Close();
            }
            else
            {
                string json = File.ReadAllText(path);   // Read all data from json file
                tasks = JsonConvert.DeserializeObject<List<Task>>(json) ?? new List<Task>(); // Covert data from json to list format and store List<Task> tasks.
            }
        }

        // ADD METHOD :
        public void Add()
        {
            Task task = new Task();

            Console.WriteLine(Environment.NewLine + "---- Add a Task ----" + Environment.NewLine);
            // Take all details by USER
            Console.Write("Enter Task Name: ");
            string name = Console.ReadLine();
            task.Name = name;

            Console.Write("Enter Description: ");
            string desc = Console.ReadLine();
            task.Description = desc;

            Console.Write("Enter Due Date (yyyy-mm-dd): ");
            DateTime dueDate;
            if (DateTime.TryParse(Console.ReadLine(), out dueDate))
            {
                task.DueDate = dueDate;
                tasks.Add(task);    // Task add in List not in file.
                Console.WriteLine(Environment.NewLine + "Task added successfully!");
            }
            else
            {
                Console.WriteLine(Environment.NewLine + "Invalid date format. Task not added.");
            }
        }

        // VIEW METHOD : 
        public void View()
        {
            Console.WriteLine(Environment.NewLine + "---- View Task ----" + Environment.NewLine);
            if(tasks.Count > 0)
            {

            for (int i = 0; i < tasks.Count; i++)
            {
                Task task = tasks[i];
                // Display task Task status with other details
                Console.WriteLine($"{i + 1}.\t1. [{(task.IsCompleted ? "Complete" : "Incomplete")}] {task.Name} (Due: {task.DueDate.ToString("yyyy-MM-dd")})");
                Console.WriteLine($" \t2. Description: {task.Description}");
                Console.WriteLine();
            }
            }
            else
            {
                Console.WriteLine("Task Not Foumd!");
            }
        }

        // MARK COMPLETED :
        public void MarkCompleted()
        {
            Console.WriteLine(Environment.NewLine + "---- Mark Completed ----");
            Console.Write("Enter the task number to mark as completed: ");
            if (int.TryParse(Console.ReadLine(), out int taskNumber) && taskNumber > 0 && taskNumber <= tasks.Count)  // Check taskNumber is valid or not those Input by USER
            {
                Task task = tasks[taskNumber - 1];  // List indexing start with 0 that's why we write -1.
                if (!task.IsCompleted)  // Check already completion or not
                {

                    task.IsCompleted = true;
                    Console.WriteLine(Environment.NewLine + "Task marked as completed!");
                }
                else
                {
                    Console.WriteLine(Environment.NewLine + "Task marked already as completed!");
                }
            }
            else
            {
                Console.WriteLine("Invalid task number.");
            }
        }

        public void DeleteTask()
        {
            Console.WriteLine("---- Delete Task -----" + Environment.NewLine);
            Console.Write("Enter the task number to delete: ");
            if (int.TryParse(Console.ReadLine(), out int taskNumber) && taskNumber > 0 && taskNumber <= tasks.Count) // Check taskNumber is valid or not those Input by USER
            {
                tasks.RemoveAt(taskNumber - 1); // Remove the Task from List.
                Console.WriteLine("\nTask deleted successfully!");
            }
            else
            {
                Console.WriteLine("Invalid task number.");
            }
        }

        public void SaveTasksToFile()
        {   // When Task Manager want to be exit , then before exit all data will be serialize and saved into JSON file.
            string json = JsonConvert.SerializeObject(tasks);
            File.WriteAllText(path, json);
        }
    }

    class Program
    {
        public static void DisplayMenu()
        {
            Console.WriteLine("******************Task Manager****************");
            Console.WriteLine("1. Add a Task");
            Console.WriteLine("2. View Task");
            Console.WriteLine("3. Mark Completed");
            Console.WriteLine("4. Remove Task");
            Console.WriteLine("5. Exit");
        }

        public static void Main(string[] args)
        {
            TaskManager manager = new TaskManager();

            int choice;
            bool exit = false;
            DisplayMenu();

            do
            {
                Console.Write(Environment.NewLine + "Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            manager.Add();
                            break;
                        case 2:
                            manager.View();
                            break;
                        case 3:
                            manager.MarkCompleted();
                            break;
                        case 4:
                            manager.DeleteTask();
                            break;
                        case 5:
                            exit = true;    // before exit call the SaveTaskToFile method for storing data in Json file
                            manager.SaveTasksToFile();
                            Console.WriteLine(Environment.NewLine + "Exiting Task Manager...");
                            break;
                        default:
                            Console.WriteLine("Invalid Input");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Choice!");
                }
            } while (!exit);

        }
    }
}
