﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practical_work_1.Configs.Interfaces;
using Practical_work_1.Entities;
using System.Net;

namespace Practical_work_1.Configs.Implementations
{
    public class TraineeRepository : GenericRepository<Trainee>, ITraineeRepository
    {
        protected readonly TraineeContext _dbContext;
        public TraineeRepository(TraineeContext traineeContext) : base(traineeContext)
        {
            _dbContext = traineeContext;
        }

        public async Task<Trainee> CreateTrainee(Trainee trainee)
        {
            _dbContext.Trainees.Add(trainee);
            await _dbContext.SaveChangesAsync();
            return trainee;
        }

        public async Task<Trainee> DeleteTrainee(long id)
        {
            var existingrainee = _dbContext.Trainees.FirstOrDefault(p => p.Id == id);
            _dbContext.Trainees.Remove(existingrainee);
            await _dbContext.SaveChangesAsync();
            return existingrainee;
        }

        public async Task<Trainee> GetById(long id)
        {
            return await _dbContext.Trainees.FindAsync(id);
        }

        public async Task<Trainee> UpdateTrainee(Trainee trainee)
        {
            var existingTrainee = _dbContext.Trainees.FirstOrDefault(p => p.Id == trainee.Id);
            if (existingTrainee == null)
            {
                throw new KeyNotFoundException("Trainee not exist");
            }
            existingTrainee.TraineeName = trainee.TraineeName;
            existingTrainee.Age = trainee.Age;
            existingTrainee.IsWorking = trainee.IsWorking;
            _dbContext.Trainees.Update(existingTrainee);
            await _dbContext.SaveChangesAsync();
            return trainee;
        }
    }
}