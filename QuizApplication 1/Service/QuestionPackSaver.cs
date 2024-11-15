﻿using QuizApplication_1.Model;
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
        public static void SaveToJson(QuestionPackViewModel questionPackViewModel, string fileName)
        {
            // Get the path to the Local Application Data folder
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "QuizApplication");

            // Ensure the directory exists
            Directory.CreateDirectory(appDataPath);

            // Create the full file path
            string filePath = Path.Combine(appDataPath, fileName);

            // Create a DTO to store the data in a serializable format
            var dto = new QuestionPackDTO
            {
                Name = questionPackViewModel.Name,
                Difficulty = questionPackViewModel.Difficulty,
                TimeLimit = questionPackViewModel.TimeLimit,
                Questions = questionPackViewModel.Questions.Select(q => new QuestionDTO
                {
                    Query = q.Query,
                    CorrectAnswer = q.CorrectAnswer,
                    IncorrectAnswers = q.IncorrectAnswers.ToArray() // Convert to array if needed
                }).ToList()
            };

            // Serialize the DTO to JSON
            string json = JsonSerializer.Serialize(dto, new JsonSerializerOptions { WriteIndented = true });

            // Write the JSON to the file
            File.WriteAllText(filePath, json);
        }

    }
}
