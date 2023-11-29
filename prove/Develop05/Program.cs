using System.IO;
using System.Collections.Generic;
 
namespace GoalTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new instance of the GoalTracker class
            GoalTracker tracker = new GoalTracker();
 
            // Load the user's goals and score from file
            tracker.Load();
 
            // Display the user's current score
            Console.WriteLine($"Your current score is: {tracker.Score}");
 
            // Display the user's goals
            tracker.DisplayGoals();
 
            // Allow the user to create new goals
            Console.WriteLine("Would you like to create a new goal? (y/n)");
            string response = Console.ReadLine();
            while (response.ToLower() == "y")
            {
                Console.WriteLine("What type of goal would you like to create? (simple/eternal/checklist)");
                string goalType = Console.ReadLine();
                Console.WriteLine("What is the name of the goal?");
                string goalName = Console.ReadLine();
                Console.WriteLine("How many points should the user receive each time they record this goal?");
                int points = int.Parse(Console.ReadLine());
 
                switch (goalType.ToLower())
                {
                    case "simple":
                        Console.WriteLine("What is the value of this goal?");
                        int value = int.Parse(Console.ReadLine());
                        tracker.CreateSimpleGoal(goalName, value, points);
                        break;
                    case "eternal":
                        tracker.CreateEternalGoal(goalName, points);
                        break;
                    case "checklist":
                        Console.WriteLine("How many times must the user complete this goal to receive the bonus?");
                        int bonusThreshold = int.Parse(Console.ReadLine());
                        tracker.CreateChecklistGoal(goalName, bonusThreshold, points);
                        break;
                    default:
                        Console.WriteLine("Invalid goal type. Please try again.");
                        break;
                }
 
                Console.WriteLine("Goal created successfully.");
                Console.WriteLine("Would you like to create another goal? (y/n)");
                response = Console.ReadLine();
            }
 
            // Allow the user to record an event
            Console.WriteLine("Would you like to record an event? (y/n)");
            response = Console.ReadLine();
            while (response.ToLower() == "y")
            {
                Console.WriteLine("Which goal would you like to record an event for?");
                string goalName = Console.ReadLine();
                Console.WriteLine("How many times did you complete the goal?");
                int count = int.Parse(Console.ReadLine());
 
                if (tracker.RecordEvent(goalName, count))
                {
                    Console.WriteLine("Event recorded successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to record event. Please try again.");
                }
 
                Console.WriteLine("Would you like to record another event? (y/n)");
                response = Console.ReadLine();
            }
 
            // Save the user's goals and score to file
            tracker.Save();
        }
    }
 
    class GoalTracker
    {
        private List<Goal> goals;
        private int score;
 
        public GoalTracker()
        {
            goals = new List<Goal>();
            score = 0;
        }
 
        public int Score
        {
            get { return score; }
        }
 
        public void CreateSimpleGoal(string name, int value, int points)
        {
            SimpleGoal goal = new SimpleGoal(name, value, points);
            goals.Add(goal);
        }
 
        public void CreateEternalGoal(string name, int points)
        {
            EternalGoal goal = new EternalGoal(name, points);
            goals.Add(goal);
        }
 
        public void CreateChecklistGoal(string name, int bonusThreshold, int points)
        {
            ChecklistGoal goal = new ChecklistGoal(name, bonusThreshold, points);
            goals.Add(goal);
        }
 
        public bool RecordEvent(string name, int count)
        {
            foreach (Goal goal in goals)
            {
                if (goal.Name.ToLower() == name.ToLower())
                {
                    if (goal is SimpleGoal)
                    {
                        SimpleGoal simpleGoal = (SimpleGoal)goal;
                        score += simpleGoal.RecordEvent(count);
                        return true;
                    }
                    else if (goal is EternalGoal)
                    {
                        EternalGoal eternalGoal = (EternalGoal)goal;
                        score += eternalGoal.RecordEvent(count);
                        return true;
                    }
                    else if (goal is ChecklistGoal)
                    {
                        ChecklistGoal checklistGoal = (ChecklistGoal)goal;
                        score += checklistGoal.RecordEvent(count);
                        return true;
                    }
                }
            }
 
            return false;
        }
 
        public void DisplayGoals()
        {
            foreach (Goal goal in goals)
            {
                Console.Write($"[{(goal.IsComplete ? "X" : " ")}] {goal.Name}");
 
                if (goal is ChecklistGoal)
                {
                    ChecklistGoal checklistGoal = (ChecklistGoal)goal;
                    Console.WriteLine($" (Completed {checklistGoal.Count}/{checklistGoal.BonusThreshold} times)");
                }
                else
                {
                    Console.WriteLine();
                }
            }
        }
 
        public void Save()
        {
            // Code to save goals and score to file
        }
 
        public void Load()
        {
            // Code to load goals and score from file
        }
    }
 
    abstract class Goal
    {
        protected string name;
        protected bool isComplete;
        protected int points;
 
        public Goal(string name, int points)
        {
            this.name = name;
            this.points = points;
            isComplete = false;
        }
 
        public string Name
        {
            get { return name; }
        }
 
        public bool IsComplete
        {
            get { return isComplete; }
        }
 
        public int Points
        {
            get { return points; }
        }
 
        public abstract int RecordEvent(int count);
    }
 
    class SimpleGoal : Goal
    {
        private int value;
 
        public SimpleGoal(string name, int value, int points) : base(name, points)
        {
            this.value = value;
        }
 
        public override int RecordEvent(int count)
        {
            if (count == value)
            {
                isComplete = true;
                return points;
            }
            else
            {
                return 0;
            }
        }
    }
 
    class EternalGoal : Goal
    {
        public EternalGoal(string name, int points) : base(name, points)
        {
            isComplete = false;
        }
 
        public override int RecordEvent(int count)
        {
            isComplete = true;
            return points;
        }
    }
 
    class ChecklistGoal : Goal
    {
        private int count;
        private int bonusThreshold;
 
        public ChecklistGoal(string name, int bonusThreshold, int points) : base(name, points)
        {
            count = 0;
            this.bonusThreshold = bonusThreshold;
        }
 
        public int Count
        {
            get { return count; }
        }
 
        public int BonusThreshold
        {
            get { return bonusThreshold; }
        }
 
        public override int RecordEvent(int count)
        {
            this.count += count;
 
            if (this.count >= bonusThreshold)
            {
                isComplete = true;
                return points + 500;
            }
            else
            {
                return points;
            }
        }
    }
}