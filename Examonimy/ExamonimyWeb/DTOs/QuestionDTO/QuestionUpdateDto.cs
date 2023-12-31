﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ExamonimyWeb.DTOs.QuestionDTO
{
    public class QuestionUpdateDto
    {
        [Required]
        public required int QuestionLevelId { get; set; }

        [Required]
        public required string QuestionContent { get; set; }
    }
}
