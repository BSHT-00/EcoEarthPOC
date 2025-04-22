using EcoEarthAppAPI.Data;
using EcoEarthAppAPI.Data.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoEarthAppAPI.Controllers
{
    [Route("api/DailyQuests")]
    [ApiController]
    public class DailyQuestController : ControllerBase
    {
        private readonly EcoEarthAppAPIDbContext _context;

        public DailyQuestController(EcoEarthAppAPIDbContext context)
        {
            _context = context;
        }



        //gets users quests
        [HttpGet("{userId}")]
        public async Task<ActionResult<List<DailyQuests>>> GetUserQuests(int userId)
        {
            var quests =  _context.DailyQuests
                .Where(dq => dq.UserId == userId)
                .ToList();

            if (!quests.Any())
            {
                return NotFound();
            }
            return Ok(quests);
        }



        //updates quest progress
        [HttpPut("{userId}/{questId}")]
        public async Task<IActionResult> UpdateProgress(int userId, int questId)
        {
            var dailyQuest = await _context.DailyQuests
                .FirstOrDefaultAsync(dq => dq.UserId == userId 
                                    && dq.QuestId == questId);

            if (dailyQuest == null)
            {
                return NotFound();
            }
            dailyQuest.Progress += 1;
            await _context.SaveChangesAsync();
            return Ok(dailyQuest.Progress);
        }



        //Generates and assigns quests
        [HttpPost("{userId}")]
        public async Task<ActionResult> AssignQuests(int userId)
        {
            var user = await _context.UserProfile.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            //generates quests
            var cat = _context.RecyclableMaterials.Select(rm => rm.CategoryId).ToList();
            var ins = new List<string> {"Scan", "Recycle"};

            var random = new Random();

            var QuestA = new Quest
            {
                QuestType = "Major",
                QuestInstruction = ins[random.Next(ins.Count)],
                CategoryId = cat[random.Next(cat.Count)],
                QuestGoal = 5,
                QuestReward = 10
            };

            var QuestB = new Quest
            {
                QuestType = "Minor",
                QuestInstruction = ins[random.Next(ins.Count)],
                CategoryId = cat[random.Next(cat.Count)],
                QuestGoal = 3,
                QuestReward = 5
            };

            var QuestC = new Quest
            {
                QuestType = "Minor",
                QuestInstruction = ins[random.Next(ins.Count)],
                CategoryId = cat[random.Next(cat.Count)],
                QuestGoal = 3,
                QuestReward = 5
            };

            _context.AddRange(QuestA, QuestB, QuestC);
            _context.SaveChangesAsync();

            //assigning quests
            var dailyQuests = new List<DailyQuests>
            {
                new DailyQuests { UserId = userId, QuestId = QuestA.QuestId, Progress = 0, isDone = false },
                new DailyQuests { UserId = userId, QuestId = QuestB.QuestId, Progress = 0, isDone = false },
                new DailyQuests { UserId = userId, QuestId = QuestC.QuestId, Progress = 0, isDone = false }
            };

            _context.DailyQuests.AddRange(dailyQuests);
            _context.SaveChanges();
            return Ok(dailyQuests);
        }



        //deletes all quests
        [HttpDelete]
        public async Task<IActionResult> DeleteQuests()
        {
            _context.DailyQuests.RemoveRange(_context.DailyQuests);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
