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
        static double[] totalTimeOnline = new double[5];
        static double[] totalSubjects = new double[5];
        static string[] SubjectMostHours = new string[5];

        //Data Lines
        static string[] allDataLines;


        static void Main(string[] args)
        {
            try
            {
                allDataLines = File.ReadAllLines(DATAFILEPATH + TIMEONLINEDATA);

                // Print each line individually
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
            Console.WriteLine(outputLine);
            // File.AppendAllText(DATAFILEPATH + SUMMARY, outputLine); No CR /LF
            File.AppendAllText(DATAFILEPATH + SUMMARY, outputLine + "\n");
        }
        static void ProcessData(string dataLine)
        {

        }

    }
}