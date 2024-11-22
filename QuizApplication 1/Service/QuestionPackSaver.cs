using QuizApplication_1.Model;
using QuizApplication_1.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuizApplication_1.Service
{
    internal class QuestionPackSaver
    {
        public static void SaveToJson(QuestionPack questionPack, string fileName)
        {
           
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "QuizApplication");

           
            Directory.CreateDirectory(appDataPath);

            
            string filePath = Path.Combine(appDataPath, fileName);



            
            string json = JsonSerializer.Serialize(questionPack, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(filePath, json);
        }

    }
}
