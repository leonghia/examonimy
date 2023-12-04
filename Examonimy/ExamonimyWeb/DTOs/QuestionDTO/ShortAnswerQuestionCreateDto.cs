﻿using ExamonimyWeb.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExamonimyWeb.DTOs.QuestionDTO
{
    public class ShortAnswerQuestionCreateDto : QuestionCreateDto
    {

        [Required]
        public required string CorrectAnswer { get; set; }
    }
}
