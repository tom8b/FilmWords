using FilmWords.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmWords.Services.Interfaces
{
    public interface ISentenceService
    {
        Task<Sentence> CreateSentenceAsync(Sentence sentence);
        Task<Sentence> GetSentenceByIdAsync(int id);
        Task<List<Sentence>> GetSentencesByTextAsync(string text);
        Task<List<Sentence>> GetUserSentencesAsync(int userId, bool learnt = false);
        Task<Sentence> ChangeIsLearntStatusByIdAsync(int id);
        Task<Sentence> UpdateSentenceByIdAsync(Sentence sentenceForUpdate);
        Task<bool> DeleteSentenceByIdAsync(int id);
    }
}
