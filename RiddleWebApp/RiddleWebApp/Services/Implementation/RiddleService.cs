using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiddleWebApp.Data;
using RiddleWebApp.Dtos;
using RiddleWebApp.Models;

namespace RiddleWebApp.Services.Implementation
{
    public class RiddleService : IRiddleService
    {
        private readonly ApplicationDbContext context;

        public RiddleService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<RiddleDto> GetAllRiddles()
        {
            var riddles = context.Riddle.ToList();
            List<RiddleDto> dtos = new List<RiddleDto>();

            foreach(var entity in riddles)
            {
                RiddleDto dto = new RiddleDto();
                dto.Id = entity.Id;
                dto.Name = entity.Name;
                dto.Question = entity.Question;
                dto.Answer = entity.Answer;
                dtos.Add(dto);
            }

            return dtos;
        }

        public void AddRiddle(RiddleDto riddleDto)
        {
            Riddle riddle = new Riddle();
            riddle.Id = riddleDto.Id;
            riddle.Name = riddleDto.Name;
            riddle.Question = riddleDto.Question;
            riddle.Answer = riddleDto.Answer;

            context.Add(riddle);
            context.SaveChanges();
        }
    }
}
