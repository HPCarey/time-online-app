namespace TimeOnlineApp
{
    internal class Program
    {
        //Paths and File names
        const string DATAFILEPATH = @"C:\Users\ICTLearner02\Desktop\TimeOnlineApp\";
        const string TIMEONLINEDATA = "TimeOnlineByAge.csv";
        const string SUMMARY = "Summary.txt";

        //Arrays
        static string[] ageGroups = { "Child", "Teenager", "Young Adult", "Adult", "Older Person" };
        static double[] totalTimeOnline = new double[5]; //stores the total time online for each category
        static int[] totalSubjects = new int[5]; //keeps a count of the number of subjects
        static string[] SubjectMostHours = new string[5]; //stores the subject with most hours for each category
        static double[] highesthours = new double[5]; //stores the highest value for hours for each category

        //Data Lines
        static string[] allDataLines;


        static void Main(string[] args)
        {
            // Read the file inside a try catch in case of errors like the file is already open
            try
            {
                allDataLines = File.ReadAllLines(DATAFILEPATH + TIMEONLINEDATA);

                // Print each line individually to test file read ok

                //foreach (var line in allDataLines)
                //{
                //    Console.WriteLine(line);
                //}

                //Skip the header
                for (int i = 1; i < allDataLines.Length; i++)
                {
                    ProcessData(allDataLines[i]);
                }
                DisplayResults();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
       static void DisplayResults()
        {
            string outputLine;
            //Delete the Summary file if it exists
            if (File.Exists(DATAFILEPATH + SUMMARY))
            {
                File.Delete(DATAFILEPATH + SUMMARY);
            }
            //iterate through ageGroup array to  get the values
            for (int i = 0; i < ageGroups.Length; i++)
            {
                outputLine = ageGroups[i];
                DisplayAndWrite(outputLine);
                outputLine = $"Total time spent online : {totalTimeOnline[i]}";
                DisplayAndWrite(outputLine);
                outputLine = $"Average time spent oneline : {totalTimeOnline[i] / totalSubjects[i]}";
                DisplayAndWrite(outputLine);
                outputLine = $"Person who spent the most time online : {SubjectMostHours[i]}";
                DisplayAndWrite(outputLine);

            }
        }
        static void DisplayAndWrite(string outputLine)
        {
            // print results to the console
            Console.WriteLine(outputLine);

            // append to a file called summary 
            // File.AppendAllText(DATAFILEPATH + SUMMARY, outputLine); No CR /LF
            File.AppendAllText(DATAFILEPATH + SUMMARY, outputLine + "\n");
        }
        static void ProcessData(string dataLine)
        {
            // Split the data lines into the datafields array
            string[] datafield = dataLine.Split(',');
            string ageGroup = datafield[1];
            double totalHours = 0;

            // Find the correct age group
            string correspondingAgeGroup = GetAgeGroup(int.Parse(ageGroup));

            // Loop through the hours columns (index 2 to 8)
            // increment the totalHours for i
            //This assumes the hours per day are recorded in the 3rd - 9th columns
            for (int i = 2; i <= 8; i++)
            {
                totalHours += double.Parse(datafield[i]);
            }

            // Update total time spent online for the corresponding age group
            int ageGroupIndex = Array.IndexOf(ageGroups, correspondingAgeGroup);
            totalTimeOnline[ageGroupIndex] += totalHours;

            // Update most hours subject if the current subject has more hours
            if (totalHours > highesthours[ageGroupIndex])
            {
                highesthours[ageGroupIndex] = totalHours;
                SubjectMostHours[ageGroupIndex] = datafield[0]; // Assuming subject ID is in the first column
            }

            // Update total subjects for the corresponding age group
            totalSubjects[ageGroupIndex]++;
            

        }

        static string GetAgeGroup(int age)
        {

            if (age <= 12)
            {
                return "Child";
            }
            else if (age <= 19)
            {
                return "Teenager";
            }
            else if (age <= 30)
            {
                return "Young Adult";
            }
            else if (age <= 60)
            {
                return "Adult";
            }
            else
            {
                return "Older Person";
            }
        }

    }
}