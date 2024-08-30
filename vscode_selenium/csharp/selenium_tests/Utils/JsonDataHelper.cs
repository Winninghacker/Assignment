using Newtonsoft.Json;
using System;
using System.IO;

namespace selenium_tests.Utils
{
    public static class JsonDataHelper
    {
        public static T LoadTestData<T>(string filePath)
        {
            try
            {
                // Check if file exists
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"The file at path '{filePath}' was not found.");
                }

                // Read JSON from the file
                var json = File.ReadAllText(filePath);

                // Deserialize JSON to the specified type
                var data = JsonConvert.DeserializeObject<T>(json);

                // Check if data is null
                if (data == null)
                {
                    throw new InvalidOperationException("Deserialized data is null. Ensure that the JSON format matches the expected type.");
                }

                return data;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"An error occurred while loading test data: {ex.Message}");
                throw; // Re-throw the exception after logging
            }
        }
    }
}
