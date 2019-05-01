using AutoMapper;
using FilmWords.Data.Dto;
using FilmWords.Data.Entities;
using FilmWords.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmWords.Controllers
{
    [Route("api/[controller]")]
    public class SentenceController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISentenceService _sentenceService;

        public SentenceController(IMapper mapper, ISentenceService sentenceService)
        {
            _mapper = mapper;
            _sentenceService = sentenceService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSentence([FromBody]SentenceDto sentenceDto)
        {
            var sentence = _mapper.Map<Sentence>(sentenceDto);

            sentence = await _sentenceService.CreateSentenceAsync(sentence);

            if(sentence == null)
            {
                return BadRequest();
            }

            sentenceDto = _mapper.Map<SentenceDto>(sentence);
            return Ok(sentenceDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sentence = await _sentenceService.GetSentenceByIdAsync(id);

            if(sentence == null)
            {
                return NotFound();
            }

            var sentenceDto = _mapper.Map<SentenceDto>(sentence);
            return Ok(sentenceDto);
        }

        /// <summary>
        /// returns same sentences of different users with different translations
        /// </summary>
        /// <param name="getSentencesDto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetByText([FromBody]GetSentencesDto getSentencesDto)
        {
            var sentences = await _sentenceService.GetSentencesByTextAsync(getSentencesDto.Text);

            if(sentences == null)
            {
                return NotFound();
            }

            var sentencesDto = _mapper.Map<List<SentenceDto>>(sentences);

            return Ok(sentencesDto);
        }

        [HttpGet("user/{id:int}")]
        public async Task<IActionResult> GetAllSentencesByUserId(int id)
        {
            var sentencesLearnt = await _sentenceService.GetUserSentencesAsync(id);
            var sentencesUnlearnt = await _sentenceService.GetUserSentencesAsync(id, true);

            var mergedSentences = sentencesLearnt
                .Union(sentencesUnlearnt)
                .ToList();

            if(mergedSentences == null)
            {
                return NotFound();
            }

            return Ok(mergedSentences);
        }


        /// <summary>
        /// returns user sentences which have been learnt or unlearnt depends on parameter isLearnt
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isLearnt">true- returns learnt, false- unlearnt</param>
        /// <returns></returns>
        [HttpGet("user/{id:int}/{isLearnt:bool}")]
        public async Task<IActionResult> GetSentencesByUserId(int id, bool isLearnt)
        {
            var sentences = await  _sentenceService.GetUserSentencesAsync(id, isLearnt);

            if(sentences == null)
            {
                return NotFound();
            }

            return Ok(sentences);
        }
    }
}
