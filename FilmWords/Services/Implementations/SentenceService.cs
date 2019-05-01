using FilmWords.Data;
using FilmWords.Data.Entities;
using FilmWords.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmWords.Services.Implementations
{
    public class SentenceService : ISentenceService
    {
        private readonly ApplicationDbContext _dbContext;

        public SentenceService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Sentence> CreateSentenceAsync(Sentence sentence)
        {
            await _dbContext.Sentences.AddAsync(sentence);

            if(await _dbContext.SaveChangesAsync() > 0)
            {
                return sentence;
            }

            return null;
        }

        public async Task<Sentence> GetSentenceByIdAsync(int id)
        {
            var sentence = await _dbContext.Sentences
                .FirstOrDefaultAsync(s => s.Id == id);

            return sentence;
        }

        public async Task<List<Sentence>> GetSentencesByTextAsync(string text)
        {
            var sentence = await _dbContext.Sentences
                .Where(s => s.Text.ToLower()
                .Equals(text.ToLower())).ToListAsync();

            return sentence;
        }

        public async Task<Sentence> ChangeIsLearntStatusByIdAsync(int id)
        {
            var sentence = await _dbContext.Sentences
                .FirstOrDefaultAsync(s => s.Id == id);

            sentence.isLearnt = !sentence.isLearnt;

            if(await _dbContext.SaveChangesAsync() > 0)
            {
                return sentence;
            }

            return null;
        }

        public async Task<bool> DeleteSentenceByIdAsync(int id)
        {
            var sentence = await _dbContext.Sentences
                .FirstOrDefaultAsync(s => s.Id == id);

            if(sentence == null)
            {
                return false;
            }

            _dbContext.Sentences.Remove(sentence);

            if(await _dbContext.SaveChangesAsync() > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="learnt">Gdy learnt == false, zwraca nienauczone fiszki. Gdy true, nauczone.</param>
        /// <returns></returns>
        public async Task<List<Sentence>> GetUserSentencesAsync(int userId, bool learnt = false)
        {
            var sentences = await _dbContext.Sentences
                .Where(s => (s.userId == userId) && (s.isLearnt == learnt))
                .ToListAsync();

            return sentences;
        }

        public async Task<Sentence> UpdateSentenceByIdAsync(Sentence sentenceForUpdate)
        {
            var sentence = await _dbContext.Sentences
                .FirstOrDefaultAsync(s => s.Id == sentenceForUpdate.Id);

            if(sentence == null)
            {
                return null;
            }

            sentence.Text = sentenceForUpdate.Text;
            sentence.Translation = sentenceForUpdate.Translation;

            _dbContext.Entry(sentence).State = EntityState.Modified;

            if(await _dbContext.SaveChangesAsync() > 0)
            {
                return sentence;
            }

            return null;
        }
    }
}
