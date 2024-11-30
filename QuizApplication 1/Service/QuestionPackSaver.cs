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
        public async static void SaveToJson(QuestionPackViewModel questionPackViewModel, string fileName)
        {
           
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "QuizApplication");

           
            Directory.CreateDirectory(appDataPath);

            
            string filePath = Path.Combine(appDataPath, fileName);

          
            var dto = new QuestionPackDTO
            {
                Name = questionPackViewModel.Name,
                Difficulty = questionPackViewModel.Difficulty,
                TimeLimit = questionPackViewModel.TimeLimit,
                Questions = questionPackViewModel.Questions.Select(q => new QuestionDTO
                {
                    Query = q.Query,
                    CorrectAnswer = q.CorrectAnswer,
                    IncorrectAnswers = q.IncorrectAnswers.ToArray() 
                }).ToList()
            };

            
            string json =  JsonSerializer.Serialize(dto, new JsonSerializerOptions { WriteIndented = true });

            await File.WriteAllTextAsync(filePath, json);
        }

    }
}
