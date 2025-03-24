﻿using MotorcycleLicenseTrainingAPI.DTO;
using MotorcycleLicenseTrainingAPI.Model;
using MotorcycleLicenseTrainingAPI.Repository.Interface;
using MotorcycleLicenseTrainingAPI.Service.Interface;

namespace MotorcycleLicenseTrainingAPI.Service.Implementation
{
    public class PracticeHistoryService : IPracticeHistoryService
    {
        private readonly IPracticeHistoryRepository _practiceHistoryRepository;

        public PracticeHistoryService(IPracticeHistoryRepository practiceHistoryRepository)
        {
            _practiceHistoryRepository = practiceHistoryRepository;
        }

        public async Task<PracticeHistories> CreatePracticeHistoryAsync(PracticeHistories practiceHistory)
        {
            return await _practiceHistoryRepository.CreatePracticeHistoryAsync(practiceHistory);
        }

        public async Task<PracticeHistories> UpdatePracticeHistoryAsync(PracticeHistories practiceHistory)
        {
            return await _practiceHistoryRepository.UpdatePracticeHistoryAsync(practiceHistory);
        }

        public async Task<PracticeHistories> GetPracticeHistoryByIdAsync(int id)
        {
            return await _practiceHistoryRepository.GetPracticeHistoryByIdAsync(id);
        }

    }
}
