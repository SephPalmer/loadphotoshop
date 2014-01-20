using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace load_photoshop
{
    class Program
    {
        

        static void Main(string[] args)
        {
            //be careful to use capitals correctly if that is how the application is named
            string application_name = "Photoshop.exe";
            string parent_folder = "\\adobe\\";
            //string application_name = "Bridge.exe";
            Boolean application_found = false;
            string application_path = "not known";

            //check if the application is installed try to get the path to the install directory from the registry
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.LocalMachine;
            Microsoft.Win32.RegistryKey subKey1 = regKey.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths");
            string[] subKeyNames = subKey1.GetSubKeyNames();

            foreach (string subKeyName in subKeyNames)
            {
                
                Microsoft.Win32.RegistryKey subKey2 = subKey1.OpenSubKey(subKeyName);
                Console.WriteLine(Environment.GetEnvironmentVariable("ProgramW6432"));
                
                String registry_entry = subKey2.ToString();
                if (registry_entry.Contains(application_name.ToLower()))
                {
                    application_found = true;
                    application_path = (string)subKey2.GetValue("Path");
                }


                subKey2.Close();
            }

            subKey1.Close();

            //if going through the registery fails then try to find photoshop by another method
            if (application_found != true)
            {
                
                string directory_32 = Environment.GetEnvironmentVariable("ProgramFiles(x86)");
                
                string appended_directory = directory_32 + parent_folder;
                string[] subdirectories = {};
                if (Directory.Exists(appended_directory))
                {
                    subdirectories = Directory.GetDirectories(appended_directory);
                }
                else
                {
                    Console.WriteLine("*******DIRECTORY DOES NOT EXIST *******");
                }

                foreach (string directory in subdirectories)
                {
                    //Console.WriteLine(directory);
                    string[] directory_files = Directory.GetFiles(directory);
                    foreach (string directory_file in directory_files)
                    {
                        if (directory_file.Contains(application_name))
                        {
                            application_found = true;
                            application_path = directory;
                        }
                    }
                }



              




            }

            //results
            Console.WriteLine("The application called " + application_name);
            if (application_found == true)
            {
                Console.WriteLine("was found");
                Console.WriteLine("The install path is " + application_path);
            }
            else
            {
                Console.WriteLine("was not found");
            }

            Console.ReadLine();

        }

        
    }
}
