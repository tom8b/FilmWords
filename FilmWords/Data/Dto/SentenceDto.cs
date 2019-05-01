﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmWords.Data.Dto
{
    public class SentenceDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Translation { get; set; }
        public int userId { get; set; }
        public bool isLearnt { get; set; }
    }
}
