namespace UtilityFunctions
{
    public class Utility
    {
        public string GetNameOfUser()
        {
            string namePath = System.Security.Principal.WindowsIdentity.GetCurrent().Name; // Get Path of Username
            string[] namePathArray = namePath.Split('\\'); // Split by Path Directory Seperator ('\')
            string name = namePathArray[namePathArray.Length - 1]; // Get Last Index of Name Path (Username)

            return name;
        }
        public string NavigateParents(string path, int num)
        {
            string[] pathArray = path.Split('\\'); // Split by Path Directory Seperator ('\')
            string[] newPathArray = new string[pathArray.Length - num];
            if (num < pathArray.Length && num > 0)
            {
                for (int i = 0; i < newPathArray.Length; i++) // Only loop up to the length of the new path array
                {
                    newPathArray[i] = pathArray[i];
                }
                string newPath = string.Join("\\", newPathArray);

                return newPath;
            }
            return path;
        }
        public void CreateFile(string filePath, string fileContents)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write(fileContents);
                }
            }
            catch (Exception exp)
            {
                Console.Write(exp.Message);
            }
        }
    }
}