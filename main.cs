using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Coodx11
{
    /// <summary>
    /// Coodx11 - The powerful console text editor for pigeons.
    /// </summary>
    public class Program
    {
        // The list holds all the lines of the document content.
        private static List<string> documentContent = new List<string>();

        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Coodx11 - The Pigeon Editor! ");

            while (true)
            {
                DisplayMenu();

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            StartEditing();
                            break;
                        case 2:
                            SaveDocument();
                            break;
                        case 3:
                            LoadDocument();
                            break;
                        case 4:
                            Console.WriteLine("Coodx11 closing. Coo coo!");
                            return; // Exit the application
                        default:
                            Console.WriteLine("\n[INFO] Invalid choice. Please select 1-4.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\n[ERROR] Invalid input. Please enter a number.");
                }
            }
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("\n--- 🐦 Coodx11 Command Line Editor ---");
            Console.WriteLine("1. Start/Continue Editing");
            Console.WriteLine("2. Save Document");
            Console.WriteLine("3. Load Document");
            Console.WriteLine("4. Exit Editor");
            Console.WriteLine("--------------------------------------");
            Console.Write("Enter your choice: ");
        }

        private static void SaveDocument()
        {
            Console.Write("Enter filename (e.g., my_notes.txt, or a full path): ");
            string filename = Console.ReadLine()?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(filename))
            {
                Console.WriteLine("\n[ERROR] Filename cannot be empty.");
                return;
            }

            try
            {
                // File.WriteAllLines efficiently writes all strings in a list to a file,
                // overwriting any existing content.
                File.WriteAllLines(filename, documentContent);
                Console.WriteLine($"\n[SUCCESS] Document saved successfully to: **{Path.GetFullPath(filename)}**");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[ERROR] Failed to save document: {ex.Message}");
                Console.WriteLine("Check file permissions or the path provided.");
            }
        }

        private static void LoadDocument()
        {
            Console.Write("Enter filename to load (or a full path): ");
            string filename = Console.ReadLine()?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(filename))
            {
                Console.WriteLine("\n[ERROR] Filename cannot be empty.");
                return;
            }

            try
            {
                // File.ReadAllLines reads all lines from the file into a string array.
                string[] lines = File.ReadAllLines(filename);

                // Clear existing content and load new lines
                documentContent.Clear();
                documentContent.AddRange(lines);

                Console.WriteLine($"\n[SUCCESS] Document loaded from: **{Path.GetFullPath(filename)}** ({documentContent.Count} lines)");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"\n[ERROR] File not found: {filename}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[ERROR] Failed to load document: {ex.Message}");
            }
        }

        private static void StartEditing()
        {
            Console.WriteLine("\n*** START EDITING ***");
            Console.WriteLine($"Current Content ({documentContent.Count} lines):");
            for (int i = 0; i < documentContent.Count; i++)
            {
                // Display content with line numbers
                Console.WriteLine($"{(i + 1),4}: {documentContent[i]}");
            }

            Console.WriteLine("\nType your new lines below. Type **'EOF'** on a new line to finish and return to the menu.");

            string? line;
            while (true)
            {
                // Read the line from the console
                line = Console.ReadLine();

                // Check for the exit command
                if (line?.Equals("EOF", StringComparison.OrdinalIgnoreCase) ?? false)
                {
                    break;
                }

                // Add the line to the document content (handles null case defensively)
                documentContent.Add(line ?? string.Empty);
            }
            Console.WriteLine("\n*** EDITING SESSION ENDED ***");
        }
    }
}
