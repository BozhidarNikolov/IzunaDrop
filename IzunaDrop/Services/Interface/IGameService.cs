﻿
using System.Collections.Generic;
using System.Threading.Tasks;
using IzunaDrop.Data.Models;
namespace IzunaDrop.Services.Interface
{
    public interface IGameService
    {
        Task<IEnumerable<Game>> GetAllGamesAsync();
        Task<Game> GetGameByIdAsync(int id);
    }
}
